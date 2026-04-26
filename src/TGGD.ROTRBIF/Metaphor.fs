namespace TGGD.ROTRBIF

type internal Metaphor =
    {
        State : MetaphorState
    }
    with 
        static member mutateState 
                (transformer : MetaphorState -> MetaphorState) 
                (metaphor    : Metaphor) =
            {metaphor with State = metaphor.State |> transformer}
