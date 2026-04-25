namespace TGGD.ROTRBIF

[<AutoOpen>]
module ReplExtensions =
    type MetaphorContext with
        static member internal mutate
                (context:MetaphorContext option) 
                : MetaphorContext option =
            match context with
            | Some s -> 
                (s.Inputter(), s)
                ||> InputProcessor.processInput
                |> MetaphorContext.mutate
            | None -> None

