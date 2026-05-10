Public Class EntityData
    Public Property Metadatas As New Dictionary(Of String, String)
    Public Property Tags As New HashSet(Of String)
    Public Property Statistics As New Dictionary(Of String, Integer)
    Public Property Yokes As New Dictionary(Of String, Guid)
    Public Property StatisticMinimums As New Dictionary(Of String, Integer)
    Public Property StatisticMaximums As New Dictionary(Of String, Integer)
End Class
