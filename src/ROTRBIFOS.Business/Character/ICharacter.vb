Public Interface ICharacter
    Inherits IInventoryEntity
    ReadOnly Property CharacterId As Guid
    Property Location As ILocation
End Interface
