using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Whee.WordBuilder.Model.Commands
{
    [Highlight(RegEx = "^\\s*translate \\\\\\{", Color = "Green", NextLevel = 2)]
    [Highlight(RegEx = "#|=>", Color = "Red", Level = 2)]
    [Highlight(RegEx = "\\\\\\}", Color = "Green", Level = 2, NextLevel = -2)]
    public class TranslateCommand : CommandBase
    {

        private Dictionary<string[], string[]> _stringmapping = new Dictionary<string[], string[]>();
        private Dictionary<Regex, string[]> _mapping = new Dictionary<Regex, string[]>();

        private string ExpandTokenSet(string token, bool ingroup)
        {
            if (token.StartsWith("$"))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                if (!ingroup)
                {
                    sb.Append("(?:");
                }

                foreach (string tk in Project.TokenSets.FindByName(token.Substring(1)).Tokens)
                {
                    sb.Append(tk);
                    sb.Append("|");
                }

                if (!ingroup)
                {
                    sb.Append(")");
                }

                return sb.ToString();
            }

            return token;
        }

        private Regex MakeRegex(string[] sourceOrig)
        {
            string[] source;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (sourceOrig[0] == "#")
            {
                sb.Append("^");
                source = new string[sourceOrig.Length - 2];
                Array.Copy(sourceOrig, 1, source, 0, sourceOrig.Length - 1);
            }
            else
            {
                source = sourceOrig;
            }

            sb.Append("\\|");

            //		bool ingroup = false;

            foreach (string tokeniter in source)
            {
                bool bPickup = false;
                string token = tokeniter;
                if (token.StartsWith("(") && token.EndsWith(")"))
                {
                    bPickup = true;
                    token = token.Substring(1, token.Length - 2);
                    sb.Append("(");
                }

                if (token.StartsWith("["))
                {
                    sb.Append("(?:");

                    foreach (string tk in ProjectSerializer.ReadTokens(token.Substring(1, token.Length - 2)))
                    {
                        sb.Append(ExpandTokenSet(tk, true));
                        sb.Append("|");
                    }

                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(")");
                }
                else if (token == "#")
                {
                    //sb.Remove(sb.Length - 2, 2)
                    sb.Append("$");
                    return new Regex(sb.ToString());
                }
                else
                {
                    sb.Append(ExpandTokenSet(token, false));
                }

                if (bPickup)
                {
                    sb.Append(")");
                }

                sb.Append("\\|");
            }

            //If sb.Length > 2 Then
            //    sb.Remove(sb.Length - 2, 2)
            //End If

            Debug.WriteLine(sb.ToString());
            return new Regex(sb.ToString());
        }

        private string ExpandReplacement(string[] destination, Match m)
        {
            List<string> result = new List<string>();

            foreach (string token in destination)
            {
                int tokenId;
                if (token.StartsWith("\\") && int.TryParse(token.Substring(1), out tokenId))
                {
                    result.Add(m.Groups[tokenId].Value);
                }
                else if (token.StartsWith("$"))
                {
                    result.Add(Project.TokenSets.FindByName(token.Substring(1)).GetToken());
                }
                else if (token.StartsWith("["))
                {
                    List<string> tokens = ProjectSerializer.ReadTokens(token.Substring(1, token.Length - 2));

                    TokenSet ts = Project.CreateTokenSet();

                    foreach (string t in tokens)
                    {
                        ts.Tokens.AddRange(ExpandTokenSet(t, true).Split('|'));
                    }

                    result.Add(ts.GetToken());
                }
                else
                {
                    result.Add(token);
                }
            }

            return "|" + string.Join("|", result.ToArray()) + "|";
        }

        public override void Execute(Context context)
        {
            string input = "|" + string.Join("|", context.Tokens.FindAll((string token) => !string.IsNullOrEmpty(token)).ToArray()) + "|";

            foreach (Regex regex in _mapping.Keys)
            {
                string[] destination = _mapping[regex];

                input = regex.Replace(input, (Match m) => ExpandReplacement(destination, m));
            }

            context.Tokens.Clear();
            context.Tokens.AddRange(input.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public override void LoadCommand(Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            m_lineNumber = serializer.LineNumber;

            ProjectV2.TranslationBlockNode tbn = new ProjectV2.TranslationBlockNode(serializer);

            Children.Add(tbn);

            foreach (ProjectV2.TranslationNode item in tbn.Children)
            {
                _stringmapping.Add(item.Source.ToArray(), item.Destination.ToArray());
            }
        }

        public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
        {
            base.LoadCommand(project, reader, line, ref lineNumber);

            List<string> parts = ProjectSerializer.ReadTokens(line);

            if (parts.Count == 2 && parts[1] == "{")
            {
                if (ProjectSerializer.ReadLines(project, reader, ReadTranslation, ref lineNumber))
                {
                    project.Warnings.Add(string.Format("Line {0}: The Translate command is not closed correctly.", lineNumber));
                }
            }
            else
            {
                project.Warnings.Add(string.Format("Line {0}: The Translate command requires a {{} at the end of the line.", lineNumber));
            }
        }

        private bool ReadTranslation(object context, TextReader reader, string line, ref int lineNumber)
        {
            if (line == "}")
            {
                return false;
            }

            if (string.IsNullOrEmpty(line))
            {
                return true;
            }

            List<string> tokens = ProjectSerializer.ReadTokens(line);

            int translatepos = tokens.IndexOf("=>");

            if (translatepos == -1)
            {
                Project.Warnings.Add(string.Format("Line {0}: Missing => in Translate directive.", lineNumber));
            }
            else if (translatepos == 0)
            {
                Project.Warnings.Add(string.Format("Line {0}: Missing source tokens in Translate directive.", lineNumber));
            }
            else
            {
                _stringmapping.Add(tokens.GetRange(0, translatepos).ToArray(), tokens.GetRange(translatepos + 1, tokens.Count - translatepos - 1).ToArray());
            }

            return true;
        }

        public override void WriteCommand(System.IO.TextWriter writer)
        {
            writer.WriteLine("Translate {");
            foreach (string[] key in _stringmapping.Keys)
            {
                writer.WriteLine("{0} => {1}", ProjectSerializer.SecureList(key), ProjectSerializer.SecureList(_stringmapping[key]));
            }
            writer.WriteLine("}");
        }

        public override void CheckSanity(Project project, Whee.WordBuilder.ProjectV2.IProjectSerializer serializer)
        {
            m_project = project;
            if (_stringmapping.Count == 0)
            {
                serializer.Warn("The Translate command requires at least one translation directive.", this);
            }

            foreach (string[] key in _stringmapping.Keys)
            {
                Regex reg = MakeRegex(key);
                _mapping.Add(reg, _stringmapping[key]);
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