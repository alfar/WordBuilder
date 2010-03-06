Public Class WeightedCommand
    Private _Command As CommandBase
    Public Property Command() As CommandBase
        Get
            Return _Command
        End Get
        Set(ByVal value As CommandBase)
            _Command = value
        End Set
    End Property

    Private _Weight As Double
    Public Property Weight() As Double
        Get
            Return _Weight
        End Get
        Set(ByVal value As Double)
            _Weight = value
        End Set
    End Property
End Class

