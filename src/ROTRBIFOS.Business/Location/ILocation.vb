Public Interface ILocation
    Inherits IEntity
    ReadOnly Property LocationId As Guid
    Sub AddCharacter(character As ICharacter)
    Function CreateCharacter(Optional characterInitializer As Action(Of ICharacter) = Nothing) As ICharacter
End Interface
