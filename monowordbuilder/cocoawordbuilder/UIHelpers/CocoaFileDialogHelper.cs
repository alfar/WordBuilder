using System;
using Monobjc;
using Monobjc.Cocoa;

namespace Whee.WordBuilder.Cocoa
{
	public class CocoaFileDialogHelper : UIHelpers.IFileDialogHelper
	{
		public CocoaFileDialogHelper ()
		{
		}

		#region IFileDialogHelper implementation
		public UIHelpers.SaveCheckDialogResult ShowSaveCheckDialog ()
		{
			int result = AppKitFramework.NSRunAlertPanel("File has changed", "The current document has unsaved changes. Do you wish to save those now?", "Save", "Don't save", "Cancel");
			
			switch (result)
			{
			case 1:
				return UIHelpers.SaveCheckDialogResult.Save;
			case 0:
				return UIHelpers.SaveCheckDialogResult.NoSave;
			default:
				return UIHelpers.SaveCheckDialogResult.Cancel;
			}
		}

		public string ShowSaveDialog ()
		{
			NSSavePanel savePanel = NSSavePanel.SavePanel;
			int result = savePanel.RunModal();
		
			if (result > 0)
			{
				return savePanel.Filename;
			}
			return null;
		}

		public string ShowOpenDialog ()
		{
			NSOpenPanel openPanel = NSOpenPanel.OpenPanel;
			openPanel.RunModal();
			return openPanel.Filename;
		}
		#endregion
	}
}

