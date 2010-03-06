Imports System.Data.SqlServerCe

Public Class RelationType
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

    Private _inverseTypeId As Integer
    Public Property InverseTypeId() As Integer
        Get
            Return _inverseTypeId
        End Get
        Set(ByVal value As Integer)
            _inverseTypeId = value
        End Set
    End Property

    Private _InverseType As RelationType
    Public Property InverseType() As RelationType
        Get
            If _InverseType Is Nothing AndAlso _inverseTypeId <> 0 Then
                _InverseType = LoadSingle(_inverseTypeId)
            End If
            Return _InverseType
        End Get
        Set(ByVal value As RelationType)
            _InverseType = value
            _inverseTypeId = _InverseType.Id
        End Set
    End Property
#End Region

#Region "Methods"
    Public Sub Save()
        If _inverseTypeId = 0 AndAlso _InverseType IsNot Nothing Then
            _InverseType.Save()
            _inverseTypeId = _InverseType.Id
        End If

        Database.Save(Me)
    End Sub

    Public Sub Delete()
        If _Id <> 0 Then
            Database.Delete(Me)
        End If
    End Sub
#End Region

#Region "Loaders"
    Public Shared Function LoadSingle(ByVal id As Integer) As RelationType
        Dim result As RelationType = Nothing

        Using dr As SqlCeDataReader = Database.GetRelationType(id)
            If dr.Read() Then
                result = New RelationType
                result.Fill(dr)
            End If
        End Using

        Return result
    End Function

    Public Shared Function LoadList() As RelationTypeCollection
        Dim result As New RelationTypeCollection()
        Using dr As SqlCeDataReader = Database.GetRelationTypes()
            While dr.Read()
                Dim rt As New RelationType()
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

        Public Shared Function GetRelationTypeId(ByVal name As String) As Int32
            Dim parms As New List(Of SqlCeParameter)()

            parms.Add(New SqlCeParameter("@name", name))

            Return DatabaseHelper.ExecuteInt32("select id from RelationTypes where name = @name", parms)
        End Function

        Public Shared Sub Save(ByVal rt As RelationType)
            If rt._Id = 0 Then
                Dim parms As New List(Of SqlCeParameter)()
                parms.Add(New SqlCeParameter("@name", rt.Name))
                parms.Add(New SqlCeParameter("@inverseTypeId", rt.InverseTypeId))

                DatabaseHelper.Execute("insert into RelationTypes (name, inverseTypeId) values (@name, @inverseTypeId)", parms)
                rt.Id = DatabaseHelper.ExecuteInt32("select max(Id) from RelationTypes", Nothing)
            Else
                Dim parms As New List(Of SqlCeParameter)()
                parms.Add(New SqlCeParameter("@id", rt.Id))
                parms.Add(New SqlCeParameter("@name", rt.Name))
                parms.Add(New SqlCeParameter("@inverseTypeId", rt.InverseTypeId))

                DatabaseHelper.Execute("update RelationTypes set name = @name, inverseTypeId = @inverseTypeId where id = @id", parms)
            End If
        End Sub

        Public Shared Sub Delete(ByVal rt As RelationType)
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@id", rt.Id))
            DatabaseHelper.Execute("delete from RelationTypes where id = @id", parms)
        End Sub

        Public Shared Function GetRelationType(ByVal id As Integer) As SqlCeDataReader
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@id", id))

            Return DatabaseHelper.ExecuteReader("select id, name, inverseTypeId from RelationTypes where id = @id", parms)
        End Function

        Public Shared Function GetRelationTypes() As SqlCeDataReader
            Return DatabaseHelper.ExecuteReader("select id, name, inverseTypeId from RelationTypes", Nothing)
        End Function
    End Class

    Private Sub Fill(ByVal dr As SqlCeDataReader)
        Me.Id = dr.GetInt32(0)
        Me.Name = dr.GetString(1)
        Me.InverseTypeId = dr.GetInt32(2)
    End Sub
#End Region
End Class
