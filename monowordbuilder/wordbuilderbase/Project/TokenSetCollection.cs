using System;

namespace Whee.WordBuilder.Project
{
	public class TokenSetCollection : System.Collections.ObjectModel.Collection<TokenSet>
	{
		public TokenSet FindByName(string name)
		{
			foreach (TokenSet t in this) {
				if (t.Name == name) {
					return t;
				}
			}
			return null;
		}

		public int CountByName(string name)
		{
			int result = 0;
			
			foreach (TokenSet t in this) {
				if (t.Name == name) {
					result++;
				}
			}
			return result;
		}
	}
}
