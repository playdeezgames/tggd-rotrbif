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

    Public Function ChangeStatistic(statisticType As String, delta As Integer) As Integer Implements IEntity.ChangeStatistic
        SetStatistic(statisticType, GetStatistic(statisticType) + delta)
        Return GetStatistic(statisticType)
    End Function

    Public Function HasStatistic(statisticType As String) As Boolean Implements IEntity.HasStatistic
        Return EntityData.Statistics.ContainsKey(statisticType)
    End Function

    Protected MustOverride ReadOnly Property EntityData As TEntity

    Public ReadOnly Property World As IWorld Implements IEntity.World
        Get
            Return Business.World.Create(WorldData)
        End Get
    End Property
End Class
