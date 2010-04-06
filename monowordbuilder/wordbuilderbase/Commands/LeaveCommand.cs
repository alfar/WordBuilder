using System.Collections.Generic;

namespace Whee.WordBuilder.Project.Commands
{
	[Highlight(RegEx = "^\\s*leave ", Color = "Green")]
	public class LeaveCommand : CommandBase
	{
	
		private int _Amount;
		public int Amount {
			get { return _Amount; }
			set { _Amount = value; }
		}
	
		public override void CheckSanity(Project project)
		{
			if (_Amount < 0) {
				project.Warnings.Add(string.Format("Line {0}: Leave command requires a positive integer or zero as its second argument.", m_lineNumber));
			}
		}
	
		public override void Execute(Context context)
		{
			if (context.Tokens.Count > _Amount) {
				context.Tokens.RemoveRange(_Amount, context.Tokens.Count - _Amount);
			}
		}
	
		public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
		{
			base.LoadCommand(project, reader, line, ref lineNumber);
	
			List<string> parts = ProjectSerializer.ReadTokens(line);
	
			if (parts.Count == 1) {
				_Amount = 1;
			}
			else if (parts.Count == 2) {
				if (!int.TryParse(parts[1], out _Amount)) {
					project.Warnings.Add(string.Format("Line {0}: Leave command requires a positive integer or zero as its second argument.", m_lineNumber));
				}
			}
		}
	
		public override void WriteCommand(System.IO.TextWriter writer)
		{
			writer.WriteLine("Leave {0}", _Amount);
		}
	}
}