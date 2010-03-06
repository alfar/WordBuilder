<Highlight(RegEx:="^\s*branch ", Color:="Green")> _
Public Class BranchCommand
    Inherits CommandBase

    Private _Rule As String
    Public Property Rule() As String
        Get
            Return _Rule
        End Get
        Set(ByVal value As String)
            _Rule = value
        End Set
    End Property

    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public Overrides Sub Execute(ByVal context As Context)
        Project.Rules.GetRuleByName(Rule).Execute(context.Branch(Name))
    End Sub

    Public Overrides Sub LoadCommand(ByVal project As Project, ByVal reader As System.IO.TextReader, ByVal line As String, ByRef lineNumber As Integer)
        MyBase.LoadCommand(project, reader, line, lineNumber)

        Dim parts As List(Of String) = ProjectSerializer.ReadTokens(line)

        If parts.Count <> 3 Then
            project.Warnings.Add(String.Format("Line {0}: The branch command requires 2 arguments.", lineNumber))
        Else
            _Name = parts(1)
            _Rule = parts(2)
        End If
    End Sub

    Public Overrides Sub WriteCommand(ByVal writer As System.IO.TextWriter)
        writer.WriteLine("Branch {0} {1}", _Name, _Rule)
    End Sub

    Public Overrides Sub CheckSanity(ByVal project As Project)
        If project.Rules.GetRuleByName(Rule) Is Nothing Then
            project.Warnings.Add(String.Format("Line {0}: The rule '{1}' does not exist.", LineNumber, Rule))
        End If
    End Sub
End Class
