using System;
using System.Collections.Generic;
using CommandLine;

namespace SpirV
{
	class Options
	{
		[Option('t', HelpText="Show types")]
		public bool ShowTypes { get; set; } = false;

		[Option ('n', HelpText = "Show object reference names if possible")]
		public bool ShowNames { get; set; } = false;

		[Value(0, MetaName="Input")]
		public string InputFile { get; set; }
	}

	class Program
	{
		static int Run (Options options)
		{
			var module = Module.ReadFrom (System.IO.File.OpenRead (options.InputFile));
			var ds = new Disassembler ();

			var settings = DisassemblyOptions.None;

			if (options.ShowNames) {
				settings |= DisassemblyOptions.ShowNames;
			}

			if (options.ShowTypes) {
				settings |= DisassemblyOptions.ShowTypes;
			}

			Console.Write (ds.Disassemble (module, settings));

			return 0;
		}

		private static int HandleError (IEnumerable<Error> errs)
		{
			return 1;
		}

		static void Main(string[] args)
		{
			CommandLine.Parser.Default.ParseArguments<Options> (args)
				.WithParsed<Options> (opts => Run (opts))
				.WithNotParsed<Options> ((errs) => HandleError (errs));
		}
	}
}
