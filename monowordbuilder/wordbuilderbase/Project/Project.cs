using System;
using System.Collections.Generic;
using Whee.WordBuilder.Helpers;

namespace Whee.WordBuilder.Model
{
	public class Project
	{
		public Project(IRandom random)
		{
			m_Random = random;
			m_Rules = new RuleCollection(m_Random);
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
			return new TokenSet(m_Random);
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
