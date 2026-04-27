Imports System.Runtime.CompilerServices

Public Enum Turn
    Left
    Right
    Around
End Enum

Public Module TurnExtensions
    Private ReadOnly nameTable As IReadOnlyDictionary(Of Turn, String) =
        New Dictionary(Of Turn, String) From
        {
            {Turn.Left, "left"},
            {Turn.Right, "right"},
            {Turn.Around, "around"}
        }
    Private ReadOnly nextFacingTable As IReadOnlyDictionary(Of Turn, IReadOnlyDictionary(Of CardinalDirection, CardinalDirection)) =
        New Dictionary(Of Turn, IReadOnlyDictionary(Of CardinalDirection, CardinalDirection)) From
        {
            {
                Turn.Left,
                New Dictionary(Of CardinalDirection, CardinalDirection) From
                {
                    {CardinalDirection.North, CardinalDirection.West},
                    {CardinalDirection.East, CardinalDirection.North},
                    {CardinalDirection.South, CardinalDirection.East},
                    {CardinalDirection.West, CardinalDirection.South}
                }
            },
            {
                Turn.Right,
                New Dictionary(Of CardinalDirection, CardinalDirection) From
                {
                    {CardinalDirection.North, CardinalDirection.East},
                    {CardinalDirection.East, CardinalDirection.South},
                    {CardinalDirection.South, CardinalDirection.West},
                    {CardinalDirection.West, CardinalDirection.North}
                }
            },
            {
                Turn.Around,
                New Dictionary(Of CardinalDirection, CardinalDirection) From
                {
                    {CardinalDirection.North, CardinalDirection.South},
                    {CardinalDirection.East, CardinalDirection.West},
                    {CardinalDirection.South, CardinalDirection.North},
                    {CardinalDirection.West, CardinalDirection.East}
                }
            }
        }
    <Extension>
    Function GetName(turn As Turn) As String
        Return nameTable(turn)
    End Function
    <Extension>
    Function NextFacing(turn As Turn, direction As CardinalDirection) As CardinalDirection
        Return nextFacingTable(turn)(direction)
    End Function
End Module