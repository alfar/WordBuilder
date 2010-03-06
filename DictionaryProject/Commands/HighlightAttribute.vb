<AttributeUsage(AttributeTargets.Class, AllowMultiple:=True)> _
Public Class HighlightAttribute
    Inherits Attribute

    Private _Level As Integer = 1
    Public Property Level() As Integer
        Get
            Return _Level
        End Get
        Set(ByVal value As Integer)
            _Level = value
        End Set
    End Property

    Private _RegEx As String
    Public Property RegEx() As String
        Get
            Return _RegEx
        End Get
        Set(ByVal value As String)
            _RegEx = value
        End Set
    End Property

    Private _Color As String
    Public Property Color() As String
        Get
            Return _Color
        End Get
        Set(ByVal value As String)
            _Color = value
        End Set
    End Property

    Private _NextLevel As Integer = -1
    Public Property NextLevel() As Integer
        Get
            Return _NextLevel
        End Get
        Set(ByVal value As Integer)
            _NextLevel = value
        End Set
    End Property
End Class
