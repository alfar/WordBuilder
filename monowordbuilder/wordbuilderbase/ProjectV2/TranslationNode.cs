using System;
using System.Collections.Generic;
using System.Text;

namespace Whee.WordBuilder.ProjectV2
{
    public class TranslationNode : ProjectNodeBase
    {
        public TranslationNode(IProjectSerializer serializer)
            : base(serializer)
        {
            Successful = true;
            LoadTranslation();
        }

        private void LoadTranslation()
        {
            Token part = m_serializer.ReadSquaredBlockToken(this);
            if (part == null)
            {
                part = m_serializer.ReadTextToken(this);
            }

            Source = new List<string>();
            Destination = new List<string>();

            while (part != null && part.Text != "=>")
            {
                Source.Add(part.Text);
                part = m_serializer.ReadSquaredBlockToken(this);
                if (part == null)
                {
                    part = m_serializer.ReadTextToken(this);
                }
            }

            if (part == null)
            {
                m_serializer.Warn("Each translation expects a => as part of the expression");
                Successful = false;
            }
            else
            {
                part.Type = TokenType.Command;

                part = m_serializer.ReadSquaredBlockToken(this);
                if (part == null)
                {
                    part = m_serializer.ReadTextToken(this);
                }

                while (part != null)
                {
                    Destination.Add(part.Text);
                    part = m_serializer.ReadSquaredBlockToken(this);
                    if (part == null)
                    {
                        part = m_serializer.ReadTextToken(this);
                    }
                }
            }
        }

        public List<string> Source { get; private set; }
        public List<string> Destination { get; private set; }
        public bool Successful { get; private set; }

        public override ProjectNodeType NodeType
        {
            get { return ProjectNodeType.Translation; }
        }
    }
}
