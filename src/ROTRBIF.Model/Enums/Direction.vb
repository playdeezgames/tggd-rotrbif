Imports System.Runtime.CompilerServices

Public Enum Direction
    North
    East
    South
    West
    [In]
    Out
    Up
    Down
End Enum

Public Module DirectionExtensions
    Private ReadOnly nameTable As IReadOnlyDictionary(Of Direction, String) =
        New Dictionary(Of Direction, String) From
        {
            {Direction.North, "north"},
            {Direction.East, "east"},
            {Direction.South, "south"},
            {Direction.West, "west"},
            {Direction.In, "in"},
            {Direction.Out, "out"},
            {Direction.Up, "up"},
            {Direction.Down, "down"}
        }
    Private ReadOnly deltaXTable As IReadOnlyDictionary(Of Direction, Integer) =
        New Dictionary(Of Direction, Integer) From
        {
            {Direction.North, 0},
            {Direction.East, 1},
            {Direction.South, 0},
            {Direction.West, -1},
            {Direction.In, 0},
            {Direction.Out, 0},
            {Direction.Up, 0},
            {Direction.Down, 0}
        }
    Private ReadOnly deltaYTable As IReadOnlyDictionary(Of Direction, Integer) =
        New Dictionary(Of Direction, Integer) From
        {
            {Direction.North, -1},
            {Direction.East, 0},
            {Direction.South, 1},
            {Direction.West, 0},
            {Direction.In, 0},
            {Direction.Out, 0},
            {Direction.Up, 0},
            {Direction.Down, 0}
        }
    <Extension>
    Function GetName(direction As Direction) As String
        Return nameTable(direction)
    End Function
    <Extension>
    Function GetNextColumn(direction As Direction, column As Integer) As Integer
        Return column + deltaXTable(direction)
    End Function
    <Extension>
    Function GetNextRow(direction As Direction, row As Integer) As Integer
        Return row + deltaYTable(direction)
    End Function
End Module