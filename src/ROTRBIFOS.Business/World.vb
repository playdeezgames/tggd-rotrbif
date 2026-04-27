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

    Public Function CreateCharacter(Optional characterInitializer As Action(Of ICharacter) = Nothing) As ICharacter Implements IWorld.CreateCharacter
        Dim characterId = Guid.NewGuid
        worldData.Characters(characterId) = New CharacterData
        Dim character = Business.Character.TryFind(worldData, characterId)
        characterInitializer?.Invoke(character)
        Return character
    End Function
End Class
