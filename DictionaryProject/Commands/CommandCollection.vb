
Public Class CommandCollection
    Inherits Collections.ObjectModel.Collection(Of CommandBase)

    Public Sub Execute(ByVal context As Context)
        For Each cmd As CommandBase In Me
            cmd.Execute(context)
        Next
    End Sub

    Public Sub WriteCommands(ByVal writer As IO.TextWriter)
        For Each c As CommandBase In Me
            c.WriteCommand(writer)
        Next
    End Sub
End Class
