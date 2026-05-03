Public Class EntityData
    Public Property Metadatas As New Dictionary(Of String, String)
    Public Property Tags As New HashSet(Of String)
    Public Property Statistics As New Dictionary(Of String, Integer)
    Public Property Triggers As New Dictionary(Of String, Guid)
End Class
