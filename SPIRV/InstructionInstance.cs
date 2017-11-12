using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SpirV
{
    public class InstructionInstance
    {
		public IList<uint> Words { get; }

		public Instruction Instruction { get; }

		public InstructionInstance (int opCode, IList<uint> words)
		{
			Words = words;

			Instruction = Instructions.OpcodeToInstruction [opCode];
		}

		private static string OperandToString (OperandKind kind, uint word)
		{
			switch (kind) {
				case IdRef r: return $"%{word}";
				default:
					return kind.ToString ();
			}
		}

		private static void AppendValue (StringBuilder sb, object value)
		{
			if (value is IList<object> asList) {
				for (int i = 0; i < asList.Count; ++i) {
					AppendValue (sb, asList [i]);
					if (i < (asList.Count - 1)) {
						sb.Append (" ");
					}
				}
			} else {
				sb.Append (value.ToString ());
			}
		}

		public override string ToString ()
		{
			// Process return type/id first -- these are always first
			if (Instruction.Operands.Count == 0) {
				return Instruction.Name;
			}

			var sb = new StringBuilder ();

			int currentWord = 0;
			int currentOperand = 0;
			if (Instruction.Operands [currentOperand].Kind is IdResultType) {
				++currentOperand;
				++currentWord;
			}

			if (Instruction.Operands [currentOperand].Kind is IdResult) {
				Instruction.Operands [currentOperand].Kind.ReadValue (Words.Skip (currentWord).ToList (),
					out object value, out int wordsUsed);

				currentWord += wordsUsed;
				++currentOperand;

				AppendValue (sb, value);
				sb.Append (" = ");
			}

			sb.Append (Instruction.Name);
			sb.Append (" ");

			for (;currentWord < Words.Count;) {
				var operand = Instruction.Operands [currentOperand];

				operand.Kind.ReadValue (Words.Skip (currentWord).ToList (),
					out object value, out int wordsUsed);

				AppendValue (sb, value);
				sb.Append (" ");

				currentWord += wordsUsed;

				if (operand.Quantifier != OperandQuantifier.Varying) { 
					++currentOperand;
				}
			}

			return sb.ToString ();
		}
    }
}
