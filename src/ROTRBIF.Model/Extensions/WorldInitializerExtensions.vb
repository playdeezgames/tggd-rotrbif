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
                   location.SetName("the loft")
                   location.CreateRoute("down", blueRoom)
                   blueRoom.CreateRoute("up", location)
                   location.CreateFeature(AddressOf InitializeLoftCrate)
               End Sub
    End Function

    Private Sub InitializeLoftCrate(feature As IFeature)
        feature.SetName("crate")
    End Sub

    Private Function InitializeNextRoom(blueRoom As ILocation) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName("the next room")
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
        location.SetName("the blue room")
        location.World.Avatar = location.CreateCharacter(AddressOf InitializeN00b)
        location.Inventory.CreateItem(AddressOf InitializeYlioppilaslakki)
    End Sub

    Private Sub InitializeYlioppilaslakki(item As IItem)
        item.SetName("ylioppilaslakki")
    End Sub

    Private Sub InitializeN00b(character As ICharacter)
        character.SetName("N00b")
        character.SetAlive()
        character.SetFacing(Direction.North)
        character.SetCheckCount(0)
        character.SetTag(Tags.HAS_ASS_KEY)
    End Sub
End Module
