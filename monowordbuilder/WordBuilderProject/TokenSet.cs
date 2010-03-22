using System;
using System.Collections.Generic;

public class TokenSet
{
    
	private string _Name;
	public string Name {
		get { return _Name; }
		set { _Name = value; }
	}
    
	private List<String> _Tokens = new List<string>();
	public List<String> Tokens {
		get { return _Tokens; }
	}
    
	private static Random _Random = new Random();
    
	public string GetToken()
	{
		return Tokens[_Random.Next(0, Tokens.Count)];
	}
}