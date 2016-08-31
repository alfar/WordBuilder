using System;
using Monobjc;
using Monobjc.Cocoa;

namespace Whee.WordBuilder.Cocoa
{
	public class CocoaTextFieldHelper : UIHelpers.ITextViewHelper
	{
		public CocoaTextFieldHelper(NSTextField textField)
		{
			m_textField = textField;
		}

		private NSTextField m_textField;

		#region ITextViewHelper implementation
		public event EventHandler<Model.Events.DocumentChangedEventArgs> BufferChanged;

		protected virtual void OnBufferChanged(Model.Events.DocumentChangedEventArgs e)
		{
			EventHandler<Model.Events.DocumentChangedEventArgs> handler = this.BufferChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		public void Clear ()
		{
			m_textField.StringValue = new NSString();
		}

		public void OnDocumentChanged (object sender, string newText, ProjectV2.IProjectNode project)
		{
			m_textField.StringValue = new NSString(newText);
		}

		public void GotoIndex(int index)
		{
			throw new NotImplementedException ();
		}

		public void DoHighlighting (ProjectV2.ProjectNode project)
		{
			throw new NotImplementedException ();
		}
		#endregion
}
}

