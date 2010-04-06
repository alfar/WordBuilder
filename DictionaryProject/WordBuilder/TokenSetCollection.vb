
Public Class TokenSetCollection
    Inherits Collections.ObjectModel.Collection(Of TokenSet)

    Public Function FindByName(ByVal name As String) As TokenSet
    	For t As TokenSet In Me
    		If t.Name = name Then
    			Return t
    		End If
    	Next
        Return Nothing
    End Function
End Class
