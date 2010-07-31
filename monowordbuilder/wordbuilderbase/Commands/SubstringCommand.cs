using System.Collections.Generic;

namespace Whee.WordBuilder.Model.Commands
{
    [Highlight(RegEx = "^\\s*substring ", Color = "Green")]
    public class SubstringCommand : CommandBase
    {

        private int _StartIndex = 1;
        public int StartIndex
        {
            get { return _StartIndex; }
            set { _StartIndex = value; }
        }

        private int _EndIndex = -1;
        public int EndIndex
        {
            get { return _EndIndex; }
            set { _EndIndex = value; }
        }

        public override void Execute(Context context)
        {
            if (context.Tokens.Count > 0)
            {
                int s = _StartIndex;
                if (s < 0)
                {
                    s = context.Tokens[context.Tokens.Count - 1].Length + s;
                }
                else
                {
                    s -= 1;
                }

                if (s < 0)
                {
                    s = 0;
                }

                int e = _EndIndex;
                if (e < 0)
                {
                    e = context.Tokens[context.Tokens.Count - 1].Length + e;
                }
                else
                {
                    e -= 1;
                }

                // Because s at this point is always >= 0, this includes the case for e < 0, so no need to double check.
                if (s > e)
                {
                    context.Tokens.RemoveAt(context.Tokens.Count - 1);
                }
                else
                {
                    context.Tokens[context.Tokens.Count - 1] = context.Tokens[context.Tokens.Count - 1].Substring(s, e - s + 1);
                }
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
                _StartIndex = (int)amount;

                amountToken = serializer.ReadNumericToken(this, ref amount, out found);

                if (amountToken != null)
                {
                    _EndIndex = (int)amount;
                    if (serializer.ReadTextToken(this) != null)
                    {
                        serializer.Warn("The substring command requires one or two arguments.", this);
                    }
                }
                else if (found)
                {
                    serializer.Warn("The substring command requires its arguments to be positive integers.", this);
                }
            }
            else if (found)
            {
                serializer.Warn("The substring command requires its arguments to be positive integers.", this);
            }
            else
            {
                serializer.Warn("The substring command requires one or two arguments.", this);
            }
        }

        public override void WriteCommand(System.IO.TextWriter writer)
        {
            writer.WriteLine("Substring {0} {1}", StartIndex, EndIndex);
        }

        public override void CheckSanity(Project project, Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            if (_StartIndex > 0 && _EndIndex > 0 && _EndIndex < _StartIndex)
            {
                serializer.Warn("You want to start after the end?", this);
            }
            if (_StartIndex < 0 && _EndIndex < 0 && _EndIndex < _StartIndex)
            {
                serializer.Warn("You want to start after the end?", this);
            }
        }
    }
}