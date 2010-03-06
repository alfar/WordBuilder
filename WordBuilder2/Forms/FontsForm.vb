Public Class FontsForm
    Public Property CodeFont() As Font
        Get
            Return CodeFontTextBox.Font
        End Get
        Set(ByVal value As Font)
            CodeFontTextBox.Text = value.Name
            CodeFontTextBox.Font = value
        End Set
    End Property

    Public Property DictionaryFont() As Font
        Get
            Return DictionaryFontTextBox.Font
        End Get
        Set(ByVal value As Font)
            DictionaryFontTextBox.Text = value.Name
            DictionaryFontTextBox.Font = value
        End Set
    End Property

    Private Sub CodeFontSelectorButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CodeFontSelectorButton.Click
        If SelectFontDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            CodeFont = SelectFontDialog.Font
        End If
    End Sub

    Private Sub DictionaryFontSelectorButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DictionaryFontSelectorButton.Click
        If SelectFontDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            DictionaryFont = SelectFontDialog.Font
        End If
    End Sub
End Class