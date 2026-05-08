Imports ROTRBIFOS.Business

Friend Module WorldInitializer
    Friend Sub Initialize(world As IWorld)
        Dim town = TownInitializer.Initialize(world)
        BlueRoomInitializer.Initialize(town)
        InnInitializer.Initialize(town)
    End Sub
End Module
