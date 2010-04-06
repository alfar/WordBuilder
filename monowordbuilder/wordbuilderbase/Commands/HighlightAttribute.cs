using System;

namespace Whee.WordBuilder.Project.Commands
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class HighlightAttribute : Attribute
	{
	
		private int _Level = 1;
		public int Level {
			get { return _Level; }
			set { _Level = value; }
		}
	
		private string _RegEx;
		public string RegEx {
			get { return _RegEx; }
			set { _RegEx = value; }
		}
	
		private string _Color;
		public string Color {
			get { return _Color; }
			set { _Color = value; }
		}
	
		private int _NextLevel = -1;
		public int NextLevel {
			get { return _NextLevel; }
			set { _NextLevel = value; }
		}
	}
}