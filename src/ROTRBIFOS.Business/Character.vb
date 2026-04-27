Imports ROTRBIFOS.Data

Friend Class Character
    Implements ICharacter

    Private ReadOnly worldData As WorldData

    Private Sub New(worldData As WorldData, characterId As Guid)
        Me.worldData = worldData
        Me.characterId = characterId
    End Sub

    Friend Shared Function TryFind(worldData As WorldData, characterId As Guid?) As ICharacter
        Return If(characterId.HasValue AndAlso worldData.Characters.ContainsKey(characterId.Value),
            New Character(worldData, characterId.Value), Nothing)
    End Function

    Public Sub SetMetadata(metadataType As String, metadataValue As String) Implements ICharacter.SetMetadata
        CharacterData.Metadatas(metadataType) = metadataValue
    End Sub

    Public Sub SetTag(tagType As String) Implements ICharacter.SetTag
        CharacterData.Tags.Add(tagType)
    End Sub

    Public Sub SetStatistic(statisticType As String, statisticValue As Integer) Implements ICharacter.SetStatistic
        CharacterData.Statistics(statisticType) = statisticValue
    End Sub

    Public Function GetMetadata(metadataType As String) As String Implements ICharacter.GetMetadata
        Return CharacterData.Metadatas(metadataType)
    End Function

    Public Function GetTag(tagType As String) As Boolean Implements ICharacter.GetTag
        Return CharacterData.Tags.Contains(tagType)
    End Function

    Public Function GetStatistic(statisticType As String) As Integer Implements ICharacter.GetStatistic
        Return CharacterData.Statistics(statisticType)
    End Function

    Private ReadOnly Property CharacterData As CharacterData
        Get
            Return worldData.Characters(CharacterId)
        End Get
    End Property

    Public ReadOnly Property CharacterId As Guid Implements ICharacter.CharacterId
End Class
