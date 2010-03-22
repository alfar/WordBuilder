using System;

public class Rule
{
	private string _Name;
	private int _LineNumber;
	private double _Probability;
	private CommandCollection _Commands = new CommandCollection();
   
	public string Name {
        get { return _Name; }
        set { _Name = value; }
	}
   
	public int LineNumber {
		get { return _LineNumber; }
		set { _LineNumber = value; }
    }
   
	public double Probability {
        get { return _Probability; }
        set { _Probability = value; }
    }
   
	public CommandCollection Commands {
        get { return _Commands; }
    }
   
	public void Execute(Context context)
    {
		if (context.IncrementRuleCount() > 500) {
            throw new ApplicationException(String.Format("Rule count exceeded 500 when running rule {0}", this.Name));
        }
        Commands.Execute(context);
    }
}
