using System;
using Monobjc;
using Monobjc.Cocoa;
using Whee.WordBuilder.Model;

namespace Whee.WordBuilder.Cocoa
{
	[ObjectiveCClass]
	public class WordBuilderDocument : NSDocument
	{
		private static readonly Class WordBuilderDocumentClass = Class.GetClassFromType(typeof (WordBuilderDocument));
		
		public WordBuilderDocument ()
		{
		}

		public WordBuilderDocument (IntPtr nativePointer) : base(nativePointer)
		{
		}
		
		public WordBuilderDocument(NSString filename) : base(filename, new NSString("text/wordbuilder"))
		{
		}
		
		public override NSString WindowNibName {
			[ObjectiveCMessage("windowNibName")]
			get {
				return new NSString("Document");
			}
		}
				
		[ObjectiveCMessage("readFromData:ofType:error:")]
		public override bool ReadFromDataOfTypeError (NSData data, NSString typeName, out NSError outError)
		{
			if (m_document == null) {
				m_document = new Document(System.Text.Encoding.UTF8.GetString(data.GetBuffer()));
			}
			else
			{
				m_document.Text = System.Text.Encoding.UTF8.GetString(data.GetBuffer());
			}

			outError = null;
			return true;
		}
		
//		[ObjectiveCMessage("readFromUrl:ofType:error:")]
//		public override bool ReadFromURLOfTypeError (NSURL absoluteURL, NSString typeName, out NSError outError)
//		{
//			AppKitFramework.NSRunAlertPanel("ReadFromUrl", "Testing", "Ok", null, null);
//			
//			NSString data = new NSString(absoluteURL, NSStringEncoding.NSUTF8StringEncoding, out outError);
//			
//			m_document.Text = data.ToString();
//			
//			data.SafeRelease();
//			
//			return true;
//		}
				
		[ObjectiveCMessage("writeToURL:ofType:error:")]
		public override bool WriteToURLOfTypeError (NSURL absoluteURL, NSString typeName, out NSError outError)
		{
			m_document.FileName = absoluteURL.Path;
			m_document.Save(m_fileSystem);
			outError = null;
			return true;
		}
						
		[ObjectiveCMessage("windowControllerDidLoadNib:")]
		public override void WindowControllerDidLoadNib (NSWindowController windowController)
		{
            this.SendMessageSuper(WordBuilderDocumentClass, "windowControllerDidLoadNib:", windowController);
            if (this.FileURL != null)
            {
				m_document.FileName = this.FileURL.Path.ToString();
			}
		}
		
		[ObjectiveCField]
		public NSTextView codeTextView;
		
		[ObjectiveCField]
		public NSTableView resultsTableView;
		
		[ObjectiveCField]
		public NSTextField resultDetailsTextField;
		
		[ObjectiveCField]
		public NSBrowser warningsBrowser;
		
		[ObjectiveCField]
		public NSDrawer warningsDrawer;

		[ObjectiveCMessage("awakeFromNib")]
		public void AwakeFromNib()
		{
			if (m_document == null)
			{
				m_document = new Document();
			}
			m_warningViewHelper = new CocoaWarningViewHelper(warningsBrowser, warningsDrawer);
			m_textViewHelper = new CocoaTextViewHelper(codeTextView);
			m_textViewHelper.BufferChanged += HandleM_textViewHelperBufferChanged;
			m_DocumentController = new Whee.WordBuilder.Controller.DocumentController(m_warningViewHelper, m_fileSystem, m_fileDialogHelper, m_textViewHelper, m_document);
			
			m_detailsViewHelper = new CocoaTextFieldHelper(resultDetailsTextField);
			m_resultViewHelper = new CocoaResultViewHelper(resultsTableView);
			m_GeneratorController = new Whee.WordBuilder.Controller.GeneratorController(m_fileSystem, m_resultViewHelper, new CocoaClipBoardHelper(), m_detailsViewHelper);
			
			if (!String.IsNullOrEmpty(m_document.Text))
			{
				m_textViewHelper.DoHighlighting(m_DocumentController.GetProjectNode());
			}
		}

		void HandleM_textViewHelperBufferChanged (object sender, Whee.WordBuilder.Model.Events.DocumentChangedEventArgs e)
		{
			this.UpdateChangeCount(NSDocumentChangeType.NSChangeDone);
		}
		
		private Whee.WordBuilder.Cocoa.CocoaTextViewHelper m_textViewHelper;
		private Whee.WordBuilder.Model.Document m_document;
		private Whee.WordBuilder.UIHelpers.IResultViewHelper m_resultViewHelper;
		private Whee.WordBuilder.UIHelpers.ITextViewHelper m_detailsViewHelper;
		private Whee.WordBuilder.UIHelpers.IFileDialogHelper m_fileDialogHelper = new CocoaFileDialogHelper();
		private Whee.WordBuilder.UIHelpers.IWarningViewHelper m_warningViewHelper;
		private Whee.WordBuilder.Helpers.IFileSystem m_fileSystem = new Whee.WordBuilder.Helpers.FileSystem();
		private Whee.WordBuilder.Controller.DocumentController m_DocumentController;
		
		private Whee.WordBuilder.Controller.GeneratorController m_GeneratorController;

		[ObjectiveCMessage("generate:")]
		public void generate(Id sender)
		{
			m_GeneratorController.Generate(m_DocumentController.Compile());
		}

		[ObjectiveCMessage("clear:")]
		public void clearResults(Id sender)
		{
			m_GeneratorController.Clear();
		}

		[ObjectiveCMessage("clearAndGenerate:")]
		public void clearAndGenerate(Id sender)
		{
			m_GeneratorController.Clear();
			m_GeneratorController.Generate(m_DocumentController.Compile());
		}
		
		[ObjectiveCMessage("copySelected:")]
		public void copySelected(Id sender)
		{
			m_GeneratorController.Copy();
		}
		
		[ObjectiveCMessage("copyDescriptions:")]
		public void copyDescriptions(Id sender)
		{
			m_GeneratorController.CopyDescription();
		}

		[ObjectiveCMessage("export:")]
		public void export(Id sender)
		{
			NSSavePanel savePanel = NSSavePanel.SavePanel;

			ExportAsViewController ctrl = ExportAsViewController.Controller;
			
			savePanel.AccessoryView = ctrl.View;
			
			int result = savePanel.RunModal();
		
			if (result > 0)
			{
				m_GeneratorController.Export(ctrl.SelectedExporter, savePanel.Filename.ToString());
			}
			
			ctrl.Release();
		}
	}
}

