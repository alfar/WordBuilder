<Highlight(RegEx:="^\s*random \\\{", Color:="Green", NextLevel:=3)> _
<Highlight(Regex:="^\s*\d+(\.\d+)?", Color:="Red", Level:=3)> _
<Highlight(Regex:="\\\}", Color:="Green", Level:=3, NextLevel:=-2)> _
Public Class RandomCommand
    Inherits CommandBase

    Public Overrides Sub CheckSanity(ByVal project As Project)
        For Each cmd As CommandBase In _Commands.Select(Function(w As WeightedCommand) w.Command)
            cmd.CheckSanity(project)
        Next
    End Sub

    Private Shared _Random As New Random()

    Public Overrides Sub Execute(ByVal context As Context)
        Dim total As Double = Commands.Sum(Function(wc As WeightedCommand) wc.Weight)

        Dim selected As Double = _Random.NextDouble() * total

        For Each wc As WeightedCommand In Commands
            selected -= wc.Weight
            If selected <= 0 Then
                wc.Command.Execute(context)
                Exit Sub
            End If
        Next
    End Sub

    Private _Commands As New List(Of WeightedCommand)
    Public ReadOnly Property Commands() As List(Of WeightedCommand)
        Get
            Return _Commands
        End Get
    End Property

    Public Overrides Sub LoadCommand(ByVal project As Project, ByVal reader As System.IO.TextReader, ByVal line As String, ByRef lineNumber As Integer)
        MyBase.LoadCommand(project, reader, line, lineNumber)

        Dim parts As List(Of String) = ProjectSerializer.ReadTokens(line)

        If parts.Last <> "{" Then
            project.Warnings.Add(String.Format("Line {0}: The Random command requires a {{ at the end of the line.", lineNumber))
        Else
            parts.RemoveAt(0)
            parts.RemoveAt(parts.Count - 1)

            If parts.Count = 0 Then
                If ProjectSerializer.ReadLines(New Object() {project, Commands}, reader, AddressOf ProjectSerializer.ParseWeightedCommands, lineNumber) Then
                    project.Warnings.Add(String.Format("Line {0}: The Random command is not closed correctly.", lineNumber))
                End If
            Else
                project.Warnings.Add(String.Format("Line {0}: The Random command must have no arguments.", lineNumber))
            End If
        End If
    End Sub

    Public Overrides Sub WriteCommand(ByVal writer As System.IO.TextWriter)
        writer.WriteLine("Random {")

        For Each wc As WeightedCommand In _Commands
            writer.Write("{0} ", wc.Weight.ToString(System.Globalization.CultureInfo.InvariantCulture))
            wc.Command.WriteCommand(writer)
        Next

        writer.WriteLine("}")
    End Sub
End Class
