using System;
using System.Collections.Generic;
using Monobjc;
using Monobjc.Cocoa;

namespace Whee.WordBuilder.Cocoa
{
	public class CocoaResultViewHelper : UIHelpers.IResultViewHelper
	{
		public CocoaResultViewHelper (NSTableView resultView)
		{
			_resultView = resultView;
			_resultView.SetDelegate(d => {
				d.TableViewSelectionDidChange += HandleResultViewTableViewSelectionDidChange;
			});
			_results = new CocoaResultsDataSource();
			_resultView.DataSource = _results;
		}

		void HandleResultViewTableViewSelectionDidChange (NSNotification aNotification)
		{
			if (SelectionChanged != null)
			{
				SelectionChanged(this, EventArgs.Empty);
			}			
		}
		
		private NSTableView _resultView;
		private CocoaResultsDataSource _results;
		
		#region IResultViewHelper implementation
		public event EventHandler<EventArgs> SelectionChanged;

		public void Clear ()
		{
			_resultView.DeselectAll(_resultView);
			_results.Clear();
			_resultView.ReloadData();
		}

		public void AddColumn (string title, string accessor)
		{
			throw new NotImplementedException ();
		}

		public void AddItem (Model.Context context)
		{
			_results.Add(context);
			_resultView.ReloadData();
		}

		public List<Model.Context> GetSelectedItems ()
		{
			List<Model.Context> result = new List<Model.Context>();
			 
			uint i = _resultView.SelectedRowIndexes.FirstIndex;
			
			while (i != NSRange.NSNotFoundRange.location)
			{
				Model.Context context = _results.Items[(int)i];
			
				result.Add(context);
				i = _resultView.SelectedRowIndexes.IndexGreaterThanIndex(i);
			}
			
			return result;
		}

		public List<Model.Context> GetAllItems ()
		{
			return new List<Model.Context>(_results.Items);
		}
		#endregion
	}
}

