Public Interface IEntity
    Sub SetMetadata(metadataType As String, metadataValue As String)
    Sub SetTag(tagType As String)
    Sub SetStatistic(statisticType As String, statisticValue As Integer)
    Function GetMetadata(metadataType As String) As String
    Function GetTag(tagType As String) As Boolean
    Function GetStatistic(statisticType As String) As Integer
    Sub ClearTag(tagType As String)
    ReadOnly Property World As IWorld
    Function ChangeStatistic(statisticType As String, delta As Integer) As Integer
    Function HasStatistic(statisticType As String) As Boolean
    Sub SetYoke(yokeType As String, identifier As Guid)
    Function HasYoke(yokeType As String) As Boolean
    Function GetYoke(yokeType As String) As Guid
    Sub ClearYoke(yokeType As String)
End Interface
