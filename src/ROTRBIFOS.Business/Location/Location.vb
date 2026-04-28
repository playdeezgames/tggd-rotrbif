Imports ROTRBIFOS.Data

Friend Class Location
    Inherits Entity(Of LocationData)
    Implements ILocation

    Private ReadOnly worldData As WorldData

    Private Sub New(worldData As WorldData, locationId As Guid)
        Me.worldData = worldData
        Me.LocationId = locationId
    End Sub

    Public ReadOnly Property LocationId As Guid Implements ILocation.LocationId

    Protected Overrides ReadOnly Property EntityData As LocationData
        Get
            Return worldData.Locations(LocationId)
        End Get
    End Property

    Public Sub AddCharacter(character As ICharacter) Implements ILocation.AddCharacter
        EntityData.CharacterIds.Add(character.CharacterId)
    End Sub

    Friend Shared Function TryFind(worldData As WorldData, locationId As Guid?) As ILocation
        Return If(locationId.HasValue AndAlso worldData.Locations.ContainsKey(locationId.Value),
            New Location(worldData, locationId.Value), Nothing)
    End Function
End Class
