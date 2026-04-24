[<RequireQualifiedAccess>]
module internal MetaphorState

type internal ``Metaphor State`` = {
        Alive: bool
        Facing: CardinalDirection.CardinalDirection
    }

let create () : ``Metaphor State`` =
    {
        Alive = true
        Facing = CardinalDirection.CardinalDirection.North
    }
