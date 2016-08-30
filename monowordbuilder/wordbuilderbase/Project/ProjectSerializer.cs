using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Whee.WordBuilder.Helpers;
using Whee.WordBuilder.Model.Commands;

public delegate bool LineParserDelegate(object context, TextReader reader, string line, ref int lineNumber);

namespace Whee.WordBuilder.Model
{
	public sealed class ProjectSerializer
	{
		private ProjectSerializer()
		{
		}
			
		public static bool ReadLines(object context, TextReader reader, LineParserDelegate parser, ref int lineNumber)
		{
			string line = reader.ReadLine();
			while (line != null) {
				line = line.Trim(' ', '\x09');
				lineNumber += 1;
				if (line.StartsWith("/*")) {
					int startingLine = lineNumber;
					while (line != null && !line.EndsWith("*/")) {
						line = reader.ReadLine();
						lineNumber += 1;
					}
					if (line == null) {
						throw new ApplicationException(string.Format("Line {0}: Block comment not closed correctly.", startingLine));
					}
				}
				else if (line.StartsWith("//")) {
				}
				// Ignore single line comments.
				else if (!parser.Invoke(context, reader, line.Trim(' ', '\x09'), ref lineNumber)) {
					return false;
				}
				line = reader.ReadLine();
			}
	
			return true;
		}
	
		public static List<string> ReadTokens(string input)
		{
			List<string> result = new List<string>();
			int start = 0;
			List<string> expand = new List<string>();
			bool didExpand = false;
			bool doRemove = false;
			
			while (start > -1) {
				string token = ReadToken(input, ref start, ref expand, ref didExpand, ref doRemove);
	
				if (didExpand) {
					result.AddRange(expand);
					expand.Clear();
				}
				else {
					result.Add(token);
				}
			}
	
			return result;
		}
	
		public static string ExpandToken(string input, int origin, ref int start, ref List<string> expandedToken, ref bool didExpand, ref bool @remove)
		{
			string token = input.Substring(origin, start - origin);
	
			if (token.Contains("[")) {
				int nextOpen = input.IndexOf('[', start);
				start = input.IndexOf(']', start - 1) + 1;
	
				while (nextOpen > -1 && nextOpen < start - 1) {
					nextOpen = input.IndexOf('[', nextOpen + 1);
					start = input.IndexOf(']', start) + 1;
				}
	
				if (start == 0) {
					throw new ApplicationException("Read token past end of line. Missing a ], are we?");
				}
	
				token = input.Substring(origin, start - origin);
				string[] parts = token.Substring(0, token.Length - 1).Split(new char[] { '[' }, 2);
	
				int copies = 0;
				if (expandedToken != null && int.TryParse(parts[0], out copies)) {
					List<string> copySet = ReadTokens(parts[1]);
					for (int i = 1; i <= copies; i++) {
						expandedToken.AddRange(copySet);
					}
					didExpand = true;
				}
				else if (expandedToken != null && parts[0] == "!") {
					expandedToken.AddRange(ReadTokens(parts[1]));
					@remove = true;
				}
			}
	
			return token;
		}
	
		public static string ReadToken(string input, ref int start)
		{
			List<string> expandedToken = null;
			bool didExpand = false;
			bool doRemove = false;
			return ReadToken(input, ref start, ref expandedToken, ref didExpand, ref doRemove);
		}
		
		public static string ReadToken(string input, ref int start,ref List<string> expandedToken, ref bool didExpand, ref bool @remove)
		{
			if (start == -1) {
				throw new ApplicationException("Read token past end of line");
			}
			didExpand = false;
			@remove = false;
	
			int len = input.Length;
	
			if (input[start] == '"') {
				int origin = start + 1;
				while (start > -1 && start < len) {
					start = input.IndexOf('"', start + 1);
	
					if (start == -1) {
						break; // TODO: might not be correct. Was : Exit While
					}
	
					if (input[start - 1] != '\\') {
						int tokenlen = start - origin;
						start += 1;
	
						while (start < len && input[start] == ' ') {
							start += 1;
						}
	
						if (start >= len) {
							start = -1;
						}
						else if (input.Substring(start).StartsWith("//")) {
							start = -1;
						}
	
						return input.Substring(origin, tokenlen).Replace("\\\"", "\"").Replace("\\\\", "\\");
					}
				}
	
				throw new ApplicationException("Read token past end of line. Missing a \", are we?");
			}
			else {
				int origin = start;
	
				start = input.IndexOf(' ', start);
	
				if (start > -1) {
					string foundtoken = ExpandToken(input, origin, ref start, ref expandedToken, ref didExpand, ref @remove);
					while (start < len && input[start] == ' ') {
						start += 1;
					}
	
					if (start >= len || input.Substring(start).StartsWith("//")) {
						start = -1;
					}
	
					return foundtoken;
				}
				else if (input[input.Length - 1] == ']') {
					string foundtoken = ExpandToken(input, origin, ref len, ref expandedToken, ref didExpand, ref @remove);
					start = -1;
	
					return foundtoken;
				}
				else {
					return input.Substring(origin);
				}
			}
		}
	
		private static void ReadProject(Project project, TextReader reader)
		{
            //int linenumber = 0;
			
            //if (!ReadLines(project, reader, ParseProject, ref linenumber)) {
            //    project.Warnings.Add("The code was not read to the end of the document");
            //}
	
            //if (project.StartRules.Count == 0) {
            //    project.StartRules.Add("root", 100);
            //}
	
            //foreach (string sr in project.StartRules.Keys) {
            //    if (project.Rules.GetRuleByName(sr) == null) {
            //        project.Warnings.Add(string.Format("The starting rule '{0}' does not exist.", sr));
            //    }
            //}
	
            //foreach (Rule r in project.Rules) {
            //    foreach (CommandBase c in r.Commands) {
            //        c.CheckSanity(project);
            //    }
            //}
		}
	
		private static bool ParseProject(object context, TextReader reader, string line, ref int lineNumber)
		{
			Project project = context as Project;
	
			int start = 0;
	
			try {
				if (!string.IsNullOrEmpty(line)) {
					string command = ReadToken(line, ref start);
	
					if (command.Equals("tokens", StringComparison.CurrentCultureIgnoreCase)) {
                        TokenSet tks = new TokenSet(null, Whee.WordBuilder.Helpers.Random.Instance);
						int len = line.Length;
						string token = ReadToken(line, ref start);
						tks.Name = token;
	
						List<string> expand = new List<string>();
						bool didExpand = false;
						bool @remove = false;
						while (start > -1 && start < len) {
							token = ReadToken(line, ref start, ref expand, ref didExpand, ref @remove);
	
							if (!didExpand && !@remove) {
								if (token.StartsWith("$") && token.Length > 1) {
									TokenSet refTks = project.TokenSets.FindByName(token.Substring(1));
									if (refTks != null) {
										tks.Tokens.AddRange(refTks.Tokens);
									}
									else {
										project.Warnings.Add(string.Format("Line {0}: The Tokens directive referenced another token set '{1}' which did not exist at the time of parsing.", lineNumber, token.Substring(1)));
									}
								}
								else {
									tks.Tokens.Add(token);
								}
							}
						}
	
						if (@remove) {
							tks.Tokens.RemoveAll((string tok) => expand.Contains(tok));
						}
						else {
							tks.Tokens.AddRange(expand);
						}
	
						if (tks.Tokens.Count == 0) {
							project.Warnings.Add(string.Format("Line {0}: The Tokens directive requires at least 2 arguments.", lineNumber));
						}
	
						project.TokenSets.Add(tks);
					}
					else if (command.Equals("rule", StringComparison.CurrentCultureIgnoreCase)) {
						Rule r = new Rule(null);
						r.Name = ReadToken(line, ref start);
						r.LineNumber = lineNumber;
						double probability = 1;
	
						string probstring = ReadToken(line, ref start);
						if (probstring == "{") {
							if (start > -1) {
								throw new ApplicationException("The Rule directive must end with a {");
							}
						}
						else {
							if (!double.TryParse(probstring, out probability)) {
								project.Warnings.Add(string.Format("Line {0}: The Rule directive requires the second argument to be numeric.", lineNumber));
							}
	
							probstring = ReadToken(line, ref start);
	
							if (probstring != "{") {
								throw new ApplicationException("The Rule directive must end with a {");
							}
						}
	
						r.Probability = probability;
	
						project.Rules.Add(r);
						if (ReadLines(new object[] { project, r.Commands }, reader, ParseCommands, ref lineNumber)) {
							project.Warnings.Add(string.Format("Line {1}: The rule '{0}' is not closed correctly.", r.Name, lineNumber));
						}
					}
					else if (command.Equals("startingrule", StringComparison.CurrentCultureIgnoreCase)) {
						string rulename = ReadToken(line, ref start);
	
						if (start > -1) {
							string amountstring = ReadToken(line, ref start);
	
							if (start > -1) {
								project.Warnings.Add(string.Format("Line {0}: The StartingRule directive requires 1 or 2 arguments.", lineNumber));
							}
	
							int val = 0;
							if (int.TryParse(amountstring, out val)) {
								project.StartRules[rulename] = val;
							}
							else {
								project.Warnings.Add(string.Format("Line {0}: The StartingRule directive requires that the second argument is a number.", lineNumber));
							}
						}
						else {
							project.StartRules[rulename] = 100;
						}
					}
					else if (command.Equals("Column", StringComparison.CurrentCultureIgnoreCase)) {
						string name = ReadToken(line, ref start);
						if (start > -1) {
							string value = ReadToken(line, ref start);
	
							project.Columns.Add(name, value);
						}
						else {
							project.Warnings.Add(string.Format("Line {0}: The Column directive requires two arguments.", lineNumber));
						}
					}
					else {
						project.Warnings.Add(string.Format("Line {1}: The command '{0}' was not recognized.", command, lineNumber));
					}
				}
			}
			catch (Exception ex) {
				project.Warnings.Add(string.Format("Line {0}: {1}", lineNumber, ex.Message));
			}
	
			return true;
		}
	
		private static List<Type> s_Commands;
		private static List<Type> Commands {
			get {
				if (s_Commands == null) {
					s_Commands = new List<Type>();
					foreach (Type t in Assembly.GetAssembly(typeof(ProjectSerializer)).GetTypes()) {
						if (t.IsSubclassOf(typeof(CommandBase))) {
							s_Commands.Add(t);
						}
					}
				}
				return s_Commands;
			}
		}
	
		public static bool ParseWeightedCommands(object context, TextReader reader, string line, ref int lineNumber)
		{
			if (string.IsNullOrEmpty(line)) {
				return true;
			}
	
			if (line.StartsWith("}")) {
				return false;
			}
	
			object[] args = context as object[];
			Project project = args[0] as Project;
			List<WeightedCommand> cmds = args[1] as List<WeightedCommand>;
	
			int start = 0;
			string weightStr = ReadToken(line, ref start);
	
			int cmdStart = start;
	
			double weight = 0;
			if (!double.TryParse(weightStr, out weight)) {
				project.Warnings.Add(string.Format("Line {0}: Expected a weighted command.", lineNumber));
			}
			else {
				string command = ReadToken(line, ref start);
	
				List<Type> commandCandidates = new List<Type>();
				foreach (Type cmd in Commands) {
					if (cmd.Name.ToLower().StartsWith(command.ToLower())) {
						commandCandidates.Add(cmd);
					}
				}
	
				if (commandCandidates.Count > 1) {
					project.Warnings.Add(string.Format("Line {1}: The command '{0}' is ambiguous.", command, lineNumber));
				}
				else {
					Type tp = commandCandidates.Count > 0 ? commandCandidates[0] : null;
	
					if (tp != null) {
						CommandBase c = Activator.CreateInstance(tp) as CommandBase;
	
						if (c != null) {
							c.LoadCommand(project, reader, line.Substring(cmdStart), ref lineNumber);
						}
						else {
							project.Warnings.Add(string.Format("Line {1}: The command '{0}' is not valid.", command, lineNumber));
						}
	
						WeightedCommand wc = new WeightedCommand();
						wc.Command = c;
						wc.Weight = weight;
						cmds.Add(wc);
					}
					else {
						project.Warnings.Add(string.Format("Line {1}: The command '{0}' is not valid.", command, lineNumber));
					}
				}
			}
	
			return true;
		}
	
		public static bool ParseCommands(object context, TextReader reader, string line, ref int lineNumber)
		{
			if (string.IsNullOrEmpty(line)) {
				return true;
			}
	
			if (line.StartsWith("}")) {
				return false;
			}
	
			object[] args = context as object[];
			Project project = args[0] as Project;
			CommandCollection cmds = args[1] as CommandCollection;
	
			int start = 0;
			string command = ReadToken(line, ref start);
	
			List<Type> commandCandidates = new List<Type>();
			foreach (Type cmd in Commands) {
				if (cmd.Name.ToLower().StartsWith(command.ToLower())) {
					commandCandidates.Add(cmd);
				}
			}
	
			if (commandCandidates.Count > 1) {
				project.Warnings.Add(string.Format("Line {1}: The command '{0}' is ambiguous.", command, lineNumber));
			}
			else {
				Type tp = commandCandidates.Count > 0 ? commandCandidates[0] : null;
	
				if (tp != null) {
					CommandBase c = Activator.CreateInstance(tp) as CommandBase;
	
					if (c != null) {
						c.LoadCommand(project, reader, line, ref lineNumber);
					}
					else {
						project.Warnings.Add(string.Format("Line {1}: The command '{0}' is not valid.", command, lineNumber));
					}
	
					cmds.Add(c);
				}
				else {
					project.Warnings.Add(string.Format("Line {1}: The command '{0}' is not valid.", command, lineNumber));
				}
			}
	
			return true;
		}
	
		public static void SaveProject(Project project, string path)
		{
			using (StreamWriter tw = new StreamWriter(path, false, System.Text.Encoding.UTF8)) {
				WriteProject(project, tw);
			}
		}
	
		private static void WriteProject(Project project, TextWriter writer)
		{
			foreach (string startRule in project.StartRules.Keys) {
				writer.WriteLine(string.Format("StartingRule {0} {1}", startRule, project.StartRules[startRule]));
			}
	
			foreach (TokenSet ts in project.TokenSets) {
				writer.WriteLine(string.Format("Tokens {0} {1}", ts.Name, string.Join(" ", ts.Tokens.ToArray())));
			}
	
			foreach (Rule r in project.Rules) {
				writer.WriteLine(string.Format("Rule {0} {1} {{}", r.Name, r.Probability));
				r.Commands.WriteCommands(writer);
				writer.WriteLine("}");
			}
		}
	
		public static string SecureString(string input)
		{
			if (input.Contains(" ")) {
				return string.Format("\"{0}\"", input);
			}
	
			return input;
		}
	
		public static string SecureList(IEnumerable<string> list)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
	
			foreach (string token in list) {
				sb.Append(" ");
				sb.Append(SecureString(token));
			}
	
			return sb.ToString().TrimStart();
		}
	}
}