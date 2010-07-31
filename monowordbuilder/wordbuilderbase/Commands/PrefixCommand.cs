using System.Collections.Generic;

namespace Whee.WordBuilder.Model.Commands
{
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

        public override void LoadCommand(Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            m_lineNumber = serializer.LineNumber;

            ProjectV2.Token tokenSet = serializer.ReadTextToken(this);

            if (tokenSet != null)
            {
                _TokenSet = tokenSet.Text;

                if (serializer.ReadTextToken(this) != null)
                {
                    serializer.Warn("The prefix command requires one argument.", this);
                }
            }
            else
            {
                serializer.Warn("The prefix command requires one argument.", this);
            }
        }

		public override void WriteCommand(System.IO.TextWriter writer)
		{
			writer.WriteLine("Prefix {0}", TokenSet);
		}

        public override void CheckSanity(Project project, Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            int count = project.TokenSets.CountByName(TokenSet);
            if (count == 0)
            {
                serializer.Warn(string.Format("The token set '{0}' does not exist.", TokenSet), this);
            }
            else if (count > 1)
            {
                serializer.Warn(string.Format("Multiple token sets with the name '{0}' exist.", TokenSet), this);
            }
        }
    }
}