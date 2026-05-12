Imports ROTRBIFOS.Business
Imports TGGD.Business

Friend Module WildernessInitializer
    Const WILDERNESS_LOCATION_COUNT = 25
    Private ReadOnly directions As IReadOnlyList(Of Direction) =
        New List(Of Direction) From
        {
            Direction.North,
            Direction.East,
            Direction.South,
            Direction.West
        }
    Friend Function Initialize(world As IWorld) As List(Of ILocation)
        Dim result As New List(Of ILocation) From {
            world.CreateLocation(AddressOf InitializeWildernessLocation)
        }
        While result.Count < WILDERNESS_LOCATION_COUNT
            Dim fromLocation = RNG.FromList(result)
            Dim direction = RNG.FromList(directions)
            If Not fromLocation.HasRoute(direction.GetName) Then
                Dim toLocation = world.CreateLocation(AddressOf InitializeWildernessLocation)
                fromLocation.CreateRoute(direction.GetName, toLocation)
                toLocation.CreateRoute(direction.GetOpposite.GetName, fromLocation)
                result.Add(toLocation)
            End If
        End While
        Return result
    End Function

    Private Sub InitializeWildernessLocation(location As ILocation)
        location.SetName("wilderness")
        location.SetWanderingMonsters(WanderingMonsters.WILDERNESS)
    End Sub
End Module
