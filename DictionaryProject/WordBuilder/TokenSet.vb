
Public Class TokenSet

    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _Tokens As New List(Of String)()
    Public ReadOnly Property Tokens() As List(Of String)
        Get
            Return _Tokens
        End Get
    End Property

    Private Shared _Random As New Random()

    Public Function GetToken() As String
        Return Tokens(_Random.Next(0, Tokens.Count))
    End Function
End Class
