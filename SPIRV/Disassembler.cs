using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
				PrintInstruction (sb, i);
				sb.AppendLine ();
			}

			var lines = sb.ToString ().Split (new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

			sb.Clear ();

			int longestPrefix = 0;
			foreach (var line in lines) {
				longestPrefix = Math.Max (longestPrefix, line.IndexOf ('='));
			}

			foreach (var line in lines) {
				if (line.StartsWith (';')) {
					sb.AppendLine (line);
				} else {
					if (line.Contains ('=')) {
						var parts = line.Split ('=');
						System.Diagnostics.Debug.Assert (parts.Length == 2);
						sb.Append (parts[0].PadLeft (longestPrefix));
						sb.Append (" = ");
						sb.Append (parts[1]);
					} else {
						sb.Append ("".PadLeft (longestPrefix + 4));
						sb.Append (line);
					}

					sb.AppendLine ();
				}
			}

			return sb.ToString ();
		}

		private static void PrintInstruction (StringBuilder sb, ParsedInstruction instruction)
		{
			if (instruction.Operands.Count == 0) {
				sb.Append (instruction.Instruction.Name);
				return;
			}

			int currentOperand = 0;
			if (instruction.Instruction.Operands[currentOperand].Type is IdResultType) {
				sb.Append (instruction.ResultType.ToString ());
				sb.Append (" ");

				++currentOperand;
			}

			if (currentOperand < instruction.Operands.Count &&
				instruction.Instruction.Operands[currentOperand].Type is IdResult) {
				if (string.IsNullOrWhiteSpace (instruction.Name)) {
					PrintOperandValue (sb, instruction.Operands[currentOperand].Value);
				} else {
					sb.Append (instruction.Name);
				}
				sb.Append (" = ");

				++currentOperand;
			}

			sb.Append (instruction.Instruction.Name);
			sb.Append (" ");

			for (; currentOperand < instruction.Operands.Count; ++currentOperand) {
				PrintOperandValue (sb, instruction.Operands[currentOperand].Value);
				sb.Append (" ");
			}
		}

		private static void PrintOperandValue (StringBuilder sb, object value)
		{
			if (value is System.Type t) {
				sb.Append (t.Name);
			} else if (value is string s) {
				sb.Append ($"\"{s}\"");
			} else if (value is IBitEnumOperandValue beov) {
				PrintBitEnumValue (sb, beov);
			} else if (value is IValueEnumOperandValue veov) {
				PrintValueEnumValue (sb, veov);
			} else {
				sb.Append (value);
			}
		}

		private static void PrintBitEnumValue (StringBuilder sb, IBitEnumOperandValue enumOperandValue)
		{
			foreach (var key in enumOperandValue.Values.Keys) {
				sb.Append (enumOperandValue.EnumerationType.GetEnumName (key));

				var value = enumOperandValue.Values[key] as IList<object>;

				if (value.Count != 0) {
					sb.Append (" ");
					foreach (var v in value) {
						PrintOperandValue (sb, v);
					}
				}
			}
		}

		private static void PrintValueEnumValue (StringBuilder sb, IValueEnumOperandValue valueOperandValue)
		{
			sb.Append (valueOperandValue.Key);
			if (valueOperandValue.Value is IList<object> valueList && valueList.Count > 0) {
				sb.Append (" ");
				foreach (var v in valueList) {
					PrintOperandValue (sb, v);
				}
			}
		}
	}
}
