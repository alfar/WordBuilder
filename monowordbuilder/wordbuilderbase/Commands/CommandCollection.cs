using System;
using System.IO;

namespace Whee.WordBuilder.Project.Commands
{
	public class CommandCollection : System.Collections.ObjectModel.Collection<CommandBase>
	{
		public void Execute(Context context)
		{
			foreach (CommandBase cmd in this) {
				cmd.Execute(context);
			}
		}
	    
		public void WriteCommands(TextWriter writer)
	    {
	        foreach (CommandBase c in this) {
	            c.WriteCommand(writer);
	        }
	    }
	}
}