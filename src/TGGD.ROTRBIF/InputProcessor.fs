namespace TGGD.ROTRBIF

open System

type private Subcommand =
    | Quit
    | Status
    | Turn of Turn

type private Command =
    | Statement of Subcommand
    | Question of Subcommand
    | Exclamation of Subcommand

module private InputParser = 
    let private parseTurn (tokens: string list) : Turn option=
        match tokens with
        | ["left"]   -> Turn.Left   |> Some
        | ["right"]  -> Turn.Right  |> Some
        | ["around"] -> Turn.Around |> Some
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

    let internal parseCommand (input: string) : Command option =
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

module private InputHandler =
    let internal showStatus 
            (context:MetaphorContext) 
            : unit =
        if context.State.Alive then "Alive" else "Dead"
        |> sprintf "Status: %s"
        |> context.Outputter

        context.State.Facing.Name
        |> sprintf "Facing: %s"
        |> context.Outputter

    let internal executeTurn 
            (turn:Turn) 
            (state:MetaphorState) 
            : MetaphorState =
        {state with Facing = state.Facing |> CardinalDirection.turn turn}

    let internal reportTurn 
            (turn:Turn) 
            (context:MetaphorContext) 
            : unit =
        turn.Name
        |> sprintf "You turn %s."
        |> context.Outputter

        context.State.Facing.Name
        |> sprintf "You are now facing %s."
        |> context.Outputter

    let internal invalidCommand 
            (context:MetaphorContext)
            : unit =
        "[red]INVALID INPUT![/]"
        |> context.Outputter

module internal InputProcessor =
    let internal processInput 
            (input: string) 
            (context: MetaphorContext) 
            : MetaphorContext option =
        match input |> InputParser.parseCommand with
        | Some command -> 
            match command with
            | Statement s -> 
                match s with
                | Quit -> 
                    None
                | Turn turn ->
                    context
                    |> MetaphorContext.transformState (InputHandler.executeTurn turn)
                    |> MetaphorContext.doSideEffect (InputHandler.reportTurn turn)
                    |> Some
                | _ -> 
                    context 
                    |> MetaphorContext.doSideEffect InputHandler.invalidCommand
                    |> Some
            | Question q -> 
                match q with
                | Status ->
                    context 
                    |> MetaphorContext.doSideEffect InputHandler.showStatus
                    |> Some
                | _ ->
                    context
                    |> MetaphorContext.doSideEffect InputHandler.invalidCommand
                    |> Some
            | _ -> 
                context 
                |> MetaphorContext.doSideEffect InputHandler.invalidCommand
                |> Some
        | _ -> 
            context 
            |> MetaphorContext.doSideEffect InputHandler.invalidCommand
            |> Some
