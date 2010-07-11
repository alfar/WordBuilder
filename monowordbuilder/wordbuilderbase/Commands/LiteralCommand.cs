using System.Collections.Generic;

namespace Whee.WordBuilder.Model.Commands
{
	[Highlight(RegEx = "^\\s*literal ", Color = "Green")]
	public class LiteralCommand : CommandBase
	{
	
		private string _Literal;
		public string Literal {
			get { return _Literal; }
			set { _Literal = value; }
		}
	
		public override void Execute(Context context)
		{
			context.Tokens.Add(Literal);
		}

        public override void LoadCommand(Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            m_lineNumber = serializer.LineNumber;
            ProjectV2.Token literal = serializer.ReadTextToken(this);
            if (literal != null)
            {
                Literal = literal.Text;

                if (serializer.ReadTextToken(this) != null)
                {
                    serializer.Warn("The literal command requires 1 argument.");
                }
            }
            else
            {
                serializer.Warn("The literal command requires 1 argument.");
            }

        }

        public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
		{
			base.LoadCommand(project, reader, line, ref lineNumber);
			List<string> parts = ProjectSerializer.ReadTokens(line);
	
			if (parts.Count != 2) {
				project.Warnings.Add(string.Format("Line {0}: The Literal command requires 1 argument.", lineNumber));
			}
			else {
				_Literal = parts[1];
			}
		}
	
		public override void WriteCommand(System.IO.TextWriter writer)
		{
			writer.WriteLine("Literal {0}", ProjectSerializer.SecureString(_Literal));
		}


        public override void CheckSanity(Project project, Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
        }
    }
}