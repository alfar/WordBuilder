Public Class RelationTypeListViewItem
    Inherits ListViewItem

    Public Sub New(ByVal rt As DictionaryData.RelationType)
        Text = rt.Name
        _RelationType = rt
    End Sub

    Private _RelationType As DictionaryData.RelationType
    Public ReadOnly Property RelationType() As DictionaryData.RelationType
        Get
            Return _RelationType
        End Get
    End Property
End Class
