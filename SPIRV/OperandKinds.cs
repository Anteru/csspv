using System;
using System.Collections.Generic;

namespace SpirV.Core
{
	public class OperandKind
	{
	}

	public class Literal : OperandKind
	{

	}

	public class LiteralInteger : Literal
	{
	}

	public class LiteralString : Literal
	{

	}

	public class LiteralContextDependentNumber : Literal
	{

	}

	public class LiteralExtInstInteger : Literal
	{

	}

	public class LiteralSpecConstantOpInteger : Literal
	{

	}

	public class Parameter
	{
		public virtual IList<OperandKind> OperandKinds { get; }
	}

	public abstract class EnumOperandKind : OperandKind
	{
		public virtual Parameter CreateParameter (uint value) => null;
	}

	public class IdScope : OperandKind
	{
		public Scope Scope { get; }
	}

	public class IdMemorySemantics : OperandKind
	{
		public MemorySemantics MemorySemantics { get; }
	}

	public class IdResult : OperandKind
	{
		public uint Id { get; }
	}

	public class IdResultType : OperandKind
	{
		public IdRef IdRef { get; }
	}

	public class IdRef : OperandKind
	{
		public uint Id { get; }
	}

	public class PairIdRefIdRef : OperandKind
	{
		public IdRef Variable { get; }
		public IdRef Parent { get; }
	}

	public class PairIdRefLiteralInteger : OperandKind
	{
		public IdRef Type { get; }
		public LiteralInteger Member { get; }
	}

	public class PairLiteralIntegerIdRef : OperandKind
	{
		public LiteralInteger Selector { get; }
		public IdRef Label { get; }
	}
}