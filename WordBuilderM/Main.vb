Imports System
Imports Gtk

Public Class MainClass

	Public Shared Sub Main ()
		Application.Init ()
		Dim win as new MyWindow ()
		win.Show ()
		Application.Run ()
	End Sub
	
End Class