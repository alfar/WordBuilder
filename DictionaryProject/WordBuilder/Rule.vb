
Public Class Rule
    Private _Name As String
    Private _LineNumber As Integer
    Private _Probability As Double
    Private _Commands As New CommandCollection()

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public Property LineNumber() As Integer
        Get
            Return _LineNumber
        End Get
        Set(ByVal value As Integer)
            _LineNumber = value
        End Set
    End Property

    Public Property Probability() As Double
        Get
            Return _Probability
        End Get
        Set(ByVal value As Double)
            _Probability = value
        End Set
    End Property

    Public ReadOnly Property Commands() As CommandCollection
        Get
            Return _Commands
        End Get
    End Property

    Public Sub Execute(ByVal context As Context)
        If context.IncrementRuleCount() > 500 Then
            Throw New ApplicationException("Rule count exceeded 500 when running rule " & Me.Name)
        End If
        Commands.Execute(context)
    End Sub
End Class
