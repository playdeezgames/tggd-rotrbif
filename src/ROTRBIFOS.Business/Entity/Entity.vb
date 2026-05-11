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
        EntityData.Statistics(statisticType) =
            Math.Clamp(
                statisticValue,
                GetStatisticMinimum(statisticType),
                GetStatisticMaximum(statisticType))
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
        Return Math.Clamp(
            EntityData.Statistics(statisticType),
            GetStatisticMinimum(statisticType),
            GetStatisticMaximum(statisticType))
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

    Public Sub SetYoke(yokeType As String, identifier As Guid) Implements IEntity.SetYoke
        EntityData.Yokes(yokeType) = identifier
    End Sub

    Public Function HasYoke(yokeType As String) As Boolean Implements IEntity.HasYoke
        Return EntityData.Yokes.ContainsKey(yokeType)
    End Function

    Public Function GetYoke(yokeType As String) As Guid Implements IEntity.GetYoke
        Return EntityData.Yokes(yokeType)
    End Function

    Public Sub ClearYoke(yokeType As String) Implements IEntity.ClearYoke
        EntityData.Yokes.Remove(yokeType)
    End Sub

    Public Function GetStatisticMinimum(statisticType As String) As Integer Implements IEntity.GetStatisticMinimum
        Dim result As Integer = Integer.MinValue
        If Not EntityData.StatisticMinimums.TryGetValue(statisticType, result) Then
            result = Integer.MinValue
        End If
        Return result
    End Function

    Public Sub SetStatisticMinimum(statisticType As String, minimum As Integer) Implements IEntity.SetStatisticMinimum
        EntityData.StatisticMinimums(statisticType) = minimum
    End Sub

    Public Sub SetStatisticMaximum(statisticType As String, maximum As Integer) Implements IEntity.SetStatisticMaximum
        EntityData.StatisticMaximums(statisticType) = maximum
    End Sub

    Public Function GetStatisticMaximum(statisticType As String) As Integer Implements IEntity.GetStatisticMaximum
        Dim result As Integer = Integer.MaxValue
        If Not EntityData.StatisticMaximums.TryGetValue(statisticType, result) Then
            result = Integer.MaxValue
        End If
        Return result
    End Function

    Public MustOverride Sub Destroy() Implements IEntity.Destroy
    Protected MustOverride ReadOnly Property EntityData As TEntity

    Public ReadOnly Property World As IWorld Implements IEntity.World
        Get
            Return Business.World.Create(WorldData)
        End Get
    End Property

    Public MustOverride ReadOnly Property Exists As Boolean Implements IEntity.Exists
End Class
