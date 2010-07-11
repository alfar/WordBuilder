using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Whee.WordBuilder.Model;
using Whee.WordBuilder.ProjectV2;

namespace Whee.WordBuilder.Model.Commands
{
	public abstract class CommandBase : ProjectNodeBase 
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
	    
		public virtual void LoadCommand(IProjectSerializer serializer)
		{
            m_lineNumber = serializer.LineNumber;
			Token t = null;
			do {
				t = serializer.ReadTextToken(this);
			} while (t != null);
		}
		
		public virtual void LoadCommand(Project project, TextReader reader, string line, ref int lineNumber)
		{
			m_lineNumber = lineNumber;
			m_project = project;
	    }
	    
		public abstract void WriteCommand(TextWriter writer);
        public abstract void CheckSanity(Project project, Whee.WordBuilder.ProjectV2.IProjectSerializer serializer);

		private static List<Type> s_Commands;
		private static List<Type> Commands {
			get {
				if (s_Commands == null) {
					s_Commands = new List<Type>();
					foreach (Type t in Assembly.GetAssembly(typeof(ProjectSerializer)).GetTypes()) {
						if (t.IsSubclassOf(typeof(CommandBase))) {
							s_Commands.Add(t);
						}
					}
				}
				return s_Commands;
			}
		}
		
		public static CommandBase FindCommand(string name)
		{
			foreach (Type t in Commands)
			{
				if (t.Name.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
				{
					return (CommandBase)Activator.CreateInstance(t);
				}
			}
			return null;
		}

        public override ProjectNodeType NodeType
        {
            get { return ProjectNodeType.Command; }
        }

        public virtual bool RequireNewLine { get { return true; } }
	}
}