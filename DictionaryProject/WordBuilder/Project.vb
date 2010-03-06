Public Class Project

    Private _Rules As New RuleCollection()
    Private _TokenSets As New TokenSetCollection()
    Public ReadOnly Property TokenSets() As TokenSetCollection
        Get
            Return _TokenSets
        End Get
    End Property

    Public ReadOnly Property Rules() As RuleCollection
        Get
            Return _Rules
        End Get
    End Property

    Private _columns As New Dictionary(Of String, String)
    Public ReadOnly Property Columns() As Dictionary(Of String, String)
        Get
            Return _columns
        End Get
    End Property

    Private _startRules As New Dictionary(Of String, Integer)
    Public ReadOnly Property StartRules() As Dictionary(Of String, Integer)
        Get
            Return _startRules
        End Get
    End Property

    Public Function GetWord(ByVal startRule As String) As Context
        Dim c As New Context()

        Dim r As Rule = _Rules.GetRuleByName(startRule)

        If r IsNot Nothing Then
            r.Execute(c)
        End If

        Return c
    End Function

    Private _Warnings As New List(Of String)
    Public ReadOnly Property Warnings() As List(Of String)
        Get
            Return _Warnings
        End Get
    End Property
End Class
