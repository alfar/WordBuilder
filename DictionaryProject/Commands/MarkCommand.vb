<Highlight(RegEx:="^\s*mark ", Color:="Green")> _
Public Class MarkCommand
    Inherits CommandBase

    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _Value As String
    Public Property Value() As String
        Get
            Return _Value
        End Get
        Set(ByVal value As String)
            _Value = value
        End Set
    End Property


    Public Overrides Sub Execute(ByVal context As Context)
        context.Mark(Name, Value)
    End Sub

    Public Overrides Sub LoadCommand(ByVal project As Project, ByVal reader As System.IO.TextReader, ByVal line As String, ByRef lineNumber As Integer)
        MyBase.LoadCommand(project, reader, line, lineNumber)

        Dim parts As List(Of String) = ProjectSerializer.ReadTokens(line)

        If parts.Count <> 3 Then
            project.Warnings.Add(String.Format("Line {0}: The mark command requires 2 arguments.", lineNumber))
        Else
            _Name = parts(1)
            _Value = parts(2)
        End If
    End Sub

    Public Overrides Sub WriteCommand(ByVal writer As System.IO.TextWriter)
        writer.WriteLine("Mark {0} {1}", _Name, _Value)
    End Sub

    Public Overrides Sub CheckSanity(ByVal project As Project)

    End Sub
End Class
