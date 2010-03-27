using System;
using Gtk;

public partial class MainWindow : Gtk.Window
{
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Gtk.TreeView resultsTreeView = new Gtk.TreeView();		
		this.Add(resultsTreeView);
		
		TreeViewColumn wordCol = new TreeViewColumn();
		wordCol.Title = "Word";
		
		CellRendererText wordColRenderer = new CellRendererText();
		wordCol.PackStart(wordColRenderer, true);
		
		wordCol.AddAttribute(wordColRenderer, "text", 0);
		
		resultsTreeView.AppendColumn(wordCol);
		
		ListStore results = new ListStore(typeof (string));
		results.AppendValues("test");
		
		resultsTreeView.Model = results;
		
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
