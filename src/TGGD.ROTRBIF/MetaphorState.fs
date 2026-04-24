[<RequireQualifiedAccess>]
module internal MetaphorState

type internal ``Metaphor State`` = {
        Alive: bool
    }

let create () : ``Metaphor State`` =
    {
        Alive = true
    }
