[<RequireQualifiedAccess>]
module internal InputProcessor

let private ``Show Status`` 
        (context:MetaphorContext.``Metaphor Context``) 
        : unit =
    if context.State.Alive then "Alive" else "Dead"
    |> sprintf "Status: %s"
    |> context.Outputter

let internal ``Process Input`` 
        (input: string) 
        (context: MetaphorContext.``Metaphor Context``) 
        : MetaphorContext.``Metaphor Context`` option =
    match input with
    | "Quit." -> None
    | "Status?" -> 
        context
        |> ``Show Status`` 
        context
        |> Some
    | _ ->
        "[red]INVALID INPUT![/]"
        |> context.Outputter
        context
        |> Some
