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

        public override void LoadCommand(Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            m_lineNumber = serializer.LineNumber;

            ProjectV2.Token name = serializer.ReadTextToken(this);
            if (name != null)
            {
                Name = name.Text;
                name.Type = Whee.WordBuilder.ProjectV2.TokenType.Name;

                ProjectV2.Token value = serializer.ReadTextToken(this);
                if (value != null)
                {
                    Value = value.Text;

                    if (serializer.ReadTextToken(this) != null)
                    {
                        serializer.Warn("The mark command requires 2 arguments.", this);
                    }
                }
                else
                {
                    serializer.Warn("The mark command requires 2 arguments.", this);
                }
            }
            else
            {
                serializer.Warn("The mark command requires 2 arguments.", this);
            }
        }

		public override void WriteCommand(System.IO.TextWriter writer)
		{
			writer.WriteLine("Mark {0} {1}", _Name, _Value);
		}

        public override void CheckSanity(Project project, Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
        }
    }
}