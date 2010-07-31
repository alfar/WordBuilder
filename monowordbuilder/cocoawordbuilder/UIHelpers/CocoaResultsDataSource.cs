using System;
using System.Collections.Generic;
using Monobjc;
using Monobjc.Cocoa;
using Whee.WordBuilder.Model;

namespace Whee.WordBuilder.Cocoa
{
	[ObjectiveCClass]
	public class CocoaResultsDataSource : NSObject, INSTableDataSource
	{
		public CocoaResultsDataSource ()
		{
		}
		
		public CocoaResultsDataSource(IntPtr nativePointer) : base(nativePointer) 
		{	
		}
		
		public void Clear() 
		{
			_items.Clear();
		}
		
		public void Add(Context value)
		{
			_items.Add(value);
		}
		
		public List<Context> Items
		{
			get 
			{
				return _items;
			}
		}
		
		private List<Context> _items = new List<Context>();
		
		#region INSTableDataSource implementation
		[ObjectiveCMessageAttribute("numberOfRowsInTableView:")]
		public int NumberOfRowsInTableView (NSTableView aTableView)
		{
			return _items.Count;
		}

		public bool TableViewAcceptDropRowDropOperation (NSTableView aTableView, INSDraggingInfo info, int row, NSTableViewDropOperation operation)
		{
			return false;
		}

		public NSArray TableViewNamesOfPromisedFilesDroppedAtDestinationForDraggedRowsWithIndexes (NSTableView aTableView, NSURL dropDestination, NSIndexSet indexSet)
		{
			throw new NotImplementedException ();
		}

		[ObjectiveCMessageAttribute("tableView:objectValueForTableColumn:row:")]
		public Id TableViewObjectValueForTableColumnRow (NSTableView aTableView, NSTableColumn aTableColumn, int rowIndex)
		{
			switch (aTableView.TableColumns.IndexOf(aTableColumn))
			{
			case 0:
				return new NSString(_items[rowIndex].ToString(), _items[rowIndex].ToString().Length);
			default:
				return new NSString();
			}
		}

		public void TableViewSetObjectValueForTableColumnRow (NSTableView aTableView, Id anObject, NSTableColumn aTableColumn, int rowIndex)
		{
			throw new NotImplementedException ();
		}

		public void TableViewSortDescriptorsDidChange (NSTableView aTableView, NSArray oldDescriptors)
		{
			throw new NotImplementedException ();
		}

		public NSDragOperation TableViewValidateDropProposedRowProposedDropOperation (NSTableView aTableView, INSDraggingInfo info, int row, NSTableViewDropOperation operation)
		{
			throw new NotImplementedException ();
		}

		public bool TableViewWriteRowsWithIndexesToPasteboard (NSTableView aTableView, NSIndexSet rowIndexes, NSPasteboard pboard)
		{
			throw new NotImplementedException ();
		}
		#endregion		
	}
}

