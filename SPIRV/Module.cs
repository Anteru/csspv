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
			ResolveTypes (instructions_, types_);
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

		private static Dictionary<uint, Type> CollectTypes (List<ParsedInstruction> instructions)
		{
			Dictionary<uint, Type> result = new Dictionary<uint, Type> ();
			
			foreach (var i in instructions) {
				switch (i.Instruction) {
					case OpTypeInt t: {
							result [i.Words [1]] = new IntegerType ((int)i.Words [2], i.Words [3] == 1u);
						}
						break;
					case OpTypeFloat t: {
							result [i.Words [1]] = new FloatingPointType ((int)i.Words [2]);
						}
						break;
					case OpTypeVector t: {
							result [i.Words [1]] = new VectorType (result [i.Words [2]] as ScalarType, (int)i.Words [3]);
						}
						break;
					case OpTypeMatrix t: {
							result [i.Words [1]] = new MatrixType (result [i.Words [2]] as VectorType, (int)i.Words [3]);
						}
						break;
					case OpTypeArray t: {
							result [i.Words [1]] = new SpirV.ArrayType (result [i.Words [2]], (int)i.Words [3]);
						}
						break;
					case OpTypeRuntimeArray t: {
							result [i.Words [1]] = new SpirV.RuntimeArrayType (result [i.Words [2]]);
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
					case OpTypeForwardPointer t: {
							result [i.Words [1]] = new PointerType ((StorageClass.Values)i.Words [2]);
						}
						break;
					case OpTypeVoid t: {
							result [i.Words [1]] = new VoidType ();
						}
						break;
					case OpTypeFunction t: {
							result [i.Words [1]] = new FunctionType ();
						}
						break;
					case OpTypePointer t: {
							if (result.ContainsKey (i.Words [1])) {
								Debug.Assert (result [i.Words [1]] is PointerType);

								(result [i.Words [1]] as PointerType).ResolveForwardReference (result [i.Words [3]]);
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

		private static void ResolveTypes (IList<ParsedInstruction> instructions, IReadOnlyDictionary<uint, Type> types)
		{
			foreach (var i in instructions) {
				i.ResolveResultType (types);
			}
		}

		public ModuleHeader Header { get; }
		public IReadOnlyList<ParsedInstruction> Instructions { get { return instructions_; } }
		
		private List<ParsedInstruction> instructions_;

		private Dictionary<uint, Type> types_ = new Dictionary<uint, Type> ();
    }
}
