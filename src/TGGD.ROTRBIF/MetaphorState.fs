namespace TGGD.ROTRBIF

[<RequireQualifiedAccess>]
type internal MetaphorState = {
        Alive: bool
        Facing: CardinalDirection
    }
    with
        static member create () : MetaphorState =
            {
                Alive = true
                Facing = CardinalDirection.North
            }
