using System;
using System.Collections.Generic;

public class Project
{	
	private RuleCollection _Rules = new RuleCollection();
	
	private TokenSetCollection _TokenSets = new TokenSetCollection();

	public TokenSetCollection TokenSets {
		get { return _TokenSets; }
	}
	
	public RuleCollection Rules {
		get { return _Rules; }
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
			Rule r = _Rules.GetRuleByName(startRule);
			
			if (r != null) {
				r.Execute(c);
			}
		}
		catch (Exception ex) {
			Warnings.Add(ex.Message);
		}
		
		return c;
	}
	
	private List<string> _Warnings = new List<string>();
	public List<string> Warnings {
		get { return _Warnings; }
	}
}
