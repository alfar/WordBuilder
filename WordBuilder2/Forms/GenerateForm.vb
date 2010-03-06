Imports DictionaryProject

Public Class GenerateForm
    Public Sub New(ByVal project As Project)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _project = project
    End Sub

    Private _project As Project

    Private Sub GenerateForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RulesTableLayoutPanel.RowCount = _project.StartRules.Count
        RulesTableLayoutPanel.RowStyles.Clear()

        Dim pos As Integer = 0
        For Each sr As String In _project.StartRules.Keys
            Dim l As New Label() With {.Text = sr, .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}

            RulesTableLayoutPanel.Controls.Add(l, 0, pos)

            Dim b As New TextBox With {.Text = _project.StartRules(sr)}

            RulesTableLayoutPanel.Controls.Add(b, 1, pos)

            RulesTableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.AutoSize))

            pos += 1
        Next
    End Sub

    Private Sub MyOkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyOkButton.Click
        For pos As Integer = 0 To RulesTableLayoutPanel.RowCount - 1
            Dim rule As String = DirectCast(RulesTableLayoutPanel.Controls(pos * 2), Label).Text
            Dim value As Integer

            If Not Integer.TryParse(DirectCast(RulesTableLayoutPanel.Controls(pos * 2 + 1), TextBox).Text, value) Then
                value = 0
            End If

            _project.StartRules(rule) = value
        Next
    End Sub
End Class