using System;
using System.Collections.Generic;
using System.Text;

namespace SpirV
{
	public struct ModuleHeader
	{
		public Version version;
		public string generatorVendor;
		public string generatorName;
		public int generatorVersion;
		public uint bound;
		public uint reserved;
	}

	public class Disassembler
	{
		public string Disassemble (Module module)
		{
			StringBuilder sb = new StringBuilder ();

			sb.AppendFormat ("; SPIR-V\n");
			sb.AppendFormat ("; Version: {0}\n", module.Header.version);
			if (module.Header.generatorName == null) { 
				sb.AppendFormat ("; Generator: {0}; {1}\n", 
					module.Header.generatorName, 
					module.Header.generatorVersion);
			} else {
				sb.AppendFormat ("; Generator: {0} {1}; {2}\n", 
					module.Header.generatorVendor, 
					module.Header.generatorName, 
					module.Header.generatorVersion);
			}
			sb.AppendFormat ("; Bound: {0}\n", module.Header.bound);
			sb.AppendFormat ("; Schema: {0}\n", module.Header.reserved);

			foreach (var i in module.Instructions) {
				sb.AppendLine (i.ToString ());
			}

			return sb.ToString ();
		}
	}
}
