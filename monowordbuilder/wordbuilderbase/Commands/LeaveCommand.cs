using System.Collections.Generic;

namespace Whee.WordBuilder.Model.Commands
{
	[Highlight(RegEx = "^\\s*leave ", Color = "Green")]
	public class LeaveCommand : CommandBase
	{
	
		private int _Amount;
		public int Amount {
			get { return _Amount; }
			set { _Amount = value; }
		}
	
		public override void Execute(Context context)
		{
			if (context.Tokens.Count > _Amount) {
				context.Tokens.RemoveRange(_Amount, context.Tokens.Count - _Amount);
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
                    serializer.Warn("The leave command requires zero or one argument.", this);
                }
            }
            else if (found)
            {
                serializer.Warn("The leave command requires the first argument to be a positive integer.", this);
            }
            else
            {
                _Amount = 1;
            }
        }
	
		public override void WriteCommand(System.IO.TextWriter writer)
		{
			writer.WriteLine("Leave {0}", _Amount);
		}

        public override void CheckSanity(Project project, Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            if (_Amount < 0)
            {
                serializer.Warn("Leave command requires a positive integer or zero as its second argument.", this);
            }
        }
    }
}