Imports System.Windows.Forms

Public Class AssignMeaningDialog

    Private _Context As DictionaryProject.Context
    Public Property Context() As DictionaryProject.Context
        Get
            Return _Context
        End Get
        Set(ByVal value As DictionaryProject.Context)
            _Context = value
            WordTextBox.Text = _Context.Description()
        End Set
    End Property

    Public Property Meaning() As String
        Get
            Return MeaningTextBox.Text
        End Get
        Set(ByVal value As String)
            MeaningTextBox.Text = value
        End Set
    End Property

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub AssignMeaningDialog_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Meaning = ""
    End Sub

    Private Sub MeaningTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MeaningTextBox.TextChanged
        If Not String.IsNullOrEmpty(MeaningTextBox.Text) Then
            SynonymsWebBrowser.DocumentText = DictionaryPresenter.RenderResultsPage(DictionaryData.Word.Search(MeaningTextBox.Text, New Integer() {}, DictionaryData.Word.SearchOptions.IncludeMeaning))
        Else
            SynonymsWebBrowser.DocumentText = "<html></html>"
        End If
    End Sub
End Class
