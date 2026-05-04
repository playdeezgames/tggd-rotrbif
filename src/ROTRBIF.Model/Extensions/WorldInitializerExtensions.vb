Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Public Module WorldInitializerExtensions
    <Extension>
    Public Sub Initialize(world As IWorld)
        world.Clear()
        Dim blueRoom = world.CreateLocation(AddressOf InitializeBlueRoom)
        world.CreateLocation(InitializeLoft(blueRoom))
        Dim nextRoom = world.CreateLocation(InitializeNextRoom(blueRoom))
    End Sub

    Private Function InitializeLoft(blueRoom As ILocation) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName(Names.THE_LOFT)
                   location.CreateRoute("down", blueRoom)
                   blueRoom.CreateRoute("up", location)
                   location.CreateFeature(AddressOf InitializeLoftCrate)
               End Sub
    End Function

    Private Sub InitializeLoftCrate(feature As IFeature)
        feature.SetName(Names.CRATE)
    End Sub

    Private Function InitializeNextRoom(blueRoom As ILocation) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName(Names.THE_NEXT_ROOM)
                   blueRoom.CreateRoute(
                        Direction.North.GetName(),
                        location,
                        Sub(r)
                            r.SetTag(Tags.IS_LOCKED)
                            r.SetMetadata(Metadatas.KEY_TYPE, KeyTypes.ASS_KEY)
                        End Sub)
                   location.CreateRoute(Direction.South.GetName(), blueRoom)
               End Sub
    End Function

    Private Sub InitializeBlueRoom(location As ILocation)
        location.SetName(Names.THE_BLUE_ROOM)
        location.World.Avatar = location.CreateCharacter(AddressOf InitializeN00b)
        location.Inventory.CreateItem(AddressOf InitializeYlioppilaslakki)
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
