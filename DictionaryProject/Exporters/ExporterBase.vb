Public MustInherit Class ExporterBase
    Private Shared _Exporters As List(Of ExporterBase)
    Public Shared ReadOnly Property Exporters() As List(Of ExporterBase)
        Get
            If _Exporters Is Nothing Then
                _Exporters = New List(Of ExporterBase)()

                For Each asm As System.Reflection.Assembly In AppDomain.CurrentDomain.GetAssemblies()
                    Try
                        For Each t As Type In asm.GetTypes()
                            If Not t.IsAbstract AndAlso GetType(ExporterBase).IsAssignableFrom(t) Then
                                _Exporters.Add(DirectCast(Activator.CreateInstance(t), ExporterBase))
                            End If
                        Next
                    Catch ex As Exception

                    End Try
                Next
            End If

            Return _Exporters
        End Get
    End Property

    Public MustOverride ReadOnly Property Type() As String
    Public MustOverride Sub Export(ByVal words As List(Of Context), ByVal filename As String)
End Class
