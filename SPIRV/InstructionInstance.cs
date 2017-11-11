using System;
using System.Collections.Generic;
using System.Text;

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

		public override string ToString ()
		{
			var leftSide = new StringBuilder ();
			var rightSide = new StringBuilder ();
			rightSide.Append ($"{Instruction.Name} ");

			for (int i = 0; i < Instruction.Operands.Count; ++i) {
				if (i >= Words.Count) {
					break;
				}

				var operand = Instruction.Operands [i];

				switch (operand.Kind) {
					case IdResult r: leftSide.Append ($"%{Words [i]} "); break;
					case IdResultType r: leftSide.Append ($"{Words [i]} "); break;
						
					case EnumOperandKind e: {
							if (e.IsBitEnumeration) {
								foreach (var bit in e.EnumerationValues) {
									if ((Words [i] & bit) != 0) {
										var p = e.CreateParameter (bit);
										rightSide.Append (e.GetValueName (Words [i]));
										rightSide.Append (" ");
										if (p != null) { 
											for (int j = 0; j < p.OperandKinds.Count; ++j) {
												rightSide.Append (OperandToString (p.OperandKinds [j], Words [i + j + 1]));
												rightSide.Append (" ");
												i += p.OperandKinds.Count;
											}
										}
									}
								}
							} else {
								rightSide.Append (e.GetValueName (Words [i]));
								rightSide.Append (" ");
								var p = e.CreateParameter (Words [i]);
								if (p != null) {
									for (int j = 0; j < p.OperandKinds.Count; ++j) {
										rightSide.Append (OperandToString (p.OperandKinds [j], Words [i + j + 1]));
										rightSide.Append (" ");
										i += p.OperandKinds.Count;
									}
								}
							}
							break;
						}
					default:
						rightSide.Append (OperandToString (operand.Kind, Words [i]));
						rightSide.Append (" ");
						break;
				}
			}

			if (leftSide.Length > 0) {
				leftSide.Append ("= ");
			}

			leftSide.Append (rightSide.ToString ());

			return leftSide.ToString ();
		}
    }
}
