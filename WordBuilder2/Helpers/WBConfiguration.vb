<Serializable()> _
Public Class WBConfiguration
    Private _CodeFont As New WBFont With {.Name = "Courier New", .Size = 11}
    Public Property CodeFont() As WBFont
        Get
            Return _CodeFont
        End Get
        Set(ByVal value As WBFont)
            _CodeFont = value
        End Set
    End Property

    Private _DictionaryFont As New WBFont With {.Name = "Times New Roman", .Size = 11}
    Public Property DictionaryFont() As WBFont
        Get
            Return _DictionaryFont
        End Get
        Set(ByVal value As WBFont)
            _DictionaryFont = value
        End Set
    End Property

    Public Function GetCodeFont() As Font
        Return New Font(_CodeFont.Name, _CodeFont.Size)
    End Function

    Public Function GetDictionaryFont() As Font
        Return New Font(_DictionaryFont.Name, _DictionaryFont.Size)
    End Function
End Class
