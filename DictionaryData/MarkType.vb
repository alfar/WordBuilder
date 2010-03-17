Imports System.Data.SqlServerCe

Public Class MarkType
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
        End If
    End Sub
#End Region

#Region "Loaders"
    Public Shared Function LoadSingle(ByVal id As Integer) As MarkType
        Dim result As MarkType = Nothing

        Using dr As SqlCeDataReader = Database.GetMarkType(id)
            If dr.Read() Then
                result = New MarkType
                result.Fill(dr)
            End If
        End Using

        Return result
    End Function

    Public Shared Function LoadList() As MarkTypeCollection
        Dim result As New MarkTypeCollection()
        Using dr As SqlCeDataReader = Database.GetMarkTypes()
            While dr.Read()
                Dim rt As New MarkType()
                rt.Fill(dr)
                result.Add(rt)
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
            Return DatabaseHelper.ExecuteInt32("select count(*) from MarkTypes where exists (select null as empty from WordMarks where MarkTypes.Id = WordMarks.markTypeId)", Nothing)
        End Function

        Public Shared Function GetMarkTypeId(ByVal name As String) As Int32
            Dim parms As New List(Of SqlCeParameter)()

            parms.Add(New SqlCeParameter("@name", name))

            Return DatabaseHelper.ExecuteInt32("select id from MarkTypes where name = @name", parms)
        End Function

        Public Shared Sub Save(ByVal rt As MarkType)
            If rt._Id = 0 Then
                Dim parms As New List(Of SqlCeParameter)()
                parms.Add(New SqlCeParameter("@name", rt.Name))

                DatabaseHelper.Execute("insert into MarkTypes (name) values (@name)", parms)
                rt.Id = DatabaseHelper.ExecuteInt32("select max(Id) from MarkTypes", Nothing)
            Else
                Dim parms As New List(Of SqlCeParameter)()
                parms.Add(New SqlCeParameter("@id", rt.Id))
                parms.Add(New SqlCeParameter("@name", rt.Name))

                DatabaseHelper.Execute("update MarkTypes set name = @name where id = @id", parms)
            End If
        End Sub

        Public Shared Sub Delete(ByVal rt As MarkType)
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@id", rt.Id))
            DatabaseHelper.Execute("delete from MarkTypes where id = @id", parms)
        End Sub

        Public Shared Function GetMarkType(ByVal id As Integer) As SqlCeDataReader
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@id", id))

            Return DatabaseHelper.ExecuteReader("select id, name from MarkTypes where id = @id", parms)
        End Function

        Public Shared Function GetMarkTypes() As SqlCeDataReader
            Return DatabaseHelper.ExecuteReader("select id, name from MarkTypes", Nothing)
        End Function
    End Class

    Public Overrides Function ToString() As String
        Return Name
    End Function

    Private Sub Fill(ByVal dr As SqlCeDataReader)
        Me.Id = dr.GetInt32(0)
        Me.Name = dr.GetString(1)
    End Sub
#End Region
End Class
