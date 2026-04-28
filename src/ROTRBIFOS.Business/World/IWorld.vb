Public Interface IWorld
    Property Avatar As ICharacter
    Sub Clear()
    Function CreateCharacter(location As ILocation, Optional characterInitializer As Action(Of ICharacter) = Nothing) As ICharacter
    Function CreateLocation(Optional locationInitializer As Action(Of ILocation) = Nothing) As ILocation
End Interface
