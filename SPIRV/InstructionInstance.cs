using System;
using System.Collections.Generic;
using System.Text;

namespace SpirV.Core
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

		public override string ToString ()
		{
			return String.Format ("{0}", Instruction.Name);
		}
    }
}
