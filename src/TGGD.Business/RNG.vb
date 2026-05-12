Public Module RNG
    Private ReadOnly random As New Random
    Public Function FromRange(
                             minimum As Integer,
                             maximum As Integer,
                             Optional random As Random = Nothing) As Integer
        Return If(random, RNG.random).Next(minimum, maximum + 1)
    End Function
    Public Function FromList(Of TItem)(source As IReadOnlyList(Of TItem)) As TItem
        Return source(FromRange(0, source.Count - 1))
    End Function
End Module
