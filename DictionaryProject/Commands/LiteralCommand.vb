<Highlight(RegEx:="^\s*literal ", Color:="Green")> _
Public Class LiteralCommand
    Inherits CommandBase

    Private _Literal As String
    Public Property Literal() As String
        Get
            Return _Literal
        End Get
        Set(ByVal value As String)
            _Literal = value
        End Set
    End Property

    Public Overrides Sub Execute(ByVal context As Context)
        context.Tokens.Add(Literal)
    End Sub

    Public Overrides Sub LoadCommand(ByVal project As Project, ByVal reader As System.IO.TextReader, ByVal line As String, ByRef lineNumber As Integer)
        MyBase.LoadCommand(project, reader, line, lineNumber)
        Dim parts As List(Of String) = ProjectSerializer.ReadTokens(line)

        If parts.Count <> 2 Then
            project.Warnings.Add(String.Format("Line {0}: The Literal command requires 1 argument.", lineNumber))
        Else
            _Literal = parts(1)
        End If
    End Sub

    Public Overrides Sub WriteCommand(ByVal writer As System.IO.TextWriter)
        writer.WriteLine("Literal {0}", ProjectSerializer.SecureString(_Literal))
    End Sub


    Public Overrides Sub CheckSanity(ByVal project As Project)
    End Sub
End Class
