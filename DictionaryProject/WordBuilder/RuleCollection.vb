
Public Class RuleCollection
    Inherits Collections.ObjectModel.Collection(Of Rule)

    Private Shared _Random As New Random()

    Public Function GetRuleByName(ByVal name As String) As Rule
        Dim rules As New List(Of Rule)()
        Dim total As Double
        
        For Each r As Rule In Me
        	If r.Name = name Then
        		rules.Add(r)
        		total += r.Probability
        	End If
        Next

        If rules.Count = 0 Then
            Return Nothing
        End If

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
