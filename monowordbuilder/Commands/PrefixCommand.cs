using System.Collections.Generic;

[Highlight(RegEx = "^\\s*prefix ", Color = "Green")]
public class PrefixCommand : CommandBase
{

	private string _TokenSet;
	public string TokenSet {
		get { return _TokenSet; }
		set { _TokenSet = value; }
	}

	public override void Execute(Context context)
	{
		TokenSet ts = Project.TokenSets.FindByName(this.TokenSet);

		if (ts != null) {
			context.Tokens.Insert(0, ts.GetToken());
		}
	}

	public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
	{
		base.LoadCommand(project, reader, line, ref lineNumber);
		List<string> parts = ProjectSerializer.ReadTokens(line);

		if (parts.Count != 2) {
			project.Warnings.Add(string.Format("Line {0}: The Prefix command requires 1 argument.", lineNumber));
		}
		else {
			_TokenSet = parts[1];
		}
	}

	public override void WriteCommand(System.IO.TextWriter writer)
	{
		writer.WriteLine("Prefix {0}", TokenSet);
	}


	public override void CheckSanity(Project project)
	{
		int count = project.TokenSets.CountByName(TokenSet);
		if (count == 0) {
			project.Warnings.Add(string.Format("Line {0}: The token set '{1}' does not exist.", LineNumber, TokenSet));
		}
		else if (count > 1) {
			project.Warnings.Add(string.Format("Line {0}: Multiple token sets with the name '{1}' exist.", LineNumber, TokenSet));
		}
	}
}
