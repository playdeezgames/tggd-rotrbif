[<RequireQualifiedAccess>]
module internal MetaphorState

open TGGD.ROTRBIF

type internal MetaphorState = {
        Alive: bool
        Facing: CardinalDirection
    }

let create () : MetaphorState =
    {
        Alive = true
        Facing = CardinalDirection.North
    }
