Public Interface IWorld
    Property Avatar As ICharacter
    Sub Clear()
    Function CreateCharacter(Optional characterInitializer As Action(Of ICharacter) = Nothing) As ICharacter
End Interface
