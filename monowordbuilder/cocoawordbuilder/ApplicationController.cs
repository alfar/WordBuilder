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
			m_warningViewHelper = new CocoaWarningViewHelper(warningsBrowser, warningsDrawer);
			m_DocumentController = new Whee.WordBuilder.Controller.DocumentController(m_warningViewHelper, m_fileSystem, m_fileDialogHelper, new CocoaTextViewHelper(codeTextView), new Document());
			
			m_detailsViewHelper = new CocoaTextFieldHelper(resultDetailsTextField);
			m_resultViewHelper = new CocoaResultViewHelper(resultsTableView);
			m_GeneratorController = new Whee.WordBuilder.Controller.GeneratorController(m_fileSystem, m_resultViewHelper, new CocoaClipBoardHelper(), m_detailsViewHelper);
		}
		
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

		[ObjectiveCMessage("clearResults:")]
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
		
		[ObjectiveCMessage("newDocument:")]
		public void newDocument(Id sender)
		{
			m_DocumentController.New();
		}
		
		[ObjectiveCMessage("openDocument:")]
		public void openDocument(Id sender)
		{
			m_DocumentController.Open();
		}
		
		[ObjectiveCMessage("saveDocument:")]
		public void saveDocument(Id sender)
		{
			m_DocumentController.Save();
		}
		
		[ObjectiveCMessage("saveDocumentAs:")]
		public void saveDocumentAs(Id sender)
		{
			m_DocumentController.SaveAs();
		}
	}
}

