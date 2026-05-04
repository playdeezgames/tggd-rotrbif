Imports System.Runtime.CompilerServices

Public Enum Direction
    North
    East
    South
    West
    [In]
    Out
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
            {Direction.Out, "out"}
        }
    <Extension>
    Function GetName(direction As Direction) As String
        Return nameTable(direction)
    End Function
End Module