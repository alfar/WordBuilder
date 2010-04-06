using System;
using System.Collections.Generic;

namespace Whee.WordBuilder.Project.Commands
{
	[Highlight(RegEx = "^\\s*loop ", Color = "Green")]
	public class LoopCommand : CommandBase
	{
	
		private CommandCollection _commands = new CommandCollection();
		public CommandCollection Commands {
			get { return _commands; }
		}
	
		private List<int> _Repetitions = new List<int>();
		public List<int> Repetitions {
			get { return _Repetitions; }
		}
	
		private static Random _Random = new Random();
	
		public override void CheckSanity(Project project)
		{
			foreach (CommandBase cmd in Commands) {
				cmd.CheckSanity(project);
			}
		}
	
		public override void Execute(Context context)
		{
			int reps = Repetitions[_Random.Next(0, Repetitions.Count)];
	
			while (reps > 0) {
				Commands.Execute(context);
				reps -= 1;
			}
		}
	
		public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
		{
			base.LoadCommand(project, reader, line, ref lineNumber);
	
			List<string> parts = ProjectSerializer.ReadTokens(line);
	
			if (parts[parts.Count - 1] != "{") {
				project.Warnings.Add(string.Format("Line {0}: The Loop command requires a {{ at the end of the line.", lineNumber));
			}
			else {
				parts.RemoveAt(0);
				parts.RemoveAt(parts.Count - 1);
	
				if (parts.Count > 0) {
					int reps;
					foreach (string rep in parts) {
						if (int.TryParse(rep, out reps)) {
							Repetitions.Add(reps);
						}
						else {
							project.Warnings.Add(string.Format("Line {0}: The loop command requires all arguments to be numeric. '{1}' is not.", lineNumber, rep));
						}
					}
	
					if (ProjectSerializer.ReadLines(new object[] { project, Commands }, reader, ProjectSerializer.ParseCommands, ref lineNumber)) {
						project.Warnings.Add(string.Format("Line {0}: The loop command is not closed correctly.", lineNumber));
					}
				}
				else {
					project.Warnings.Add(string.Format("Line {0}: The loop command must have one or more numeric arguments.", lineNumber));
				}
			}
		}
	
		public override void WriteCommand(System.IO.TextWriter writer)
		{
			writer.WriteLine(string.Format("Loop {0} {{", ProjectSerializer.SecureList(Repetitions.ConvertAll<string>((int rep) => rep.ToString()))));
			Commands.WriteCommands(writer);
			writer.WriteLine("}");
		}
	}
}