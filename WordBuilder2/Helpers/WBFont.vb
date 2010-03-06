<Serializable()> _
Public Class WBFont
    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _Size As Single
    Public Property Size() As Single
        Get
            Return _Size
        End Get
        Set(ByVal value As Single)
            _Size = value
        End Set
    End Property
End Class
