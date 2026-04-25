module internal Repl

open TGGD.ROTRBIF

let rec internal mutateMetaphor 
        (context:MetaphorContext option) 
        : MetaphorContext option =
    match context with
    | Some s -> 
        (s.Inputter(), s)
        ||> InputProcessor.processInput
        |> mutateMetaphor
    | None -> None

