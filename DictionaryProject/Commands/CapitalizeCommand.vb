<Highlight(RegEx:="^\s*capitalize ", Color:="Green")> _
Public Class CapitalizeCommand
    Inherits CommandBase

    Public Overrides Sub CheckSanity(ByVal project As Project)
        If _Index = 0 Then
            project.Warnings.Add(String.Format("Line {0}: The capitalize command requires the first argument to be a non-zero integer.", LineNumber))
        End If
    End Sub

    Private _Index As Integer
    Public Property Index() As Integer
        Get
            Return _Index
        End Get
        Set(ByVal value As Integer)
            _Index = value
        End Set
    End Property

    Public Overrides Sub Execute(ByVal context As Context)
        If context.Tokens.Count > 0 Then
            Dim pos As Integer = _Index
            If pos < 0 Then
                pos = context.Tokens.Count + pos
            Else
                pos -= 1
            End If

            If pos < 0 Then
                pos = 0
            End If

            If pos < context.Tokens.Count Then
                context.Tokens(pos) = String.Format("{0}{1}", Char.ToUpper(context.Tokens(pos).Chars(0), System.Globalization.CultureInfo.CurrentCulture), context.Tokens(pos).Substring(1))
            End If
        End If
    End Sub

    Public Overrides Sub LoadCommand(ByVal project As Project, ByVal reader As System.IO.TextReader, ByVal line As String, ByRef lineNumber As Integer)
        MyBase.LoadCommand(project, reader, line, lineNumber)

        Dim parts As List(Of String) = ProjectSerializer.ReadTokens(line)

        If parts.Count = 1 Then
            _Index = -1
        ElseIf parts.Count = 2 Then
            If Not Integer.TryParse(parts(1), _Index) Then
                project.Warnings.Add(String.Format("Line {0}: The capitalize command requires the first argument to be an integer.", lineNumber))
            End If
        Else
            project.Warnings.Add(String.Format("Line {0}: The capitalize command requires zero or one argument.", lineNumber))
        End If
    End Sub

    Public Overrides Sub WriteCommand(ByVal writer As System.IO.TextWriter)
        writer.WriteLine("Capitalize {0}", _Index)
    End Sub
End Class
