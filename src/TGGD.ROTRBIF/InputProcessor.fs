[<RequireQualifiedAccess>]
module internal InputProcessor

open System

type private Subcommand =
    | Quit
    | Status
    | Turn of Turn.Turn

type private Command =
    | Statement of Subcommand
    | Question of Subcommand
    | Exclamation of Subcommand

let private parseTurn (tokens: string list) : Turn.Turn option=
    match tokens with
    | ["left"]   -> Turn.Turn.Left   |> Some
    | ["right"]  -> Turn.Turn.Right  |> Some
    | ["around"] -> Turn.Turn.Around |> Some
    | _          -> None

let private parseSubcommand(input:string) : Subcommand option =
    let tokens = input.Split([|' '|], StringSplitOptions.RemoveEmptyEntries) |> Array.toList
    match tokens with
    | ["Quit"] -> 
        Subcommand.Quit 
        |> Some
    | ["Status"] -> 
        Subcommand.Status 
        |> Some
    | "Turn" :: subtokens -> 
        subtokens 
        |> parseTurn 
        |> Option.map Subcommand.Turn
    | _ -> 
        None

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
    | Some '.', Some x -> x |> Command.Statement   |> Some
    | Some '?', Some x -> x |> Command.Question    |> Some
    | Some '!', Some x -> x |> Command.Exclamation |> Some
    | _                -> None

// ^ Parsing
/////////////////////////////////////
// v Handling

let private showStatus 
        (context:MetaphorContext.MetaphorContext) 
        : unit =
    if context.State.Alive then "Alive" else "Dead"
    |> sprintf "Status: %s"
    |> context.Outputter

    context.State.Facing
    |> CardinalDirection.getName
    |> sprintf "Facing: %s"
    |> context.Outputter

let private executeTurn 
        (turn:Turn.Turn) 
        (state:MetaphorState.MetaphorState) 
        : MetaphorState.MetaphorState =
    {state with Facing = state.Facing |> CardinalDirection.turn turn}

let private reportTurn 
        (turn:Turn.Turn) 
        (context:MetaphorContext.MetaphorContext) 
        : unit =
    turn.Name
    |> sprintf "You turn %s."
    |> context.Outputter

    context.State.Facing
    |> CardinalDirection.getName
    |> sprintf "You are now facing %s."
    |> context.Outputter

let private invalidCommand 
        (context:MetaphorContext.MetaphorContext)
        : unit =
    "[red]INVALID INPUT![/]"
    |> context.Outputter

let internal processInput 
        (input: string) 
        (context: MetaphorContext.MetaphorContext) 
        : MetaphorContext.MetaphorContext option =
    match input |> parseCommand with
    | Some command -> 
        match command with
        | Statement s -> 
            match s with
            | Quit -> 
                None
            | Turn turn ->
                context
                |> MetaphorContext.transformState (executeTurn turn)
                |> MetaphorContext.doSideEffect (reportTurn turn)
                |> Some
            | _ -> 
                context 
                |> MetaphorContext.doSideEffect invalidCommand
                |> Some
        | Question q -> 
            match q with
            | Status ->
                context 
                |> MetaphorContext.doSideEffect showStatus
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
