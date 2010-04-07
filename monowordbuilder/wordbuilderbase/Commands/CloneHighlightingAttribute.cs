using System;

namespace Whee.WordBuilder.Model.Commands
{
	public class CloneHighlightingAttribute : Attribute
	{
	
		public CloneHighlightingAttribute(int source, int destination)
		{
			_SourceLevel = source;
			_DestinationLevel = destination;
		}
	
		private int _SourceLevel;
		public int SourceLevel {
			get { return _SourceLevel; }
			set { _SourceLevel = value; }
		}
	
		private int _DestinationLevel;
		public int DestinationLevel {
			get { return _DestinationLevel; }
			set { _DestinationLevel = value; }
		}
	}
}