Imports ROTRBIFOS.Business
Imports TGGD.Business

Friend Module BlueRoomInitializer

    Friend Sub Initialize(town As List(Of ILocation))
        Dim townLocation = RNG.FromList(town)
        Dim world = townLocation.World
        town.Remove(townLocation)
        Dim blueRoom = world.CreateLocation(BlueRoomInitializer.InitializeRoom(townLocation))
        world.CreateLocation(LoftInitializer.Initialize(blueRoom))
    End Sub

    Private Function InitializeRoom(exitDestination As ILocation) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName(Names.THE_BLUE_ROOM)
                   location.World.Avatar = location.CreateCharacter(AddressOf InitializeN00b)
                   location.Inventory.CreateItem(AddressOf InitializeYlioppilaslakki)
                   location.CreateFeature(AddressOf InitializeBed)
                   location.CreateRoute(Direction.Out.GetName, exitDestination)
                   exitDestination.CreateRoute(Direction.In.GetName, location)
               End Sub
    End Function

    Private Sub InitializeBed(feature As IFeature)
        feature.SetName(Names.BED)
        feature.SetObjectIdentifier(ObjectIdentifier.BED)
    End Sub

    Private Sub InitializeYlioppilaslakki(item As IItem)
        item.SetName(Names.YLIOPPILASLAKKI)
    End Sub

    Private Sub InitializeN00b(character As ICharacter)
        character.SetName(Names.N00B)
        character.SetAlive()
        character.SetFacing(Direction.North)
        character.SetCheckCount(0)
        character.SetTag(Tags.HAS_ASS_KEY)
        character.SetStatistic(Statistics.JOOLS, 0)
    End Sub
End Module
