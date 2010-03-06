
Public MustInherit Class CommandBase
    Protected m_lineNumber As Integer
    Public ReadOnly Property LineNumber() As Integer
        Get
            Return m_lineNumber
        End Get
    End Property

    Protected m_project As Project
    Public ReadOnly Property Project() As Project
        Get
            Return m_project
        End Get
    End Property

    Public MustOverride Sub Execute(ByVal context As Context)

    Public Overridable Sub LoadCommand(ByVal project As Project, ByVal reader As IO.TextReader, ByVal line As String, ByRef lineNumber As Integer)
        m_lineNumber = lineNumber
        m_project = project
    End Sub

    Public MustOverride Sub WriteCommand(ByVal writer As IO.TextWriter)
    Public MustOverride Sub CheckSanity(ByVal project As Project)

End Class
