using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace SpirV
{
	public class Module
    {
		public Module (ModuleHeader header, List<ParsedInstruction> instructions)
		{
			Header = header;
			instructions_ = instructions;

			Read (instructions_, objects_);
		}

		private static void Read (IList<ParsedInstruction> instructions,
			Dictionary<uint, ModuleObject> objects)
		{
			foreach (var instruction in instructions) {
				if (instruction.Instruction.Name.StartsWith ("OpType")) {
					ProcessTypeInstruction (instruction, objects);
				} else if (instruction.Instruction is OpMemberName) {
					var t = objects [instruction.Words [1]] as StructType;

					System.Diagnostics.Debug.Assert (t != null);

					//TODO: This should use proper accessors eventually
					t.SetMemberName ((uint)instruction.Operands [1].Value,
						instruction.Operands [2].Value as string);
				}

				instruction.ResolveResultType (objects);

				// Constants require that the result type has been resolved
				if (instruction.Instruction is OpConstant) {
					var t = instruction.ResultType;
					System.Diagnostics.Debug.Assert (t != null);
					System.Diagnostics.Debug.Assert (t is ScalarType);

					instruction.Operands [2].Value = ConvertConstant (
						instruction.ResultType as ScalarType,
						instruction.Words.Skip (3).ToList ());
				}
			}
		}

		public static Module ReadFrom (System.IO.Stream stream)
		{
			var br = new System.IO.BinaryReader (stream);
			var reader = new Reader (br);

			var versionNumber = reader.ReadWord ();
			var version = new Version (
				(int)(versionNumber >> 16),
				(int)((versionNumber >> 8) & 0xFF));

			var generatorMagicNumber = reader.ReadWord ();
			var generatorToolId = (int)(generatorMagicNumber >> 16);

			string generatorVendor = "unknown";
			string generatorName = null;

			if (SpirV.Meta.Tools.ContainsKey (generatorToolId)) {
				var toolInfo = SpirV.Meta.Tools [generatorToolId];

				generatorVendor = toolInfo.Vendor;

				if (toolInfo.Name != null) {
					generatorName = toolInfo.Name;
				}
			}

			// Read header
			var header = new ModuleHeader
			{
				version = version,
				generatorName = generatorName,
				generatorVendor = generatorVendor,
				generatorVersion = (int)(generatorMagicNumber & 0xFFFF),
				bound = reader.ReadWord (),
				reserved = reader.ReadWord ()
			};

			var instructions = new List<ParsedInstruction> ();

			try {
				while (true) {
					var instructionStart = reader.ReadWord ();
					var wordCount = (ushort)(instructionStart >> 16);
					var opCode = (int)(instructionStart & 0xFF);

					List<uint> words = new List<uint> ()
					{
						instructionStart
					};

					for (ushort i = 0; i < wordCount - 1; ++i) {
						words.Add (reader.ReadWord ());
					}

					instructions.Add (new ParsedInstruction (opCode, words));
				}
			} catch (System.IO.EndOfStreamException) {
			}

			return new Module (header, instructions);
		}

		/// <summary>
		/// Collect types from OpType* instructions
		/// </summary>
		private static void ProcessTypeInstruction (ParsedInstruction i,
			Dictionary<uint, ModuleObject> objects)
		{
			switch (i.Instruction) {
				case OpTypeInt t: {
						objects [i.Words [1]] = new IntegerType (
							(int)i.Words [2],
							i.Words [3] == 1u);
					}
					break;
				case OpTypeFloat t: {
						objects [i.Words [1]] = new FloatingPointType (
							(int)i.Words [2]);
					}
					break;
				case OpTypeVector t: {
						objects [i.Words [1]] = new VectorType (
							objects [i.Words [2]] as ScalarType,
							(int)i.Words [3]);
					}
					break;
				case OpTypeMatrix t: {
						objects [i.Words [1]] = new MatrixType (
							objects [i.Words [2]] as VectorType,
							(int)i.Words [3]);
					}
					break;
				case OpTypeArray t: {
						objects [i.Words [1]] = new ArrayType (
							objects [i.Words [2]] as Type,
							(int)i.Words [3]);
					}
					break;
				case OpTypeRuntimeArray t: {
						objects [i.Words [1]] = new RuntimeArrayType (
							objects [i.Words [2]] as Type);
					}
					break;
				case OpTypeBool t: {
						objects [i.Words [1]] = new BoolType ();
					}
					break;
				case OpTypeOpaque t: {
						objects [i.Words [1]] = new OpaqueType ();
					}
					break;
				case OpTypeVoid t: {
						objects [i.Words [1]] = new VoidType ();
					}
					break;
				case OpTypeImage t: {
						var sampledType = objects [((ObjectReference)i.Operands[1].Value).Id] as Type;
						var dim = i.Operands[2].GetSingleEnumValue<Dim> ();
						var depth = (uint)i.Operands [3].Value;
						var isArray = (uint)i.Operands [4].Value != 0;
						var isMultiSampled = (uint)i.Operands [5].Value != 0;
						var sampled = (uint)i.Operands [6].Value;

						var imageFormat = i.Operands [7].GetSingleEnumValue<ImageFormat> ();

						objects [i.Words [1]] = new ImageType (sampledType,
							dim,
							(int)depth, isArray, isMultiSampled,
							(int)sampled, imageFormat,
							i.Operands.Count > 8 ?
							i.Operands[8].GetSingleEnumValue<AccessQualifier> () : AccessQualifier.ReadOnly);
					}
					break;
				case OpTypeSampledImage t: {
						objects [i.Words [1]] = new SampledImageType (
							objects [i.Words [2]] as ImageType
						);
					}
					break;
				case OpTypeFunction t: {
						var parameterTypes = new List<Type> ();
						for (int j = 3; j < i.Words.Count; ++j) {
							parameterTypes.Add (objects [i.Words [j]] as Type);
						}
						objects [i.Words [1]] = new FunctionType (objects [i.Words [2]] as Type,
							parameterTypes);
					}
					break;
				case OpTypeForwardPointer t: {
						// We create a normal pointer, but with unspecified type
						// This will get resolved later on
						objects [i.Words [1]] = new PointerType ((StorageClass)i.Words [2]);
					}
					break;
				case OpTypePointer t: {
						if (objects.ContainsKey (i.Words [1])) {
							// If there is something present, it must have been
							// a forward reference. The storage type must
							// match
							var pt = objects [i.Words [1]] as PointerType;
							Debug.Assert (pt != null);
							Debug.Assert (pt.StorageClass == (StorageClass)i.Words [2]);

							pt.ResolveForwardReference (objects [i.Words [3]] as Type);
						} else {
							objects [i.Words [1]] = new PointerType (
								(StorageClass)i.Words [2],
								objects [i.Words [3]] as Type
								);
						}
					}
					break;
				case OpTypeStruct t: {
						var memberTypes = new List<Type> ();
						for (int j = 2; j < i.Words.Count; ++j) {
							memberTypes.Add (objects [i.Words [j]] as Type);
						}

						objects [i.Words [1]] = new StructType (memberTypes);
					}
					break;
			}
		}

		private static object ConvertConstant (ScalarType type,
			IReadOnlyList<uint> words)
		{
			byte [] bytes = new byte [words.Count * 4];

			for (int i = 0; i < words.Count; ++i) {
				BitConverter.GetBytes (words [i]).CopyTo (bytes, i * 4);
			}

			switch (type) {
				case IntegerType i: {
						if (i.Signed) {
							if (i.Width == 16) {
								return (short)BitConverter.ToInt32 (bytes, 0);
							} else if (i.Width == 32) {
								return BitConverter.ToInt32 (bytes, 0);
							} else if (i.Width == 64) {
								return BitConverter.ToInt64 (bytes, 0);
							}
						} else {
							if (i.Width == 16) {
								return (ushort)words [0];
							} else if (i.Width == 32) {
								return words [0];
							} else if (i.Width == 64) {
								return BitConverter.ToUInt64 (bytes, 0);
							}
						}

						throw new Exception ("Cannot construct floating point literal.");
					}

				case FloatingPointType f: {
						if (f.Width == 32) {
							return BitConverter.ToSingle (bytes, 0);
						} else if (f.Width == 64) {
							return BitConverter.ToDouble (bytes, 0);
						} else {
							throw new Exception ("Cannot construct floating point literal.");
						}
					}
			}

			return null;
		}

		public ModuleHeader Header { get; }
		public IReadOnlyList<ParsedInstruction> Instructions { get { return instructions_; } }

		private List<ParsedInstruction> instructions_;

		private Dictionary<uint, ModuleObject> objects_ = new Dictionary<uint, ModuleObject> ();
    }
}
