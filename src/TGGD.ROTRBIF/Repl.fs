module internal Repl

let rec internal mutateMetaphor 
        (context:MetaphorContext.MetaphorContext option) 
        : MetaphorContext.MetaphorContext option =
    match context with
    | Some s -> 
        (s.Inputter(), s)
        ||> InputProcessor.processInput
        |> mutateMetaphor
    | None -> None

