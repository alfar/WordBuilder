using System.Collections.Generic;

namespace Whee.WordBuilder.Model.Commands
{
    [Highlight(RegEx = "^\\s*token ", Color = "Green")]
    public class TokenCommand : CommandBase
    {

        private string _TokenSet;
        public string TokenSet
        {
            get { return _TokenSet; }
            set { _TokenSet = value; }
        }

        public override void Execute(Context context)
        {
            TokenSet ts = Project.TokenSets.FindByName(this.TokenSet);

            if (ts != null)
            {
                context.Tokens.Add(ts.GetToken());
            }
        }

        public override void LoadCommand(Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            m_lineNumber = serializer.LineNumber;

            ProjectV2.Token tokenSet = serializer.ReadTextToken(this);

            if (tokenSet != null)
            {
                _TokenSet = tokenSet.Text;
                tokenSet.Type = Whee.WordBuilder.ProjectV2.TokenType.Name;

                if (serializer.ReadTextToken(this) != null)
                {
                    serializer.Warn("The token command requires one argument.");
                }
            }
            else
            {
                serializer.Warn("The token command requires one argument.");
            }
        }

        public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
        {
            base.LoadCommand(project, reader, line, ref lineNumber);
            List<string> parts = ProjectSerializer.ReadTokens(line);

            if (parts.Count != 2)
            {
                project.Warnings.Add(string.Format("Line {0}: The Token command requires 1 argument.", lineNumber));
            }
            else
            {
                _TokenSet = parts[1];
            }
        }

        public override void WriteCommand(System.IO.TextWriter writer)
        {
            writer.WriteLine("Token {0}", TokenSet);
        }

        public override void CheckSanity(Project project, Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            m_project = project;
            int count = project.TokenSets.CountByName(TokenSet);
            if (count == 0)
            {
                serializer.Warn(string.Format("Line {0}: The token set '{1}' does not exist.", LineNumber, TokenSet));
            }
            else if (count > 1)
            {
                serializer.Warn(string.Format("Line {0}: Multiple token sets with the name '{1}' exist.", LineNumber, TokenSet));
            }
        }
    }
}