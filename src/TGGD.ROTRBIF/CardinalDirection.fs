[<RequireQualifiedAccess>]
module internal CardinalDirection

type internal CardinalDirection =
    | North
    | East
    | South
    | West

let internal getName (direction: CardinalDirection) : string =
    match direction with
    | North -> "North"
    | East  -> "East"
    | South -> "South"
    | West  -> "West"

let internal turn (turn:Turn.Turn) (direction: CardinalDirection) : CardinalDirection =
    match turn, direction with
    | Turn.Turn.Left  , CardinalDirection.North -> CardinalDirection.West
    | Turn.Turn.Left  , CardinalDirection.East  -> CardinalDirection.North
    | Turn.Turn.Left  , CardinalDirection.South -> CardinalDirection.East
    | Turn.Turn.Left  , CardinalDirection.West  -> CardinalDirection.South
    | Turn.Turn.Right , CardinalDirection.North -> CardinalDirection.East
    | Turn.Turn.Right , CardinalDirection.East  -> CardinalDirection.South
    | Turn.Turn.Right , CardinalDirection.South -> CardinalDirection.West
    | Turn.Turn.Right , CardinalDirection.West  -> CardinalDirection.North
    | Turn.Turn.Around, CardinalDirection.North -> CardinalDirection.South
    | Turn.Turn.Around, CardinalDirection.East  -> CardinalDirection.West
    | Turn.Turn.Around, CardinalDirection.South -> CardinalDirection.North
    | Turn.Turn.Around, CardinalDirection.West  -> CardinalDirection.East
