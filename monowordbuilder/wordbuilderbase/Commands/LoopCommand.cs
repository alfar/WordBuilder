using System;
using System.Collections.Generic;
using Whee.WordBuilder.ProjectV2;

namespace Whee.WordBuilder.Model.Commands
{
    [Highlight(RegEx = "^\\s*loop ", Color = "Green")]
    public class LoopCommand : CommandBase
    {

        private CommandCollection _commands = new CommandCollection();
        public CommandCollection Commands
        {
            get { return _commands; }
        }

        private List<int> _Repetitions = new List<int>();
        public List<int> Repetitions
        {
            get { return _Repetitions; }
        }

        private static Random _Random = new Random();

        public override void Execute(Context context)
        {
            int reps = Repetitions[_Random.Next(0, Repetitions.Count)];

            while (reps > 0)
            {
                Commands.Execute(context);
                reps -= 1;
            }
        }

        public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
        {
            base.LoadCommand(project, reader, line, ref lineNumber);

            List<string> parts = ProjectSerializer.ReadTokens(line);

            if (parts[parts.Count - 1] != "{")
            {
                project.Warnings.Add(string.Format("Line {0}: The Loop command requires a {{ at the end of the line.", lineNumber));
            }
            else
            {
                parts.RemoveAt(0);
                parts.RemoveAt(parts.Count - 1);

                if (parts.Count > 0)
                {
                    int reps;
                    foreach (string rep in parts)
                    {
                        if (int.TryParse(rep, out reps))
                        {
                            Repetitions.Add(reps);
                        }
                        else
                        {
                            project.Warnings.Add(string.Format("Line {0}: The loop command requires all arguments to be numeric. '{1}' is not.", lineNumber, rep));
                        }
                    }

                    if (ProjectSerializer.ReadLines(new object[] { project, Commands }, reader, ProjectSerializer.ParseCommands, ref lineNumber))
                    {
                        project.Warnings.Add(string.Format("Line {0}: The loop command is not closed correctly.", lineNumber));
                    }
                }
                else
                {
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

        public override void LoadCommand(Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            double num = 0;
            bool found = true;

            Token amount = null;
            while (found)
            {
                amount = serializer.ReadNumericToken(this, ref num, out found);
                if (amount != null)
                {
                    _Repetitions.Add((int)num);
                }

                if (amount == null && found)
                {
                    int reps = 0;
                    string data;
                    amount = serializer.ReadRepeatingToken(this, out reps, out data);

                    if (amount != null)
                    {
                        List<int> numbers = new List<int>();
                        foreach (string numstring in data.Split(' '))
                        {
                            int number = 0;
                            if (int.TryParse(numstring, out number))
                            {
                                numbers.Add(number);
                            }
                            else
                            {
                                serializer.Warn("Expected numbers only", this);
                            }
                        }

                        for (int i = 0; i < reps; i++)
                        {
                            _Repetitions.AddRange(numbers);
                        }
                    }
                    else
                    {
                        found = false;
                    }
                }
            }

            CommandBlockNode cbn = new CommandBlockNode(serializer);

            Children.Add(cbn);

            foreach (CommandBase cmd in cbn.Commands)
            {
                Commands.Add(cmd);
            }
        }

        public override void CheckSanity(Project project, Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            foreach (CommandBase cmd in Commands)
            {
                cmd.CheckSanity(project, serializer);
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