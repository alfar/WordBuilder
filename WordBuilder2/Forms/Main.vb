Imports System.Reflection
Imports DictionaryProject

Public Class Main
#Region "Config"
    Private Sub LoadConfig()
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "config.xml")
        If IO.File.Exists(path) Then
            Dim xs As New Xml.Serialization.XmlSerializer(GetType(WBConfiguration))

            Using sr As New IO.StreamReader(path)
                _configuration = TryCast(xs.Deserialize(sr), WBConfiguration)

                CodeTextBox.CodeFont = _configuration.GetCodeFont()
                DictionaryPresenter.Font = _configuration.DictionaryFont
            End Using
        End If
    End Sub

    Private _configuration As New WBConfiguration()

    Private Sub SaveConfig()
        Dim xs As New Xml.Serialization.XmlSerializer(GetType(WBConfiguration))

        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "config.xml")

        xs.Serialize(New IO.StreamWriter(path), _configuration)
    End Sub
#End Region

#Region "Filename"
    Private _dirty As Boolean = False
    Private Property Dirty() As Boolean
        Get
            Return _dirty
        End Get
        Set(ByVal value As Boolean)
            _dirty = value

            UpdateTitle()
        End Set
    End Property

    Private _DictionaryFileName As String = Nothing
    Public Property DictionaryFileName() As String
        Get
            Return _DictionaryFileName
        End Get
        Set(ByVal value As String)
            _DictionaryFileName = value
        End Set
    End Property

    Private _FileName As String = Nothing
    Private Property FileName() As String
        Get
            Return _FileName
        End Get
        Set(ByVal value As String)
            _FileName = value
            UpdateTitle()
        End Set
    End Property

    Private Sub UpdateTitle()
        Me.Text = String.Format("WordBuilder: {0}{1} - {2}", If(FileName, "[New project]"), If(Dirty, "*", ""), If(DictionaryFileName, "[No dictionary]"))
    End Sub
#End Region

#Region "Closing"
    Private Sub WordBuilderForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason <> CloseReason.WindowsShutDown AndAlso CheckSave() = Windows.Forms.DialogResult.Cancel Then
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "Load"
    Private Sub WordBuilderForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadConfig()

        DictionarySearchOperationComboBox.SelectedIndex = 1

        UpdateTitle()

        For Each asm As Assembly In AppDomain.CurrentDomain.GetAssemblies()
            If asm.GetName().Name = "DictionaryProject" Then
                For Each t As Type In asm.GetTypes()
                    For Each ha As HighlightAttribute In t.GetCustomAttributes(GetType(HighlightAttribute), True)
                        CodeTextBox.AddKeyword(ha.Level, ha.RegEx, Color.FromName(ha.Color), ha.NextLevel)
                    Next
                Next
                For Each t As Type In asm.GetTypes()
                    For Each ha As CloneHighlightingAttribute In t.GetCustomAttributes(GetType(CloneHighlightingAttribute), True)
                        CodeTextBox.CloneKeywords(ha.SourceLevel, ha.DestinationLevel)
                    Next
                Next
            End If
        Next
    End Sub
#End Region

#Region "Menus"
#Region "File"
    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click
        If CheckSave() <> Windows.Forms.DialogResult.Cancel Then
            FileName = Nothing
            CodeTextBox.Text = ""
            Dirty = False
        End If
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        If CheckSave() <> Windows.Forms.DialogResult.Cancel Then
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Using sr As New IO.StreamReader(OpenFileDialog1.OpenFile(), System.Text.Encoding.UTF8)
                    CodeTextBox.Text = sr.ReadToEnd()
                End Using
                FileName = OpenFileDialog1.FileName
                Dirty = False
            End If
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        SaveProject()
    End Sub

    Private Sub SaveasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveasToolStripMenuItem.Click
        SaveProjectAs(Nothing)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
#End Region
#Region "Word"
    Private Sub WordsToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles WordsToolStripMenuItem.DropDownOpening
        EditwordToolStripMenuItem.Enabled = ResultsListBox.SelectedItems.Count = 1
    End Sub

    Private Sub GenerateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GenerateToolStripMenuItem.Click
        GenerateWords(False, False)
    End Sub

    Private Sub ClearListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearListToolStripMenuItem.Click
        ClearOutput()
    End Sub

    Private Sub QuickGenerateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuickGenerateToolStripMenuItem.Click
        GenerateWords(True, False)
    End Sub

    Private Sub WarningsListBox_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles WarningsListBox.MouseDoubleClick
        If WarningsListBox.SelectedItem IsNot Nothing Then
            Dim warning As String = WarningsListBox.SelectedItem.ToString()

            If warning.StartsWith("Line ") Then
                Dim ln As Integer
                If Integer.TryParse(warning.Substring(5, warning.IndexOf(":"c) - 5), ln) Then
                    Dim starting As Integer = CodeTextBox.GetFirstCharIndexFromLine(ln - 1)
                    CodeTextBox.Select()
                    CodeTextBox.Select(starting, 0)
                    CodeTextBox.ScrollToCaret()
                End If
            End If
        End If
    End Sub

    Private Sub QuickClearAndGenerateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuickClearAndGenerateToolStripMenuItem.Click
        GenerateWords(True, True)
    End Sub

    Private Sub CopyListToClipboardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyListToClipboardToolStripMenuItem.Click
        CopyListToClipboard()
    End Sub

    Private Sub CopyDetailsToClipboardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyDetailsToClipboardToolStripMenuItem.Click
        CopyListDetailsToClipboard()
    End Sub

    Private Sub ExportListToFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportListToFileToolStripMenuItem.Click
        Dim sb As New List(Of String)()

        For Each eb As ExporterBase In ExporterBase.Exporters
            sb.Add(eb.Type)
        Next

        SaveFileDialog2.Filter = String.Join("|", sb.ToArray())

        Dim items As IEnumerable = ResultsListBox.Items

        If ResultsListBox.SelectedItems.Count > 0 Then
            items = ResultsListBox.SelectedItems
        End If

        Dim contexts As New List(Of Context)()

        For Each c As ContextListViewItem In items
            contexts.Add(c.Context)
        Next

        If SaveFileDialog2.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                ExporterBase.Exporters(SaveFileDialog2.FilterIndex - 1).Export(contexts, SaveFileDialog2.FileName)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "An error occurred during export.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub EditwordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditwordToolStripMenuItem.Click
        EditSelectedWord()
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        For Each c As ContextListViewItem In ResultsListBox.Items
            c.Selected = True
        Next
    End Sub
#End Region
#Region "Help"
    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim ab As New AboutBox()

        ab.ShowDialog()
    End Sub
#End Region
#End Region

#Region "Utilities"
    Private Sub ClearOutput()
        ResultsTextBox.Clear()
        ResultsListBox.Items.Clear()
    End Sub

    Private _populating As Boolean = False

    Private Sub PopulateRulesList(ByVal p As Project)
        _populating = True
        RulesComboBox.DataSource = p.Rules
        _populating = False
    End Sub

    Private Sub GenerateWords(ByVal quick As Boolean, ByVal clear As Boolean)
        Dim p As Project = GetProject()

        If p IsNot Nothing Then
            PopulateRulesList(p)

            Dim dlg As New GenerateForm(p)

            If quick OrElse dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If dlg.ClearListCheckBox.Checked OrElse clear Then
                    ClearOutput()
                End If
                ResultsListBox.Columns.Clear()
                ResultsListBox.Columns.Add("Word")
                For Each col As String In p.Columns.Keys
                    ResultsListBox.Columns.Add(col)
                Next
                For Each sr As String In p.StartRules.Keys
                    For i As Integer = 1 To p.StartRules(sr)
                        Dim c As Context = p.GetWord(sr)
                        ResultsListBox.Items.Add(New ContextListViewItem(p, c))
                    Next
                Next

                For Each column As ColumnHeader In ResultsListBox.Columns
                    column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
                Next
            End If
        End If
    End Sub

    Public Function GetProject() As Project
        Dim p As Project = ProjectSerializer.LoadProjectString(CodeTextBox.Text)

        WarningsListBox.Items.Clear()
        If p.Warnings.Count > 0 Then
            For Each warning As String In p.Warnings
                WarningsListBox.Items.Add(warning)
            Next
            WarningsPanel.Visible = True
            WarningsSplitter.Visible = True

            Return Nothing
        Else
            WarningsPanel.Visible = False
            WarningsSplitter.Visible = False
        End If

        Return p
    End Function

    Private Sub CopyListToClipboard()
        Dim list As New System.Text.StringBuilder()

        Dim items As IEnumerable = ResultsListBox.Items

        If ResultsListBox.SelectedItems.Count > 0 Then
            items = ResultsListBox.SelectedItems
        End If

        For Each c As ContextListViewItem In items
            list.AppendLine(c.Context.ToString())
        Next

        Windows.Forms.Clipboard.SetText(list.ToString())
    End Sub

    Private Function GenerateListDetails() As String
        Dim list As New System.Text.StringBuilder()

        Dim items As IEnumerable = ResultsListBox.Items

        If ResultsListBox.SelectedItems.Count > 0 Then
            items = ResultsListBox.SelectedItems
        End If

        For Each c As ContextListViewItem In items
            list.AppendLine(c.Context.Description())
            list.AppendLine()
        Next

        Return list.ToString()
    End Function

    Private Sub CopyListDetailsToClipboard()
        Windows.Forms.Clipboard.SetText(GenerateListDetails())
    End Sub

    Private Function EditContext(ByVal ctx As Context) As Context
        Dim proj As Project = GetProject()

        Dim ewf As New ContextEditorForm(ctx, proj)

        If ewf.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Return ewf.GetContext()
        End If

        Return Nothing
    End Function

    Private Sub EditSelectedWord()
        If ResultsListBox.SelectedItems.Count = 1 Then
            Dim ctx As ContextListViewItem = ResultsListBox.SelectedItems(0)

            Dim result As Context = EditContext(ctx.Context)

            If result IsNot Nothing Then
                ResultsListBox.Items.Insert(ResultsListBox.SelectedIndices(0), New ContextListViewItem(GetProject(), result) With {.Selected = True})
                ResultsListBox.Items.RemoveAt(ResultsListBox.SelectedIndices(0) + 1)
            End If
        End If
    End Sub
#End Region

#Region "Save"
    Private Function CheckSave() As DialogResult
        If Dirty Then
            Dim result As DialogResult = MessageBox.Show("Do you want to save before closing this project?", "Closing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)

            If result = Windows.Forms.DialogResult.Yes Then
                SaveProject()
            End If

            Return result
        End If

        Return DialogResult.No
    End Function

    Private Sub SaveProjectAs(ByVal path As String)
        If String.IsNullOrEmpty(path) AndAlso SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            path = SaveFileDialog1.FileName
        End If

        If Not String.IsNullOrEmpty(path) Then
            Using sw As New IO.StreamWriter(path, False, System.Text.Encoding.UTF8)
                sw.Write(CodeTextBox.Text)
            End Using
            FileName = path
            Dirty = False
        End If
    End Sub

    Private Sub SaveProject()
        SaveProjectAs(FileName)
    End Sub
#End Region

#Region "Actions"
    Private Sub ResultsListBox_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ResultsListBox.MouseDoubleClick
        EditSelectedWord()
    End Sub

    Private Sub ResultsListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResultsListBox.SelectedIndexChanged
        If ResultsListBox.SelectedItems.Count > 0 Then
            ResultsTextBox.Text = GenerateListDetails()
        Else
            ResultsTextBox.Clear()
        End If
    End Sub

    Private Sub ResultsListBox_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ResultsListBox.KeyDown
        If e.Control Then
            e.SuppressKeyPress = True
            Select Case e.KeyCode
                Case Keys.A
                    For Each c As ContextListViewItem In ResultsListBox.Items
                        c.Selected = True
                    Next
                Case Keys.C
                    If e.Shift Then
                        CopyListDetailsToClipboard()
                    Else
                        CopyListToClipboard()
                    End If
                Case Else
                    e.SuppressKeyPress = False
            End Select
        End If
    End Sub

    Private Sub CodeTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Control Then
            e.SuppressKeyPress = True
            Select Case e.KeyCode
                Case Keys.A
                    CodeTextBox.SelectAll()
                Case Else
                    e.SuppressKeyPress = False
            End Select
        End If
    End Sub

    Private Sub CodeTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dirty = True
    End Sub
#End Region

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

    Private Sub TranslateToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TranslateToolStripButton.Click
        Dim proj As Project = GetProject()

        Dim result As New List(Of String)()
        Dim tokens As List(Of String) = ProjectSerializer.ReadTokens(TranslatorSourceTextBox.Text.Trim())

        For Each token As String In tokens
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
        Next

        TranslatorResultWebBrowser.DocumentText = "<html><head><style type=""text/css"">body { font-family: " & _configuration.DictionaryFont.Name & "; font-size: " & _configuration.DictionaryFont.Size.ToString(System.Globalization.CultureInfo.InvariantCulture) & "; } .missing { font-style: italic; color: #ff0000; } .multimatch { color: #0000ff; } </style></head><body>" & String.Join(" ", result.ToArray()) & "</body></html>"
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
                AddWordToolStripMenuItem.Visible = False
                Exit While
            ElseIf If(el.GetAttribute("className"), "") = "missing" Then
                AddWordToolStripMenuItem.Visible = True
                AddWordToolStripMenuItem.Tag = el
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

    Private Sub FontsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FontsToolStripMenuItem.Click
        Dim ff As New FontsForm()
        ff.CodeFont = CodeTextBox.CodeFont
        ff.DictionaryFont = _configuration.GetDictionaryFont()
        If ff.ShowDialog() = Windows.Forms.DialogResult.OK Then
            CodeTextBox.CodeFont = ff.CodeFont
            _configuration.CodeFont = New WBFont() With {.Name = ff.CodeFont.Name, .Size = ff.CodeFont.Size}
            _configuration.DictionaryFont = New WBFont() With {.Name = ff.DictionaryFont.Name, .Size = ff.DictionaryFont.Size}
            DictionaryPresenter.Font = _configuration.DictionaryFont
            SaveConfig()
            If Not String.IsNullOrEmpty(DictionaryData.DatabaseHelper.ConnectionString) Then
                DoSearch()
            End If
        End If
    End Sub

    Private Sub AddToDictionaryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToDictionaryToolStripMenuItem1.Click
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

    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        EditSelectedWord()
    End Sub

    Private Sub RulesComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RulesComboBox.SelectedIndexChanged
        If Not _populating Then
            Dim r As Rule = TryCast(RulesComboBox.SelectedItem, Rule)

            If r IsNot Nothing Then
                CodeTextBox.SelectionStart = CodeTextBox.GetFirstCharIndexFromLine(r.LineNumber - 1)
                CodeTextBox.Focus()
            End If
        End If
    End Sub

    Private Sub CodeTextBox_Highlighted(ByVal sender As Object, ByVal e As System.EventArgs) Handles CodeTextBox.Highlighted
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
        CodeTextBox.Cut()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        CodeTextBox.Copy()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        CodeTextBox.Paste(DataFormats.GetFormat(DataFormats.UnicodeText))
    End Sub

    Private Sub FindInCode(ByVal checkempty As Boolean)
        If String.IsNullOrEmpty(FindInCodeDialog.FindTextBox.Text) Then
            If checkempty Then
                If FindInCodeDialog.ShowDialog() = Windows.Forms.DialogResult.Cancel OrElse String.IsNullOrEmpty(FindInCodeDialog.FindTextBox.Text) Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        End If

        Dim findOptions As RichTextBoxFinds = RichTextBoxFinds.None

        If FindInCodeDialog.SearchUpCheckBox.Checked Then
            findOptions = RichTextBoxFinds.Reverse
        End If

        If FindInCodeDialog.WholeWordCheckBox.Checked Then
            findOptions = findOptions Or RichTextBoxFinds.WholeWord
        End If

        If CodeTextBox.Find(FindInCodeDialog.FindTextBox.Text, CodeTextBox.SelectionStart + CodeTextBox.SelectionLength, findOptions) = -1 Then
            MessageBox.Show("No further matches found.", "Find in code", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub FindToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindToolStripMenuItem.Click
        If FindInCodeDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            FindInCode(False)
        End If
    End Sub

    Private Sub FindAgainToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindAgainToolStripMenuItem.Click
        FindInCode(True)
    End Sub
End Class