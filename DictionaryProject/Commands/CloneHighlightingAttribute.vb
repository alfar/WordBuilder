Public Class CloneHighlightingAttribute
    Inherits Attribute

    Public Sub New(ByVal source As Integer, ByVal destination As Integer)
        _SourceLevel = source
        _DestinationLevel = destination
    End Sub

    Private _SourceLevel As Integer
    Public Property SourceLevel() As Integer
        Get
            Return _SourceLevel
        End Get
        Set(ByVal value As Integer)
            _SourceLevel = value
        End Set
    End Property

    Private _DestinationLevel As Integer
    Public Property DestinationLevel() As Integer
        Get
            Return _DestinationLevel
        End Get
        Set(ByVal value As Integer)
            _DestinationLevel = value
        End Set
    End Property
End Class
