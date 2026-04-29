Public Interface IWorld
    Property Avatar As ICharacter
    Sub Clear()
    Function CreateLocation(Optional locationInitializer As Action(Of ILocation) = Nothing) As ILocation
End Interface
