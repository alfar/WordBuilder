using System;
using Monobjc.Cocoa;
using Monobjc;

namespace Whee.WordBuilder.Cocoa
{
	public class CocoaClipBoardHelper : UIHelpers.IClipBoardHelper
	{
		public CocoaClipBoardHelper ()
		{
		}
	
		#region IClipBoardHelper implementation
		public void Copy (string data)
		{
			NSPasteboard glob = NSPasteboard.GeneralPasteboard;
			
			NSMutableArray types = new NSMutableArray();
			types.Add(NSPasteboard.NSStringPboardType);
			
			glob.DeclareTypesOwner(types, null);

			glob.SetStringForType(data, NSPasteboard.NSStringPboardType);
		}

		public string Paste ()
		{
			return NSPasteboard.GeneralPasteboard.StringForType("NSString").ToString();
		}
		#endregion
	}
}

