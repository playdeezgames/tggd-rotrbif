Public Module RNG
    Private ReadOnly random As New Random
    Public Function FromRange(
                             minimum As Integer,
                             maximum As Integer,
                             Optional random As Random = Nothing) As Integer
        Return If(random, RNG.random).Next(minimum, maximum + 1)
    End Function
End Module
