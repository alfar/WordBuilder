Imports System.Text.RegularExpressions

<Highlight(Regex:="^\s*translate \\\{", Color:="Green", NextLevel:=2)> _
<Highlight(Regex:="#|=>", Color:="Red", Level:=2)> _
<Highlight(Regex:="\\\}", Color:="Green", Level:=2, NextLevel:=-2)> _
Public Class TranslateCommand
    Inherits CommandBase

    Private _stringmapping As New Dictionary(Of String(), String())
    Private _mapping As New Dictionary(Of Regex, String())

    Public Overrides Sub CheckSanity(ByVal project As Project)
        If _stringmapping.Count = 0 Then
            project.Warnings.Add(String.Format("Line {0}: The Translate command requires at least one translation directive.", LineNumber))
        End If

        For Each key As String() In _stringmapping.Keys
            Dim reg As Regex = MakeRegex(key)
            _mapping.Add(reg, _stringmapping(key))
        Next
    End Sub

    Private Function ExpandTokenSet(ByVal token As String, ByVal ingroup As Boolean) As String
        If token.StartsWith("$") Then
            Dim sb As New System.Text.StringBuilder()

            If Not ingroup Then
                sb.Append("(?:")
            End If

            For Each tk As String In Project.TokenSets.FindByName(token.Substring(1)).Tokens
                sb.Append(tk)
                sb.Append("|")
            Next

            If Not ingroup Then
                sb.Append(")")
            End If

            Return sb.ToString()
        End If

        Return token
    End Function

    Private Function MakeRegex(ByVal source As String()) As Regex
        Dim sb As New System.Text.StringBuilder()

        If source.First = "#" Then
            sb.Append("^")
            source = source.Skip(1).ToArray()
        End If

        sb.Append("\|")

        Dim ingroup As Boolean = False

        For Each token As String In source
            Dim bPickup As Boolean = False
            If token.StartsWith("(") AndAlso token.EndsWith(")") Then
                bPickup = True
                token = token.Substring(1, token.Length - 2)
                sb.Append("(")
            End If

            If token.StartsWith("[") Then
                sb.Append("(?:")

                For Each tk As String In ProjectSerializer.ReadTokens(token.Substring(1, token.Length - 2))
                    sb.Append(ExpandTokenSet(tk, True))
                    sb.Append("|")
                Next

                sb.Remove(sb.Length - 1, 1)
                sb.Append(")")
                ingroup = False
            ElseIf token = "#" Then
                'sb.Remove(sb.Length - 2, 2)
                sb.Append("$")
                Return New Regex(sb.ToString())
            Else
                sb.Append(ExpandTokenSet(token, False))
            End If

            If bPickup Then
                sb.Append(")")
            End If

            sb.Append("\|")
        Next

        'If sb.Length > 2 Then
        '    sb.Remove(sb.Length - 2, 2)
        'End If

        Debug.WriteLine(sb.ToString())
        Return New Regex(sb.ToString())
    End Function

    Private Function ExpandReplacement(ByVal destination As String(), ByVal m As Match) As String
        Dim result As New List(Of String)()

        For Each token As String In destination
            Dim tokenId As Integer
            If token.StartsWith("\") AndAlso Integer.TryParse(token.Substring(1), tokenId) Then
                result.Add(m.Groups(tokenId).Value)
            ElseIf token.StartsWith("$") Then
                result.Add(Project.TokenSets.FindByName(token.Substring(1)).GetToken())
            ElseIf token.StartsWith("[") Then
                Dim tokens As List(Of String) = ProjectSerializer.ReadTokens(token.Substring(1, token.Length - 2))

                Dim ts As New TokenSet()

                For Each t As String In tokens
                    ts.Tokens.AddRange(ExpandTokenSet(t, True).Split("|"c))
                Next

                result.Add(ts.GetToken())
            Else
                result.Add(token)
            End If
        Next

        Return "|" & String.Join("|", result.ToArray()) & "|"
    End Function

    Public Overrides Sub Execute(ByVal context As Context)
        Dim input As String = "|" & String.Join("|", context.Tokens.Where(Function(token As String) Not String.IsNullOrEmpty(token)).ToArray()) & "|"

        For Each regex As Regex In _mapping.Keys
            Dim destination As String() = _mapping(regex)

            input = regex.Replace(input, Function(m As Match) ExpandReplacement(destination, m))
        Next

        context.Tokens.Clear()
        context.Tokens.AddRange(input.Split(New Char() {"|"c}, StringSplitOptions.RemoveEmptyEntries))
    End Sub

    Public Overrides Sub LoadCommand(ByVal project As Project, ByVal reader As System.IO.TextReader, ByVal line As String, ByRef lineNumber As Integer)
        MyBase.LoadCommand(project, reader, line, lineNumber)

        Dim parts As List(Of String) = ProjectSerializer.ReadTokens(line)

        If parts.Count = 2 AndAlso parts(1) = "{" Then
            If ProjectSerializer.ReadLines(project, reader, AddressOf ReadTranslation, lineNumber) Then
                project.Warnings.Add(String.Format("Line {0}: The Translate command is not closed correctly.", lineNumber))
            End If
        Else
            project.Warnings.Add(String.Format("Line {0}: The Translate command requires a {{} at the end of the line.", lineNumber))
        End If
    End Sub

    Private Function ReadTranslation(ByVal context As Object, ByVal reader As IO.TextReader, ByVal line As String, ByRef lineNumber As Integer) As Boolean
        If line = "}" Then
            Return False
        End If

        If String.IsNullOrEmpty(line) Then
            Return True
        End If

        Dim tokens As List(Of String) = ProjectSerializer.ReadTokens(line)

        Dim source As New List(Of String)()
        Dim dest As New List(Of String)()

        Dim translatepos As Integer = tokens.IndexOf("=>")

        If translatepos = -1 Then
            Project.Warnings.Add(String.Format("Line {0}: Missing => in Translate directive.", lineNumber))
        ElseIf translatepos = 0 Then
            Project.Warnings.Add(String.Format("Line {0}: Missing source tokens in Translate directive.", lineNumber))
        Else
            _stringmapping.Add(tokens.Take(translatepos).ToArray(), tokens.Skip(translatepos + 1).ToArray())
        End If

        Return True
    End Function

    Public Overrides Sub WriteCommand(ByVal writer As System.IO.TextWriter)
        writer.WriteLine("Translate {")
        For Each key As String() In _stringmapping.Keys
            writer.WriteLine("{0} => {1}", ProjectSerializer.SecureList(key), ProjectSerializer.SecureList(_stringmapping(key)))
        Next
        writer.WriteLine("}")
    End Sub
End Class
