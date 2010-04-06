using System.Collections.Generic;

namespace Whee.WordBuilder.Project.Commands
{
	[Highlight(RegEx = "^\\s*capitalize ", Color = "Green")]
	public class CapitalizeCommand : CommandBase
	{
	
		public override void CheckSanity(Project project)
		{
			if (_Index == 0) {
				project.Warnings.Add(string.Format("Line {0}: The capitalize command requires the first argument to be a non-zero integer.", LineNumber));
			}
		}
	
		private int _Index;
		public int Index {
			get { return _Index; }
			set { _Index = value; }
		}
	
		public override void Execute(Context context)
		{
			if (context.Tokens.Count > 0) {
				int pos = _Index;
				if (pos < 0) {
					pos = context.Tokens.Count + pos;
				}
				else {
					pos -= 1;
				}
	
				if (pos < 0) {
					pos = 0;
				}
	
				if (pos < context.Tokens.Count) {
					context.Tokens[pos] = string.Format("{0}{1}", char.ToUpper(context.Tokens[pos][0], System.Globalization.CultureInfo.CurrentCulture), context.Tokens[pos].Substring(1));
				}
			}
		}
	
		public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
		{
			base.LoadCommand(project, reader, line, ref lineNumber);
	
			List<string> parts = ProjectSerializer.ReadTokens(line);
	
			if (parts.Count == 1) {
				_Index = -1;
			}
			else if (parts.Count == 2) {
				if (!int.TryParse(parts[1], out _Index)) {
					project.Warnings.Add(string.Format("Line {0}: The capitalize command requires the first argument to be an integer.", lineNumber));
				}
			}
			else {
				project.Warnings.Add(string.Format("Line {0}: The capitalize command requires zero or one argument.", lineNumber));
			}
		}
	
		public override void WriteCommand(System.IO.TextWriter writer)
		{
			writer.WriteLine("Capitalize {0}", _Index);
		}
	}
}