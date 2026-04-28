Imports ROTRBIFOS.Data

Friend Class Character
    Inherits Entity(Of CharacterData)
    Implements ICharacter

    Private ReadOnly worldData As WorldData

    Private Sub New(worldData As WorldData, characterId As Guid)
        Me.worldData = worldData
        Me.CharacterId = characterId
    End Sub

    Friend Shared Function TryFind(worldData As WorldData, characterId As Guid?) As ICharacter
        Return If(characterId.HasValue AndAlso worldData.Characters.ContainsKey(characterId.Value),
            New Character(worldData, characterId.Value), Nothing)
    End Function

    Public ReadOnly Property CharacterId As Guid Implements ICharacter.CharacterId

    Protected Overrides ReadOnly Property EntityData As CharacterData
        Get
            Return worldData.Characters(CharacterId)
        End Get
    End Property

    Public Property Location As ILocation Implements ICharacter.Location
        Get
            Return Business.Location.TryFind(worldData, EntityData.LocationId)
        End Get
        Set(value As ILocation)
            Throw New NotImplementedException()
        End Set
    End Property
End Class
