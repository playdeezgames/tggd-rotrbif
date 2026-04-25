namespace TGGD.ROTRBIF

[<RequireQualifiedAccess>]
type internal CardinalDirection =
    | North
    | East
    | South
    | West
    member internal this.Name = 
        match this with
        | North -> "North"
        | East  -> "East"
        | South -> "South"
        | West  -> "West"
    static member turn (turn:Turn) (direction: CardinalDirection) : CardinalDirection =
        match turn, direction with
        | Turn.Left  , CardinalDirection.North -> CardinalDirection.West
        | Turn.Left  , CardinalDirection.East  -> CardinalDirection.North
        | Turn.Left  , CardinalDirection.South -> CardinalDirection.East
        | Turn.Left  , CardinalDirection.West  -> CardinalDirection.South
        | Turn.Right , CardinalDirection.North -> CardinalDirection.East
        | Turn.Right , CardinalDirection.East  -> CardinalDirection.South
        | Turn.Right , CardinalDirection.South -> CardinalDirection.West
        | Turn.Right , CardinalDirection.West  -> CardinalDirection.North
        | Turn.Around, CardinalDirection.North -> CardinalDirection.South
        | Turn.Around, CardinalDirection.East  -> CardinalDirection.West
        | Turn.Around, CardinalDirection.South -> CardinalDirection.North
        | Turn.Around, CardinalDirection.West  -> CardinalDirection.East
