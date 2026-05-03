Imports ROTRBIFOS.Data

Friend MustInherit Class Entity(Of TEntity As EntityData)
    Implements IEntity

    Protected ReadOnly worldData As WorldData

    Protected Sub New(worldData As WorldData)
        Me.worldData = worldData
    End Sub

    Public Sub SetMetadata(metadataType As String, metadataValue As String) Implements IEntity.SetMetadata
        EntityData.Metadatas(metadataType) = metadataValue
    End Sub

    Public Sub SetTag(tagType As String) Implements IEntity.SetTag
        EntityData.Tags.Add(tagType)
    End Sub

    Public Sub SetStatistic(statisticType As String, statisticValue As Integer) Implements IEntity.SetStatistic
        EntityData.Statistics(statisticType) = statisticValue
    End Sub

    Public Function GetMetadata(metadataType As String) As String Implements IEntity.GetMetadata
        Dim metadataValue As String = Nothing
        If EntityData.Metadatas.TryGetValue(metadataType, metadataValue) Then
            Return metadataValue
        End If
        Return Nothing
    End Function

    Public Function GetTag(tagType As String) As Boolean Implements IEntity.GetTag
        Return EntityData.Tags.Contains(tagType)
    End Function

    Public Function GetStatistic(statisticType As String) As Integer Implements IEntity.GetStatistic
        Return EntityData.Statistics(statisticType)
    End Function

    Public Sub ClearTag(tagType As String) Implements IEntity.ClearTag
        EntityData.Tags.Remove(tagType)
    End Sub

    Public Function HasTrigger(triggerType As String) As Boolean Implements IEntity.HasTrigger
        Return EntityData.Triggers.ContainsKey(triggerType)
    End Function

    Public Function GetTrigger(triggerType As String) As ITrigger Implements IEntity.GetTrigger
        Dim triggerId As Guid
        If EntityData.Triggers.TryGetValue(triggerType, triggerId) Then
            Return Trigger.TryFind(worldData, triggerId)
        End If
        Return Nothing
    End Function

    Public Sub SetTrigger(triggerType As String, trigger As ITrigger) Implements IEntity.SetTrigger
        If trigger IsNot Nothing Then
            EntityData.Triggers(triggerType) = trigger.TriggerId
        Else
            EntityData.Triggers.Remove(triggerType)
        End If
    End Sub

    Public Function CreateTrigger(triggerType As String, Optional triggerInitializer As Action(Of ITrigger) = Nothing) As ITrigger Implements IEntity.CreateTrigger
        Dim triggerId = Guid.NewGuid
        worldData.Triggers(triggerId) = New TriggerData
        Dim trigger = Business.Trigger.TryFind(worldData, triggerId)
        SetTrigger(triggerType, trigger)
        triggerInitializer?.Invoke(trigger)
        Return trigger
    End Function

    Protected MustOverride ReadOnly Property EntityData As TEntity

    Public ReadOnly Property World As IWorld Implements IEntity.World
        Get
            Return Business.World.Create(WorldData)
        End Get
    End Property
End Class
