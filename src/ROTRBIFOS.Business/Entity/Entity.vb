Imports ROTRBIFOS.Data

Friend MustInherit Class Entity(Of TEntity As EntityData)
    Implements IEntity

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
        Return EntityData.Metadatas(metadataType)
    End Function

    Public Function GetTag(tagType As String) As Boolean Implements IEntity.GetTag
        Return EntityData.Tags.Contains(tagType)
    End Function

    Public Function GetStatistic(statisticType As String) As Integer Implements IEntity.GetStatistic
        Return EntityData.Statistics(statisticType)
    End Function

    Protected MustOverride ReadOnly Property EntityData As TEntity
End Class
