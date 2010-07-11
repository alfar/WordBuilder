using System;
using System.Collections.Generic;
using Whee.WordBuilder.Helpers;
using Whee.WordBuilder.ProjectV2;

namespace Whee.WordBuilder.Model
{
	public class Project
	{
        [Obsolete()]
        public Project(IRandom random)
        {
            m_Random = random;
            m_Rules = new RuleCollection(m_Random);
        }
        
        public Project(ProjectNode root)
        {
            m_Random = root.Serializer.Random;
            m_Rules = new RuleCollection(m_Random);

            foreach (IProjectNode node in root.Children)
            {
                switch (node.NodeType)
                {
                    case ProjectNodeType.TokenSetDeclaration:
                        this.TokenSets.Add(new TokenSet(node, m_Random));
                        break;
                    case ProjectNodeType.RuleDeclaration:
                        this.Rules.Add(new Rule(node as RuleNode));
                        break;
                    case ProjectNodeType.StartingRuleDeclaration:
                        StartingRuleNode srn = node as StartingRuleNode;
                        this.StartRules.Add(srn.Name, srn.Amount);
                        break;
                    case ProjectNodeType.ColumnDeclaration:
                        ColumnNode cn = node as ColumnNode;
                        this.Columns.Add(cn.Title, cn.Expression);
                        break;
                    default:
                        break;
                }                
            }

            foreach (Rule r in this.Rules)
            {
                foreach (Whee.WordBuilder.Model.Commands.CommandBase c in r.Commands)
                {
                    c.CheckSanity(this, root.Serializer);
                }
            }
        }
		
		private IRandom m_Random;

		private RuleCollection m_Rules;
		public RuleCollection Rules {
			get { return m_Rules; }
		}

		private TokenSetCollection m_TokenSets = new TokenSetCollection();
		public TokenSetCollection TokenSets {
			get { return m_TokenSets; }
		}
		
		public TokenSet CreateTokenSet() {
			return new TokenSet(null, m_Random);
		}

		private Dictionary<string, string> _columns = new Dictionary<string, string>();
		public Dictionary<string, string> Columns {
			get { return _columns; }
		}

		private Dictionary<string, int> _startRules = new Dictionary<string, int>();
		public Dictionary<string, int> StartRules {
			get { return _startRules; }
		}

		public Context GetWord(string startRule)
		{
			Context c = new Context();
			
			try {
				Rule r = m_Rules.GetRuleByName (startRule);
				
				if (r != null) {
					r.Execute (c);
				}
			} catch (Exception ex) {
				Warnings.Add (ex.Message);
			}
			
			return c;
		}

		private List<string> _Warnings = new List<string>();
		public List<string> Warnings {
			get { return _Warnings; }
		}
	}
}
