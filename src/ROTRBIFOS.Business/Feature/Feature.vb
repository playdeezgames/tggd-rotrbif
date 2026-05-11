Imports ROTRBIFOS.Data

Friend Class Feature
    Inherits Entity(Of FeatureData)
    Implements IFeature

    Private Sub New(worldData As Data.WorldData, featureId As Guid)
        MyBase.New(worldData)
        Me.FeatureId = featureId
    End Sub

    Public ReadOnly Property FeatureId As Guid Implements IFeature.FeatureId

    Public Overrides ReadOnly Property Exists As Boolean
        Get
            Return worldData.Features.ContainsKey(FeatureId)
        End Get
    End Property

    Protected Overrides ReadOnly Property EntityData As FeatureData
        Get
            Return worldData.Features(FeatureId)
        End Get
    End Property

    Public Overrides Sub Destroy()
        worldData.Features.Remove(FeatureId)
    End Sub

    Friend Shared Function TryFind(worldData As WorldData, featureId As Guid?) As IFeature
        Return If(
            featureId.HasValue AndAlso worldData.Features.ContainsKey(featureId.Value),
            New Feature(worldData, featureId.Value),
            Nothing)
    End Function
End Class
