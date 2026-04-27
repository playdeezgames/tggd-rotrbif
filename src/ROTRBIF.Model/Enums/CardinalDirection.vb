Imports System.Runtime.CompilerServices

Public Enum CardinalDirection
    North
    East
    South
    West
End Enum

Public Module CardinalDirectionExtensions
    Private ReadOnly nameTable As IReadOnlyDictionary(Of CardinalDirection, String) =
        New Dictionary(Of CardinalDirection, String) From
        {
            {CardinalDirection.North, "north"},
            {CardinalDirection.East, "east"},
            {CardinalDirection.South, "south"},
            {CardinalDirection.West, "west"}
        }
    <Extension>
    Function GetName(direction As CardinalDirection) As String
        Return nameTable(direction)
    End Function
End Module