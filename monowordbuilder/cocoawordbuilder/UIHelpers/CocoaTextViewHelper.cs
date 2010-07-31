using System;
using Monobjc.Cocoa;
using Whee.WordBuilder.ProjectV2;

namespace Whee.WordBuilder.Cocoa
{
	public class CocoaTextViewHelper : UIHelpers.ITextViewHelper
	{
		public CocoaTextViewHelper (NSTextView textField)
		{
			m_textView = textField;
			m_textView.SetDelegate(d => {
				d.TextDidChange += HandleTextViewTextDidChange;
			});
		}

		void HandleTextViewTextDidChange (NSNotification aNotification)
		{
            if (BufferChanged != null && !m_highlighting)
			{
				BufferChanged.Invoke(this, new Whee.WordBuilder.Model.Events.DocumentChangedEventArgs(m_textView.String.ToString()));
			}
		}
	
		private NSTextView m_textView;

		#region ITextViewHelper implementation
		public event EventHandler<Model.Events.DocumentChangedEventArgs> BufferChanged;

		public void Clear ()
		{
			m_textView.String = new NSString();
		}

		public void OnDocumentChanged (object sender, string newText, ProjectV2.IProjectNode project)
		{
			m_textView.String = new NSString(newText);
		}

		public void GotoIndex(int index)
		{
			uint charIndex = (uint)index;
			
			m_textView.SelectedRange = new NSRange(charIndex, 0);
			m_textView.ScrollRangeToVisible(m_textView.SelectedRange);
			m_textView.Window.MakeFirstResponder(m_textView);
		}

		private static System.Collections.Generic.Dictionary<Whee.WordBuilder.ProjectV2.TokenType, NSColor> s_Colors;
		private static System.Collections.Generic.Dictionary<Whee.WordBuilder.ProjectV2.TokenType, NSColor> Colors
		{
			get 
			{
				if (s_Colors == null)
				{
					s_Colors = new System.Collections.Generic.Dictionary<Whee.WordBuilder.ProjectV2.TokenType, Monobjc.Cocoa.NSColor>();
					s_Colors.Add(TokenType.Comment, NSColor.GrayColor);
					s_Colors.Add(TokenType.Directive, NSColor.BrownColor);
					s_Colors.Add(TokenType.Command, NSColor.OrangeColor);
					s_Colors.Add(TokenType.BlockStarter, NSColor.DarkGrayColor);
					s_Colors.Add(TokenType.BlockEnder, NSColor.DarkGrayColor);
					s_Colors.Add(TokenType.Name, NSColor.BlueColor);
//					s_Colors.Add(TokenType.Number, NSColor.Color);
					s_Colors.Add(TokenType.Error, NSColor.WhiteColor);
					             
				}
				
				return s_Colors;
			}
		}

		private static System.Collections.Generic.Dictionary<Whee.WordBuilder.ProjectV2.TokenType, NSColor> s_BGColors;
		private static System.Collections.Generic.Dictionary<Whee.WordBuilder.ProjectV2.TokenType, NSColor> BGColors
		{
			get 
			{
				if (s_BGColors == null)
				{
					s_BGColors = new System.Collections.Generic.Dictionary<Whee.WordBuilder.ProjectV2.TokenType, Monobjc.Cocoa.NSColor>();
					s_BGColors.Add(TokenType.Comment, NSColor.LightGrayColor);
					s_BGColors.Add(TokenType.Error, NSColor.RedColor);					             
				}
				
				return s_BGColors;
			}
		}

		private bool m_highlighting = false;
		
		public void DoHighlighting (ProjectV2.ProjectNode project)
		{
			m_highlighting = true;

			m_textView.TextStorage.RemoveAttributeRange(NSAttributedString.NSForegroundColorAttributeName, new NSRange(0, m_textView.String.Length));
			m_textView.TextStorage.RemoveAttributeRange(NSAttributedString.NSBackgroundColorAttributeName, new NSRange(0, m_textView.String.Length));
			
    	    foreach (Token tok in project.Serializer.Tokens)
	    	{
				if (Colors.ContainsKey(tok.Type))
				{					
					m_textView.TextStorage.AddAttributeValueRange(NSAttributedString.NSForegroundColorAttributeName, Colors[tok.Type], new NSRange((uint)tok.Offset, (uint)tok.Length));
				}
				if (BGColors.ContainsKey(tok.Type))
				{					
					m_textView.TextStorage.AddAttributeValueRange(NSAttributedString.NSBackgroundColorAttributeName, BGColors[tok.Type], new NSRange((uint)tok.Offset, (uint)tok.Length));
				}
            }
			m_highlighting = false;

		}
		#endregion
	}
}

