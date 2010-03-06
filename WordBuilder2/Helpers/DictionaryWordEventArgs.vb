Public Class DictionaryWordEventArgs
    Inherits EventArgs

    Private _WordId As Integer
    Public Property WordId() As Integer
        Get
            Return _WordId
        End Get
        Set(ByVal value As Integer)
            _WordId = value
        End Set
    End Property
End Class
