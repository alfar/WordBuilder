<Highlight(RegEx:="^\s*drop$", Color:="Green")> _
<Highlight(RegEx:="^\s*drop ", Color:="Green")> _
Public Class DropCommand
    Inherits CommandBase

    Private _Amount As Integer
    Public Property Amount() As Integer
        Get
            Return _Amount
        End Get
        Set(ByVal value As Integer)
            _Amount = value
        End Set
    End Property

    Public Overrides Sub CheckSanity(ByVal project As Project)
        If _Amount <= 0 Then
            project.Warnings.Add(String.Format("Line {0}: Drop command requires a positive integer as its second argument.", m_lineNumber))
        End If
    End Sub

    Public Overrides Sub Execute(ByVal context As Context)
        If context.Tokens.Count > _Amount Then
            context.Tokens.RemoveRange(context.Tokens.Count - _Amount, _Amount)
        Else
            context.Tokens.Clear()
        End If
    End Sub

    Public Overrides Sub LoadCommand(ByVal project As Project, ByVal reader As System.IO.TextReader, ByVal line As String, ByRef lineNumber As Integer)
        MyBase.LoadCommand(project, reader, line, lineNumber)

        Dim parts As List(Of String) = ProjectSerializer.ReadTokens(line)

        If parts.Count = 1 Then
            _Amount = 1
        ElseIf parts.Count = 2 Then
            If Not Integer.TryParse(parts(1), _Amount) Then
                project.Warnings.Add(String.Format("Line {0}: Drop command requires a positive integer as its second argument.", m_lineNumber))
            End If
        End If
    End Sub

    Public Overrides Sub WriteCommand(ByVal writer As System.IO.TextWriter)
        writer.WriteLine("Drop {0}", _Amount)
    End Sub
End Class
