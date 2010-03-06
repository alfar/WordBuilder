Imports System.Xml

Public Class CsvExporter
    Inherits ExporterBase

    Public Overrides Sub Export(ByVal words As System.Collections.Generic.List(Of Context), ByVal filename As String)
        Dim sb As New Text.StringBuilder()
        Dim columns As New List(Of String)()
        columns.Add(".Word.")

        For Each ctx As Context In words
            Dim dic As Dictionary(Of String, String) = ToDictionary(ctx)

            For Each k As String In dic.Keys
                If Not columns.Contains(k) Then
                    columns.Add(k)
                End If
            Next

            For Each c As String In columns
                If dic.ContainsKey(c) Then
                    sb.Append(dic(c))
                End If
                sb.Append(";")
            Next

            sb.AppendLine()
        Next

        Dim header As String = String.Join(";", columns.ToArray())

        IO.File.WriteAllText(filename, header & vbCrLf & sb.ToString())
    End Sub

    Private Function ToDictionary(ByVal ctx As Context) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)()

        result.Add(".Word.", ctx.ToString())

        ApplyContext(ctx, result)

        Return result
    End Function

    Private Sub ApplyContext(ByVal ctx As Context, ByVal dic As Dictionary(Of String, String), Optional ByVal prefix As String = "")
        For Each m As String In ctx.Marks.Keys
            dic.Add(prefix & "!" & m, ctx.Marks(m))
        Next

        For Each b As String In ctx.Branches.Keys
            dic.Add(b, ctx.Branches(b).ToString())
            ApplyContext(ctx.Branches(b), dic, prefix & b & ".")
        Next
    End Sub

    Public Overrides ReadOnly Property Type() As String
        Get
            Return "Csv file|*.csv"
        End Get
    End Property
End Class
