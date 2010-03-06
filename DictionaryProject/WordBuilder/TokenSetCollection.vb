
Public Class TokenSetCollection
    Inherits Collections.ObjectModel.Collection(Of TokenSet)

    Public Function FindByName(ByVal name As String) As TokenSet
        Return (From t As TokenSet In Me Where t.Name = name).FirstOrDefault
    End Function
End Class
