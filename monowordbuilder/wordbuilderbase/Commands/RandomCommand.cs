using System;
using System.Collections.Generic;

namespace Whee.WordBuilder.Model.Commands
{
	[Highlight(RegEx = "^\\s*random \\\\\\{", Color = "Green", NextLevel = 3)]
	[Highlight(RegEx = "^\\s*\\d+(\\.\\d+)?", Color = "Red", Level = 3)]
	[Highlight(RegEx = "\\\\\\}", Color = "Green", Level = 3, NextLevel = -2)]
	public class RandomCommand : CommandBase
	{
	
		private static Random _Random = new Random();
	
		public override void Execute(Context context)
		{
			double total = 0;
			foreach (WeightedCommand wc in Commands) 
			{
				total += wc.Weight;
			}
	
			double selected = _Random.NextDouble() * total;
	
			foreach (WeightedCommand wc in Commands) {
				selected -= wc.Weight;
				if (selected <= 0) {
					wc.Command.Execute(context);
					return;
				}
			}
		}
	
		private List<WeightedCommand> _Commands = new List<WeightedCommand>();
		public List<WeightedCommand> Commands {
			get { return _Commands; }
		}

        public override void LoadCommand(Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            ProjectV2.WeightedCommandBlockNode wcbn = new ProjectV2.WeightedCommandBlockNode(serializer);

            Children.Add(wcbn);

            foreach (ProjectV2.WeightedCommandNode wcn in wcbn.Children)
            {
                WeightedCommand wc = new WeightedCommand();
                wc.Weight = wcn.Weight;
                wc.Command = wcn.Command;
                Commands.Add(wc);
            }
        }
	
		public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
		{
			base.LoadCommand(project, reader, line, ref lineNumber);
	
			List<string> parts = ProjectSerializer.ReadTokens(line);
	
			if (parts[parts.Count - 1] != "{") {
				project.Warnings.Add(string.Format("Line {0}: The Random command requires a {{ at the end of the line.", lineNumber));
			}
			else {
				parts.RemoveAt(0);
				parts.RemoveAt(parts.Count - 1);
	
				if (parts.Count == 0) {
					if (ProjectSerializer.ReadLines(new object[] { project, Commands }, reader, ProjectSerializer.ParseWeightedCommands, ref lineNumber)) {
						project.Warnings.Add(string.Format("Line {0}: The Random command is not closed correctly.", lineNumber));
					}
				}
				else {
					project.Warnings.Add(string.Format("Line {0}: The Random command must have no arguments.", lineNumber));
				}
			}
		}
	
		public override void WriteCommand(System.IO.TextWriter writer)
		{
			writer.WriteLine("Random {");
	
			foreach (WeightedCommand wc in _Commands) {
				writer.Write("{0} ", wc.Weight.ToString(System.Globalization.CultureInfo.InvariantCulture));
				wc.Command.WriteCommand(writer);
			}
	
			writer.WriteLine("}");
		}

        public override void CheckSanity(Project project, Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            foreach (WeightedCommand cmd in _Commands)
            {
                cmd.Command.CheckSanity(project, serializer);
            }
        }

        public override bool RequireNewLine
        {
            get
            {
                return false;
            }
        }
    }
}