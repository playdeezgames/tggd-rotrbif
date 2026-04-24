[<RequireQualifiedAccess>]
module internal InputProcessor

let private ``Show Status`` 
        (context:MetaphorContext.``Metaphor Context``) 
        : unit =
    if context.State.Alive then "Alive" else "Dead"
    |> sprintf "Status: %s"
    |> context.Outputter

type private Subcommand =
    | Quit
    | Status

type private Command =
    | Statement of Subcommand
    | Question of Subcommand
    | Exclamation of Subcommand

let private parseSubcommand(input:string) : Subcommand option =
    match input with
    | "Quit" -> Subcommand.Quit |> Some
    | "Status" -> Subcommand.Status |> Some
    | _ -> None

let private parseCommand (input: string) : Command option =
    let subcommand = 
        input 
        |> Seq.truncate (max 0 (input.Length-1)) 
        |> System.String.Concat 
        |> parseSubcommand
    let ending = 
        input 
        |> Seq.tryLast
    match ending, subcommand with
    | Some '.', Some x -> x |> Command.Statement |> Some
    | Some '?', Some x -> x |> Command.Question |> Some
    | Some '!', Some x -> x |> Command.Exclamation |> Some
    | _ -> None

let private invalidCommand 
        (context:MetaphorContext.``Metaphor Context``)
        : unit =
    "[red]INVALID INPUT![/]"
    |> context.Outputter

let internal ``Process Input`` 
        (input: string) 
        (context: MetaphorContext.``Metaphor Context``) 
        : MetaphorContext.``Metaphor Context`` option =
    match input |> parseCommand with
    | Some command -> 
        match command with
        | Statement s -> 
            match s with
            | Quit -> 
                None
            | _ -> 
                context 
                |> MetaphorContext.doSideEffect invalidCommand
                |> Some
        | Question q -> 
            match q with
            | Status ->
                context 
                |> MetaphorContext.doSideEffect ``Show Status``
                |> Some
            | _ ->
                context
                |> MetaphorContext.doSideEffect invalidCommand
                |> Some
        | _ -> 
            context 
            |> MetaphorContext.doSideEffect invalidCommand
            |> Some
    | _ -> 
        context 
        |> MetaphorContext.doSideEffect invalidCommand
        |> Some
