using System;
namespace Whee.WordBuilder.ProjectV2
{
	public class ProblemAreaNode : ProjectNodeBase 
	{
		#region implemented abstract members of Whee.WordBuilder.ProjectV2.ProjectNodeBase
		public override ProjectNodeType NodeType {
			get {
				return ProjectNodeType.ProblemArea ;
			}
		}
		
		#endregion
		public ProblemAreaNode (int index)
		{
			this.Index = index;
		}
	}
}

