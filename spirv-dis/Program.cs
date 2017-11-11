using System;
using SpirV.Core;

namespace SpirV.Disassembler
{
    class Program
    {
        static void Main(string[] args)
        {
			var module = Module.ReadFrom (System.IO.File.OpenRead (args [0]));
			var ds = new SpirV.Core.Disassembler ();

			Console.Write (ds.Disassemble (module));
		}
    }
}
