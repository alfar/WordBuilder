
Public Class RuleCollection
    Inherits Collections.ObjectModel.Collection(Of Rule)

    Private Shared _Random As New Random()

    Public Function GetRuleByName(ByVal name As String) As Rule
        Dim rules As List(Of Rule) = (From r As Rule In Me Where r.Name = name).ToList()

        If rules.Count = 0 Then
            Return Nothing
        End If

        Dim total As Double = rules.Sum(Function(r As Rule) r.Probability)

        Dim selected As Double = _Random.NextDouble() * total

        For Each r As Rule In rules
            selected -= r.Probability
            If selected <= 0 Then
                Return r
            End If
        Next

        Return rules.LastOrDefault()
    End Function
End Class
