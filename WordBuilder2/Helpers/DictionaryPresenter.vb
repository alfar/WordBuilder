Public NotInheritable Class DictionaryPresenter
    Private Sub New()
    End Sub

    Private Shared s_font As New WBFont() With {.Name = "Times New Roman", .Size = 10}
    Public Shared Property Font() As WBFont
        Get
            Return s_font
        End Get
        Set(ByVal value As WBFont)
            s_font = value
        End Set
    End Property

    Private Shared Sub RenderWord(ByVal word As DictionaryData.Word, ByVal sb As System.Text.StringBuilder)
        sb.AppendFormat("<dt id=""term_{0}"">", word.Id)
        sb.AppendFormat("<span class=""word"">{0}</span>", word.Word)

        If word.ParentBranch IsNot Nothing Then
            sb.AppendFormat(" <span class=""parent"">({2} for <a href=""wbword://{0}"">{1}</a>)</span><br />", word.ParentBranch.SourceId, word.ParentBranch.Source.Word, word.ParentBranch.Name)
        End If

        sb.AppendFormat("</dt><dd id=""definition_{1}""><span class=""meaning"">{0}</span>", word.Meaning, word.Id)

        For Each m As DictionaryData.Mark In word.Marks
            sb.AppendFormat("<div class=""mark""><span class=""markname"">{0}</span>: <span class=""markvalue"">{1}</span></div>", m.Name, m.Value)
        Next

        For Each b As DictionaryData.Branch In word.Branches
            sb.AppendFormat("<div class=""branch""><span class=""branchname"">{0}</span>: <span class=""branchword""><a href=""wbword://{1}"">{2}</a></span></div>", b.Name, b.TargetId, b.Target.Word)
        Next

        For Each r As DictionaryData.Relation In word.Relations
            sb.AppendFormat("<div class=""relation""><span class=""relationname"">{0}</span>: <span class=""relatedword"" id=""link_{3}""><a href=""wbword://{1}"">{2}</a></span></div>", r.Name, r.TargetId, r.Target.Word, r.Id)
        Next

        sb.Append("</dd>")
    End Sub

    Private Shared Sub RenderWordList(ByVal words As IEnumerable(Of DictionaryData.Word), ByVal sb As System.Text.StringBuilder)
        For Each w As DictionaryData.Word In words
            RenderWord(w, sb)
        Next
    End Sub

    Private Shared Sub RenderResultsStyle(ByVal sb As System.Text.StringBuilder, ByVal font As WBFont)
        sb.AppendLine()
        sb.AppendLine("<style type=""text/css"">")
        sb.AppendLine("body {")
        sb.AppendLine("  font-family: " & font.Name & ";")
        sb.AppendLine("  font-size: " & font.Size.ToString(System.Globalization.CultureInfo.InvariantCulture) & ";")
        sb.AppendLine("}")
        sb.AppendLine("a {")
        sb.AppendLine("  color: #ff0000;")
        sb.AppendLine("}")
        sb.AppendLine("a:hover {")
        sb.AppendLine("  color: #ff7f00;")
        sb.AppendLine("}")
        sb.AppendLine(".tools {")
        sb.AppendLine("  float: right;")
        sb.AppendLine("  font-size: 8pt;")
        sb.AppendLine("}")
        sb.AppendLine(".word {")
        sb.AppendLine("  font-weight: bold;")
        sb.AppendLine("}")
        sb.AppendLine(".meaning {")
        sb.AppendLine("  font-style: italic;")
        sb.AppendLine("}")
        sb.AppendLine(".parent {")
        sb.AppendLine("  font-size: 8pt;")
        sb.AppendLine("}")
        sb.AppendLine(".mark {")
        sb.AppendLine("  font-size: 8pt;")
        sb.AppendLine("}")
        sb.AppendLine(".branch {")
        sb.AppendLine("  font-size: 8pt;")
        sb.AppendLine("}")
        sb.AppendLine(".relation {")
        sb.AppendLine("  font-size: 8pt;")
        sb.AppendLine("}")

        sb.AppendLine("</style>")
    End Sub

    Private Shared Sub RenderResultsHeader(ByVal sb As System.Text.StringBuilder)
        sb.Append("<html><head>")

        RenderResultsStyle(sb, Font)

        sb.Append("</head><body><dl>")
    End Sub

    Private Shared Sub RenderResultsFooter(ByVal sb As System.Text.StringBuilder)
        sb.Append("</dl></body></html>")
    End Sub

    Public Shared Function RenderResultsPage(ByVal words As IEnumerable(Of DictionaryData.Word)) As String
        If words.Count > 0 Then
            Dim sb As New System.Text.StringBuilder()

            RenderResultsHeader(sb)

            RenderWordList(words, sb)

            RenderResultsFooter(sb)

            Return sb.ToString()
        Else
            Return RenderNoResultsPage()
        End If
    End Function

    Public Shared Function RenderResultPage(ByVal word As DictionaryData.Word) As String
        Dim sb As New System.Text.StringBuilder()

        RenderResultsHeader(sb)

        RenderWord(word, sb)

        RenderResultsFooter(sb)

        Return sb.ToString()
    End Function

    Public Shared Function RenderNoResultsPage() As String
        Dim sb As New System.Text.StringBuilder()
        RenderResultsHeader(sb)

        sb.AppendLine("<p>No matches</p>")

        RenderResultsFooter(sb)
        Return sb.ToString()
    End Function
End Class
