Public Interface ICharacter
    Inherits IEntity
    ReadOnly Property CharacterId As Guid
    Property Location As ILocation
End Interface
