Imports WheeControls
Imports DictionaryData
Imports DictionaryProject

Partial Public Class Main
#Region "Dictionary"
    Private Sub DoSearch()
        If MeaningTextBox.Text.StartsWith("word:") Then
            Dim id As Integer
            If Integer.TryParse(MeaningTextBox.Text.Split(":"c).Last, id) Then
                Dim wd As DictionaryData.Word = DictionaryData.Word.LoadSingle(id)

                If wd IsNot Nothing Then
                    DictionaryResultsWebBrowser.DocumentText = DictionaryPresenter.RenderResultPage(wd)
                Else
                    DictionaryResultsWebBrowser.DocumentText = DictionaryPresenter.RenderNoResultsPage()
                End If
            End If
        Else
            Dim so As DictionaryData.Word.SearchOptions
            Dim ids As New List(Of Integer)()

            If DictionarySearchWordCheckBox.Checked Then
                so = so Or DictionaryData.Word.SearchOptions.IncludeWord
            End If

            If DictionarySearchMeaningCheckBox.Checked Then
                so = so Or DictionaryData.Word.SearchOptions.IncludeMeaning
            End If

            Select Case DictionarySearchOperationComboBox.SelectedIndex
                Case 1
                    so = so Or DictionaryData.Word.SearchOptions.WildcardEnd
                Case 2
                    so = so Or DictionaryData.Word.SearchOptions.WildcardStart
                Case 3
                    so = so Or DictionaryData.Word.SearchOptions.WildcardStart Or DictionaryData.Word.SearchOptions.WildcardEnd
            End Select

            If DictionaryMarksCheckBox.Checked AndAlso DictionarySearchMarksCheckedListBox.CheckedItems.Count > 0 Then
                so = so Or DictionaryData.Word.SearchOptions.IncludeMarks
                For Each mt As DictionaryData.MarkType In DictionarySearchMarksCheckedListBox.CheckedItems
                    ids.Add(mt.Id)
                Next
            End If

            DictionaryResultsWebBrowser.DocumentText = DictionaryPresenter.RenderResultsPage(DictionaryData.Word.Search(MeaningTextBox.Text, ids.ToArray(), so))
        End If
    End Sub

    Private Sub MeaningTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MeaningTextBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            DoSearch()
        End If
    End Sub

    Private Sub DictionaryLookupButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DictionaryLookupButton.Click
        DoSearch()
    End Sub

    Private Sub DictionaryResultsWebBrowser_Navigating(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles DictionaryResultsWebBrowser.Navigating
        If e.Url.Scheme = "wbword" Then
            Dim id As Integer = 0

            If Integer.TryParse(e.Url.Host, id) Then
                ShowWordInDictionary(id)
            End If
            e.Cancel = True
        End If
    End Sub

    Private Function CreateContextFromWord(ByVal word As DictionaryData.Word) As Context
        Dim c As New Context

        c.Tokens.AddRange(word.Tokens)

        For Each b As DictionaryData.Branch In word.Branches
            c.Branches.Add(b.Name, CreateContextFromWord(b.Target))
        Next

        For Each m As DictionaryData.Mark In word.Marks
            c.Mark(m.Name, m.Value)
        Next

        Return c
    End Function

    Private Sub UpdateWordFromContext(ByVal word As DictionaryData.Word, ByVal ctx As Context)
        word.Tokens = ctx.Tokens.ToArray()
        word.Word = ctx.ToString()
        word.Save()

        Dim removeMarks As New List(Of DictionaryData.Mark)(word.Marks)
        For Each m As String In ctx.Marks.Keys
            Dim markName As String = m
            If removeMarks.RemoveAll(Function(rm As DictionaryData.Mark) rm.Name = markName) = 0 Then
                Dim newMark As New DictionaryData.Mark()
                newMark.Name = m
                newMark.Value = ctx.Marks(m)
                newMark.WordId = word.Id
                newMark.Save()
            End If
        Next

        For Each mark As DictionaryData.Mark In removeMarks
            mark.Delete()
        Next

        Dim removeBranches As New List(Of DictionaryData.Branch)(word.Branches)
        For Each b As String In ctx.Branches.Keys
            Dim branchName As String = b
            Dim branchMatch As DictionaryData.Branch = removeBranches.Where(Function(rb As DictionaryData.Branch) rb.Name = branchName).FirstOrDefault
            If branchMatch Is Nothing Then
                Dim newBranch As New DictionaryData.Branch()
                newBranch.Name = b
                newBranch.SourceId = word.Id
                newBranch.TargetId = CreateWordFromContext(ctx.Branches(b), "").Id
                newBranch.Save()
            Else
                UpdateWordFromContext(branchMatch.Target, ctx.Branches(b))
                removeBranches.Remove(branchMatch)
            End If
        Next

        For Each branch As DictionaryData.Branch In removeBranches
            branch.Delete()
        Next
    End Sub

    Private Function CreateWordFromContext(ByVal ctx As Context, ByVal meaning As String, Optional ByVal InheritedMeaning As Boolean = False) As DictionaryData.Word
        Dim w As New DictionaryData.Word()
        w.Word = ctx.ToString()
        w.Tokens = ctx.Tokens.ToArray()

        If ctx.Marks.ContainsKey("AutoMeaning") Then
            w.Meaning = ctx.Marks("AutoMeaning").Replace("~", meaning)
        ElseIf InheritedMeaning Then
            w.Meaning = ""
        Else
            w.Meaning = meaning
        End If

        w.Save()

        For Each bk As String In ctx.Branches.Keys
            Dim b As New DictionaryData.Branch()
            b.Name = bk
            b.SourceId = w.Id
            b.TargetId = CreateWordFromContext(ctx.Branches(bk), meaning, True).Id
            b.Save()
        Next

        For Each mk As String In ctx.Marks.Keys
            If mk <> "AutoMeaning" Then
                Dim m As New DictionaryData.Mark()
                m.Name = mk
                m.Value = ctx.Marks(mk)
                m.WordId = w.Id
                m.Save()
            End If
        Next

        Return w
    End Function

    Private AssignMeaningDialog1 As New AssignMeaningDialog()

    Private Sub AddToDictionaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToDictionaryToolStripMenuItem.Click
        If CheckDictionary() Then
            If ResultsListBox.SelectedItems.Count = 1 Then
                Dim ctx As ContextListViewItem = ResultsListBox.SelectedItems(0)

                AssignMeaningDialog1.Context = ctx.Context
                If AssignMeaningDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    ShowWordInDictionary(CreateWordFromContext(ctx.Context, AssignMeaningDialog1.Meaning).Id)
                End If
            End If
        End If
    End Sub

    Private Sub ShowWordInDictionary(ByVal id As Integer)
        MeaningTextBox.Text = "word:" & id.ToString()
        DoSearch()
        ProjectTabControl.SelectedIndex = 1
    End Sub

    Private Sub DictionaryResultsWebBrowser_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles DictionaryResultsWebBrowser.DocumentCompleted

    End Sub

    Private Sub NewDictionaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewDictionaryToolStripMenuItem.Click
        CreateDictionary()
    End Sub

    Private Sub OpenDictionaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenDictionaryToolStripMenuItem.Click
        OpenDictionary()
    End Sub

    Private Function CreateDictionary() As Boolean
        If SaveFileDialog2.ShowDialog() = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(IO.Path.Combine(IO.Path.GetDirectoryName(Application.ExecutablePath), "BaseDictionary.sdf"), SaveFileDialog2.FileName)
            DictionaryData.DatabaseHelper.ConnectionString = "DataSource=" & SaveFileDialog2.FileName
            DictionaryResultsWebBrowser.DocumentText = ""
            DictionaryFileName = IO.Path.GetFileName(SaveFileDialog2.FileName)
            UpdateTitle()
            Return True
        End If

        Return False
    End Function

    Private Function OpenDictionary() As Boolean
        If OpenFileDialog2.ShowDialog() = Windows.Forms.DialogResult.OK Then
            DictionaryData.DatabaseHelper.ConnectionString = "DataSource=" & OpenFileDialog2.FileName
            DictionaryResultsWebBrowser.DocumentText = ""
            DictionaryFileName = IO.Path.GetFileName(OpenFileDialog2.FileName)
            RebindMarkTypesList()
            UpdateTitle()
            Return True
        End If

        Return False
    End Function

    Private Sub RebindMarkTypesList()
        DictionarySearchMarksCheckedListBox.Items.Clear()
        DictionarySearchMarksCheckedListBox.Items.AddRange(DictionaryData.MarkType.LoadList().ToArray())
    End Sub

    Private Function CheckDictionary() As Boolean
        If String.IsNullOrEmpty(DictionaryData.DatabaseHelper.ConnectionString) Then
            Dim pdd As New PickDictionaryDialog()
            Select Case pdd.ShowDialog()
                Case Windows.Forms.DialogResult.Yes
                    Return OpenDictionary()
                Case Windows.Forms.DialogResult.No
                    Return CreateDictionary()
            End Select
            Return False
        End If

        Return True
    End Function

    Private Sub DictionaryTabPage_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DictionaryTabPage.Enter
        If Not CheckDictionary() Then
            ProjectTabControl.SelectedIndex = 0
        End If
    End Sub

    Private Sub EditWordToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditWordToolStripMenuItem1.Click
        Dim word As DictionaryData.Word = DictionaryData.Word.LoadSingle(_DictionaryActiveWordId)

        If word IsNot Nothing Then
            Dim ctx As Context = CreateContextFromWord(word)

            Dim result As Context = EditContext(ctx)

            If result IsNot Nothing Then
                UpdateWordFromContext(word, result)
                ShowWordInDictionary(word.Id)
            End If
        End If
    End Sub

    Private Sub LinkToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkToolStripMenuItem.Click
        Dim word As DictionaryData.Word = DictionaryData.Word.LoadSingle(_DictionaryActiveWordId)

        If word IsNot Nothing Then
            Dim dld As New DictionaryLinkDialog()
            dld.SearchTextBox.Text = word.Word
            If dld.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim relType As DictionaryData.RelationType = DictionaryData.RelationType.LoadSingle(dld.RelationTypeComboBox.SelectedValue)
                Dim inverseType As DictionaryData.RelationType = relType.InverseType

                If relType IsNot Nothing Then
                    For Each wid As Integer In dld.GetSelectedWordIds()
                        Dim rel As New DictionaryData.Relation()
                        rel.SourceId = word.Id
                        rel.TargetId = wid
                        rel.RelationTypeId = relType.Id
                        rel.Save()
                    Next
                End If
                DoSearch()
            End If
        End If
    End Sub

    Private Sub DeleteLinkToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteLinkToolStripMenuItem.Click
        If _DictionaryActiveLinkId <> 0 Then
            Dim r As DictionaryData.Relation = DictionaryData.Relation.LoadSingle(_DictionaryActiveLinkId)
            If MessageBox.Show("Are you sure you want to delete this relation?", "Delete relation: " & r.Source.Word & " -> " & r.Target.Word, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                r.Delete()
                DoSearch()
            End If
        End If
    End Sub

    Private Sub DeleteWordToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteWordToolStripMenuItem.Click
        If _DictionaryActiveWordId <> 0 Then
            Dim w As DictionaryData.Word = DictionaryData.Word.LoadSingle(_DictionaryActiveWordId)
            If MessageBox.Show("Are you sure you want to delete this word and all its branches, marks and relations?", "Delete word: " & w.Word, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                w.Delete()
                DoSearch()
            End If
        End If
    End Sub

    Private _DictionaryActiveWordId As Integer
    Private _DictionaryActiveLinkId As Integer

    Private Sub DictionaryResultsContextMenuStrip_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DictionaryResultsContextMenuStrip.Opening
        Dim el As HtmlElement = DictionaryResultsWebBrowser.Document.GetElementFromPoint(DictionaryResultsWebBrowser.PointToClient(Form.MousePosition))

        While el IsNot Nothing AndAlso String.IsNullOrEmpty(el.Id)
            el = el.Parent
        End While

        If el IsNot Nothing Then
            Dim parts As String() = el.Id.Split("_"c)
            Dim id As Integer
            If Integer.TryParse(parts.Last, id) Then
                Select Case parts.First
                    Case "term"
                        _DictionaryActiveWordId = id
                        _DictionaryActiveLinkId = 0
                        EditWordToolStripMenuItem1.Visible = True
                        LinkToolStripMenuItem.Visible = True
                        DictionarySeparatorToolStripMenuItem.Visible = True
                        DeleteWordToolStripMenuItem.Visible = True
                        DeleteLinkToolStripMenuItem.Visible = False
                        DictionaryResultEditMeaningToolStripMenuItem.Visible = True
                    Case "link"
                        _DictionaryActiveWordId = 0
                        _DictionaryActiveLinkId = id
                        EditWordToolStripMenuItem1.Visible = False
                        LinkToolStripMenuItem.Visible = False
                        DictionarySeparatorToolStripMenuItem.Visible = False
                        DeleteWordToolStripMenuItem.Visible = False
                        DeleteLinkToolStripMenuItem.Visible = True
                        DictionaryResultEditMeaningToolStripMenuItem.Visible = False
                End Select
            End If
        End If
    End Sub

    Private Sub DictionaryResultEditMeaningToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DictionaryResultEditMeaningToolStripMenuItem.Click
        If _DictionaryActiveWordId <> 0 Then
            Dim w As DictionaryData.Word = DictionaryData.Word.LoadSingle(_DictionaryActiveWordId)

            AssignMeaningDialog1.Context = CreateContextFromWord(w)
            AssignMeaningDialog1.Meaning = w.Meaning
            If AssignMeaningDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                w.Meaning = AssignMeaningDialog1.Meaning
                w.Save()
                ShowWordInDictionary(w.Id)
            End If
        End If
    End Sub

    Private Sub DictionaryMarksCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DictionaryMarksCheckBox.CheckedChanged
        DictionarySearchMarksCheckedListBox.Enabled = DictionaryMarksCheckBox.Checked
    End Sub
#End Region

    Private Sub AddAnotherMeaningToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddAnotherMeaningToolStripMenuItem.Click
        Dim word As DictionaryData.Word = DictionaryData.Word.LoadSingle(_DictionaryActiveWordId)

        If word IsNot Nothing Then
            Dim ctx As Context = CreateContextFromWord(word)

            AssignMeaningDialog1.Context = ctx
            If AssignMeaningDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                ShowWordInDictionary(CreateWordFromContext(ctx, AssignMeaningDialog1.Meaning).Id)
            End If
        End If
    End Sub

    Private Sub StatisticsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatisticsToolStripMenuItem.Click
        If CheckDictionary() Then
            Dim statsDlg As New DictionaryStatisticsDialog()

            statsDlg.ShowDialog()
        End If
    End Sub
End Class
