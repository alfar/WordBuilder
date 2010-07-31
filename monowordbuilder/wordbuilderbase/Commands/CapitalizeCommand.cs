using System.Collections.Generic;

namespace Whee.WordBuilder.Model.Commands
{
	[Highlight(RegEx = "^\\s*capitalize ", Color = "Green")]
	public class CapitalizeCommand : CommandBase
	{	
		private int _Position;
		public int Position {
			get { return _Position; }
			set { _Position = value; }
		}
	
		public override void Execute(Context context)
		{
			if (context.Tokens.Count > 0) {
				int pos = _Position;
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

        public override void LoadCommand(Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            m_lineNumber = serializer.LineNumber;

            double position = 0;
            bool found = false;
            ProjectV2.Token posToken = serializer.ReadNumericToken(this, ref position, out found);
            if (posToken != null)
            {
                _Position = (int)position;

                if (serializer.ReadTextToken(this) != null)
                {
                    serializer.Warn("The capitalize command requires zero or one argument.", this);
                }
            }
            else if (found)
            {
                serializer.Warn("The capitalize command requires the first argument to be an integer.", this);
            }
            else
            {
                _Position = -1;
            }

        }
	
		public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
		{
			base.LoadCommand(project, reader, line, ref lineNumber);
	
			List<string> parts = ProjectSerializer.ReadTokens(line);
	
			if (parts.Count == 1) {
				_Position = -1;
			}
			else if (parts.Count == 2) {
				if (!int.TryParse(parts[1], out _Position)) {
					project.Warnings.Add("The capitalize command requires the first argument to be an integer.");
				}
			}
			else {
				project.Warnings.Add("The capitalize command requires zero or one argument.");
			}
		}
	
		public override void WriteCommand(System.IO.TextWriter writer)
		{
			writer.WriteLine("Capitalize {0}", _Position);
		}

        public override void CheckSanity(Project project, Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            if (_Position == 0)
            {
                serializer.Warn("The capitalize command requires the first argument to be a non-zero integer.", this);
            }
        }
    }
}