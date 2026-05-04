Imports ROTRBIFOS.Data

Public Class World
    Implements IWorld

    Private ReadOnly worldData As WorldData

    Private Sub New(worldData As WorldData)
        Me.worldData = worldData
    End Sub

    Public Property Avatar As ICharacter Implements IWorld.Avatar
        Get
            Return Character.TryFind(worldData, worldData.AvatarCharacterId)
        End Get
        Set
            worldData.AvatarCharacterId = Value?.CharacterId
        End Set
    End Property

    Public Sub Clear() Implements IWorld.Clear
        worldData.AvatarCharacterId = Nothing
        worldData.Characters.Clear()
    End Sub

    Public Shared Function Create(worldData As WorldData) As IWorld
        Return New World(worldData)
    End Function

    Public Function CreateLocation(Optional locationInitializer As Action(Of ILocation) = Nothing) As ILocation Implements IWorld.CreateLocation
        Dim locationId = Guid.NewGuid
        worldData.Locations(locationId) = New LocationData
        Dim location = Business.Location.TryFind(worldData, locationId)
        locationInitializer?.Invoke(location)
        Return location
    End Function

    Public Function GetFeature(featureId As Guid) As IFeature Implements IWorld.GetFeature
        Return Feature.TryFind(worldData, featureId)
    End Function
End Class
