Imports ROTRBIFOS.Data

Friend Class Character
    Inherits InventoryEntity(Of CharacterData)
    Implements ICharacter

    Private Sub New(worldData As WorldData, characterId As Guid)
        MyBase.New(worldData)
        Me.CharacterId = characterId
    End Sub

    Friend Shared Function TryFind(worldData As WorldData, characterId As Guid?) As ICharacter
        Return If(
            characterId.HasValue AndAlso worldData.Characters.ContainsKey(characterId.Value),
            New Character(worldData, characterId.Value),
            Nothing)
    End Function

    Public Sub Destroy() Implements ICharacter.Destroy
        Location.RemoveCharacter(Me)
        worldData.Characters.Remove(CharacterId)
    End Sub

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
            If value.LocationId <> Location.LocationId Then
                Location.RemoveCharacter(Me)
                EntityData.LocationId = value.LocationId
                Location.AddCharacter(Me)
            End If
        End Set
    End Property

    Public ReadOnly Property IsAvatar As Boolean Implements ICharacter.IsAvatar
        Get
            Return worldData.AvatarCharacterId.HasValue AndAlso worldData.AvatarCharacterId.Value = CharacterId
        End Get
    End Property

    Public Overrides ReadOnly Property Exists As Boolean
        Get
            Return worldData.Characters.ContainsKey(CharacterId)
        End Get
    End Property
End Class
