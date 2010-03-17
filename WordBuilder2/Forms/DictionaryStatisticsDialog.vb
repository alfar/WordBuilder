Imports System.Windows.Forms

Public Class DictionaryStatisticsDialog

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DictionaryStatisticsDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sb As New System.Text.StringBuilder()

        sb.AppendFormat("Total words: {0}{1}", DictionaryData.Word.CountTotal(), vbCrLf)
        sb.AppendFormat("Root words: {0}{1}", DictionaryData.Word.CountRootWords(), vbCrLf)
        sb.AppendFormat("Unlinked words: {0}{1}", DictionaryData.Word.CountUnlinkedWords(), vbCrLf)
        sb.AppendFormat("Unlinked root words: {0}{1}", DictionaryData.Word.CountUnlinkedWords(), vbCrLf)
        sb.AppendFormat("Total links: {0}{1}", DictionaryData.Relation.CountTotal(), vbCrLf)
        sb.AppendFormat("Total marks: {0}{1}", DictionaryData.Mark.CountTotal(), vbCrLf)
        sb.AppendFormat("Mark types in use: {0}{1}", DictionaryData.MarkType.CountTotal(), vbCrLf)

        StatisticsLabel.Text = SB.ToString()
    End Sub

    Private Sub CopyButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyButton.Click
        My.Computer.Clipboard.SetText(StatisticsLabel.Text)
    End Sub
End Class
