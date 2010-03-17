Imports System.Data.SqlServerCe

Public Class Relation
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

    Private _relationTypeId As Integer
    Public Property RelationTypeId() As Integer
        Get
            Return _relationTypeId
        End Get
        Set(ByVal value As Integer)
            _relationTypeId = value
        End Set
    End Property

    Private _RelationType As RelationType
    Public ReadOnly Property RelationType() As RelationType
        Get
            If _RelationType Is Nothing AndAlso _relationTypeId > 0 Then
                _RelationType = RelationType.LoadSingle(_relationTypeId)
            End If
            Return _RelationType
        End Get
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

    Private _SourceId As Integer
    Public Property SourceId() As Integer
        Get
            Return _SourceId
        End Get
        Set(ByVal value As Integer)
            _SourceId = value
        End Set
    End Property

    Private _Source As Word
    Public Property Source() As Word
        Get
            If _Source Is Nothing AndAlso _SourceId <> 0 Then
                _Source = Word.LoadSingle(_SourceId)
            End If

            Return _Source
        End Get
        Set(ByVal value As Word)
            _Source = value
            _SourceId = _Source.Id
        End Set
    End Property

    Private _TargetId As Integer
    Public Property TargetId() As Integer
        Get
            Return _TargetId
        End Get
        Set(ByVal value As Integer)
            _TargetId = value
        End Set
    End Property

    Private _Target As Word
    Public Property Target() As Word
        Get
            If _Target Is Nothing AndAlso _TargetId <> 0 Then
                _Target = Word.LoadSingle(_TargetId)
            End If
            Return _Target
        End Get
        Set(ByVal value As Word)
            _Target = value
            _TargetId = _Target.Id
        End Set
    End Property
#End Region

#Region "Methods"
    Public Shared Function CountTotal() As Integer
        Return Database.CountTotal()
    End Function

    Public Sub Save()
        If RelationTypeId = 0 And Not String.IsNullOrEmpty(Name) Then
            Dim rt As RelationType = RelationType.LoadSingle(Name)

            If rt Is Nothing Then
                rt = New RelationType()
                rt.Name = Name
                rt.Save()
            End If

            RelationTypeId = rt.Id
        End If

        If TargetId = 0 AndAlso Target IsNot Nothing Then
            Target.Save()
            TargetId = Target.Id
        End If

        Database.Save(Me)
    End Sub

    Public Sub Delete()
        If Me._Id <> 0 Then
            Database.Delete(Me)
            Me._Id = 0
        End If
    End Sub
#End Region

#Region "Loaders"
    Public Shared Function LoadList(ByVal word As Word) As RelationCollection
        Dim result As New RelationCollection()
        Using dr As SqlCeDataReader = Database.GetRelationsForWord(word)
            While dr.Read()
                Dim r As New Relation()
                r.Fill(dr)
                result.Add(r)
            End While
        End Using

        Using dr As SqlCeDataReader = Database.GetInverseRelationsForWord(word)
            While dr.Read()
                Dim r As New Relation()
                r.Fill(dr)
                result.Add(r)
            End While
        End Using

        Return result
    End Function

    Public Shared Function LoadSingle(ByVal id As Integer) As Relation
        Dim result As Relation = Nothing

        Using dr As SqlCeDataReader = Database.Getrelation(id)
            If dr.Read() Then
                result = New Relation
                result.Fill(dr)
            End If
        End Using

        Return result
    End Function
#End Region

#Region "Helpers"
    Private NotInheritable Class Database
        Private Sub New()
        End Sub

        Public Shared Function CountTotal() As Integer
            Return DatabaseHelper.ExecuteInt32("select count(*) from WordRelations", Nothing)
        End Function

        Public Shared Sub Save(ByVal rel As Relation)
            Dim parms As New List(Of SqlCeParameter)()

            parms.Add(New SqlCeParameter("@relationTypeId", rel.RelationTypeId))
            parms.Add(New SqlCeParameter("@sourceWordId", rel.SourceId))
            parms.Add(New SqlCeParameter("@destinationWordId", rel.TargetId))

            If rel.Id = 0 Then
                DatabaseHelper.Execute("insert into WordRelations (relationTypeId, sourceWordId, destinationWordId) values (@relationTypeId, @sourceWordId, @destinationWordId)", parms)

                rel.Id = DatabaseHelper.ExecuteInt32("select max(Id) from WordRelations", Nothing)
            Else
                parms.Add(New SqlCeParameter("@id", rel.Id))

                DatabaseHelper.Execute("update WordRelations set relationTypeId = @relationTypeId, sourceWordId = @sourceWordId, destinationWordId = @destinationWordId where id = @id", parms)
            End If
        End Sub

        Public Shared Sub Delete(ByVal rel As Relation)
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@id", rel.Id))
            DatabaseHelper.Execute("delete from WordRelations where id = @id", parms)
        End Sub

        Public Shared Function GetRelation(ByVal id As Integer) As SqlCeDataReader
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@id", id))

            Return DatabaseHelper.ExecuteReader("select WordRelations.Id, RelationTypes.name, RelationTypes.id, WordRelations.sourceWordId, WordRelations.destinationWordId from WordRelations inner join RelationTypes on WordRelations.relationTypeId = RelationTypes.Id where WordRelations.Id = @id", parms)
        End Function

        Public Shared Function GetRelationsForWord(ByVal w As Word) As SqlCeDataReader
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@sourceId", w.Id))

            Return DatabaseHelper.ExecuteReader("select WordRelations.Id, RelationTypes.name, RelationTypes.id, WordRelations.sourceWordId, WordRelations.destinationWordId from WordRelations inner join RelationTypes on WordRelations.relationTypeId = RelationTypes.Id where WordRelations.sourceWordId = @sourceId", parms)
        End Function

        Public Shared Function GetInverseRelationsForWord(ByVal w As Word) As SqlCeDataReader
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@sourceId", w.Id))

            Return DatabaseHelper.ExecuteReader("select WordRelations.Id, RelationTypes.name, RelationTypes.id, WordRelations.destinationWordId, WordRelations.sourceWordId from WordRelations inner join RelationTypes on WordRelations.relationTypeId = RelationTypes.inverseTypeId where WordRelations.destinationWordId = @sourceId", parms)
        End Function
    End Class

    Private Sub Fill(ByVal dr As SqlCeDataReader)
        Me.Id = dr.GetInt32(0)
        Me.Name = dr.GetString(1)
        Me.RelationTypeId = dr.GetInt32(2)
        Me.SourceId = dr.GetInt32(3)
        Me.TargetId = dr.GetInt32(4)
    End Sub
#End Region
End Class
