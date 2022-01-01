using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Formatting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SpirV
{
	enum EnumType
	{
		Value,
		Bit
	}

	class Meta
	{
		public Meta (System.Text.Json.JsonElement meta,
			XmlElement ids)
		{
			MagicNumber = meta.GetProperty ("MagicNumber").GetUInt32();
			Version = meta.GetProperty("Version").GetUInt32();
			Revision = meta.GetProperty("Revision").GetUInt32();
			OpCodeMask = meta.GetProperty("OpCodeMask").GetUInt32();
			WordCountShift = meta.GetProperty("WordCountShift").GetUInt32();

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
			sb.Append ("private readonly static Dictionary<int, ToolInfo> toolInfos_ = new Dictionary<int, ToolInfo> {");

			foreach (var kv in toolInfos_) {
				if (kv.Value.name == null) {
					sb.Append ($"{{ {kv.Key}, new ToolInfo (\"{kv.Value.vendor}\") }},");
				} else {

					sb.Append ($"{{ {kv.Key}, new ToolInfo (\"{kv.Value.vendor}\", \"{kv.Value.name}\") }},");
				}
			}

			sb.Append ("};\n");
			sb.Append ("public static IReadOnlyDictionary<int, ToolInfo> Tools {get => toolInfos_;}\n");

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
				DeclarationModifiers.Static | DeclarationModifiers.ReadOnly,
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

		private static void ProcessInstructions (System.Text.Json.JsonElement instructions,
			IReadOnlyDictionary<string, bool> knownEnumerands,
			SyntaxGenerator generator, IList<SyntaxNode> nodes)
		{
			var ins = new List<InstructionItem> ();

			foreach (var instruction in instructions.EnumerateArray()) {
				var i = new InstructionItem ()
				{
					Name = instruction.GetProperty ("opname").GetString(),
					Id = instruction.GetProperty("opcode").GetInt32()
				};

				if (instruction.TryGetProperty("operands", out System.Text.Json.JsonElement operands)) {
					i.Operands = new List<OperandItem> ();
					foreach (var operand in operands.EnumerateArray()) {
						var oe = new OperandItem ()
						{
							Kind = operand.GetProperty ("kind").GetString()
						};

						if (operand.TryGetProperty("quantifier", out System.Text.Json.JsonElement quantifier)) {
							switch (quantifier.GetString()) {
								case "*": oe.Quantifier = "Varying"; break;
								case "?": oe.Quantifier = "Optional"; break;
							}
						} else {
							oe.Quantifier = "Default";
						}

						if (operand.TryGetProperty("name", out System.Text.Json.JsonElement name)) {
							var operandName = name.GetString();

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
			sb.Append ("private static readonly Dictionary<int, Instruction> instructions_ = new Dictionary<int, Instruction> {");

			foreach (var instruction in ins) {
				sb.AppendLine ($"{{ {instruction.Id}, new {instruction.Name}() }},");
			}

			sb.Append (@"};

			public static IReadOnlyDictionary<int, Instruction> OpcodeToInstruction { get => instructions_; }
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

		private static IReadOnlyDictionary<string, bool> ProcessOperandTypes (System.Text.Json.JsonElement OperandTypes,
			SyntaxGenerator generator, IList<SyntaxNode> nodes)
		{
			var result = new Dictionary<string, bool> ();

			// We gather all of the types up-front as we need them in the loop
			foreach (var n in OperandTypes.EnumerateArray()) {
				// We only handle the Enums here, the others are handled 
				// manually
				if (n.GetProperty ("category").GetString() != "ValueEnum"
					&& n.GetProperty ("category").GetString() != "BitEnum") continue;

				var kind = n.GetProperty ("kind").GetString();

				bool hasParameters = false;

				foreach (var enumerant in n.GetProperty("enumerants").EnumerateArray()) {
					if (enumerant.TryGetProperty("parameters", out _))
                    {
						hasParameters = true;
						break;
                    }
				}

				result.Add (kind, hasParameters);
			}

			foreach (var n in OperandTypes.EnumerateArray()) {
				// We only handle the Enums here, the others are handled 
				// manually
				if (n.GetProperty ("category").GetString() != "ValueEnum"
					&& n.GetProperty("category").GetString() != "BitEnum") continue;

				var kind = n.GetProperty("kind").GetString();

				var enumType = n.GetProperty("category").GetString() == "ValueEnum"
					? EnumType.Value : EnumType.Bit;

				IList<OperatorKindEnumerant> enumerants = new List<OperatorKindEnumerant> ();

				foreach (var enumerant in n.GetProperty("enumerants").EnumerateArray()) {
					var oke = new OperatorKindEnumerant
					{
						Name = enumerant.GetProperty ("enumerant").GetString(),
						Value = ParseEnumValue (enumerant.GetProperty("value"))
					};

					if (Char.IsDigit (oke.Name[0])) {
						oke.Name = kind + oke.Name;
					}

					if (enumerant.TryGetProperty("parameters", out System.Text.Json.JsonElement parameters)) {

						oke.Parameters = new List<string>();

						foreach (var parameter in parameters.EnumerateArray())
						{
							oke.Parameters.Add(parameter.GetProperty("kind").GetString());
						}
					}

					enumerants.Add (oke);
				}

				StringBuilder sb = new StringBuilder ();

				if (enumType == EnumType.Bit) {
					sb.AppendLine ("[Flags]");
				}
				sb.AppendLine ($"public enum {kind} : uint");
				sb.AppendLine ("{");
				foreach (var e in enumerants) {
					sb.Append ($"{e.Name} = {e.Value},\n");
				}
				sb.AppendLine ("}");

				sb.AppendLine ($"public class {kind}ParameterFactory : ParameterFactory");
				sb.AppendLine ("{");
				foreach (var e in enumerants) {
					if (e.Parameters == null) continue;
					sb.AppendLine ($"public class {e.Name}Parameter : Parameter");
					sb.AppendLine ("{");
					sb.AppendLine ("public override IReadOnlyList<OperandType> OperandTypes { get => operandTypes_; }");
					sb.AppendLine ("private static readonly List<OperandType> operandTypes_ = new List<OperandType> () {");

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

		private static uint ParseEnumValue (System.Text.Json.JsonElement value)
		{
			if (value.ValueKind == System.Text.Json.JsonValueKind.String) {
				var s = value.ToString ();

				if (s.StartsWith ("0x")) {
					return uint.Parse (s[2..], System.Globalization.NumberStyles.HexNumber);
				} else {
					return uint.Parse (s);
				}
			} else {
				return value.GetUInt32();
			}
		}

		private static void ProcessGrammars (
			SyntaxGenerator generator,
			Workspace workspace)
		{
			var doc = System.Text.Json.JsonDocument.Parse(System.IO.File.ReadAllText(
					"spirv.core.grammar.json"));

			var nodes = new List<SyntaxNode> ();

			var knownEnumerands = ProcessOperandTypes (doc.RootElement.GetProperty("operand_kinds"), generator, nodes);
			ProcessInstructions (doc.RootElement.GetProperty("instructions"), knownEnumerands, generator, nodes);

			var cu = generator.CompilationUnit (
				generator.NamespaceImportDeclaration ("System"),
				generator.NamespaceImportDeclaration ("System.Collections.Generic"),
				generator.NamespaceDeclaration ("SpirV", nodes));

			GenerateCode (cu, workspace, "../../../../SPIRV/SpirV.Core.Grammar.cs");
		}

		private static void ProcessSpirv (SyntaxGenerator generator, Workspace workspace)
		{
			var doc = System.Text.Json.JsonDocument.Parse (System.IO.File.ReadAllText (
					"spirv.json"));

			var xmlDoc = new XmlDocument ();
			xmlDoc.Load ("spir-v.xml");

			var nodes = new List<SyntaxNode> ();

			CreateSpirvMeta (doc.RootElement, xmlDoc, generator, nodes);

			var cu = generator.CompilationUnit (
				generator.NamespaceImportDeclaration ("System"),
				generator.NamespaceImportDeclaration ("System.Collections.Generic"),
				generator.NamespaceDeclaration ("SpirV", nodes));

			GenerateCode (cu, workspace, "../../../../SPIRV/SpirV.Meta.cs");
		}

		private static void GenerateCode (SyntaxNode node, Workspace workspace,
			string path)
		{
			node = Formatter.Format (node, workspace);

			System.IO.File.WriteAllText (path,
				node.ToFullString ());
		}

		private static void CreateSpirvMeta (System.Text.Json.JsonElement jr,
			XmlDocument doc, SyntaxGenerator generator, IList<SyntaxNode> nodes)
		{
			var meta = new Meta (jr.GetProperty("spv").GetProperty("meta"), doc.SelectSingleNode ("/registry/ids") as XmlElement);

			nodes.Add (meta.ToSourceFragment (generator));
		}
	}
}
