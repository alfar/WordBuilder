using System.Collections.Generic;

namespace Whee.WordBuilder.Model.Commands
{
	[Highlight(RegEx = "^\\s*mark ", Color = "Green")]
	public class MarkCommand : CommandBase
	{
	
		private string _Name;
		public string Name {
			get { return _Name; }
			set { _Name = value; }
		}
	
		private string _Value;
		public string Value {
			get { return _Value; }
			set { _Value = value; }
		}
	
	
		public override void Execute(Context context)
		{
			context.Mark(Name, Value);
		}
	
		public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
		{
			base.LoadCommand(project, reader, line, ref lineNumber);
	
			List<string> parts = ProjectSerializer.ReadTokens(line);
	
			if (parts.Count != 3) {
				project.Warnings.Add(string.Format("Line {0}: The mark command requires 2 arguments.", lineNumber));
			}
			else {
				_Name = parts[1];
				_Value = parts[2];
			}
		}
	
		public override void WriteCommand(System.IO.TextWriter writer)
		{
			writer.WriteLine("Mark {0} {1}", _Name, _Value);
		}
	
		public override void CheckSanity(Project project)
		{
	
		}
	}
}