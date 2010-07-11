using System;
using System.Collections.Generic;
using System.Text;

namespace Whee.WordBuilder.ProjectV2
{
    public class WeightedCommandNode : ProjectNodeBase 
    {
        public WeightedCommandNode(IProjectSerializer serializer) : base(serializer)
        {
            Successful = true;
            LoadWeightedCommand();
        }

        public bool Successful { get; private set; }
        public double Weight { get; private set; }
        public Whee.WordBuilder.Model.Commands.CommandBase Command { get; private set; }

        private void LoadWeightedCommand()
        {
            bool found = false;
            double probability = 0;
            Token probabilityToken = m_serializer.ReadNumericToken(this, ref probability, out found);

            if (probabilityToken != null)
            {
                Weight = probability;
                Token command = m_serializer.ReadTextToken(this);

                if (command != null)
                {
                    Model.Commands.CommandBase cb = Model.Commands.CommandBase.FindCommand(command.Text);

                    if (cb != null)
                    {
                        command.Type = TokenType.Command;
                        cb.Index = command.Offset;
                        cb.LoadCommand(m_serializer);
                        Command = cb;
                        Children.Add(cb);
                        //cb.Length = m_serializer.Position - cb.Index;
                    }
                    else
                    {
                        m_serializer.Warn(string.Format("Command '{0}' not found.", command.Text));
                        command.Type = TokenType.Error;
                        Successful = false;
                    }
                }
                else
                {
                    m_serializer.Warn("Expected a command");
                    probabilityToken.Type = TokenType.Error;
                    Successful = false;
                }
            }
            else if (found)
            {
                m_serializer.Warn("Expected a probability expressed as a decimal number.");
                Successful = false;

                m_serializer.ReadTextToken(this).Type = TokenType.Error;
            }
        }

        public override ProjectNodeType NodeType
        {
            get { return ProjectNodeType.Command; }
        }
    }
}
