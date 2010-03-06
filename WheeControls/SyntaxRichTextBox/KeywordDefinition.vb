Imports System.Text.RegularExpressions

Public Class KeywordDefinition
    Public Sub New(ByVal regex As String, ByVal color As Color, Optional ByVal toggleComment As Boolean = False, Optional ByVal level As Integer = -1)
        _RegEx = New Regex(regex, RegexOptions.IgnoreCase)
        _color = color
        _toggleComment = toggleComment
        _level = level
    End Sub

    Private _RegEx As Regex
    Public Property RegEx() As Regex
        Get
            Return _RegEx
        End Get
        Set(ByVal value As Regex)
            _RegEx = value
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
