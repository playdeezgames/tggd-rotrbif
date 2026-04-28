Public Interface ILocation
    Inherits IEntity
    ReadOnly Property LocationId As Guid
    Sub AddCharacter(character As ICharacter)
End Interface
