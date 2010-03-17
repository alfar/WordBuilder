Imports System.Reflection
Imports DictionaryProject

Partial Public Class Main
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

#Region "Closing"
    Private Sub WordBuilderForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason <> CloseReason.WindowsShutDown AndAlso CheckSave() = Windows.Forms.DialogResult.Cancel Then
            e.Cancel = True
        End If
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

    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
        Dim ctb As WheeControls.SyntaxRichTextBox = TryCast(Me.ActiveControl, WheeControls.SyntaxRichTextBox)

        If ctb IsNot Nothing Then
            ctb.Cut()
        Else
            Dim tb As TextBox = TryCast(Me.ActiveControl, TextBox)

            If tb IsNot Nothing Then
                tb.Cut()
            End If
        End If
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        Dim ctb As WheeControls.SyntaxRichTextBox = TryCast(Me.ActiveControl, WheeControls.SyntaxRichTextBox)

        If ctb IsNot Nothing Then
            ctb.Copy()
        Else
            Dim tb As TextBox = TryCast(Me.ActiveControl, TextBox)

            If tb IsNot Nothing Then
                tb.Copy()
            Else
                Dim b As WebBrowser = TryCast(Me.ActiveControl, WebBrowser)

                If b IsNot Nothing Then
                    b.Document.ExecCommand("Copy", False, Nothing)
                End If
            End If
        End If
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        Dim ctb As WheeControls.SyntaxRichTextBox = TryCast(Me.ActiveControl, WheeControls.SyntaxRichTextBox)

        If ctb IsNot Nothing Then
            ctb.Paste(DataFormats.GetFormat(DataFormats.UnicodeText))
        Else
            Dim tb As TextBox = TryCast(Me.ActiveControl, TextBox)

            If tb IsNot Nothing Then
                tb.Paste()
            End If
        End If
    End Sub


    Private Sub SelectAllToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem1.Click
        Dim ctb As WheeControls.SyntaxRichTextBox = TryCast(Me.ActiveControl, WheeControls.SyntaxRichTextBox)

        If ctb IsNot Nothing Then
            ctb.SelectionStart = 0
            ctb.SelectionLength = ctb.TextLength
        Else
            Dim tb As TextBox = TryCast(Me.ActiveControl, TextBox)

            If tb IsNot Nothing Then
                tb.SelectAll()
            Else
                Dim b As WebBrowser = TryCast(Me.ActiveControl, WebBrowser)

                If b IsNot Nothing Then
                    b.Document.ExecCommand("SelectAll", False, Nothing)
                End If
            End If
        End If
    End Sub

End Class