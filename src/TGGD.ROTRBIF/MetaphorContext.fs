[<RequireQualifiedAccess>]
module internal MetaphorContext

open TGGD.ROTRBIF

type internal Outputter = string -> unit
type internal Inputter = unit -> string

type internal MetaphorContext = {
        State: MetaphorState
        Inputter: Inputter
        Outputter: Outputter
    }

let create 
        (inputter: Inputter) 
        (outputter:Outputter) 
        (state: MetaphorState)
        : MetaphorContext =
    {
        Inputter = inputter
        Outputter = outputter
        State = state
    }

let doSideEffect 
        (sideEffect: MetaphorContext -> unit) 
        (context: MetaphorContext) 
        : MetaphorContext =
    context 
    |> sideEffect
    context

let transformState
        (transformer : MetaphorState -> MetaphorState)
        (context: MetaphorContext)
        : MetaphorContext =
    {context with 
        State = context.State |> transformer}