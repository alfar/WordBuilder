using System;
using Monobjc;
using Monobjc.Cocoa;
using Whee.WordBuilder.Model;
using Whee.WordBuilder.ProjectV2;

namespace Whee.WordBuilder.Cocoa
{
	[ObjectiveCClass]
	public class ApplicationController : NSObject
	{
		public ApplicationController ()
		{
		}
		
		public ApplicationController(IntPtr nativePointer) : base(nativePointer)
		{
		}
		
		[ObjectiveCField]
		public NSWindow mainWindow;
		
//		[ObjectiveCMessage("newDocument:")]
//		public void newDocument(Id sender)
//		{
//			m_DocumentController.New();
//		}
//		
//		[ObjectiveCMessage("openDocument:")]
//		public void openDocument(Id sender)
//		{
//			m_DocumentController.Open();
//		}
//		
//		[ObjectiveCMessage("saveDocument:")]
//		public void saveDocument(Id sender)
//		{
//			m_DocumentController.Save();
//		}
//		
//		[ObjectiveCMessage("saveDocumentAs:")]
//		public void saveDocumentAs(Id sender)
//		{
//			m_DocumentController.SaveAs();
//		}
	}
}

