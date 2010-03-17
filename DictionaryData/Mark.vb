Imports System.Data.SqlServerCe

Public Class Mark
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

    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _WordId As Integer
    Public Property WordId() As Integer
        Get
            Return _WordId
        End Get
        Set(ByVal value As Integer)
            _WordId = value
        End Set
    End Property

    Private _Word As Word
    Public Property Word() As Word
        Get
            If _Word Is Nothing AndAlso _WordId <> 0 Then
                _Word = Word.LoadSingle(_WordId)
            End If

            Return _Word
        End Get
        Set(ByVal value As Word)
            _Word = value
        End Set
    End Property

    Private _Value As String
    Public Property Value() As String
        Get
            Return _Value
        End Get
        Set(ByVal value As String)
            _Value = value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Shared Function CountTotal() As Integer
        Return Database.CountTotal()
    End Function

    Public Sub Save()
        Database.Save(Me)
    End Sub

    Public Sub Delete()
        If _Id <> 0 Then
            Database.Delete(Me)
            _Id = 0
        End If
    End Sub
#End Region

#Region "Loaders"
    Public Shared Function LoadList(ByVal word As Word) As MarkCollection
        Dim result As New MarkCollection()
        Using dr As SqlCeDataReader = Database.GetMarksForWord(word)
            While dr.Read()
                Dim r As New Mark()
                r.Fill(dr)
                result.Add(r)
            End While
        End Using

        Return result
    End Function
#End Region

#Region "Helpers"
    Private NotInheritable Class Database
        Private Sub New()
        End Sub

        Public Shared Function CountTotal() As Integer
            Return DatabaseHelper.ExecuteInt32("select count(*) from WordMarks", Nothing)
        End Function

        Public Shared Function GetMarkTypeId(ByVal name As String) As Int32
            Dim parms As New List(Of SqlCeParameter)()

            parms.Add(New SqlCeParameter("@markName", name))

            Return DatabaseHelper.ExecuteInt32("select id from MarkTypes where name = @markName", parms)
        End Function

        Public Shared Sub Save(ByVal mark As Mark)
            Dim parms As New List(Of SqlCeParameter)()

            Dim markTypeId As Integer = GetMarkTypeId(mark.Name)

            If markTypeId = 0 Then
                parms.Add(New SqlCeParameter("@markName", mark.Name))
                If DatabaseHelper.Execute("insert into MarkTypes (name) values (@markName)", parms) Then
                    markTypeId = DatabaseHelper.ExecuteInt32("select max(id) from MarkTypes", Nothing)
                End If
            End If

            parms.Clear()
            parms.Add(New SqlCeParameter("@markTypeId", markTypeId))
            parms.Add(New SqlCeParameter("@wordId", mark.WordId))
            parms.Add(New SqlCeParameter("@value", mark.Value))

            If mark.Id = 0 Then
                DatabaseHelper.Execute("insert into WordMarks (markTypeId, wordId, value) values (@markTypeId, @wordId, @value)", parms)
                mark.Id = DatabaseHelper.ExecuteInt32("select max(id) from WordMarks", Nothing)
            Else
                parms.Add(New SqlCeParameter("@id", mark.Id))
                DatabaseHelper.Execute("update WordMarks set markTypeId = @markTypeId, wordId = @wordId, value = @value where id = @id", parms)
            End If
        End Sub

        Public Shared Sub Delete(ByVal mark As Mark)
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@id", Mark.Id))
            DatabaseHelper.Execute("delete from WordMarks where id = @id", parms)
        End Sub

        Public Shared Function GetMarksForWord(ByVal w As Word) As SqlCeDataReader
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@wordId", w.Id))

            Return DatabaseHelper.ExecuteReader("select WordMarks.Id, MarkTypes.name, WordMarks.wordId, WordMarks.value from WordMarks inner join MarkTypes on WordMarks.markTypeId = MarkTypes.Id where WordMarks.wordId = @wordId", parms)
        End Function
    End Class

    Private Sub Fill(ByVal dr As SqlCeDataReader)
        Me.Id = dr.GetInt32(0)
        Me.Name = dr.GetString(1)
        Me.WordId = dr.GetInt32(2)
        Me.Value = dr.GetString(3)
    End Sub
#End Region
End Class
