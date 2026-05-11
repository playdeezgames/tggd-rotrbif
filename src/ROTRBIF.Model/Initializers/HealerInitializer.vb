Imports ROTRBIFOS.Business
Imports TGGD.Business

Friend Module HealerInitializer
    Friend Sub Initialize(town As List(Of ILocation))
        Dim townLocation = RNG.FromList(town)
        Dim world = townLocation.World
        town.Remove(townLocation)
        world.CreateLocation(HealerInitializer.InitializeRoom(townLocation))
    End Sub

    Private Function InitializeRoom(exitDestination As ILocation) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName(Names.HEALER_HOUSE)
                   location.CreateCharacter(AddressOf InitializeMårten)
                   location.CreateRoute(Direction.Out.GetName, exitDestination)
                   exitDestination.CreateRoute(Direction.In.GetName, location)
#If DEBUG Then
                   location.World.Avatar = location.CreateCharacter(AddressOf InitializeN00b)
#End If
               End Sub
    End Function

    Private Sub InitializeMårten(character As ICharacter)
        character.SetName(Names.MÅRTEN)
        character.SetObjectIdentifier(ObjectIdentifier.HEALER)
    End Sub
End Module
