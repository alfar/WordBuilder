using System;
using System.IO;
using Whee.WordBuilder.Model;

namespace Whee.WordBuilder.Model.Commands
{
	public abstract class CommandBase
	{
		protected int m_lineNumber;
		public int LineNumber {
			get { return m_lineNumber; }
		}
	    
		protected Project m_project;
		public Project Project {
			get { return m_project; }
	    }
	    
		public abstract void Execute(Context context);
	    
		public virtual void LoadCommand(Project project, TextReader reader, string line, ref int lineNumber)
		{
			m_lineNumber = lineNumber;
			m_project = project;
	    }
	    
		public abstract void WriteCommand(TextWriter writer);
		public abstract void CheckSanity(Project project);
	}
}