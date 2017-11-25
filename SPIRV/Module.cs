using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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
							result [i.Words [1]] = new SpirV.ArrayType (
								result [i.Words [2]], 
								(int)i.Words [3]);
						}
						break;
					case OpTypeRuntimeArray t: {
							result [i.Words [1]] = new SpirV.RuntimeArrayType (
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
							result [i.Words [1]] = new PointerType ((StorageClass.Values)i.Words [2]);
						}
						break;
					case OpTypePointer t: { 
							if (result.ContainsKey (i.Words [1])) {
								// If there is something present, it must have been
								// a forward reference. The storage type must
								// match
								var pt = result [i.Words [1]] as PointerType;
								Debug.Assert (pt != null);
								Debug.Assert (pt.StorageClass == (StorageClass.Values)i.Words [2]);

								pt.ResolveForwardReference (result [i.Words [3]]);
							} else { 
								result [i.Words [1]] = new PointerType (
									(StorageClass.Values)i.Words [2],
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

		public ModuleHeader Header { get; }
		public IReadOnlyList<ParsedInstruction> Instructions { get { return instructions_; } }
		public IReadOnlyDictionary<uint, Type> Types { get { return types_; } }
		
		private List<ParsedInstruction> instructions_;

		private Dictionary<uint, Type> types_;
    }
}
