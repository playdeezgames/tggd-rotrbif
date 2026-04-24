module internal Repl

let rec internal ``Game Loop`` 
        (context:MetaphorContext.``Metaphor Context`` option) 
        : MetaphorContext.``Metaphor Context`` option =
    match context with
    | Some s -> 
        (s.Inputter(), s)
        ||> InputProcessor.``Process Input``
        |> ``Game Loop``
    | None -> None

