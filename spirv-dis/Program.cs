using System;

namespace SpirV
{
    class Program
    {
        static void Main(string[] args)
        {
			var module = Module.ReadFrom (System.IO.File.OpenRead (args [0]));
			var ds = new Disassembler ();

			Console.Write (ds.Disassemble (module));
		}
    }
}
