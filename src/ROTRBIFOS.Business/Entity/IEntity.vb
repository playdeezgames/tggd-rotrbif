Public Interface IEntity
    Sub SetMetadata(metadataType As String, metadataValue As String)
    Sub SetTag(tagType As String)
    Sub SetStatistic(statisticType As String, statisticValue As Integer)
    Function GetMetadata(metadataType As String) As String
    Function GetTag(tagType As String) As Boolean
    Function GetStatistic(statisticType As String) As Integer
    Sub ClearTag(tagType As String)
End Interface
