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
    | East -> "East"
    | South -> "South"
    | West -> "West"