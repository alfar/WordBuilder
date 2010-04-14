
using System;
using Gtk;
using Whee.WordBuilder.Helpers;
using Whee.WordBuilder.Exporters;

namespace Whee.WordBuilder.Dialogs
{


	public partial class ExportDialog : Gtk.Dialog
	{

		public ExportDialog ()
		{
			this.Build ();

			m_formats = new ListStore(typeof(string));
			
			foreach(string name in ExporterFactory.GetExporterNames())
			{
				m_formats.AppendValues(name);
			}
			
			exportFormatComboBox.Model = m_formats;

			TreeIter iter;
			exportFormatComboBox.Model.GetIterFirst(out iter);
			
			exportFormatComboBox.SetActiveIter(iter);
		}
		
		private ListStore m_formats;
		
		public IExporter GetExporter()
		{
			TreeIter iter;
			
			exportFormatComboBox.GetActiveIter(out iter);
			
			return ExporterFactory.GetExporter((string)m_formats.GetValue(iter, 0));
		}
		
		public string GetFilename()
		{
			return exportFileChooserButton.Filename;
		}
	}
}
