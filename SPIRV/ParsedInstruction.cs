using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SpirV
{
	public class ParsedOperand
	{
		public IList<uint> Words { get; }
	}

    public class ParsedInstruction
    {
		public IList<uint> Words { get; }

		public Instruction Instruction { get; }

		public ParsedInstruction (int opCode, IList<uint> words)
		{
			Words = words;

			Instruction = Instructions.OpcodeToInstruction [opCode];
		}

		private static string OperandToString (OperandType kind, uint word)
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

		public Type ResultType { get; private set; }

		public void ResolveResultType (IReadOnlyDictionary<uint, Type> types)
		{
			if (Instruction.Operands.Count > 0 && Instruction.Operands [0].Type is IdResultType) {
				ResultType = types [Words [1]];
			}
		}

		public override string ToString ()
		{
			// Process return type/id first -- these are always first
			if (Instruction.Operands.Count == 0) {
				return Instruction.Name;
			}

			var sb = new StringBuilder ();

			// Word 0 describes this instruction so we can ignore it
			int currentWord = 1;
			int currentOperand = 0;
			if (Instruction.Operands [currentOperand].Type is IdResultType) {
				++currentOperand;
				++currentWord;

				sb.Append (ResultType.ToString ());
				sb.Append (" ");
			}

			if (Instruction.Operands [currentOperand].Type is IdResult) {
				Instruction.Operands [currentOperand].Type.ReadValue (Words.Skip (currentWord).ToList (),
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

				operand.Type.ReadValue (Words.Skip (currentWord).ToList (),
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
