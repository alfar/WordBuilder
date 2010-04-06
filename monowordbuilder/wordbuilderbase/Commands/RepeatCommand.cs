
namespace Whee.WordBuilder.Project.Commands
{
	[Highlight(RegEx = "^\\s*repeat$", Color = "Green")]
	public class RepeatCommand : CommandBase
	{
	
		public override void Execute(Context context)
		{
			if (context.Tokens.Count > 0) {
				context.Tokens.Add(context.Tokens[context.Tokens.Count - 1]);
			}
		}
	
		public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
		{
			base.LoadCommand(project, reader, line, ref lineNumber);
			if (line.ToLower() != "repeat") {
				project.Warnings.Add(string.Format("Line {0}: The repeat command requires 0 arguments.", lineNumber));
			}
		}
	
		public override void WriteCommand(System.IO.TextWriter writer)
		{
			writer.WriteLine("Repeat");
		}
	
		public override void CheckSanity(Project project)
		{
		}
	}
}