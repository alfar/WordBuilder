Imports System.Windows.Forms

Public Class ManageRelationTypesDialog

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ManageRelationTypes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InverseTypeComboBox.DisplayMember = "Name"
        InverseTypeComboBox.ValueMember = "Id"

        PopulateRelationTypes()
    End Sub

    Private Sub PopulateRelationTypes()
        Dim relationTypes As DictionaryData.RelationTypeCollection = DictionaryData.RelationType.LoadList()

        RelationTypesListView.Items.Clear()
        RelationTypesListView.Items.AddRange(relationTypes.Select(Function(rt As DictionaryData.RelationType) New RelationTypeListViewItem(rt)).ToArray())

        relationTypes.Insert(0, New DictionaryData.RelationType())

        InverseTypeComboBox.DataSource = relationTypes
    End Sub

    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        Dim relType As DictionaryData.RelationType
        If RelationTypesListView.SelectedItems.Count > 0 Then
            reltype = DirectCast(RelationTypesListView.SelectedItems(0), RelationTypeListViewItem).RelationType
        Else
            relType = New DictionaryData.RelationType()
        End If

        relType.Name = NameTextBox.Text
        relType.InverseTypeId = InverseTypeComboBox.SelectedValue
        relType.Save()

        PopulateRelationTypes()
    End Sub

    Private Sub RelationTypesListView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RelationTypesListView.SelectedIndexChanged
        If RelationTypesListView.SelectedItems.Count > 0 Then
            Dim relType As DictionaryData.RelationType = DirectCast(RelationTypesListView.SelectedItems(0), RelationTypeListViewItem).RelationType
            NameTextBox.Text = relType.Name
            InverseTypeComboBox.SelectedValue = relType.InverseTypeId
        Else
            NameTextBox.Text = ""
            InverseTypeComboBox.SelectedValue = 0
        End If
    End Sub
End Class
