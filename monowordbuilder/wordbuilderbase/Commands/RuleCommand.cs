using System.Collections.Generic;

namespace Whee.WordBuilder.Project.Commands
{
	[Highlight(RegEx = "^\\s*rule ", Color = "Green")]
	public class RuleCommand : CommandBase
	{
	
		private string _Rule;
	
		public string Rule {
			get { return _Rule; }
			set { _Rule = value; }
		}
	
		public override void Execute(Context context)
		{
			Project.Rules.GetRuleByName(Rule).Execute(context);
		}
	
		public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
		{
			base.LoadCommand(project, reader, line, ref lineNumber);
	
			List<string> parts = ProjectSerializer.ReadTokens(line);
	
			if (parts.Count != 2) {
				project.Warnings.Add(string.Format("Line {0}: The rule command requires 1 argument.", lineNumber));
			}
			else {
				_Rule = parts[1];
			}
		}
	
		public override void WriteCommand(System.IO.TextWriter writer)
		{
			writer.WriteLine("Rule {0}", Rule);
		}
	
		public override void CheckSanity(Project project)
		{
			if (project.Rules.GetRuleByName(Rule) == null) {
				project.Warnings.Add(string.Format("Line {0}: The rule '{1}' does not exist.", LineNumber, Rule));
			}
		}
	}
}