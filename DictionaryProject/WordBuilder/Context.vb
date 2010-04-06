
Public Class Context
    Public Sub New()
    End Sub

    Private _RuleCount As Integer = 0
    Public ReadOnly Property RuleCount() As Integer
        Get
            Return _RuleCount
        End Get
    End Property

    Public Sub ResetRuleCount()
        _RuleCount = 0
    End Sub

    Public Function IncrementRuleCount() As Integer
        _RuleCount += 1
        Return _RuleCount
    End Function

    Private _Tokens As New List(Of String)()
    Public ReadOnly Property Tokens() As List(Of String)
        Get
            Return _Tokens
        End Get
    End Property

    Private _Branches As New Dictionary(Of String, Context)
    Public ReadOnly Property Branches() As Dictionary(Of String, Context)
        Get
            Return _Branches
        End Get
    End Property

    Public Function Branch(ByVal name As String) As Context
        Dim result As New Context()
        result._RuleCount = Me.RuleCount
        result.Tokens.AddRange(Me.Tokens)

        If _Branches.ContainsKey(name) Then
            Dim num As Integer = 1
            While _Branches.ContainsKey(String.Format("{0}{1}", name, num))
                num += 1
            End While

            name = String.Format("{0}{1}", name, num)
        End If

        _Branches.Add(name, result)

        Return result
    End Function

    Private _Marks As New Dictionary(Of String, String)
    Public ReadOnly Property Marks() As Dictionary(Of String, String)
        Get
            Return _Marks
        End Get
    End Property

    Public Sub Mark(ByVal name As String, ByVal value As String)
        _Marks(name) = value
    End Sub

    Public Overrides Function ToString() As String
        Return String.Join("", Tokens.ToArray())
    End Function

    Public Function Description(Optional ByVal indent As String = "") As String
        Dim result As New List(Of String)()
        result.Add(String.Join("", Tokens.ToArray()))
        For Each m As String In Marks.Keys
            result.Add(String.Format("{0}{1}: {2}", indent, m, Marks(m)))
        Next
        For Each b As String In Branches.Keys
            result.Add(String.Format("  {0}{1}: {2}", indent, b, Branches(b).Description(indent & "  ")))
        Next
        Return String.Join(Environment.NewLine, result.ToArray())
    End Function

    Public Function GetColumn(ByVal path As String, ByVal project As Project) As String
        If String.IsNullOrEmpty(path) Then
            Return ToString()
        End If

        Dim parts() = path.Split(New Char() {"."c}, 2)

        If parts(0).StartsWith("!") Then
            Dim mark As String = parts(0).Substring(1)
            If Marks.ContainsKey(mark) Then
                Return Marks(mark)
            Else
                Return "N/A"
            End If
        ElseIf parts(0).StartsWith("*") Then
            Dim r As Rule = project.Rules.GetRuleByName(parts(0).Substring(1))

            Dim c As New Context()
            c._Tokens = New List(Of String)(Me.Tokens)
            r.Execute(c)

            If parts.Length > 1 Then
                Return c.GetColumn(parts(1), project)
            Else
                Return c.ToString()
            End If
        Else
            If Branches.ContainsKey(parts(0)) Then
                If parts.Length > 1 Then
                    Return Branches(parts(0)).GetColumn(parts(1), project)
                Else
                    Return Branches(parts(0)).ToString()
                End If
            Else
                Return "N/A"
            End If
        End If

        Return ""
    End Function
End Class
