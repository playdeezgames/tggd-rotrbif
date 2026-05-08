Imports ROTRBIFOS.Business
Imports TGGD.Business

Friend Module InnInitializer
    Friend Sub Initialize(town As List(Of ILocation))
        Dim townLocation = RNG.FromList(town)
        Dim world = townLocation.World
        town.Remove(townLocation)
        Dim blueRoom = world.CreateLocation(InnInitializer.InitializeRoom(townLocation))
    End Sub

    Private Function InitializeRoom(exitDestination As ILocation) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName(Names.JUSDATIP_INN)
                   location.CreateCharacter(AddressOf InitializeGorachan)
                   location.CreateRoute(Direction.Out.GetName, exitDestination)
                   exitDestination.CreateRoute(Direction.In.GetName, location)
               End Sub
    End Function

    Private Sub InitializeGorachan(character As ICharacter)
        character.SetName(Names.GORACHAN)
    End Sub
End Module
