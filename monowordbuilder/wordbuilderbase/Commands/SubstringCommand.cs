using System.Collections.Generic;

namespace Whee.WordBuilder.Project.Commands
{
	[Highlight(RegEx = "^\\s*substring ", Color = "Green")]
	public class SubstringCommand : CommandBase
	{
	
		private int _StartIndex = 1;
		public int StartIndex {
			get { return _StartIndex; }
			set { _StartIndex = value; }
		}
	
		private int _EndIndex = -1;
		public int EndIndex {
			get { return _EndIndex; }
			set { _EndIndex = value; }
		}
	
		public override void CheckSanity(Project project)
		{
			if (_StartIndex > 0 && _EndIndex > 0 && _EndIndex < _StartIndex) {
				project.Warnings.Add(string.Format("Line {0}: You want to start after the end?"));
			}
			if (_StartIndex < 0 && _EndIndex < 0 && _EndIndex < _StartIndex) {
				project.Warnings.Add(string.Format("Line {0}: You want to start after the end?"));
			}
		}
	
		public override void Execute(Context context)
		{
			if (context.Tokens.Count > 0) {
				int s = _StartIndex;
				if (s < 0) {
					s = context.Tokens[context.Tokens.Count - 1].Length + s;
				}
				else {
					s -= 1;
				}
	
				if (s < 0) {
					s = 0;
				}
	
				int e = _EndIndex;
				if (e < 0) {
					e = context.Tokens[context.Tokens.Count - 1].Length + e;
				}
				else {
					e -= 1;
				}
	
				// Because s at this point is always >= 0, this includes the case for e < 0, so no need to double check.
				if (s > e) {
					context.Tokens.RemoveAt(context.Tokens.Count - 1);
				}
				else {
					context.Tokens[context.Tokens.Count - 1] = context.Tokens[context.Tokens.Count - 1].Substring(s, e - s + 1);
				}
			}
		}
	
		public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
		{
			base.LoadCommand(project, reader, line, ref lineNumber);
	
			List<string> parts = ProjectSerializer.ReadTokens(line);
	
			if (parts.Count == 2) {
				if (!int.TryParse(parts[1], out _StartIndex)) {
					project.Warnings.Add(string.Format("Line {0}: Substring command requires an integer as its first argument.", m_lineNumber));
				}
			}
			else if (parts.Count == 3) {
				if (!int.TryParse(parts[1], out _StartIndex)) {
					project.Warnings.Add(string.Format("Line {0}: Substring command requires an integer as its first argument.", m_lineNumber));
				}
	
				if (!int.TryParse(parts[2], out _EndIndex)) {
					project.Warnings.Add(string.Format("Line {0}: Substring command requires an integer as its second argument.", m_lineNumber));
				}
			}
			else {
				project.Warnings.Add(string.Format("Line {0}: Substring command requires one or two integer arguments.", m_lineNumber));
			}
		}
	
		public override void WriteCommand(System.IO.TextWriter writer)
		{
			writer.WriteLine("Substring {0} {1}", StartIndex, EndIndex);
		}
	}
}