using System;
using System.Collections.Generic;
using System.Text;

namespace SpirV
{
	public enum OperandQuantifier
	{
		// 1
		Default,
		// 0 or 1
		Optional,
		// 0+
		Varying
	}

	public class Operand
	{
		public string Name { get; }
		public OperandKind Kind { get; }
		public OperandQuantifier Quantifier { get; }
		
		public Operand(OperandKind kind, string name, OperandQuantifier quantifier)
		{
			Name = name;
			Kind = kind;
			Quantifier = quantifier;
		}
	}

	public class Instruction
    {
		public string Name { get; }
		public IList<Operand> Operands
		{
			get;
		}

		public Instruction (string name)
		{
			Operands = new List<Operand> ();
			Name = name;
		}

		public Instruction (string name, IList<Operand> operands)
		{
			Operands = operands;
			Name = name;
		}
    }
}
