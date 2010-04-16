using System;
using System.Collections.Generic;

namespace Whee.WordBuilder.Model
{
	public class Context
	{
		public Context ()
		{
		}

		private int _RuleCount = 0;
		public int RuleCount {
			get { return _RuleCount; }
		}

		public void ResetRuleCount ()
		{
			_RuleCount = 0;
		}

		public int IncrementRuleCount ()
		{
			_RuleCount += 1;
			return _RuleCount;
		}

		private List<string> _Tokens = new List<string> ();
		public List<string> Tokens {
			get { return _Tokens; }
		}

		private Dictionary<string, Context> _Branches = new Dictionary<string, Context> ();
		public Dictionary<string, Context> Branches {
			get { return _Branches; }
		}

		public Context Branch (string name)
		{
			Context result = new Context ();
			result._RuleCount = this.RuleCount;
			result.Tokens.AddRange (this.Tokens);
			
			if (_Branches.ContainsKey (name)) {
				int num = 1;
				while (_Branches.ContainsKey (string.Format ("{0}{1}", name, num))) {
					num += 1;
				}
				
				name = string.Format ("{0}{1}", name, num);
			}
			
			_Branches.Add (name, result);
			
			return result;
		}

		private Dictionary<string, string> _Marks = new Dictionary<string, string> ();
		public Dictionary<string, string> Marks {
			get { return _Marks; }
		}

		public void Mark (string name, string value)
		{
			_Marks[name] = value;
		}

		public override string ToString ()
		{
			return string.Join ("", Tokens.ToArray ());
		}

		public string Description (string indent)
		{
			List<string> result = new List<string> ();
			result.Add (string.Join ("", Tokens.ToArray ()));
			foreach (string m in Marks.Keys) {
				result.Add (string.Format ("{0}{1}: {2}", indent, m, Marks[m]));
			}
			foreach (string b in Branches.Keys) {
				result.Add (string.Format ("\t{0}{1}: {2}", indent, b, Branches[b].Description (indent + "\t")));
			}
			return string.Join (Environment.NewLine, result.ToArray ());
		}

		public string GetColumn (string path, Project project)
		{
			if (string.IsNullOrEmpty (path)) {
				return ToString ();
			}
			
			string[] parts = path.Split (new char[] { '.' }, 2);
			
			if (parts[0].StartsWith ("!")) {
				string mark = parts[0].Substring (1);
				if (Marks.ContainsKey (mark)) {
					return Marks[mark];
				}
			} else if (parts[0].StartsWith ("*") && project != null) {
				Rule r = project.Rules.GetRuleByName (parts[0].Substring (1));
				
				Context c = new Context ();
				c._Tokens = new List<string> (this.Tokens);
				r.Execute (c);
				
				if (parts.Length > 1) {
					return c.GetColumn (parts[1], project);
				} else {
					return c.ToString ();
				}
			} else if (Branches.ContainsKey (parts[0])) {
				if (parts.Length > 1) {
					return Branches[parts[0]].GetColumn (parts[1], project);
				} else {
					return Branches[parts[0]].ToString ();
				}
			}
			
			return "N/A";
		}
	}
}
