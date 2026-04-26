namespace TGGD.ROTRBIF

type internal Outputter = string -> unit
type internal Inputter = unit -> string

[<RequireQualifiedAccess>]
type internal MetaphorContext = {
        Metaphor  : Metaphor
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
            let state' = state |> StateInitializer.initialize
            {
                Inputter  = inputter
                Outputter = outputter
                State     = state'
                Metaphor  = {State = state'}
            }

        static member sideEffect 
                (sideEffect : MetaphorContext -> unit) 
                (context    : MetaphorContext) 
                : MetaphorContext =
            context 
            |> sideEffect
            context

        static member mutateState
                (transformer : MetaphorState -> MetaphorState)
                (context     : MetaphorContext)
                : MetaphorContext =
            {context with 
                State = 
                    context.State 
                    |> transformer}