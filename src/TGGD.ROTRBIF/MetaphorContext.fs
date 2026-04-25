[<RequireQualifiedAccess>]
module internal MetaphorContext

type internal Outputter = string -> unit
type internal Inputter = unit -> string

type internal ``Metaphor Context`` = {
        State: MetaphorState.MetaphorState
        Inputter: Inputter
        Outputter: Outputter
    }

let create 
        (inputter: Inputter) 
        (outputter:Outputter) 
        (state: MetaphorState.MetaphorState)
        : ``Metaphor Context`` =
    {
        Inputter = inputter
        Outputter = outputter
        State = state
    }

let doSideEffect 
        (sideEffect: ``Metaphor Context`` -> unit) 
        (context: ``Metaphor Context``) 
        : ``Metaphor Context`` =
    context 
    |> sideEffect
    context

let transformState
        (transformer : MetaphorState.MetaphorState -> MetaphorState.MetaphorState)
        (context: ``Metaphor Context``)
        : ``Metaphor Context`` =
    {context with 
        State = context.State |> transformer}