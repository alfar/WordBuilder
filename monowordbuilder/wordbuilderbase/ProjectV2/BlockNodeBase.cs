using System;
using System.Collections.Generic;
using System.Text;

namespace Whee.WordBuilder.ProjectV2
{
    public abstract class BlockNodeBase : ProjectNodeBase
    {
        public BlockNodeBase(IProjectSerializer serializer)
            : base(serializer)
        {
            LoadItems();
        }

        private void LoadItems()
        {
            int indentationLevel = -1;
            bool useIndentation = true;

            Token starter = null;
            Token ender = null;
            Token newLine = m_serializer.ReadLineBreakToken(this);
            Token indentation = null;

            if (newLine != null)
            {
                indentation = m_serializer.ReadIndentationToken(this);
                if (indentation != null)
                {
                    indentation.ProjectNode = this;
                    indentationLevel = m_serializer.GetIndentationLevel(indentation.Text);
                }
            }

            starter = m_serializer.ReadBlockStarterToken(this);

            if (starter != null)
            {
                starter.ProjectNode = this;
                useIndentation = false;
                newLine = m_serializer.ReadLineBreakToken(this);
            }
            else if (indentationLevel <= m_serializer.LastIndentationLevel)
            {
                if (indentation != null)
                {
                    m_serializer.RollBackToken(indentation);
                }
                m_serializer.Warn("The expected block is empty. Check indentation.", this);
                return;
            }

            bool requireNewLine = false;
            bool starting = true;

            bool ended = false;
            while (!ended)
            {
                if (!useIndentation)
                {
                    ender = m_serializer.ReadBlockEnderToken(this);
                }

                if (ender != null)
                {
                    ended = true;
                }
                else if (m_serializer.Done)
                {
                    if (!useIndentation)
                    {
                        m_serializer.Warn("Expected } before end of file.", this);
                    }
                    ended = true;
                }
                else
                {
                    if (!useIndentation || !starting)
                    {
                        indentation = m_serializer.ReadIndentationToken(this);
                    }

                    if (useIndentation)
                    {
                        if (indentation != null)
                        {
                            if (indentationLevel == -1)
                            {
                                indentationLevel = m_serializer.GetIndentationLevel(indentation.Text);
                            }
                            else if (indentationLevel != m_serializer.GetIndentationLevel(indentation.Text))
                            {
                                m_serializer.RollBackToken(indentation);
                                ended = true;
                                break;
                            }
                        }
                        else
                        {
                            ended = true;
                            break;
                        }
                    }
                    else
                    {
                        ender = m_serializer.ReadBlockEnderToken(this);

                        if (ender != null)
                        {
                            ended = true;
                        }
                        else if (m_serializer.Done)
                        {
                            if (!useIndentation)
                            {
                                m_serializer.Warn("Expected } before end of file.", this);
                            }
                            ended = true;
                        }
                    }

                    if (!ended)
                    {
                        requireNewLine = LoadSingleItem();
                    }

                    newLine = m_serializer.ReadLineBreakToken(this);

                    if (newLine == null && requireNewLine)
                    {
                        m_serializer.Warn("New line expected", this);
                    }

                    starting = false;
                }
            }
        }

        public abstract bool LoadSingleItem();
    }
}
