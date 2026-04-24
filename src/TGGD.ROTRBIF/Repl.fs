module internal Repl

type internal Outputter = string -> unit
type internal Inputter = unit -> string

type internal ``Metaphor Context`` = {
        Alive: bool
        Inputter: Inputter
        Outputter: Outputter
    }

let private ``Show Status`` 
        (context:``Metaphor Context``) 
        : unit =
    if context.Alive then "Alive" else "Dead"
    |> sprintf "Status: %s"
    |> context.Outputter

let private ``Process Input`` 
        (input: string) 
        (context: ``Metaphor Context``) 
        : ``Metaphor Context`` option =
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

let rec internal ``Game Loop`` 
        (context:``Metaphor Context`` option) 
        : ``Metaphor Context`` option =
    match context with
    | Some s -> 
        (s.Inputter(), s)
        ||> ``Process Input``
        |> ``Game Loop``
    | None -> None

