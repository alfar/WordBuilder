<Highlight(RegEx:="^\s*substring ", Color:="Green")> _
Public Class SubstringCommand
    Inherits CommandBase

    Private _StartIndex As Integer = 1
    Public Property StartIndex() As Integer
        Get
            Return _StartIndex
        End Get
        Set(ByVal value As Integer)
            _StartIndex = value
        End Set
    End Property

    Private _EndIndex As Integer = -1
    Public Property EndIndex() As Integer
        Get
            Return _EndIndex
        End Get
        Set(ByVal value As Integer)
            _EndIndex = value
        End Set
    End Property

    Public Overrides Sub CheckSanity(ByVal project As Project)
        If _StartIndex > 0 AndAlso _EndIndex > 0 AndAlso _EndIndex < _StartIndex Then
            project.Warnings.Add(String.Format("Line {0}: You want to start after the end?"))
        End If
        If _StartIndex < 0 AndAlso _EndIndex < 0 AndAlso _EndIndex < _StartIndex Then
            project.Warnings.Add(String.Format("Line {0}: You want to start after the end?"))
        End If
    End Sub

    Public Overrides Sub Execute(ByVal context As Context)
        If context.Tokens.Count > 0 Then
            Dim s As Integer = _StartIndex
            If s < 0 Then
                s = context.Tokens.Last.Length + s
            Else
                s -= 1
            End If

            If s < 0 Then
                s = 0
            End If

            Dim e As Integer = _EndIndex
            If e < 0 Then
                e = context.Tokens.Last.Length + e
            Else
                e -= 1
            End If

            If s > e Then ' Because s at this point is always >= 0, this includes the case for e < 0, so no need to double check.
                context.Tokens.RemoveAt(context.Tokens.Count - 1)
            Else
                context.Tokens(context.Tokens.Count - 1) = context.Tokens.Last.Substring(s, e - s + 1)
            End If
        End If
    End Sub

    Public Overrides Sub LoadCommand(ByVal project As Project, ByVal reader As System.IO.TextReader, ByVal line As String, ByRef lineNumber As Integer)
        MyBase.LoadCommand(project, reader, line, lineNumber)

        Dim parts As List(Of String) = ProjectSerializer.ReadTokens(line)

        If parts.Count = 2 Then
            If Not Integer.TryParse(parts(1), _StartIndex) Then
                project.Warnings.Add(String.Format("Line {0}: Substring command requires an integer as its first argument.", m_lineNumber))
            End If
        ElseIf parts.Count = 3 Then
            If Not Integer.TryParse(parts(1), _StartIndex) Then
                project.Warnings.Add(String.Format("Line {0}: Substring command requires an integer as its first argument.", m_lineNumber))
            End If

            If Not Integer.TryParse(parts(2), _EndIndex) Then
                project.Warnings.Add(String.Format("Line {0}: Substring command requires an integer as its second argument.", m_lineNumber))
            End If
        Else
            project.Warnings.Add(String.Format("Line {0}: Substring command requires one or two integer arguments.", m_lineNumber))
        End If
    End Sub

    Public Overrides Sub WriteCommand(ByVal writer As System.IO.TextWriter)
        writer.WriteLine("Substring {0} {1}", StartIndex, EndIndex)
    End Sub
End Class
