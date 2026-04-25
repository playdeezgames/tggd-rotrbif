[<RequireQualifiedAccess>]
module internal CardinalDirection

open TGGD.ROTRBIF

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

let internal turn (turn:Turn) (direction: CardinalDirection) : CardinalDirection =
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
