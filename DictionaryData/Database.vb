Imports System.Data.SqlServerCe

Public NotInheritable Class DatabaseHelper
    Private Sub New()
    End Sub

    Private Shared _ConnectionString As String
    Public Shared Property ConnectionString() As String
        Get
            Return _ConnectionString
        End Get
        Set(ByVal value As String)
            _ConnectionString = value
        End Set
    End Property

    Private Shared Function SetupCommand(ByVal query As String, ByVal parms As IEnumerable(Of SqlCeParameter), ByRef connection As SqlCeConnection) As SqlCeCommand
        If connection Is Nothing Then
            connection = New System.Data.SqlServerCe.SqlCeConnection(_ConnectionString)
        End If

        Dim cmd As New SqlCeCommand(query, connection)
        cmd.CommandType = CommandType.Text
        If parms IsNot Nothing Then
            cmd.Parameters.AddRange(parms.ToArray())
        End If

        Return cmd
    End Function

    Public Shared Function Execute(ByVal query As String, ByVal parms As IEnumerable(Of SqlCeParameter)) As Int32
        Dim c As SqlCeConnection = Nothing
        Dim cmd As SqlCeCommand = SetupCommand(query, parms, c)

        Try
            c.Open()
            Return cmd.ExecuteNonQuery()
        Finally
            If c.State <> ConnectionState.Closed Then
                c.Close()
            End If
        End Try

        Return 0
    End Function

    Public Shared Function ExecuteInt32(ByVal query As String, ByVal parms As IEnumerable(Of SqlCeParameter)) As Int32
        Dim c As SqlCeConnection = Nothing
        Dim cmd As SqlCeCommand = SetupCommand(query, parms, c)

        Try
            c.Open()
            Dim result As Object = cmd.ExecuteScalar()

            If TypeOf result Is System.DBNull Then
                Return 0
            End If

            Return CInt(result)
        Finally
            If c.State <> ConnectionState.Closed Then
                c.Close()
            End If
        End Try

        Return 0
    End Function

    Public Shared Function ExecuteReader(ByVal query As String, ByVal parms As IEnumerable(Of SqlCeParameter)) As SqlCeDataReader
        Dim c As SqlCeConnection = Nothing
        Dim cmd As SqlCeCommand = SetupCommand(query, parms, c)

        Try
            c.Open()
            Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch
            If c.State <> ConnectionState.Closed Then
                c.Close()
            End If
            Throw
        End Try

        Return Nothing
    End Function
End Class
