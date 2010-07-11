using System.Collections.Generic;

namespace Whee.WordBuilder.Model.Commands
{
    [Highlight(RegEx = "^\\s*drop$", Color = "Green")]
    [Highlight(RegEx = "^\\s*drop ", Color = "Green")]
    public class DropCommand : CommandBase
    {

        private int _Amount;
        public int Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        public override void Execute(Context context)
        {
            if (context.Tokens.Count > _Amount)
            {
                context.Tokens.RemoveRange(context.Tokens.Count - _Amount, _Amount);
            }
            else
            {
                context.Tokens.Clear();
            }
        }

        public override void LoadCommand(Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            m_lineNumber = serializer.LineNumber;

            bool found = false;
            double amount = 0.0;
            ProjectV2.Token amountToken = serializer.ReadNumericToken(this, ref amount, out found);

            if (amountToken != null)
            {
                _Amount = (int)amount;
                if (serializer.ReadTextToken(this) != null)
                {
                    serializer.Warn("The drop command requires zero or one argument.");
                }
            }
            else if (found)
            {
                serializer.Warn("The drop command requires the first argument to be a positive integer.");
            }
            else
            {
                _Amount = 1;
            }
        }

        public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
        {
            base.LoadCommand(project, reader, line, ref lineNumber);

            List<string> parts = ProjectSerializer.ReadTokens(line);

            if (parts.Count == 1)
            {
                _Amount = 1;
            }
            else if (parts.Count == 2)
            {
                if (!int.TryParse(parts[1], out _Amount))
                {
                    project.Warnings.Add(string.Format("Line {0}: Drop command requires a positive integer as its second argument.", m_lineNumber));
                }
            }
        }

        public override void WriteCommand(System.IO.TextWriter writer)
        {
            writer.WriteLine("Drop {0}", _Amount);
        }

        public override void CheckSanity(Project project, Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            if (_Amount <= 0)
            {
                serializer.Warn(string.Format("Line {0}: Drop command requires a positive integer as its second argument.", m_lineNumber));
            }
        }
    }
}