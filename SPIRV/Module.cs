using System;
using System.Collections.Generic;
using System.Text;

namespace SpirV
{
	public class Module
    {
		public Module (ModuleHeader header, List<InstructionInstance> instructions)
		{
			Header = header;
			instructions_ = instructions;
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

			var instructions = new List<InstructionInstance> ();

			try {
				while (true) {
					var instructionStart = reader.ReadWord ();
					var wordCount = (ushort)(instructionStart >> 16);
					var opCode = (int)(instructionStart & 0xFF);

					List<uint> words = new List<uint> ();

					for (ushort i = 0; i < wordCount - 1; ++i) {
						words.Add (reader.ReadWord ());
					}

					instructions.Add (new InstructionInstance (opCode, words));
				}
			} catch (System.IO.EndOfStreamException) {
			}

			return new Module (header, instructions);
		}

		public ModuleHeader Header { get; }
		public IReadOnlyList<InstructionInstance> Instructions { get { return instructions_; } }
		
		private List<InstructionInstance> instructions_;
    }
}
