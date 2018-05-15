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

			types_ = CollectTypes (instructions_);
			ResolveResultTypes (instructions_, types_);
			AssignMemberNames (instructions_, types_);
			ResolveConstants (instructions_, types_);
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
		private static Dictionary<uint, Type> CollectTypes (List<ParsedInstruction> instructions)
		{
			Dictionary<uint, Type> result = new Dictionary<uint, Type> ();

			foreach (var i in instructions) {
				switch (i.Instruction) {
					case OpTypeInt t: {
							result [i.Words [1]] = new IntegerType (
								(int)i.Words [2],
								i.Words [3] == 1u);
						}
						break;
					case OpTypeFloat t: {
							result [i.Words [1]] = new FloatingPointType (
								(int)i.Words [2]);
						}
						break;
					case OpTypeVector t: {
							result [i.Words [1]] = new VectorType (
								result [i.Words [2]] as ScalarType,
								(int)i.Words [3]);
						}
						break;
					case OpTypeMatrix t: {
							result [i.Words [1]] = new MatrixType (
								result [i.Words [2]] as VectorType,
								(int)i.Words [3]);
						}
						break;
					case OpTypeArray t: {
							result [i.Words [1]] = new ArrayType (
								result [i.Words [2]],
								(int)i.Words [3]);
						}
						break;
					case OpTypeRuntimeArray t: {
							result [i.Words [1]] = new RuntimeArrayType (
								result [i.Words [2]]);
						}
						break;
					case OpTypeBool t: {
							result [i.Words [1]] = new BoolType ();
						}
						break;
					case OpTypeOpaque t: {
							result [i.Words [1]] = new OpaqueType ();
						}
						break;
					case OpTypeVoid t: {
							result [i.Words [1]] = new VoidType ();
						}
						break;
					case OpTypeImage t: {
							var sampledType = result [((ObjectReference)i.Operands[1].Value).Id];
							var dim = i.Operands[2].GetSingleEnumValue<Dim> ();
							var depth = (uint)i.Operands [3].Value;
							var isArray = (uint)i.Operands [4].Value != 0;
							var isMultiSampled = (uint)i.Operands [5].Value != 0;
							var sampled = (uint)i.Operands [6].Value;

							var imageFormat = i.Operands [7].GetSingleEnumValue<ImageFormat> ();

							result [i.Words [1]] = new ImageType (sampledType,
								dim,
								(int)depth, isArray, isMultiSampled,
								(int)sampled, imageFormat,
								i.Operands.Count > 8 ?
								i.Operands[8].GetSingleEnumValue<AccessQualifier> () : AccessQualifier.ReadOnly);
						}
						break;
					case OpTypeSampledImage t: {
							result [i.Words [1]] = new SampledImageType (
								result [i.Words [2]] as ImageType
							);
						}
						break;
					case OpTypeFunction t: {
							var parameterTypes = new List<Type> ();
							for (int j = 3; j < i.Words.Count; ++j) {
								parameterTypes.Add (result [i.Words [j]]);
							}
							result [i.Words [1]] = new FunctionType (result [i.Words [2]],
								parameterTypes);
						}
						break;
					case OpTypeForwardPointer t: {
							// We create a normal pointer, but with unspecified type
							// This will get resolved later on
							result [i.Words [1]] = new PointerType ((StorageClass)i.Words [2]);
						}
						break;
					case OpTypePointer t: {
							if (result.ContainsKey (i.Words [1])) {
								// If there is something present, it must have been
								// a forward reference. The storage type must
								// match
								var pt = result [i.Words [1]] as PointerType;
								Debug.Assert (pt != null);
								Debug.Assert (pt.StorageClass == (StorageClass)i.Words [2]);

								pt.ResolveForwardReference (result [i.Words [3]]);
							} else {
								result [i.Words [1]] = new PointerType (
									(StorageClass)i.Words [2],
									result [i.Words [3]]
									);
							}
						}
						break;
					case OpTypeStruct t: {
							var memberTypes = new List<Type> ();
							for (int j = 2; j < i.Words.Count; ++j) {
								memberTypes.Add (result [i.Words [j]]);
							}

							result [i.Words [1]] = new StructType (memberTypes);
						}
						break;
				}
			}

			return result;
		}

		/// <summary>
		/// Resolve the result types for every instruction
		/// </summary>
		private static void ResolveResultTypes (IList<ParsedInstruction> instructions,
			IReadOnlyDictionary<uint, Type> types)
		{
			foreach (var i in instructions) {
				i.ResolveResultType (types);
			}
		}

		/// <summary>
		/// Assign member names to struct types
		/// </summary>
		private static void AssignMemberNames (IList<ParsedInstruction> instructions,
			IDictionary<uint, Type> types)
		{
			foreach (var i in instructions) {
				if (i.Instruction is OpMemberName nm) {
					var t = types [i.Words [1]] as StructType;

					System.Diagnostics.Debug.Assert (t != null);

					//TODO: This should use proper accessors eventually
					t.SetMemberName ((uint)i.Operands [1].Value,
						i.Operands [2].Value as string);
				}
			}
		}

		/// <summary>
		/// Resolve constants which require the type to be known
		///
		/// This must run after all types have been collected
		/// </summary>
		private static void ResolveConstants (IList<ParsedInstruction> instructions,
			IDictionary<uint, Type> types)
		{
			foreach (var i in instructions) {
				if (i.Instruction is OpConstant c) {
					var t = i.ResultType;
					System.Diagnostics.Debug.Assert (t != null);
					System.Diagnostics.Debug.Assert (t is ScalarType);

					i.Operands [2].Value = ConvertConstant (
						i.ResultType as ScalarType,
						i.Words.Skip (3).ToList ());
				}
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
		public IReadOnlyDictionary<uint, Type> Types { get { return types_; } }

		private List<ParsedInstruction> instructions_;

		private Dictionary<uint, Type> types_;

		private Dictionary<uint, ModuleObject> objects_ = new Dictionary<uint, ModuleObject> ();
    }
}
