<Highlight(RegEx:="^\s*loop ", Color:="Green")> _
Public Class LoopCommand
    Inherits CommandBase

    Private _commands As New CommandCollection
    Public ReadOnly Property Commands() As CommandCollection
        Get
            Return _commands
        End Get
    End Property

    Private _Repetitions As New List(Of Integer)
    Public ReadOnly Property Repetitions() As List(Of Integer)
        Get
            Return _Repetitions
        End Get
    End Property

    Private Shared _Random As New Random()

    Public Overrides Sub CheckSanity(ByVal project As Project)
        For Each cmd As CommandBase In Commands
            cmd.CheckSanity(project)
        Next
    End Sub

    Public Overrides Sub Execute(ByVal context As Context)
        Dim reps As Integer = Repetitions(_Random.Next(0, Repetitions.Count))

        While reps > 0
            Commands.Execute(context)
            reps -= 1
        End While
    End Sub

    Public Overrides Sub LoadCommand(ByVal project As Project, ByVal reader As System.IO.TextReader, ByVal line As String, ByRef lineNumber As Integer)
        MyBase.LoadCommand(project, reader, line, lineNumber)

        Dim parts As List(Of String) = ProjectSerializer.ReadTokens(line)

        If parts.Last <> "{" Then
            project.Warnings.Add(String.Format("Line {0}: The Loop command requires a {{ at the end of the line.", lineNumber))
        Else
            parts.RemoveAt(0)
            parts.RemoveAt(parts.Count - 1)

            If parts.Count > 0 Then
                Dim reps As Integer
                For Each rep As String In parts
                    If Integer.TryParse(rep, reps) Then
                        Repetitions.Add(reps)
                    Else
                        project.Warnings.Add(String.Format("Line {0}: The loop command requires all arguments to be numeric. '{1}' is not.", lineNumber, rep))
                    End If
                Next

                If ProjectSerializer.ReadLines(New Object() {project, Commands}, reader, AddressOf ProjectSerializer.ParseCommands, lineNumber) Then
                    project.Warnings.Add(String.Format("Line {0}: The loop command is not closed correctly.", lineNumber))
                End If
            Else
                project.Warnings.Add(String.Format("Line {0}: The loop command must have one or more numeric arguments.", lineNumber))
            End If
        End If
    End Sub

    Public Overrides Sub WriteCommand(ByVal writer As System.IO.TextWriter)
        writer.WriteLine(String.Format("Loop {0} {{", ProjectSerializer.SecureList(Repetitions.ConvertAll(Of String)(Function(rep As Integer) rep.ToString))))
        Commands.WriteCommands(writer)
        writer.WriteLine("}")
    End Sub
End Class
