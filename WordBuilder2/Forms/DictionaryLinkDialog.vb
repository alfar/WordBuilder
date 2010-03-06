Imports System.Windows.Forms

Public Class DictionaryLinkDialog

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DictionaryLinkDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RelationTypeComboBox.DataSource = DictionaryData.RelationType.LoadList()
        RelationTypeComboBox.ValueMember = "Id"
        RelationTypeComboBox.DisplayMember = "Name"
    End Sub

    Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        DestinationWordCheckedListBox.Items.Clear()
        DestinationWordCheckedListBox.Items.AddRange(DictionaryData.Word.Search(SearchTextBox.Text, New Integer() {}, DictionaryData.Word.SearchOptions.IncludeMeaning Or DictionaryData.Word.SearchOptions.IncludeWord Or DictionaryData.Word.SearchOptions.WildcardStart Or DictionaryData.Word.SearchOptions.WildcardEnd).Select(Function(w As DictionaryData.Word) New WordListItem() With {.Id = w.Id, .Name = w.Word}).ToArray())
    End Sub

    Public Function GetSelectedWordIds() As List(Of Integer)
        Return DestinationWordCheckedListBox.SelectedItems.Cast(Of WordListItem).Select(Function(w As WordListItem) w.Id).ToList()
    End Function

    Private Sub ManageRelationTypesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManageRelationTypesButton.Click
        Dim dialog As New ManageRelationTypesDialog()

        dialog.ShowDialog()
    End Sub
End Class
