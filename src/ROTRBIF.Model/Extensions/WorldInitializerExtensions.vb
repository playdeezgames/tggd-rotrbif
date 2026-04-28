Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Public Module WorldInitializerExtensions
    <Extension>
    Public Sub Initialize(world As IWorld)
        world.Clear()
        Dim blueRoom = world.CreateLocation(AddressOf InitializeBlueRoom)
        world.Avatar = world.CreateCharacter(blueRoom, AddressOf InitializeN00b)
    End Sub

    Private Sub InitializeBlueRoom(location As ILocation)
        location.SetName("The Blue Room")
    End Sub

    Private Sub InitializeN00b(character As ICharacter)
        character.SetName("N00b")
        character.SetAlive()
        character.SetFacing(CardinalDirection.North)
    End Sub
End Module
