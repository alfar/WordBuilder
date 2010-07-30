using System;
using Monobjc;
using Monobjc.Cocoa;
using Whee.WordBuilder.Exporters;

namespace Whee.WordBuilder.Cocoa
{
	[ObjectiveCClass]
	public class ExportAsViewController : NSViewController
	{
		public ExportAsViewController ()
		{
		}

		public ExportAsViewController(IntPtr nativePointer) : base(nativePointer)
		{
		}
		
		public ExportAsViewController(NSString nibOrNull, NSBundle bundleOrNull) : base(nibOrNull, bundleOrNull)
		{
		}

		[ObjectiveCMessage("awakeFromNib")]
		public void AwakeFromNib()
		{
			while (exportTypePopUpButton.ItemArray.Count > 0)
			{
				exportTypePopUpButton.RemoveItemAtIndex(0);
			}

			foreach(string name in ExporterFactory.GetExporterNames())
			{
				exportTypePopUpButton.AddItemWithTitle(name);
			}
			
		}
		
		public static ExportAsViewController Controller
		{
			get 
			{
				return new ExportAsViewController("ExportAsView.nib", null);
			}
		}
				
		[ObjectiveCField]
		public NSPopUpButton exportTypePopUpButton;
		
		public IExporter SelectedExporter
		{
			get
			{
				return ExporterFactory.GetExporter(exportTypePopUpButton.TitleOfSelectedItem.ToString());
			}
		}
	}
}

