Public Interface IEntity
    Sub SetMetadata(metadataType As String, metadataValue As String)
    Sub SetTag(tagType As String)
    Sub SetStatistic(statisticType As String, statisticValue As Integer)
    Function GetMetadata(metadataType As String) As String
    Function GetTag(tagType As String) As Boolean
    Function GetStatistic(statisticType As String) As Integer
    Sub ClearTag(tagType As String)
    ReadOnly Property World As IWorld
    Function HasTrigger(triggerType As String) As Boolean
    Function GetTrigger(triggerType As String) As ITrigger
    Sub SetTrigger(triggerType As String, trigger As ITrigger)
    Function CreateTrigger(triggerType As String, Optional triggerInitializer As Action(Of ITrigger) = Nothing) As ITrigger
End Interface
