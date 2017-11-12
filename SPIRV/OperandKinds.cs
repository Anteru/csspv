using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SpirV
{
	public class OperandKind
	{
		public virtual bool ReadValue (IList<uint> words, out object value, out int wordsUsed)
		{
			value = this.GetType ().Name;
			wordsUsed = 1;
			return true;
		}
	}

	public class Literal : OperandKind
	{

	}

	public class LiteralInteger : Literal
	{
		public override bool ReadValue (IList<uint> words, out object value, out int wordsUsed)
		{
			value = words [0];
			wordsUsed = 1;

			return true;
		}
	}

	public class LiteralString : Literal
	{
		public override bool ReadValue (IList<uint> words, out object value, out int wordsUsed)
		{
			StringBuilder sb = new StringBuilder ();
			// This is just a fail-safe -- the loop below must terminate
			wordsUsed = 1;

			List<byte> bytes = new List<byte> ();
			for (int i = 0; i < words.Count; ++i) {
				var wordBytes = BitConverter.GetBytes (words [i]);

				int zeroOffset = -1;
				for (int j = 0; j < wordBytes.Length; ++j) {
					if (wordBytes [j] == 0) {
						zeroOffset = j;
						break;
					} else {
						bytes.Add (wordBytes [j]);
					}
				}

				if (zeroOffset != -1) {
					wordsUsed = i + 1;
					break;
				}
			}

			var decoder = new UTF8Encoding ();
			value = decoder.GetString (bytes.ToArray ());

			return true;
		}
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
		public virtual bool IsBitEnumeration { get; }
		public virtual IEnumerable<uint> EnumerationValues { get; }
		public virtual string GetValueName (uint value) => null;

		public override bool ReadValue (IList<uint> words, out object value, out int wordsUsed)
		{
			IList<object> result = new List<object> ();
			var wordsUsedForParameters = 0;

			if (IsBitEnumeration) {
				foreach (var bit in EnumerationValues) {
					if ((words [0] & bit) != 0) {
						var p = CreateParameter (bit);

						result.Add (GetValueName (words [0]));

						if (p != null) {
							for (int j = 0; j < p.OperandKinds.Count; ++j) {
								p.OperandKinds [j].ReadValue (
									words.Skip (1 + wordsUsedForParameters).ToList (), 
									out object pValue, out int pWordsUsed);
								wordsUsedForParameters += pWordsUsed;
								result.Add (pValue);
							}
						}
					}
				}

			} else {
				result.Add (GetValueName (words [0]));

				var p = CreateParameter (words [0]);
				if (p != null) {
					for (int j = 0; j < p.OperandKinds.Count; ++j) {
						p.OperandKinds [j].ReadValue (
							words.Skip (1 + wordsUsedForParameters).ToList (),
							out object pValue, out int pWordsUsed);
						wordsUsedForParameters += pWordsUsed;
						result.Add (pValue);
					}
				}

			}

			wordsUsed = wordsUsedForParameters + 1;
			value = result;

			return true;
		}
	}

	public class IdScope : OperandKind
	{
		public Scope Scope { get; }
	}

	public class IdMemorySemantics : OperandKind
	{
		public MemorySemantics MemorySemantics { get; }
	}

	public class IdOperandKind : OperandKind
	{
		public override bool ReadValue (IList<uint> words, out object value, out int wordsUsed)
		{
			value = words [0];
			wordsUsed = 1;

			return true;
		}
	}

	public class IdResult : IdOperandKind
	{
	}

	public class IdResultType : IdOperandKind
	{
	}

	public class IdRef : IdOperandKind
	{
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