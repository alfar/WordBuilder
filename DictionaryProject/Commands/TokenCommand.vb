<Highlight(RegEx:="^\s*token ", Color:="Green")> _
Public Class TokenCommand
    Inherits CommandBase

    Private _TokenSet As String
    Public Property TokenSet() As String
        Get
            Return _TokenSet
        End Get
        Set(ByVal value As String)
            _TokenSet = value
        End Set
    End Property

    Public Overrides Sub Execute(ByVal context As Context)
        Dim ts As TokenSet = (From t As TokenSet In Project.TokenSets Where t.Name = TokenSet).FirstOrDefault()

        If ts IsNot Nothing Then
            context.Tokens.Add(ts.GetToken())
        End If
    End Sub

    Public Overrides Sub LoadCommand(ByVal project As Project, ByVal reader As System.IO.TextReader, ByVal line As String, ByRef lineNumber As Integer)
        MyBase.LoadCommand(project, reader, line, lineNumber)
        Dim parts As List(Of String) = ProjectSerializer.ReadTokens(line)

        If parts.Count <> 2 Then
            project.Warnings.Add(String.Format("Line {0}: The Token command requires 1 argument.", lineNumber))
        Else
            _TokenSet = parts(1)
        End If
    End Sub

    Public Overrides Sub WriteCommand(ByVal writer As System.IO.TextWriter)
        writer.WriteLine("Token {0}", TokenSet)
    End Sub


    Public Overrides Sub CheckSanity(ByVal project As Project)
        Dim count As Integer = project.TokenSets.Where(Function(t As TokenSet) t.Name = TokenSet).Count
        If count = 0 Then
            project.Warnings.Add(String.Format("Line {0}: The token set '{1}' does not exist.", LineNumber, TokenSet))
        ElseIf count > 1 Then
            project.Warnings.Add(String.Format("Line {0}: Multiple token sets with the name '{1}' exist.", LineNumber, TokenSet))
        End If
    End Sub
End Class
