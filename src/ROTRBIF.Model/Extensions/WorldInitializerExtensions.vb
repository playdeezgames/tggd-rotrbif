Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Public Module WorldInitializerExtensions
    <Extension>
    Public Sub Initialize(world As IWorld)
        world.Clear()
        Dim blueRoom = world.CreateLocation(AddressOf InitializeBlueRoom)
    End Sub

    Private Sub InitializeBlueRoom(location As ILocation)
        location.SetName("The Blue Room")
        location.World.Avatar = location.CreateCharacter(AddressOf InitializeN00b)
    End Sub

    Private Sub InitializeN00b(character As ICharacter)
        character.SetName("N00b")
        character.SetAlive()
        character.SetFacing(Direction.North)
        character.SetCheckCount(0)
    End Sub
End Module
