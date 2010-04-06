using System.Collections.Generic;

namespace Whee.WordBuilder.Project.Commands
{
	[Highlight(RegEx = "^\\s*branch ", Color = "Green")]
	public class BranchCommand : CommandBase
	{
	
		private string _Rule;
		public string Rule {
			get { return _Rule; }
			set { _Rule = value; }
		}
	
		private string _Name;
		public string Name {
			get { return _Name; }
			set { _Name = value; }
		}
	
		public override void Execute(Context context)
		{
			Project.Rules.GetRuleByName(Rule).Execute(context.Branch(Name));
		}
	
		public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
		{
			base.LoadCommand(project, reader, line, ref lineNumber);
	
			List<string> parts = ProjectSerializer.ReadTokens(line);
	
			if (parts.Count != 3) {
				project.Warnings.Add(string.Format("Line {0}: The branch command requires 2 arguments.", lineNumber));
			}
			else {
				_Name = parts[1];
				_Rule = parts[2];
			}
		}
	
		public override void WriteCommand(System.IO.TextWriter writer)
		{
			writer.WriteLine("Branch {0} {1}", _Name, _Rule);
		}
	
		public override void CheckSanity(Project project)
		{
			if (project.Rules.GetRuleByName(Rule) == null) {
				project.Warnings.Add(string.Format("Line {0}: The rule '{1}' does not exist.", LineNumber, Rule));
			}
		}
	}
}