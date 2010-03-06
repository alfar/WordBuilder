Imports System.Runtime.InteropServices
Imports System.Security.Permissions

<PermissionSet(SecurityAction.Demand, Name:="FullTrust")> _
<ComVisible(True)> _
Public Class DictionaryScriptingTool
    Public Event EditWord As EventHandler(Of DictionaryWordEventArgs)
    Public Event DeleteWord As EventHandler(Of DictionaryWordEventArgs)
    Public Event LinkWord As EventHandler(Of DictionaryWordEventArgs)

    Public Sub Edit(ByVal id As Integer)
        RaiseEvent EditWord(Me, New DictionaryWordEventArgs() With {.WordId = id})
    End Sub

    Public Sub Delete(ByVal id As Integer)
        RaiseEvent DeleteWord(Me, New DictionaryWordEventArgs() With {.WordId = id})
    End Sub

    Public Sub Link(ByVal id As Integer)
        RaiseEvent LinkWord(Me, New DictionaryWordEventArgs() With {.WordId = id})
    End Sub
End Class
