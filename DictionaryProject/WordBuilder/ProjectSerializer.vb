Public Delegate Function LineParserDelegate(ByVal context As Object, ByVal reader As IO.TextReader, ByVal line As String, ByRef lineNumber As Integer) As Boolean

Public NotInheritable Class ProjectSerializer
    Private Sub New()
    End Sub

    Public Shared Function LoadDictionary(ByVal path As String) As DictionaryProject
        Dim result As New DictionaryProject()

        Using tr As New IO.StreamReader(path, Text.Encoding.UTF8)
            ReadDictionary(result, tr)
        End Using

        Return result
    End Function

    Private Shared Sub ReadDictionary(ByVal dic As DictionaryProject, ByVal reader As IO.TextReader)
        If Not ReadLines(dic, reader, New LineParserDelegate(AddressOf ParseDictionaryLine)) Then
            Throw New ApplicationException("Invalid dictionaryFile")
        End If
    End Sub

    Private Shared Function ParseDictionaryLine(ByVal context As Object, ByVal reader As IO.TextReader, ByVal line As String, ByRef lineNumber As Integer) As Boolean
        Dim parts As List(Of String) = ReadTokens(line)

        If parts.Count <> 2 OrElse parts(1) <> "{" Then
            Throw New ApplicationException(String.Format("Line {0}: Invalid dictionary file. Meaning lines must contain meaning followed by {{", lineNumber))
        End If

        Dim dic As DictionaryProject = DirectCast(context, DictionaryProject)

        Dim ctx As Context = ReadContext(reader, lineNumber)

        dic.AddWord(parts(0), ctx)

        Return True
    End Function

    Private Shared Function ReadContext(ByVal reader As IO.TextReader, ByVal lineNumber As Integer) As Context
        Dim result As New Context()

        If ReadLines(result, reader, AddressOf ParseContextLines, lineNumber) Then
            Throw New ApplicationException("End of file reached while reading a context.")
        End If

        Return result
    End Function

    Public Shared Sub SaveDictionary(ByVal dic As DictionaryProject, ByVal path As String)
        Using tw As New IO.StreamWriter(path, False, Text.Encoding.UTF8)
            For Each word In dic.Words.Keys
                For Each ctx As Context In dic.Words(word)
                    tw.WriteLine(String.Format("{0} {{", word))

                    WriteContext(ctx, tw)

                    tw.WriteLine("}")
                Next
            Next
        End Using
    End Sub

    Private Shared Sub WriteContext(ByVal context As Context, ByVal tw As IO.TextWriter, Optional ByVal indent As String = "")
        tw.WriteLine(String.Format("{0}{1}", indent, SecureList(context.Tokens)))

        For Each b As String In context.Branches.Keys
            tw.WriteLine(String.Format("{0}. {1} {{", indent, SecureString(b)))

            WriteContext(context.Branches(b), tw, indent & "  ")

            tw.WriteLine(String.Format("{0}}}", indent))
        Next

        For Each m As String In context.Marks.Keys
            tw.WriteLine(String.Format("{0}! {1} {2}", indent, SecureString(m), SecureString(context.Marks(m))))
        Next
    End Sub

    Private Shared Function ParseContextLines(ByVal context As Object, ByVal reader As IO.TextReader, ByVal line As String, ByRef lineNumber As Integer) As Boolean
        Dim parts As List(Of String) = ReadTokens(line)

        Dim ctx As Context = DirectCast(context, Context)

        If parts(0).Equals(".") Then
            If parts.Count <> 3 OrElse parts(2) <> "{" Then
                Throw New ApplicationException(String.Format("Line {0}: Invalid dictionary file. Branch lines must contain . followed by branch name followed by {{", lineNumber))
            End If
            Dim branch As Context = ctx.Branch(parts(1))
            branch.Tokens.Clear()
            If ReadLines(branch, reader, AddressOf ParseContextLines, lineNumber) Then
                Throw New ApplicationException("End of file reached while reading a context.")
            End If
        ElseIf parts(0).Equals("!") Then
            If parts.Count <> 3 Then
                Throw New ApplicationException(String.Format("Line {0}: Invalid dictionary file. Mark lines must contain ! followed by mark name followed by value.", lineNumber))
            End If
            ctx.Mark(parts(1), parts(2))
        ElseIf line.Equals("}") Then
            Return False
        Else
            ctx.Tokens.AddRange(parts)
        End If

        Return True
    End Function

    Public Shared Function LoadProject(ByVal path As String) As Project
        Dim result As New Project()

        Try
            Using tr As New IO.StreamReader(path, Text.Encoding.UTF8)
                ReadProject(result, tr)
            End Using
        Catch ex As Exception
            result.Warnings.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Shared Function LoadProjectString(ByVal code As String) As Project
        Dim result As New Project

        Try
            Using tr As New IO.StringReader(code)
                ReadProject(result, tr)
            End Using
        Catch ex As Exception
            result.Warnings.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Shared Function ReadLines(ByVal context As Object, ByVal reader As IO.TextReader, ByVal parser As LineParserDelegate, Optional ByRef lineNumber As Integer = 0) As Boolean
        Dim line As String = reader.ReadLine()
        While line IsNot Nothing
            line = line.Trim(" "c, Chr(9))
            lineNumber += 1
            If line.StartsWith("/*") Then
                Dim startingLine As Integer = lineNumber
                While line IsNot Nothing AndAlso Not line.EndsWith("*/")
                    line = reader.ReadLine()
                    lineNumber += 1
                End While
                If line Is Nothing Then
                    Throw New ApplicationException(String.Format("Line {0}: Block comment not closed correctly.", startingLine))
                End If
            ElseIf line.StartsWith("//") Then
                ' Ignore single line comments.
            ElseIf Not parser.Invoke(context, reader, line.Trim(" "c, Chr(9)), lineNumber) Then
                Return False
            End If
            line = reader.ReadLine()
        End While

        Return True
    End Function

    Public Shared Function ReadTokens(ByVal input As String) As List(Of String)
        Dim result As New List(Of String)()
        Dim start As Integer = 0
        Dim expand As New List(Of String)
        Dim didExpand As Boolean = False

        While start > -1
            Dim token As String = ReadToken(input, start, expand, didExpand)

            If didExpand Then
                result.AddRange(expand)
                expand.Clear()
            Else
                result.Add(token)
            End If
        End While

        Return result
    End Function

    Public Shared Function ExpandToken(ByVal input As String, ByVal origin As Integer, ByRef start As Integer, ByRef expandedToken As List(Of String), ByRef didExpand As Boolean, ByRef remove As Boolean) As String
        Dim token As String = input.Substring(origin, start - origin)

        If token.Contains("[") Then
            Dim nextOpen As Integer = input.IndexOf("["c, start)
            start = input.IndexOf("]"c, start - 1) + 1

            While nextOpen > -1 AndAlso nextOpen < start - 1
                nextOpen = input.IndexOf("["c, nextOpen + 1)
                start = input.IndexOf("]"c, start) + 1
            End While

            If start = 0 Then
                Throw New ApplicationException("Read token past end of line. Missing a ], are we?")
            End If

            token = input.Substring(origin, start - origin)
            Dim parts() As String = token.Substring(0, token.Length - 1).Split(New Char() {"["c}, 2)

            Dim copies As Integer = 0
            If expandedToken IsNot Nothing AndAlso Integer.TryParse(parts(0), copies) Then
                Dim copySet As List(Of String) = ReadTokens(parts(1))
                For i As Integer = 1 To copies
                    expandedToken.AddRange(copySet)
                Next
                didExpand = True
            ElseIf expandedToken IsNot Nothing AndAlso parts(0) = "!" Then
                expandedToken.AddRange(ReadTokens(parts(1)))
                remove = True
            End If
        End If

        Return token
    End Function

    Public Shared Function ReadToken(ByVal input As String, ByRef start As Integer, Optional ByRef expandedToken As List(Of String) = Nothing, Optional ByRef didExpand As Boolean = False, Optional ByRef remove As Boolean = False) As String
        If start = -1 Then
            Throw New ApplicationException("Read token past end of line")
        End If
        didExpand = False
        remove = False

        Dim len As Integer = input.Length

        If input(start) = """"c Then
            Dim origin As Integer = start + 1
            While start > -1 AndAlso start < len
                start = input.IndexOf(""""c, start + 1)

                If start = -1 Then
                    Exit While
                End If

                If input(start - 1) <> "\"c Then
                    Dim tokenlen As Integer = start - origin
                    start += 1

                    While start < len AndAlso input(start) = " "c
                        start += 1
                    End While

                    If start >= len Then
                        start = -1
                    ElseIf input.Substring(start).StartsWith("//") Then
                        start = -1
                    End If

                    Return input.Substring(origin, tokenlen).Replace("\""", """").Replace("\\", "\")
                End If
            End While

            Throw New ApplicationException("Read token past end of line. Missing a "", are we?")
        Else
            Dim origin As Integer = start

            start = input.IndexOf(" "c, start)

            If start > -1 Then
                Dim foundtoken As String = ExpandToken(input, origin, start, expandedToken, didExpand, remove)
                While start < len AndAlso input(start) = " "c
                    start += 1
                End While

                If start >= len OrElse input.Substring(start).StartsWith("//") Then
                    start = -1
                End If

                Return foundtoken
            ElseIf input.Last = "]"c Then
                Dim foundtoken As String = ExpandToken(input, origin, len, expandedToken, didExpand, remove)
                start = -1

                Return foundtoken
            Else
                Return input.Substring(origin)
            End If
        End If
    End Function

    Private Shared Sub ReadProject(ByVal project As Project, ByVal reader As IO.TextReader)
        If Not ReadLines(project, reader, AddressOf ParseProject) Then
            project.Warnings.Add("The code was not read to the end of the document")
        End If

        If project.StartRules.Count = 0 Then
            project.StartRules.Add("root", 100)
        End If

        For Each sr As String In project.StartRules.Keys
            If project.Rules.GetRuleByName(sr) Is Nothing Then
                project.Warnings.Add(String.Format("The starting rule '{0}' does not exist.", sr))
            End If
        Next

        For Each r As Rule In project.Rules
            For Each c As CommandBase In r.Commands
                c.CheckSanity(project)
            Next
        Next
    End Sub

    Private Shared Function ParseProject(ByVal context As Object, ByVal reader As IO.TextReader, ByVal line As String, ByRef lineNumber As Integer) As Boolean
        Dim project As Project = TryCast(context, Project)

        Dim start As Integer = 0

        Try
            If Not String.IsNullOrEmpty(line) Then
                Dim command = ReadToken(line, start)

                If command.Equals("tokens", StringComparison.CurrentCultureIgnoreCase) Then
                    Dim tks As New TokenSet()
                    Dim len As Integer = line.Length
                    Dim token As String = ReadToken(line, start)
                    tks.Name = token

                    Dim expand As New List(Of String)()
                    Dim didExpand As Boolean = False
                    Dim remove As Boolean = False
                    While start > -1 AndAlso start < len
                        token = ReadToken(line, start, expand, didExpand, remove)

                        If Not didExpand AndAlso Not remove Then
                            If token.StartsWith("$") AndAlso token.Length > 1 Then
                                Dim refTks As TokenSet = project.TokenSets.Where(Function(ts As TokenSet) ts.Name = token.Substring(1)).FirstOrDefault()
                                If refTks IsNot Nothing Then
                                    tks.Tokens.AddRange(refTks.Tokens)
                                Else
                                    project.Warnings.Add(String.Format("Line {0}: The Tokens directive referenced another token set '{1}' which did not exist at the time of parsing.", lineNumber, token.Substring(1)))
                                End If
                            Else
                                tks.Tokens.Add(token)
                            End If
                        End If
                    End While

                    If remove Then
                        tks.Tokens.RemoveAll(Function(tok As String) expand.Contains(tok))
                    Else
                        tks.Tokens.AddRange(expand)
                    End If

                    If tks.Tokens.Count = 0 Then
                        project.Warnings.Add(String.Format("Line {0}: The Tokens directive requires at least 2 arguments.", lineNumber))
                    End If

                    project.TokenSets.Add(tks)
                ElseIf command.Equals("rule", StringComparison.CurrentCultureIgnoreCase) Then
                    Dim r As New Rule()
                    r.Name = ReadToken(line, start)
                    r.LineNumber = lineNumber
                    Dim probability As Double = 1

                    Dim probstring As String = ReadToken(line, start)
                    If probstring = "{" Then
                        If start > -1 Then
                            Throw New ApplicationException("The Rule directive must end with a {")
                        End If
                    Else
                        If Not Double.TryParse(probstring, probability) Then
                            project.Warnings.Add(String.Format("Line {0}: The Rule directive requires the second argument to be numeric.", lineNumber))
                        End If

                        probstring = ReadToken(line, start)

                        If probstring <> "{" Then
                            Throw New ApplicationException("The Rule directive must end with a {")
                        End If
                    End If

                    r.Probability = probability

                    project.Rules.Add(r)
                    If ReadLines(New Object() {project, r.Commands}, reader, AddressOf ParseCommands, lineNumber) Then
                        project.Warnings.Add(String.Format("Line {1}: The rule '{0}' is not closed correctly.", r.Name, lineNumber))
                    End If
                ElseIf command.Equals("startingrule", StringComparison.CurrentCultureIgnoreCase) Then
                    Dim rulename As String = ReadToken(line, start)

                    If start > -1 Then
                        Dim amountstring As String = ReadToken(line, start)

                        If start > -1 Then
                            project.Warnings.Add(String.Format("Line {0}: The StartingRule directive requires 1 or 2 arguments.", lineNumber))
                        End If

                        Dim val As Integer
                        If Integer.TryParse(amountstring, val) Then
                            project.StartRules(rulename) = val
                        Else
                            project.Warnings.Add(String.Format("Line {0}: The StartingRule directive requires that the second argument is a number.", lineNumber))
                        End If
                    Else
                        project.StartRules(rulename) = 100
                    End If
                ElseIf command.Equals("Column", StringComparison.CurrentCultureIgnoreCase) Then
                    Dim name As String = ReadToken(line, start)
                    If start > -1 Then
                        Dim value As String = ReadToken(line, start)

                        project.Columns.Add(name, value)
                    Else
                        project.Warnings.Add(String.Format("Line {0}: The Column directive requires two arguments.", lineNumber))
                    End If
                Else
                    project.Warnings.Add(String.Format("Line {1}: The command '{0}' was not recognized.", command, lineNumber))
                End If
            End If
        Catch ex As Exception
            project.Warnings.Add(String.Format("Line {0}: {1}", lineNumber, ex.Message))
        End Try

        Return True
    End Function

    Private Shared s_Commands As List(Of Type)
    Private Shared ReadOnly Property Commands() As List(Of Type)
        Get
            If s_Commands Is Nothing Then
                s_Commands = (From t As Type In Reflection.Assembly.GetAssembly(GetType(ProjectSerializer)).GetTypes() Where t.IsSubclassOf(GetType(CommandBase))).ToList()
            End If
            Return s_Commands
        End Get
    End Property

    Public Shared Function ParseWeightedCommands(ByVal context As Object, ByVal reader As IO.TextReader, ByVal line As String, ByRef lineNumber As Integer) As Boolean
        If String.IsNullOrEmpty(line) Then
            Return True
        End If

        If line.StartsWith("}") Then
            Return False
        End If

        Dim args() As Object = TryCast(context, Object())
        Dim project As Project = TryCast(args(0), Project)
        Dim cmds As List(Of WeightedCommand) = TryCast(args(1), List(Of WeightedCommand))

        Dim start As Integer = 0
        Dim weightStr As String = ReadToken(line, start)

        Dim cmdStart As Integer = start

        Dim weight As Double = 0D
        If Not Double.TryParse(weightStr, weight) Then
            project.Warnings.Add(String.Format("Line {0}: Expected a weighted command.", lineNumber))
        Else
            Dim command As String = ReadToken(line, start)

            Dim commandCandidates As IEnumerable(Of Type) = From cmd As Type In Commands Where cmd.Name.ToLower().StartsWith(command.ToLower())

            If commandCandidates.Count > 1 Then
                project.Warnings.Add(String.Format("Line {1}: The command '{0}' is ambiguous.", command, lineNumber))
            Else
                Dim tp As Type = commandCandidates.FirstOrDefault()

                If tp IsNot Nothing Then
                    Dim c As CommandBase = TryCast(Activator.CreateInstance(tp), CommandBase)

                    If c IsNot Nothing Then
                        c.LoadCommand(project, reader, line.Substring(cmdStart), lineNumber)
                    Else
                        project.Warnings.Add(String.Format("Line {1}: The command '{0}' is not valid.", command, lineNumber))
                    End If

                    cmds.Add(New WeightedCommand() With {.Command = c, .Weight = weight})
                Else
                    project.Warnings.Add(String.Format("Line {1}: The command '{0}' is not valid.", command, lineNumber))
                End If
            End If
        End If

        Return True
    End Function

    Public Shared Function ParseCommands(ByVal context As Object, ByVal reader As IO.TextReader, ByVal line As String, ByRef lineNumber As Integer) As Boolean
        If String.IsNullOrEmpty(line) Then
            Return True
        End If

        If line.StartsWith("}") Then
            Return False
        End If

        Dim args() As Object = TryCast(context, Object())
        Dim project As Project = TryCast(args(0), Project)
        Dim cmds As CommandCollection = TryCast(args(1), CommandCollection)

        Dim start As Integer = 0
        Dim command As String = ReadToken(line, start)

        Dim commandCandidates As IEnumerable(Of Type) = From cmd As Type In Commands Where cmd.Name.ToLower().StartsWith(command.ToLower())

        If commandCandidates.Count > 1 Then
            project.Warnings.Add(String.Format("Line {1}: The command '{0}' is ambiguous.", command, lineNumber))
        Else
            Dim tp As Type = commandCandidates.FirstOrDefault()

            If tp IsNot Nothing Then
                Dim c As CommandBase = TryCast(Activator.CreateInstance(tp), CommandBase)

                If c IsNot Nothing Then
                    c.LoadCommand(project, reader, line, lineNumber)
                Else
                    project.Warnings.Add(String.Format("Line {1}: The command '{0}' is not valid.", command, lineNumber))
                End If

                cmds.Add(c)
            Else
                project.Warnings.Add(String.Format("Line {1}: The command '{0}' is not valid.", command, lineNumber))
            End If
        End If

        Return True
    End Function

    Public Shared Sub SaveProject(ByVal project As Project, ByVal path As String)
        Using tw As New IO.StreamWriter(path, False, Text.Encoding.UTF8)
            WriteProject(project, tw)
        End Using
    End Sub

    Private Shared Sub WriteProject(ByVal project As Project, ByVal writer As IO.TextWriter)
        For Each startRule As String In project.StartRules.Keys
            writer.WriteLine(String.Format("StartingRule {0} {1}", startRule, project.StartRules(startRule)))
        Next

        For Each ts As TokenSet In project.TokenSets
            writer.WriteLine(String.Format("Tokens {0} {1}", ts.Name, String.Join(" ", ts.Tokens.ToArray())))
        Next

        For Each r As Rule In project.Rules
            writer.WriteLine(String.Format("Rule {0} {1} {{}", r.Name, r.Probability))
            r.Commands.WriteCommands(writer)
            writer.WriteLine("}")
        Next
    End Sub

    Public Shared Function SecureString(ByVal input As String) As String
        If input.Contains(" ") Then
            Return String.Format("""{0}""", input)
        End If

        Return input
    End Function

    Public Shared Function SecureList(ByVal list As IEnumerable(Of String)) As String
        Dim sb As New System.Text.StringBuilder()

        For Each token As String In list
            sb.Append(" ")
            sb.Append(SecureString(token))
        Next

        Return sb.ToString().TrimStart()
    End Function
End Class
