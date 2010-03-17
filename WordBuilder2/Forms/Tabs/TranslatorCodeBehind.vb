Imports System.Reflection
Imports DictionaryProject
Imports WheeControls

Partial Public Class Main

    Private Sub TranslateToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TranslateToolStripButton.Click
        If Not String.IsNullOrEmpty(TranslatorSourceTextBox.Text) Then
            Dim proj As Project = GetProject()

            Dim result As New List(Of String)()
            Dim tokens As List(Of String) = ProjectSerializer.ReadTokens(TranslatorSourceTextBox.Text.Trim().Replace(vbCrLf, " " & vbCrLf & " "))

            For Each token As String In tokens
                If token = vbCrLf Then
                    result.Add("</p><p>")
                Else
                    Dim parts() As String = token.Split(New Char() {"."c}, 2)

                    Dim word As String = parts(0)

                    Dim matches As DictionaryData.WordCollection = FindCandidates(DictionaryData.Word.Search(word, New Integer() {}, DictionaryData.Word.SearchOptions.IncludeMeaning), If(parts.Length = 2, parts(1), ""), proj)
                    If matches.Count > 0 Then
                        Dim bestWord As DictionaryData.Word = matches.First()
                        If matches.Count > 1 Then
                            If parts.Length = 2 Then
                                result.Add("<span class=""multimatch"" wordfor=""" & word & """ path=""" & parts(1) & """ title=""" & matches.Count.ToString() & " match(es)"">" & BrowseWord(bestWord, parts(1), proj) & "</span>")
                            Else
                                result.Add("<span class=""multimatch"" wordfor=""" & word & """ title=""" & matches.Count.ToString() & " match(es)"">" & bestWord.Word & "</span>")
                            End If
                        Else
                            If parts.Length = 2 Then
                                result.Add("<span wordfor=""" & word & """>" & BrowseWord(bestWord, parts(1), proj) & "</span>")
                            Else
                                result.Add("<span wordfor=""" & word & """>" & bestWord.Word & "</span>")
                            End If
                        End If
                    Else
                        result.Add("<span class=""missing"">" & word & "</span>")
                    End If
                End If
            Next

            TranslatorResultWebBrowser.DocumentText = "<html><head><style type=""text/css"">body { font-family: " & _configuration.DictionaryFont.Name & "; font-size: " & _configuration.DictionaryFont.Size.ToString(System.Globalization.CultureInfo.InvariantCulture) & "; } .missing { font-style: italic; color: #ff0000; } .multimatch { color: #0000ff; } </style></head><body><p>" & String.Join(" ", result.ToArray()) & "</p></body></html>"
        End If
    End Sub

    Private Sub TranslateBackToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TranslateBackToolStripButton.Click
        If Not String.IsNullOrEmpty(TranslatorSourceTextBox.Text) Then
            Dim proj As Project = GetProject()

            Dim result As New List(Of String)()
            Dim tokens As List(Of String) = ProjectSerializer.ReadTokens(TranslatorSourceTextBox.Text.Trim().Replace(vbCrLf, " " & vbCrLf & " "))

            For Each token As String In tokens
                If token = vbCrLf Then
                    result.Add("</p><p>")
                Else
                    Dim matches As DictionaryData.WordCollection = DictionaryData.Word.Search(token, New Integer() {}, DictionaryData.Word.SearchOptions.IncludeWord Or DictionaryData.Word.SearchOptions.RequireMeaning)

                    If matches.Count > 0 Then
                        Dim bestWord As DictionaryData.Word = matches.First()
                        If matches.Count > 1 Then
                            result.Add("<span class=""multimatch"" meaningfor=""" & token & """ title=""" & matches.Count.ToString() & " match(es)"">" & bestWord.Meaning & "</span>")
                        Else
                            result.Add("<span meaningfor=""" & token & """>" & bestWord.Meaning & "</span>")
                        End If
                    Else
                        result.Add("<span class=""missing"" type=""meaning"">" & token & "</span>")
                    End If
                End If
            Next

            TranslatorResultWebBrowser.DocumentText = "<html><head><style type=""text/css"">body { font-family: " & _configuration.DictionaryFont.Name & "; font-size: " & _configuration.DictionaryFont.Size.ToString(System.Globalization.CultureInfo.InvariantCulture) & "; } .missing { font-style: italic; color: #ff0000; } .multimatch { color: #0000ff; } </style></head><body><p>" & String.Join(" ", result.ToArray()) & "</p></body></html>"
        End If
    End Sub

    Private Function FindCandidates(ByVal words As DictionaryData.WordCollection, ByVal path As String, ByVal project As DictionaryProject.Project) As DictionaryData.WordCollection
        If String.IsNullOrEmpty(path) Then
            Return words
        End If

        Dim result As New DictionaryData.WordCollection

        If path.StartsWith("!") Then
            Dim mark As String = path.Substring(1)
            For Each wd As DictionaryData.Word In words.Where(Function(w As DictionaryData.Word) w.Marks.Any(Function(m As DictionaryData.Mark) m.Name = mark))
                result.Add(wd)
            Next
        ElseIf path.StartsWith("*") Then
            Return words
        Else
            Dim parts() = path.Split(New Char() {"."c}, 2)

            For Each word As DictionaryData.Word In words
                Dim br As DictionaryData.Branch = word.Branches.Where(Function(b As DictionaryData.Branch) b.Name = parts(0)).FirstOrDefault()
                If br IsNot Nothing Then
                    If parts.Length = 2 Then
                        Dim branchResult As String = BrowseWord(br.Target, parts(1), project)
                        If Not String.IsNullOrEmpty(branchResult) Then
                            result.Add(word)
                        End If
                    Else
                        result.Add(word)
                    End If
                End If
            Next
        End If

        Return result
    End Function

    Private Function BrowseWord(ByVal word As DictionaryData.Word, ByVal path As String, ByVal project As DictionaryProject.Project) As String
        If String.IsNullOrEmpty(path) Then
            Return word.Word
        End If

        Dim parts() = path.Split(New Char() {"."c}, 2)

        If parts(0).StartsWith("!") Then
            Dim mark As String = parts(0).Substring(1)
            Dim m As DictionaryData.Mark = word.Marks.Where(Function(ma As DictionaryData.Mark) ma.Name = mark).FirstOrDefault()
            If m IsNot Nothing Then
                Return m.Value
            End If
        ElseIf parts(0).StartsWith("*") Then
            Dim r As Rule = project.Rules.GetRuleByName(parts(0).Substring(1))

            Dim c As New Context()
            c.Tokens.AddRange(word.Tokens)
            r.Execute(c)

            If parts.Length > 1 Then
                Return c.GetColumn(parts(1), project)
            Else
                Return c.ToString()
            End If
        Else
            Dim br As DictionaryData.Branch = word.Branches.Where(Function(b As DictionaryData.Branch) b.Name = parts(0)).FirstOrDefault()
            If br IsNot Nothing Then
                If parts.Length > 1 Then
                    Return BrowseWord(br.Target, parts(1), project)
                Else
                    Return br.Target.Word
                End If
            End If
        End If

        Return ""
    End Function

    Private Sub TranslatorTabPage_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TranslatorTabPage.Enter
        If Not CheckDictionary() Then
            ProjectTabControl.SelectedIndex = 0
        End If
    End Sub

    Private Sub TranslatorResultWebBrowser_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles TranslatorResultWebBrowser.DocumentCompleted

    End Sub

    Private Sub TranslatorResultContextMenuStrip_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TranslatorResultContextMenuStrip.Opening
        Dim el As HtmlElement = TranslatorResultWebBrowser.Document.GetElementFromPoint(TranslatorResultWebBrowser.PointToClient(Form.MousePosition))

        While TranslatorResultContextMenuStrip.Items.Count > 1
            TranslatorResultContextMenuStrip.Items.RemoveAt(1)
        End While

        While el IsNot Nothing
            If If(el.GetAttribute("className"), "") = "multimatch" Then
                AddWordToolStripMenuItem.Visible = True

                Dim wordfor As String = el.GetAttribute("wordfor")

                If Not String.IsNullOrEmpty(wordfor) Then
                    For Each w As DictionaryData.Word In DictionaryData.Word.Search(el.GetAttribute("wordfor"), New Integer() {}, DictionaryData.Word.SearchOptions.IncludeMeaning)
                        Dim word As String
                        If Not String.IsNullOrEmpty(el.GetAttribute("path")) Then
                            word = BrowseWord(w, el.GetAttribute("path"), GetProject())
                        Else
                            word = w.Word
                        End If
                        Dim t As ToolStripItem = TranslatorResultContextMenuStrip.Items.Add(word, Nothing, AddressOf TranslatorChooseWord_Click)
                        t.Tag = el
                    Next
                Else
                    Dim meaningfor As String = el.GetAttribute("meaningfor")

                    If Not String.IsNullOrEmpty(meaningfor) Then
                        For Each w As DictionaryData.Word In DictionaryData.Word.Search(meaningfor, New Integer() {}, DictionaryData.Word.SearchOptions.IncludeWord Or DictionaryData.Word.SearchOptions.RequireMeaning)
                            Dim word As String
                            word = w.Meaning
                            Dim t As ToolStripItem = TranslatorResultContextMenuStrip.Items.Add(word, Nothing, AddressOf TranslatorChooseWord_Click)
                            t.Tag = el
                        Next
                    End If
                End If
                AddWordToolStripMenuItem.Visible = False
                Exit While
            ElseIf If(el.GetAttribute("className"), "") = "missing" Then
                AddWordToolStripMenuItem.Visible = False
                '                AddWordToolStripMenuItem.Tag = el
                Exit While
            End If
            el = el.Parent
        End While

        If el Is Nothing Then
            e.Cancel = True
        End If
    End Sub

    Private Sub TranslatorChooseWord_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim t As ToolStripItem = DirectCast(sender, ToolStripItem)

        Dim el As HtmlElement = DirectCast(t.Tag, HtmlElement)

        el.InnerText = t.Text
    End Sub

    Private Sub AddWordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddWordToolStripMenuItem.Click
        Dim el As HtmlElement = DirectCast(AddWordToolStripMenuItem.Tag, HtmlElement)

        Dim ctx As New Context()

        ctx = EditContext(ctx)

        If ctx IsNot Nothing Then
            Dim w As DictionaryData.Word = CreateWordFromContext(ctx, el.InnerText)

            w.Save()

            el.SetAttribute("className", "")
            el.InnerText = w.Word
        End If
    End Sub

End Class
