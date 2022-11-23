Friend Module RNG
    Private random As New Random
    Friend Function Roll(maximum As Double) As Double
        Return random.NextDouble * maximum
    End Function
End Module
