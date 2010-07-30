using System;
namespace Whee.WordBuilder.UIHelpers
{
	public class Warning
	{
		public Warning (Whee.WordBuilder.ProjectV2.IProjectNode node, string message)
		{
			Node = node;
			Message = message;
		}
		
		public Whee.WordBuilder.ProjectV2.IProjectNode Node { get; private set; }
		public string Message { get; private set; }
	}
}

