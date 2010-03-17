Imports DictionaryProject
Imports WheeControls
Imports System.Reflection

Partial Public Class Main

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

                        If p.Warnings.Count > 0 Then
                            For Each warning As String In p.Warnings
                                WarningsListBox.Items.Add(warning)
                            Next
                            WarningsPanel.Visible = True
                            WarningsSplitter.Visible = True
                            Exit For
                        End If
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

#Region "Actions"
    Private Sub ResultsListBox_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ResultsListBox.MouseDoubleClick
        EditSelectedWord()
    End Sub

    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        EditSelectedWord()
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

    Private Sub RulesComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RulesComboBox.SelectedIndexChanged
        If Not _populating Then
            Dim r As Rule = TryCast(RulesComboBox.SelectedItem, Rule)

            If r IsNot Nothing Then
                CodeTextBox.SelectionStart = CodeTextBox.GetFirstCharIndexFromLine(r.LineNumber - 1)
                CodeTextBox.Focus()
            End If
        End If
    End Sub

    Private FindInCodeDialog As New FindInCodeDialog()
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

        Dim startIndex As Integer = CodeTextBox.SelectionStart + CodeTextBox.SelectionLength
        Dim endIndex As Integer = -1

        Dim findOptions As RichTextBoxFinds = RichTextBoxFinds.None

        If FindInCodeDialog.SearchUpCheckBox.Checked Then
            findOptions = RichTextBoxFinds.Reverse
            endIndex = CodeTextBox.SelectionStart
            startIndex = 0
        End If

        If FindInCodeDialog.WholeWordCheckBox.Checked Then
            findOptions = findOptions Or RichTextBoxFinds.WholeWord
        End If

        If CodeTextBox.Find(FindInCodeDialog.FindTextBox.Text, startIndex, endIndex, findOptions) = -1 Then
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
#End Region

End Class
