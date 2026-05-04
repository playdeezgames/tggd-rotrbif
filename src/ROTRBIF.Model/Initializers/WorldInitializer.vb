Imports ROTRBIFOS.Business
Imports TGGD.Business

Friend Module WorldInitializer
    Friend Sub Initialize(world As IWorld)
        Dim town = TownInitializer.Initialize(world)
        Dim townLocation = RNG.FromList(town)
        town.Remove(townLocation)
        Dim blueRoom = world.CreateLocation(BlueRoomInitializer.Initialize(townLocation))
        world.CreateLocation(LoftInitializer.Initialize(blueRoom))
    End Sub
End Module
