Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Public Module WorldInitializerExtensions
    <Extension>
    Public Sub Initialize(world As IWorld)
        world.Clear()
        Dim blueRoom = world.CreateLocation(AddressOf InitializeBlueRoom)
    End Sub

    Private Sub InitializeBlueRoom(location As ILocation)
        location.SetName("the blue room")
        location.World.Avatar = location.CreateCharacter(AddressOf InitializeN00b)
        location.CreateRoute(Direction.North.GetName(), location, AddressOf InitializeBlueRoomNorthDoor)
    End Sub

    Private Sub InitializeBlueRoomNorthDoor(route As IRoute)
    End Sub

    Private Sub InitializeN00b(character As ICharacter)
        character.SetName("N00b")
        character.SetAlive()
        character.SetFacing(Direction.North)
        character.SetCheckCount(0)
        character.SetTag(Tags.HAS_ASS_KEY)
    End Sub
End Module
