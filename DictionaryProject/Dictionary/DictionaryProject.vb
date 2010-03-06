Public Class DictionaryProject
    Private _words As New Dictionary(Of String, List(Of Context))
    Public ReadOnly Property Words() As Dictionary(Of String, List(Of Context))
        Get
            Return _words
        End Get
    End Property

    Private _meanings As New Dictionary(Of String, List(Of String))()
    Public ReadOnly Property Meanings() As Dictionary(Of String, List(Of String))
        Get
            Return _meanings
        End Get
    End Property

    Public Sub AddWord(ByVal meaning As String, ByVal word As Context)
        If Not _words.ContainsKey(meaning) Then
            _words(meaning) = New List(Of Context)()
        End If
        _words(meaning).Add(word)

        Dim wordString As String = word.ToString()
        If Not _meanings.ContainsKey(wordString) Then
            _meanings(wordString) = New List(Of String)()
        End If
        _meanings(wordString).Add(meaning)

        RaiseEvent WordAdded(Me, System.EventArgs.Empty)
    End Sub

    Public Event WordAdded As EventHandler
End Class
