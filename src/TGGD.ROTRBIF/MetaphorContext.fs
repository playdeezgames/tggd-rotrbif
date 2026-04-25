namespace TGGD.ROTRBIF

type internal Outputter = string -> unit
type internal Inputter = unit -> string

[<RequireQualifiedAccess>]
type internal MetaphorContext = {
        State     : MetaphorState
        Inputter  : Inputter
        Outputter : Outputter
    }
    with
        static member create 
                (inputter  : Inputter) 
                (outputter : Outputter) 
                (state     : MetaphorState)
                : MetaphorContext =
            {
                Inputter  = inputter
                Outputter = outputter
                State     = state
            }
        static member doSideEffect 
                (sideEffect : MetaphorContext -> unit) 
                (context    : MetaphorContext) 
                : MetaphorContext =
            context 
            |> sideEffect
            context
        static member transformState
                (transformer : MetaphorState -> MetaphorState)
                (context     : MetaphorContext)
                : MetaphorContext =
            {context with 
                State = 
                    context.State 
                    |> transformer}