using System;
using System.Collections.Generic;
using Monobjc;
using Monobjc.Cocoa;
using Whee.WordBuilder.UIHelpers;

namespace Whee.WordBuilder.Cocoa
{
	public class CocoaWarningViewHelper : UIHelpers.IWarningViewHelper
	{
		public CocoaWarningViewHelper (NSBrowser warningsBrowser, NSDrawer drawer)
		{
			m_drawer = drawer;
			m_warningsBrowser = warningsBrowser;
			m_warningsBrowser.SetDelegate(d => {
				d.BrowserNumberOfRowsInColumn += HandleDBrowserNumberOfRowsInColumn;
				d.BrowserWillDisplayCellAtRowColumn += HandleDBrowserWillDisplayCellAtRowColumn;
			});
			m_warningsBrowser.AddColumn();
			m_warningsBrowser.SetTitleOfColumn("Warnings", 0);
			m_warningsBrowser.ActionEvent += HandleM_warningsBrowserActionEvent;
		}

		private NSDrawer m_drawer;
		
		void HandleM_warningsBrowserActionEvent (Id sender)
		{
			if (WarningActivated != null)
			{
				int row = m_warningsBrowser.SelectedRowInColumn(0);
				WarningActivated(this, new UIHelpers.WarningEventArgs(_warnings[row].Node.Index));
			}	
		}

		int HandleDBrowserNumberOfRowsInColumn (NSBrowser sender, int column)
		{
			return _warnings.Count;
		}

		void HandleDBrowserWillDisplayCellAtRowColumn (NSBrowser sender, Id cell, int row, int column)
		{
			NSBrowserCell bcell = cell.CastAs<NSBrowserCell>();
			bcell.StringValue = _warnings[row].Message;
			bcell.IsLeaf = true;
		}

		private NSBrowser m_warningsBrowser;
		
		#region IWarningViewHelper implementation
		public event EventHandler<UIHelpers.WarningEventArgs> WarningActivated;
		
		private List<Warning> _warnings = new List<Warning>();
		
		public void Clear ()
		{
			_warnings.Clear();
			m_warningsBrowser.ReloadColumn(0);
		}

		public void AddWarning (Whee.WordBuilder.ProjectV2.IProjectNode node, string message)
		{
			_warnings.Add(new Warning(node, message));
			m_warningsBrowser.LoadColumnZero();
			m_drawer.Open();
		}

		public bool HasWarnings {
			get {
				return _warnings.Count > 0;
			}
		}
		#endregion
}
}

