[<RequireQualifiedAccess>]
module internal MetaphorState

type internal MetaphorState = {
        Alive: bool
        Facing: CardinalDirection.CardinalDirection
    }

let create () : MetaphorState =
    {
        Alive = true
        Facing = CardinalDirection.CardinalDirection.North
    }
