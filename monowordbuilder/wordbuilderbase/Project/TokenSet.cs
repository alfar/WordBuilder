using System;
using System.Collections.Generic;
using Whee.WordBuilder.Helpers;
using Whee.WordBuilder.ProjectV2;

namespace Whee.WordBuilder.Model
{
	public class TokenSet
	{
        [Obsolete()]
        public TokenSet(IRandom random)
        {
            m_Random = random;
        }

		public TokenSet(IProjectNode node, IRandom random)
		{
            TokenSetNode tsn = node as TokenSetNode;
            Name = tsn.Name;
            Tokens.AddRange(tsn.Tokens);
			m_Random = random;
		}
		
		private IRandom m_Random;
	    
		public string Name { get; set; }
	    
		private List<String> _Tokens = new List<string>();
		public List<String> Tokens {
			get { return _Tokens; }
		}
	    
		public string GetToken()
		{
			return Tokens[m_Random.Next(Tokens.Count)];
		}
	}
}