Imports ROTRBIFOS.Business
Imports TGGD.Business

Friend Module InnInitializer
    Friend Sub Initialize(town As List(Of ILocation))
        Dim townLocation = RNG.FromList(town)
        Dim world = townLocation.World
        town.Remove(townLocation)
        world.CreateLocation(InnInitializer.InitializeRoom(townLocation))
    End Sub

    Private Function InitializeRoom(exitDestination As ILocation) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName(Names.JUSDATIP_INN)
                   location.CreateCharacter(AddressOf InitializeGorachan)
                   location.CreateRoute(Direction.Out.GetName, exitDestination)
                   exitDestination.CreateRoute(Direction.In.GetName, location)
                   location.World.CreateLocation(CreateCellar(location))
#If DEBUG Then
                   location.World.Avatar = location.CreateCharacter(AddressOf InitializeN00b)
#End If
               End Sub
    End Function

    Private Function CreateCellar(inn As ILocation) As Action(Of ILocation)
        Return Sub(cellar)
                   cellar.SetName(Names.CELLAR)
                   cellar.SetObjectIdentifier(ObjectIdentifier.CELLAR)
                   cellar.CreateRoute(Direction.Up.GetName, inn)
                   inn.CreateRoute(Direction.Down.GetName, cellar, AddressOf CreateCellarDoor)
               End Sub
    End Function

    Private Sub CreateCellarDoor(route As IRoute)
        route.SetTag(Tags.IS_LOCKED)
        route.SetMetadata(Metadatas.KEY_TYPE, KeyTypes.CELLAR_KEY)
    End Sub

    Private Sub InitializeGorachan(character As ICharacter)
        character.SetName(Names.GORACHAN)
        character.SetObjectIdentifier(ObjectIdentifier.GORACHAN)
    End Sub
End Module
