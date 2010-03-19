Imports System
Imports Gtk

Public Class MyWindow
	Inherits Window
	
	Public Sub New ()
		 MyBase.New("MyWindow")
		 
		 Me.SetDefaultSize (400, 300)
		 AddHandler MyBase.DeleteEvent, AddressOf OnMyWindowDelete
		 
		 Me.ShowAll ()
	End Sub
			
	Private Sub OnMyWindowDelete (ByVal sender As Object, ByVal a As DeleteEventArgs)
		Application.Quit ()
		a.RetVal = true
	End Sub
	
End Class