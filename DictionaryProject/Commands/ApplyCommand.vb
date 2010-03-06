<Highlight(RegEx:="^\s*apply \\\{", Color:="Green", NextLevel:=4)> _
<CloneHighlighting(1, 4)> _
<Highlight(Regex:="\\\}", Color:="Green", Level:=4, NextLevel:=-2)> _
Public Class ApplyCommand
    Inherits CommandBase

    Private _commands As New CommandCollection
    Public ReadOnly Property Commands() As CommandCollection
        Get
            Return _commands
        End Get
    End Property

    Public Overrides Sub CheckSanity(ByVal project As Project)
        For Each cmd As CommandBase In Commands
            cmd.CheckSanity(project)
        Next
    End Sub

    Public Overrides Sub Execute(ByVal context As Context)
        Dim branchList As New List(Of Context)()
        branchList.Add(context)

        Dim branchStack As New Stack(Of IEnumerator(Of Context))
        branchStack.Push(Nothing)

        Dim branches As IEnumerator(Of Context) = context.Branches.Values.GetEnumerator()

        While branchStack.Count > 0
            While branches.MoveNext()
                branchList.Add(branches.Current)

                branchStack.Push(branches)
                branches = branches.Current.Branches.Values.GetEnumerator()
            End While

            branches = branchStack.Pop()
        End While

        For Each c As Context In branchList
            Commands.Execute(c)
        Next
    End Sub

    Public Overrides Sub LoadCommand(ByVal project As Project, ByVal reader As System.IO.TextReader, ByVal line As String, ByRef lineNumber As Integer)
        MyBase.LoadCommand(project, reader, line, lineNumber)

        Dim parts As List(Of String) = ProjectSerializer.ReadTokens(line)

        If parts.Last <> "{" Then
            project.Warnings.Add(String.Format("Line {0}: The Apply command requires a {{ at the end of the line.", lineNumber))
        Else
            parts.RemoveAt(0)
            parts.RemoveAt(parts.Count - 1)

            If parts.Count = 0 Then
                If ProjectSerializer.ReadLines(New Object() {project, Commands}, reader, AddressOf ProjectSerializer.ParseCommands, lineNumber) Then
                    project.Warnings.Add(String.Format("Line {0}: The Apply command is not closed correctly.", lineNumber))
                End If
            Else
                project.Warnings.Add(String.Format("Line {0}: The Apply command must have no arguments.", lineNumber))
            End If
        End If
    End Sub

    Public Overrides Sub WriteCommand(ByVal writer As System.IO.TextWriter)
        writer.WriteLine("Apply {")
        Commands.WriteCommands(writer)
        writer.WriteLine("}")
    End Sub
End Class
