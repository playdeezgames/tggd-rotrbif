Public Interface ICharacter
    Inherits IInventoryEntity
    ReadOnly Property CharacterId As Guid
    Property Location As ILocation
    ReadOnly Property IsAvatar As Boolean
    Sub Destroy()
End Interface
