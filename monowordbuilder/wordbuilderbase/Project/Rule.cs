using System;
using Whee.WordBuilder.Model.Commands;

namespace Whee.WordBuilder.Model
{
	public class Rule
	{
		public string Name { get; set; }
	   
		public int LineNumber { get; set; }
	   
		public double Probability { get; set; }
	   
		private CommandCollection m_Commands = new CommandCollection();
		public CommandCollection Commands {
	        get { return m_Commands; }
	    }
	   
		public void Execute(Context context)
	    {
			if (context.IncrementRuleCount() > 500) {
	            throw new ApplicationException(String.Format("Rule count exceeded 500 when running rule {0}", this.Name));
	        }
	        Commands.Execute(context);
	    }
	}
}