Imports ROTRBIFOS.Business

Friend Module TownInitializer
    Const TOWN_COLUMNS = 3
    Const TOWN_ROWS = 3
    ReadOnly directions As IReadOnlyList(Of Direction) =
        New List(Of Direction) From
        {
            Direction.North,
            Direction.East,
            Direction.South,
            Direction.West
        }
    Friend Function Initialize(world As IWorld) As List(Of ILocation)
        Const TOWN_LOCATION_COUNT = TOWN_COLUMNS * TOWN_ROWS
        Dim result As List(Of ILocation) =
            Enumerable.Range(0, TOWN_LOCATION_COUNT).
            Select(Function(x) world.CreateLocation(InitializeTownLocation(x))).ToList
        For Each townLocation In result
            Dim column = townLocation.GetStatistic(Statistics.TOWN_COLUMN)
            Dim row = townLocation.GetStatistic(Statistics.TOWN_ROW)
            For Each direction In directions
                Dim nextColumn = direction.GetNextColumn(column)
                Dim nextRow = direction.GetNextRow(row)
                Dim nextLocation = result.SingleOrDefault(Function(x) x.GetStatistic(Statistics.TOWN_COLUMN) = nextColumn AndAlso x.GetStatistic(Statistics.TOWN_ROW) = nextRow)
                If nextLocation IsNot Nothing Then
                    townLocation.CreateRoute(direction.GetName, nextLocation)
                End If
            Next
        Next
        Return result
    End Function

    Private Function InitializeTownLocation(index As Integer) As Action(Of ILocation)
        Return Sub(location)
                   Dim column = index Mod TOWN_COLUMNS
                   Dim row = index \ TOWN_COLUMNS
                   location.SetStatistic(Statistics.TOWN_COLUMN, column)
                   location.SetStatistic(Statistics.TOWN_ROW, row)
                   location.SetName($"Town Location ({column},{row})")
               End Sub
    End Function
End Module
