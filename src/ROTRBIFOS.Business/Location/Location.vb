Imports ROTRBIFOS.Data

Friend Class Location
    Inherits InventoryEntity(Of LocationData)
    Implements ILocation


    Private Sub New(worldData As WorldData, locationId As Guid)
        MyBase.New(worldData)
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
        Return If(
            locationId.HasValue AndAlso worldData.Locations.ContainsKey(locationId.Value),
            New Location(worldData, locationId.Value),
            Nothing)
    End Function

    Public Function CreateCharacter(Optional characterInitializer As Action(Of ICharacter) = Nothing) As ICharacter Implements ILocation.CreateCharacter
        Dim characterId = Guid.NewGuid
        worldData.Characters(characterId) = New CharacterData With
            {
                .LocationId = LocationId
            }
        Dim character = Business.Character.TryFind(worldData, characterId)
        AddCharacter(character)
        characterInitializer?.Invoke(character)
        Return character
    End Function
End Class
