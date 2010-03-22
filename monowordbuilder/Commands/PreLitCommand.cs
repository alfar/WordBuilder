using System.Collections.Generic;

[Highlight(RegEx = "^\\s*prelit ", Color = "Green")]
public class PreLitCommand : CommandBase
{

	private string _Literal;
	public string Literal {
		get { return _Literal; }
		set { _Literal = value; }
	}

	public override void Execute(Context context)
	{
		context.Tokens.Insert(0, Literal);
	}

	public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
	{
		base.LoadCommand(project, reader, line, ref lineNumber);
		List<string> parts = ProjectSerializer.ReadTokens(line);

		if (parts.Count != 2) {
			project.Warnings.Add(string.Format("Line {0}: The PreLit command requires 1 argument.", lineNumber));
		}
		else {
			_Literal = parts[1];
		}
	}

	public override void WriteCommand(System.IO.TextWriter writer)
	{
		writer.WriteLine("PreLit {0}", ProjectSerializer.SecureString(_Literal));
	}


	public override void CheckSanity(Project project)
	{
	}
}
