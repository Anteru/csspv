using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SpirV
{
	public class OperandType
	{
		public virtual bool ReadValue (IList<uint> words, 
			out object value, out int wordsUsed)
		{
			value = this.GetType ();
			wordsUsed = 1;
			return true;
		}
	}

	public class Literal : OperandType
	{

	}

	public class LiteralNumber : Literal
	{
	}

	// The SPIR-V JSON file uses only literal integers
	public class LiteralInteger : LiteralNumber
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
		public virtual IList<OperandType> OperandTypes { get; }
	}

	public abstract class Enum : OperandType
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
							for (int j = 0; j < p.OperandTypes.Count; ++j) {
								p.OperandTypes [j].ReadValue (
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
					for (int j = 0; j < p.OperandTypes.Count; ++j) {
						p.OperandTypes [j].ReadValue (
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

	public class IdScope : OperandType
	{
		public override bool ReadValue (IList<uint> words, out object value, out int wordsUsed)
		{
			value = (Scope.Values)words [0];
			wordsUsed = 1;

			return true;
		}
	}

	public class IdMemorySemantics : OperandType
	{
		public override bool ReadValue (IList<uint> words, out object value, out int wordsUsed)
		{
			value = (MemorySemantics.Values)words [0];
			wordsUsed = 1;

			return true;
		}
	}

	public class IdType : OperandType
	{
		public override bool ReadValue (IList<uint> words, out object value, out int wordsUsed)
		{
			value = words [0];
			wordsUsed = 1;

			return true;
		}
	}

	public class IdResult : IdType
	{
		public override bool ReadValue (IList<uint> words, out object value, out int wordsUsed)
		{
			value = new ObjectReference (words [0]);
			wordsUsed = 1;

			return true;
		}
	}

	public class IdResultType : IdType
	{
	}

	public class IdRef : IdType
	{
		public override bool ReadValue (IList<uint> words, out object value, out int wordsUsed)
		{
			value = new ObjectReference (words [0]);
			wordsUsed = 1;

			return true;
		}
	}

	public class PairIdRefIdRef : OperandType
	{
		public override bool ReadValue (IList<uint> words, out object value, out int wordsUsed)
		{
			value = new { Variable = new ObjectReference (words [0]), Parent = new ObjectReference (words [1]) };
			wordsUsed = 2;
			return true;
		}
	}

	public class PairIdRefLiteralInteger : OperandType
	{
		public override bool ReadValue (IList<uint> words, out object value, out int wordsUsed)
		{
			value = new { Type = new ObjectReference (words [0]), Member = words [1] };
			wordsUsed = 2;
			return true;
		}
	}

	public class PairLiteralIntegerIdRef : OperandType
	{
		public override bool ReadValue (IList<uint> words, out object value, out int wordsUsed)
		{
			value = new { Selector = words [0], Label = new ObjectReference (words [1]) };
			wordsUsed = 2;
			return true;
		}
	}
}