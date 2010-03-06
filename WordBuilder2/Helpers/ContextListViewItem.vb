Public Class ContextListViewItem
    Inherits ListViewItem

    Private _context As DictionaryProject.Context
    Public ReadOnly Property Context() As DictionaryProject.Context
        Get
            Return _context
        End Get
    End Property

    Public Sub New(ByVal project As DictionaryProject.Project, ByVal context As DictionaryProject.Context)
        _context = context
        Me.Text = _context.ToString()
        For Each col As String In project.Columns.Keys
            Me.SubItems.Add(_context.GetColumn(project.Columns(col), project))
        Next
    End Sub
End Class
