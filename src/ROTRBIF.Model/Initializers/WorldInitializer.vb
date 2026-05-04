Imports ROTRBIFOS.Business

Friend Module WorldInitializer
    Friend Sub Initialize(world As IWorld)
        Dim blueRoom = world.CreateLocation(AddressOf BlueRoomInitializer.Initialize)
        world.CreateLocation(LoftInitializer.Initialize(blueRoom))
        Dim nextRoom = world.CreateLocation(NextRoomInitializer.Initialize(blueRoom))
    End Sub
End Module
