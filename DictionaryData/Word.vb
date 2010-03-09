Imports System.Data.SqlServerCe

Public Class Word
#Region "Enumerations"
    <Flags()> _
    Public Enum SearchOptions
        IncludeMeaning = 1
        IncludeWord = 2
        IncludeMarks = 4
        WildcardStart = 8
        WildcardEnd = 16
        RequireMeaning = 32
    End Enum
#End Region

#Region "Properties"
    Private _Id As Integer
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property

    Private _Word As String
    Public Property Word() As String
        Get
            Return _Word
        End Get
        Set(ByVal value As String)
            _Word = value
        End Set
    End Property

    Private _Tokens As String()
    Public Property Tokens() As String()
        Get
            Return _Tokens
        End Get
        Set(ByVal value As String())
            _Tokens = value
        End Set
    End Property

    Private _Meaning As String
    Public Property Meaning() As String
        Get
            Return _Meaning
        End Get
        Set(ByVal value As String)
            _Meaning = value
        End Set
    End Property

    Private _ParentBranch As Branch
    Public ReadOnly Property ParentBranch() As Branch
        Get
            If _ParentBranch Is Nothing Then
                _ParentBranch = Branch.LoadParent(Me)
            End If
            Return _ParentBranch
        End Get
    End Property

    Private _Branches As BranchCollection
    Public ReadOnly Property Branches() As BranchCollection
        Get
            If _Branches Is Nothing Then
                _Branches = Branch.LoadList(Me)
            End If
            Return _Branches
        End Get
    End Property

    Private _Relations As RelationCollection
    Public ReadOnly Property Relations() As RelationCollection
        Get
            If _Relations Is Nothing Then
                _Relations = Relation.LoadList(Me)
            End If

            Return _Relations
        End Get
    End Property

    Private _Marks As MarkCollection
    Public ReadOnly Property Marks() As MarkCollection
        Get
            If _Marks Is Nothing Then
                _Marks = Mark.LoadList(Me)
            End If

            Return _Marks
        End Get
    End Property
#End Region

#Region "Methods"
    Public Sub Save()
        Database.Save(Me)
    End Sub

    Public Sub Delete()
        If _Id <> 0 Then
            For Each b As Branch In Branches
                b.Delete()
            Next

            If ParentBranch IsNot Nothing Then
                ParentBranch.Delete()
            Else
                Database.Delete(Me)
            End If

            _Id = 0
        End If
    End Sub
#End Region

#Region "Loaders"
    Public Shared Function LoadSingle(ByVal id As Integer) As Word
        Dim result As Word = Nothing

        Using dr As SqlCeDataReader = Database.GetWord(id)
            If dr.Read() Then
                result = New Word
                result.Fill(dr)
            End If
        End Using

        Return result
    End Function

    Private Shared Function LoadList(ByVal reader As SqlCeDataReader) As WordCollection
        Dim result As New WordCollection()

        While reader IsNot Nothing AndAlso reader.Read()
            Dim w As New Word()
            w.Fill(reader)
            result.Add(w)
        End While

        Return result
    End Function

    Public Shared Function Search(ByVal searchTerm As String, ByVal markTypeIds As Integer(), ByVal searchOptions As SearchOptions) As WordCollection
        Using dr As SqlCeDataReader = Database.SearchWords(searchTerm, markTypeIds, searchOptions)
            Return LoadList(dr)
        End Using
    End Function

    Public Shared Function LoadListWithMeaning(ByVal meaning As String) As WordCollection
        Using dr As SqlCeDataReader = Database.GetWordsWithMeaning(meaning)
            Return LoadList(dr)
        End Using
    End Function

    Public Shared Function LoadList() As WordCollection
        Dim result As New WordCollection()

        Using dr As SqlCeDataReader = Database.GetWords()
            While dr IsNot Nothing AndAlso dr.Read()
                Dim w As New Word()
                w.Fill(dr)
                result.Add(w)
            End While
        End Using

        Return result
    End Function

#End Region

#Region "Helpers"
    Private NotInheritable Class Database
        Private Sub New()
        End Sub

        Public Shared Sub Save(ByVal word As Word)
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@word", word.Word))
            parms.Add(New SqlCeParameter("@tokens", String.Join(" ", word.Tokens)))
            parms.Add(New SqlCeParameter("@meaning", word.Meaning))

            If word.Id <> 0 Then
                parms.Add(New SqlCeParameter("@id", word.Id))
                DatabaseHelper.Execute("update Words set Word = @word, Tokens = @tokens, Meaning = @meaning where id = @id", parms)
            Else
                If DatabaseHelper.Execute("insert into Words (Word, Tokens, Meaning) values (@word, @tokens, @meaning)", parms) > 0 Then
                    word.Id = DatabaseHelper.ExecuteInt32("select max(id) from Words", Nothing)
                End If
            End If
        End Sub

        Public Shared Sub Delete(ByVal word As Word)
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@id", word.Id))

            DatabaseHelper.Execute("delete from Words where id = @id", parms)
        End Sub

        Public Shared Function GetWords() As SqlCeDataReader
            Return DatabaseHelper.ExecuteReader("select Id, Word, Tokens, Meaning from Words order by Words.Word", Nothing)
        End Function

        Public Shared Function SearchWords(ByVal search As String, ByVal markTypeIds As Integer(), ByVal searchOptions As SearchOptions) As SqlCeDataReader
            Dim parms As New List(Of SqlCeParameter)()

            If (searchOptions And DictionaryData.Word.SearchOptions.WildcardStart) = DictionaryData.Word.SearchOptions.WildcardStart Then
                If (searchOptions And DictionaryData.Word.SearchOptions.WildcardEnd) = DictionaryData.Word.SearchOptions.WildcardEnd Then
                    parms.Add(New SqlCeParameter("@term", "%" & search & "%"))
                Else
                    parms.Add(New SqlCeParameter("@term", "%" & search))
                End If
            Else
                If (searchOptions And DictionaryData.Word.SearchOptions.WildcardEnd) = DictionaryData.Word.SearchOptions.WildcardEnd Then
                    parms.Add(New SqlCeParameter("@term", search & "%"))
                Else
                    parms.Add(New SqlCeParameter("@term", search))
                End If
            End If


            Dim sb As New System.Text.StringBuilder()

            Dim crit As New List(Of String)()

            If (searchOptions And DictionaryData.Word.SearchOptions.IncludeWord) = DictionaryData.Word.SearchOptions.IncludeWord Then
                crit.Add("Words.Word like @term")
            End If

            If (searchOptions And DictionaryData.Word.SearchOptions.IncludeMeaning) = DictionaryData.Word.SearchOptions.IncludeMeaning Then
                crit.Add("Words.Meaning like @term")
            End If

            Dim requireMeaning As String = ""
            If (searchOptions And DictionaryData.Word.SearchOptions.RequireMeaning) = DictionaryData.Word.SearchOptions.RequireMeaning Then
                requireMeaning = "Words.Meaning <> '' and "
            End If

            If (searchOptions And DictionaryData.Word.SearchOptions.IncludeMarks) = DictionaryData.Word.SearchOptions.IncludeMarks Then
                crit.Add("WordMarks.Value like @term and WordMarks.markTypeId in (" & String.Join(", ", Array.ConvertAll(markTypeIds, Function(i As Integer) i.ToString())) & ")")
                sb.Append(" inner join WordMarks on WordMarks.wordId = Words.id")
            End If

            Return DatabaseHelper.ExecuteReader("select Words.Id, Words.Word, Words.Tokens, Words.Meaning from Words" & sb.ToString() & " where " & requireMeaning & "(" & String.Join(" or ", crit.ToArray()) & ") order by Words.Word", parms)
        End Function

        Public Shared Function GetWordsWithMeaning(ByVal meaning As String) As SqlCeDataReader
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@meaning", meaning & "%"))

            Return DatabaseHelper.ExecuteReader("select Id, Word, Tokens, Meaning from Words where Words.Meaning like @meaning order by Words.Word", parms)
        End Function

        Public Shared Function GetWord(ByVal id As Integer) As SqlCeDataReader
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@id", id))

            Return DatabaseHelper.ExecuteReader("select Id, Word, Tokens, Meaning from Words where Words.Id = @id", parms)
        End Function
    End Class

    Private Sub Fill(ByVal dr As SqlCeDataReader)
        Me.Id = dr.GetInt32(0)
        Me.Word = dr.GetString(1)
        Me.Tokens = dr.GetString(2).Split(" ")
        Me.Meaning = dr.GetString(3)
    End Sub
#End Region
End Class
