Imports System.Text.RegularExpressions

Public Class KeywordMatch
    Public Sub New(ByVal match As Match, ByVal color As Color, Optional ByVal toggleComment As Boolean = False, Optional ByVal level As Integer = 0)
        _match = match
        _color = color
        _toggleComment = toggleComment
        _level = Level
    End Sub

    Private _match As Match
    Public Property Match() As Match
        Get
            Return _match
        End Get
        Set(ByVal value As Match)
            _match = value
        End Set
    End Property

    Private _color As Color
    Public Property Color() As Color
        Get
            Return _color
        End Get
        Set(ByVal value As Color)
            _color = value
        End Set
    End Property

    Private _toggleComment As Boolean
    Public Property ToggleComment() As Boolean
        Get
            Return _toggleComment
        End Get
        Set(ByVal value As Boolean)
            _toggleComment = value
        End Set
    End Property

    Private _level As Integer
    Public Property Level() As Integer
        Get
            Return _level
        End Get
        Set(ByVal value As Integer)
            _level = value
        End Set
    End Property

End Class
