using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SpirV
{
	public class ParsedOperand
	{
		public IList<uint> Words { get; }
		public object Value { get; }

		public Operand Operand { get; }

		public ParsedOperand (IList<uint> words, object value, Operand operand)
		{
			Words = words;
			Value = value;
			Operand = operand;
		}
	}

    public class ParsedInstruction
    {
		public IList<uint> Words { get; }

		public Instruction Instruction { get; }
		public IList<ParsedOperand> Operands { get; } = new List<ParsedOperand> ();

		public ParsedInstruction (int opCode, IList<uint> words)
		{
			Words = words;

			Instruction = Instructions.OpcodeToInstruction [opCode];

			ParseOperands ();
		}

		private void ParseOperands ()
		{
			if (Instruction.Operands.Count == 0) {
				return;
			}

			// Word 0 describes this instruction so we can ignore it
			int currentWord = 1;
			int currentOperand = 0;

			for (; currentWord < Words.Count;) {
				var operand = Instruction.Operands [currentOperand];

				operand.Type.ReadValue (Words.Skip (currentWord).ToList (),
					out object value, out int wordsUsed);

				Operands.Add (new ParsedOperand (Words.Skip (currentWord).Take (wordsUsed).ToList (),
					value, operand));

				currentWord += wordsUsed;

				if (operand.Quantifier != OperandQuantifier.Varying) {
					++currentOperand;
				}
			}
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
			if (Operands.Count == 0) {
				return String.Empty;
			}

			var sb = new StringBuilder ();

			int currentOperand = 0;
			if (Instruction.Operands [currentOperand].Type is IdResultType) {
				sb.Append (ResultType.ToString ());
				sb.Append (" ");

				++currentOperand;
			}

			if (currentOperand < Operands.Count && 
				Instruction.Operands [currentOperand].Type is IdResult) {				
				AppendValue (sb, Operands [currentOperand].Value);
				sb.Append (" = ");

				++currentOperand;
			}

			sb.Append (Instruction.Name);
			sb.Append (" ");

			for (;currentOperand < Operands.Count; ++currentOperand) {
				AppendValue (sb, Operands [currentOperand].Value);
				sb.Append (" ");
			}

			return sb.ToString ();
		}
    }
}
