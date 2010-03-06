Imports System.Data.SqlServerCe

Public Class Branch
#Region "Properties"
    Private _Saved As Boolean

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
        End Set
    End Property
#End Region

#Region "Methods"
    Public Sub Save()
        If TargetId = 0 AndAlso Target IsNot Nothing Then
            Target.Save()
            TargetId = Target.Id
        End If

        Database.Save(Me)
        _Saved = True
    End Sub

    Public Sub Delete()
        If _Saved Then
            Database.Delete(Me)
            _Saved = False
            If Target IsNot Nothing Then
                Target.Delete()
                _TargetId = 0
            End If
        End If
    End Sub
#End Region

#Region "Loaders"
    Public Shared Function LoadParent(ByVal word As Word) As Branch
        Dim result As Branch = Nothing

        Using dr As SqlCeDataReader = Database.GetParentBranchForWord(word)
            If dr.Read() Then
                result = New Branch()
                result.Fill(dr)
            End If
        End Using

        Return result
    End Function

    Public Shared Function LoadList(ByVal word As Word) As BranchCollection
        Dim result As New BranchCollection()
        Using dr As SqlCeDataReader = Database.GetBranchesForWord(word)
            While dr.Read()
                Dim b As New Branch()
                b.Fill(dr)
                result.Add(b)
            End While
        End Using

        Return result
    End Function
#End Region

#Region "Helpers"
    Private NotInheritable Class Database
        Private Sub New()
        End Sub

        Public Shared Function GetBranchTypeId(ByVal name As String) As Int32
            Dim parms As New List(Of SqlCeParameter)()

            parms.Add(New SqlCeParameter("@branchName", name))

            Return DatabaseHelper.ExecuteInt32("select id from Branches where name = @branchName", parms)
        End Function

        Public Shared Sub Save(ByVal branch As Branch)
            If Not branch._Saved Then
                Dim parms As New List(Of SqlCeParameter)()

                Dim branchTypeId As Integer = GetBranchTypeId(branch.Name)

                If branchTypeId = 0 Then
                    parms.Add(New SqlCeParameter("@branchName", branch.Name))
                    If DatabaseHelper.Execute("insert into Branches (name) values (@branchName)", parms) Then
                        branchTypeId = DatabaseHelper.ExecuteInt32("select max(id) from Branches", Nothing)
                    End If
                End If

                If branchTypeId > 0 Then
                    parms.Clear()
                    parms.Add(New SqlCeParameter("@branchTypeId", branchTypeId))
                    parms.Add(New SqlCeParameter("@sourceWordId", branch.SourceId))
                    parms.Add(New SqlCeParameter("@targetWordId", branch.TargetId))

                    DatabaseHelper.Execute("insert into WordBranches (branchId, sourceWordId, targetWordId) values (@branchTypeId, @sourceWordId, @targetWordId)", parms)
                End If
            End If
        End Sub

        Public Shared Sub Delete(ByVal branch As Branch)
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@branchTypeId", GetBranchTypeId(branch.Name)))
            parms.Add(New SqlCeParameter("@sourceWordId", branch.SourceId))
            DatabaseHelper.Execute("delete from WordBranches where branchId = @branchTypeId and sourceWordId = @sourceWordId", parms)
        End Sub

        Public Shared Function GetParentBranchForWord(ByVal w As Word) As SqlCeDataReader
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@targetId", w.Id))

            Return DatabaseHelper.ExecuteReader("select Branches.name, WordBranches.sourceWordId, WordBranches.targetWordId from WordBranches inner join Branches on WordBranches.branchId = Branches.Id where WordBranches.targetWordId = @targetId", parms)
        End Function

        Public Shared Function GetBranchesForWord(ByVal w As Word) As SqlCeDataReader
            Dim parms As New List(Of SqlCeParameter)()
            parms.Add(New SqlCeParameter("@sourceId", w.Id))

            Return DatabaseHelper.ExecuteReader("select Branches.name, WordBranches.sourceWordId, WordBranches.targetWordId from WordBranches inner join Branches on WordBranches.branchId = Branches.Id where WordBranches.sourceWordId = @sourceId", parms)
        End Function
    End Class

    Private Sub Fill(ByVal dr As SqlCeDataReader)
        Me.Name = dr.GetString(0)
        Me.SourceId = dr.GetInt32(1)
        Me.TargetId = dr.GetInt32(2)
        Me._Saved = True
    End Sub
#End Region
End Class
