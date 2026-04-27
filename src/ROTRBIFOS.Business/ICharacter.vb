Public Interface ICharacter
    ReadOnly Property CharacterId As Guid
    Sub SetMetadata(metadataType As String, metadataValue As String)
    Sub SetTag(tagType As String)
    Sub SetStatistic(statisticType As String, statisticValue As Integer)
    Function GetMetadata(metadataType As String) As String
    Function GetTag(tagType As String) As Boolean
    Function GetStatistic(statisticType As String) As Integer
End Interface
