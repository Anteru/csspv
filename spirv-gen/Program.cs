using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Formatting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SpvGen
{
	enum SpirvEnumType
	{
		Value,
		Bit
	}

	class SpirvMeta
	{
		public SpirvMeta (Newtonsoft.Json.Linq.JToken meta,
			XmlElement ids)
		{
			MagicNumber = meta.Value<uint> ("MagicNumber");
			Version = meta.Value<uint> ("Version");
			Revision = meta.Value<uint> ("Revision");
			OpCodeMask = meta.Value<uint> ("OpCodeMask");
			WordCountShift = meta.Value<uint> ("WordCountShift");

			foreach (XmlElement toolId in ids.SelectNodes ("id")) {
				var ti = new ToolInfo
				{
					vendor = toolId.GetAttribute ("vendor")
				};

				if (toolId.HasAttribute ("tool")) {
					ti.name = toolId.GetAttribute ("tool");
				}

				toolInfos_.Add (Int32.Parse (toolId.GetAttribute ("value")), ti);
			}
		}

		public SyntaxNode ToSourceFragment (SyntaxGenerator generator)
		{
			IList<SyntaxNode> members = new List<SyntaxNode>
			{
				CreateProperty(generator, "MagicNumber", MagicNumber),
				CreateProperty(generator, "Version", Version),
				CreateProperty(generator, "Revision", Revision),
				CreateProperty(generator, "OpCodeMask", OpCodeMask),
				CreateProperty(generator, "WordCountShift", WordCountShift)
			};

			StringBuilder sb = new StringBuilder ();

			sb.Append (@"public class ToolInfo { 
				public ToolInfo (string vendor)
				{
					Vendor = vendor;
				}

				public ToolInfo (string vendor, string name)
				{
					Vendor = vendor;
					Name = name;
				}

				public String Name {get;} 
				public String Vendor {get;} 
			}");
			sb.Append ("private static Dictionary<int, ToolInfo> toolInfos_ = new Dictionary<int, ToolInfo> {");

			foreach (var kv in toolInfos_) {
				if (kv.Value.name == null) {
					sb.AppendFormat ("{{ {0}, new ToolInfo (\"{1}\") }},",
						kv.Key, kv.Value.vendor);
				} else {

					sb.AppendFormat ("{{ {0}, new ToolInfo (\"{1}\", \"{2}\") }},",
						kv.Key, kv.Value.vendor, kv.Value.name);
				}
			}

			sb.Append ("};\n");
			sb.Append ("public static IReadOnlyDictionary<int, ToolInfo> Tools {get {return toolInfos_;}}\n");

			var tree = CSharpSyntaxTree.ParseText (sb.ToString ());
			foreach (var node in tree.GetRoot ().ChildNodes ()) {
				members.Add (node.NormalizeWhitespace ());
			}

			var n = generator.ClassDeclaration ("Meta", members: members);

			return n;
		}

		private SyntaxNode CreateProperty (SyntaxGenerator generator,
			string name, object value)
		{
			return generator.PropertyDeclaration (name,
				generator.TypeExpression (SpecialType.System_UInt32),
				Accessibility.Public,
				DeclarationModifiers.Static,
				getAccessorStatements: new SyntaxNode[] {
					generator.ReturnStatement (
						generator.LiteralExpression (value))
					});
		}

		public uint MagicNumber { get; }
		public uint Version { get; }
		public uint Revision { get; }
		public uint OpCodeMask { get; }
		public uint WordCountShift { get; }

		class ToolInfo
		{
			public string vendor;
			public string name;
		}

		Dictionary<int, ToolInfo> toolInfos_ = new Dictionary<int, ToolInfo> ();
	}

	class Program
	{
		static void Main (string[] args)
		{
			var workspace = new AdhocWorkspace ();
			var generator = SyntaxGenerator.GetGenerator (workspace,
				LanguageNames.CSharp);

			ProcessSpirv (generator, workspace);
			ProcessGrammars (generator, workspace);
		}

		class OperandItem
		{
			public string Kind;
			public string Quantifier;
			public string Name;
		}

		class InstructionItem
		{
			public string Name;
			public int Id;
			public IList<OperandItem> Operands;
		}

		private static void ProcessInstructions (Newtonsoft.Json.Linq.JToken instructions,
			IReadOnlyDictionary<string, bool> knownEnumerands,
			SyntaxGenerator generator, IList<SyntaxNode> nodes)
		{
			var ins = new List<InstructionItem> ();

			foreach (var instruction in instructions) {
				var i = new InstructionItem ()
				{
					Name = instruction.Value<string> ("opname"),
					Id = instruction.Value<int> ("opcode")
				};

				if (instruction["operands"] != null) {
					i.Operands = new List<OperandItem> ();
					foreach (var operand in instruction["operands"]) {
						var oe = new OperandItem ()
						{
							Kind = operand.Value<string> ("kind")
						};

						if (operand["quantifier"] != null) {
							var q = operand.Value<string> ("quantifier");
							switch (q) {
								case "*": oe.Quantifier = "Varying"; break;
								case "?": oe.Quantifier = "Optional"; break;
							}
						} else {
							oe.Quantifier = "Default";
						}

						if (operand["name"] != null) {
							var operandName = operand.Value<string> ("name");

							if (operandName.StartsWith ('\'')) {
								operandName = operandName.Replace ("\'", "");
							}

							operandName = operandName.Replace ("\n", "");

							oe.Name = operandName;
						}

						i.Operands.Add (oe);
					}
				}

				ins.Add (i);
			}

			var sb = new StringBuilder ();

			foreach (var instruction in ins) {
				CreateInstructionClass (sb, instruction, knownEnumerands);
			}

			sb.AppendLine ("public static class Instructions {");
			sb.Append ("private static Dictionary<int, Instruction> instructions_ = new Dictionary<int, Instruction> {");

			foreach (var instruction in ins) {
				sb.AppendLine ($"{{ {instruction.Id}, new {instruction.Name}() }},");
			}

			sb.Append (@"};

			public static IReadOnlyDictionary<int, Instruction> OpcodeToInstruction { get { return instructions_; } }
			}");

			var s = sb.ToString ();

			var tree = CSharpSyntaxTree.ParseText (s);
			var tn = tree.GetRoot ().ChildNodes ();
			foreach (var n in tn) {
				nodes.Add (n.NormalizeWhitespace ());
			}
		}

		private static void CreateInstructionClass (StringBuilder sb, InstructionItem instruction, IReadOnlyDictionary<string, bool> knownEnumerands)
		{
			sb.AppendLine ($"public class {instruction.Name} : Instruction");
			sb.AppendLine ("{");

			sb.AppendLine ($"public {instruction.Name} ()");

			if (instruction.Operands == null) {
				sb.AppendLine ($" : base (\"{instruction.Name}\")");
			} else {
				sb.AppendLine ($" : base (\"{instruction.Name}\", new List<Operand> () {{");
				foreach (var operand in instruction.Operands) {
					string constructorParameter = null;
					if (knownEnumerands.ContainsKey (operand.Kind)) {
						constructorParameter = $"new EnumType<{operand.Kind}, {operand.Kind}ParameterFactory> ()";
					} else {
						constructorParameter = $"new {operand.Kind} ()";
					}
					if (operand.Name == null) {
						sb.AppendLine ($"new Operand ({constructorParameter}, null, OperandQuantifier.{operand.Quantifier}),");
					} else {
						sb.AppendLine ($"new Operand ({constructorParameter}, \"{operand.Name}\", OperandQuantifier.{operand.Quantifier}),");
					}
				}
				sb.AppendLine ("} )");
			}

			sb.AppendLine ("{}");

			sb.AppendLine ("}");
		}

		class OperatorKindEnumerant
		{
			public string Name;
			public uint Value;

			public IList<string> Parameters;
		}

		private static IReadOnlyDictionary<string, bool> ProcessOperandTypes (Newtonsoft.Json.Linq.JToken OperandTypes,
			SyntaxGenerator generator, IList<SyntaxNode> nodes)
		{
			var result = new Dictionary<string, bool> ();

			// We gather all of the types up-front as we need them in the loop
			foreach (var n in OperandTypes) {
				// We only handle the Enums here, the others are handled 
				// manually
				if (n.Value<string> ("category") != "ValueEnum"
					&& n.Value<string> ("category") != "BitEnum") continue;

				var kind = n.Value<string> ("kind");

				bool hasParameters = false;

				foreach (var enumerant in n["enumerants"]) {
					var parameters = enumerant["parameters"];

					if (parameters != null) {
						hasParameters = true;
						break;
					}
				}

				result.Add (kind, hasParameters);
			}

			foreach (var n in OperandTypes) {
				// We only handle the Enums here, the others are handled 
				// manually
				if (n.Value<string> ("category") != "ValueEnum"
					&& n.Value<string> ("category") != "BitEnum") continue;

				var kind = n.Value<string> ("kind");

				var enumType = n.Value<string> ("category") == "ValueEnum"
					? SpirvEnumType.Value : SpirvEnumType.Bit;

				IList<OperatorKindEnumerant> enumerants = new List<OperatorKindEnumerant> ();

				foreach (var enumerant in n["enumerants"]) {
					var oke = new OperatorKindEnumerant
					{
						Name = enumerant.Value<string> ("enumerant"),
						Value = ParseEnumValue (enumerant["value"])
					};

					if (Char.IsDigit (oke.Name[0])) {
						oke.Name = kind + oke.Name;
					}

					var parameters = enumerant["parameters"];

					if (parameters != null) {
						oke.Parameters = new List<string> ();

						foreach (var parameter in parameters) {
							oke.Parameters.Add (parameter.Value<string> ("kind"));
						}
					}

					enumerants.Add (oke);
				}

				StringBuilder sb = new StringBuilder ();

				if (enumType == SpirvEnumType.Bit) {
					sb.AppendLine ("[Flags]");
				}
				sb.AppendLine ($"public enum {kind} : uint");
				sb.AppendLine ("{");
				foreach (var e in enumerants) {
					sb.AppendFormat ("{0} = {1},\n", e.Name, e.Value);
				}
				sb.AppendLine ("}");

				sb.AppendLine ($"public class {kind}ParameterFactory : ParameterFactory");
				sb.AppendLine ("{");
				foreach (var e in enumerants) {
					if (e.Parameters == null) continue;
					sb.AppendLine ($"public class {e.Name}Parameter : Parameter");
					sb.AppendLine ("{");
					sb.AppendLine ("public override IList<OperandType> OperandTypes { get { return OperandTypes_; } }");
					sb.AppendLine ("private static IList<OperandType> OperandTypes_ = new List<OperandType> () {");

					foreach (var p in e.Parameters) {
						if (result.ContainsKey (p)) {
							if (result[p]) {
								sb.AppendLine ($"new EnumType<{p},{p}Parameters> (),");
							} else {
								sb.AppendLine ($"new EnumType<{p}> (),");
							}
						} else {
							sb.AppendLine ($"new {p} (),");
						}
					}

					sb.AppendLine ("};");
					sb.AppendLine ("}");
				}

				if (result[kind]) {
					OperandTypeCreateParameterMethod (kind, enumerants, sb);
				}

				sb.AppendLine ("}");

				var tree = CSharpSyntaxTree.ParseText (sb.ToString ());
				foreach (var node in tree.GetRoot ().ChildNodes ()) {
					nodes.Add (node.NormalizeWhitespace ());
				}
			}

			return result;
		}

		private static void OperandTypeCreateParameterMethod (string enumName,
			IList<OperatorKindEnumerant> enumerants, StringBuilder sb)
		{
			sb.AppendLine ($"public override Parameter CreateParameter (object value)");
			sb.AppendLine ("{");
			sb.AppendLine ($"switch (({enumName})value) {{");
			foreach (var e in enumerants) {
				if (e.Parameters == null) continue;

				sb.AppendLine ($"case {enumName}.{e.Name}: return new {e.Name}Parameter ();");
			}
			sb.AppendLine ("}");
			sb.AppendLine ("return null;");
			sb.AppendLine ("}");
		}

		private static uint ParseEnumValue (Newtonsoft.Json.Linq.JToken value)
		{
			if (value.Type == Newtonsoft.Json.Linq.JTokenType.String) {
				var s = value.ToString ();

				if (s.StartsWith ("0x")) {
					return uint.Parse (s.Substring (2), System.Globalization.NumberStyles.HexNumber);
				} else {
					return uint.Parse (s);
				}
			} else {
				return (uint)value;
			}
		}

		private static void ProcessGrammars (
			SyntaxGenerator generator,
			Workspace workspace)
		{
			var doc = Newtonsoft.Json.Linq.JObject.ReadFrom (
				new Newtonsoft.Json.JsonTextReader (System.IO.File.OpenText (
					"spirv.core.grammar.json")));

			var nodes = new List<SyntaxNode> ();

			var knownEnumerands = ProcessOperandTypes (doc["operand_kinds"], generator, nodes);
			ProcessInstructions (doc["instructions"], knownEnumerands, generator, nodes);

			var cu = generator.CompilationUnit (
				generator.NamespaceImportDeclaration ("System"),
				generator.NamespaceImportDeclaration ("System.Collections.Generic"),
				generator.NamespaceDeclaration ("SpirV", nodes));

			GenerateCode (cu, workspace, "../SPIRV/SpirV.Core.Grammar.cs");
		}

		private static void ProcessSpirv (SyntaxGenerator generator, Workspace workspace)
		{
			var doc = Newtonsoft.Json.Linq.JObject.ReadFrom (
				new Newtonsoft.Json.JsonTextReader (System.IO.File.OpenText (
					"spirv.json")));

			var xmlDoc = new XmlDocument ();
			xmlDoc.Load ("spir-v.xml");

			var nodes = new List<SyntaxNode> ();

			CreateSpirvMeta (doc, xmlDoc, generator, nodes);

			var cu = generator.CompilationUnit (
				generator.NamespaceImportDeclaration ("System"),
				generator.NamespaceImportDeclaration ("System.Collections.Generic"),
				generator.NamespaceDeclaration ("SpirV", nodes));

			GenerateCode (cu, workspace, "../SPIRV/SpirV.Meta.cs");
		}

		private static void GenerateCode (SyntaxNode node, Workspace workspace,
			string path)
		{
			node = Formatter.Format (node, workspace);

			System.IO.File.WriteAllText (path,
				node.ToFullString ());
		}

		private static void CreateSpirvMeta (Newtonsoft.Json.Linq.JToken jr,
			XmlDocument doc, SyntaxGenerator generator, IList<SyntaxNode> nodes)
		{
			var meta = new SpirvMeta (jr["spv"]["meta"], doc.SelectSingleNode ("/registry/ids") as XmlElement);

			nodes.Add (meta.ToSourceFragment (generator));
		}
	}
}
