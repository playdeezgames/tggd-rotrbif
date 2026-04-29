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
    Private ReadOnly nextFacingTable As IReadOnlyDictionary(Of Turn, IReadOnlyDictionary(Of Direction, Direction)) =
        New Dictionary(Of Turn, IReadOnlyDictionary(Of Direction, Direction)) From
        {
            {
                Turn.Left,
                New Dictionary(Of Direction, Direction) From
                {
                    {Direction.North, Direction.West},
                    {Direction.East, Direction.North},
                    {Direction.South, Direction.East},
                    {Direction.West, Direction.South}
                }
            },
            {
                Turn.Right,
                New Dictionary(Of Direction, Direction) From
                {
                    {Direction.North, Direction.East},
                    {Direction.East, Direction.South},
                    {Direction.South, Direction.West},
                    {Direction.West, Direction.North}
                }
            },
            {
                Turn.Around,
                New Dictionary(Of Direction, Direction) From
                {
                    {Direction.North, Direction.South},
                    {Direction.East, Direction.West},
                    {Direction.South, Direction.North},
                    {Direction.West, Direction.East}
                }
            }
        }
    <Extension>
    Function GetName(turn As Turn) As String
        Return nameTable(turn)
    End Function
    <Extension>
    Function NextFacing(turn As Turn, direction As Direction) As Direction
        Return nextFacingTable(turn)(direction)
    End Function
End Module