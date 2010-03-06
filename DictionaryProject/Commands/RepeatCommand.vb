<Highlight(RegEx:="^\s*repeat$", Color:="Green")> _
Public Class RepeatCommand
    Inherits CommandBase

    Public Overrides Sub Execute(ByVal context As Context)
        If context.Tokens.Count > 0 Then
            context.Tokens.Add(context.Tokens.Last)
        End If
    End Sub

    Public Overrides Sub LoadCommand(ByVal project As Project, ByVal reader As System.IO.TextReader, ByVal line As String, ByRef lineNumber As Integer)
        MyBase.LoadCommand(project, reader, line, lineNumber)
        If line.ToLower() <> "repeat" Then
            project.Warnings.Add(String.Format("Line {0}: The repeat command requires 0 arguments.", lineNumber))
        End If
    End Sub

    Public Overrides Sub WriteCommand(ByVal writer As System.IO.TextWriter)
        writer.WriteLine("Repeat")
    End Sub

    Public Overrides Sub CheckSanity(ByVal project As Project)
    End Sub
End Class
