using System;
using System.Collections.Generic;
using System.Text;

namespace Whee.WordBuilder.ProjectV2
{
    class ColumnNode : ProjectNodeBase
    {
        public ColumnNode(IProjectSerializer serializer)
            : base(serializer)
        {
            LoadColumn();
        }

        public override ProjectNodeType NodeType
        {
            get { return ProjectNodeType.ColumnDeclaration; }
        }

        private void LoadColumn()
        {
            Token titleToken = m_serializer.ReadTextToken(this);
            if (titleToken != null)
            {
                Title = titleToken.Text;
                titleToken.Type = TokenType.Name;
            }
            else
            {
                m_serializer.Warn("The column directive expects two arguments, a title and an expression.", this);
            }

            Token exprToken = m_serializer.ReadTextToken(this);
            if (exprToken != null)
            {
                Expression = exprToken.Text;
            }
            else
            {
                m_serializer.Warn("The column directive expects two arguments, a title and an expression.", this);
            }

            Token lineBreak = m_serializer.ReadLineBreakToken(this);
            if (lineBreak == null)
            {
                m_serializer.Warn("The column directive expects no more than two arguments, a title and an expression.", this);
            }
        }

        public string Title { get; set; }
        public string Expression { get; set; }
    }
}
