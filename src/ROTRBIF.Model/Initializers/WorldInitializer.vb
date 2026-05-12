Imports ROTRBIFOS.Business

Friend Module WorldInitializer
    Friend Sub Initialize(world As IWorld)
        Dim wilderness = WildernessInitializer.Initialize(world)
        Dim town = TownInitializer.Initialize(world, wilderness)
        BlueRoomInitializer.Initialize(town)
        InnInitializer.Initialize(town)
        HealerInitializer.Initialize(town)
    End Sub
End Module
