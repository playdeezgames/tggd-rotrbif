[<RequireQualifiedAccess>]
module internal MetaphorContext

type internal Outputter = string -> unit
type internal Inputter = unit -> string

type internal ``Metaphor Context`` = {
        State: MetaphorState.``Metaphor State``
        Inputter: Inputter
        Outputter: Outputter
    }

let create 
        (inputter: Inputter) 
        (outputter:Outputter) 
        (state: MetaphorState.``Metaphor State``)
        : ``Metaphor Context`` =
    {
        Inputter = inputter
        Outputter = outputter
        State = state
    }