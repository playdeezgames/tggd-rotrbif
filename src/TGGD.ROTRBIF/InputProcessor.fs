namespace TGGD.ROTRBIF

[<AutoOpen>]
module internal InputProcessor =
    let private processInvalidCommand (context: MetaphorContext) : MetaphorContext option =
        context 
        |> MetaphorContext.doSideEffect MetaphorContext.invalidCommand
        |> Some
    let private processCommand (command:Command) (context: MetaphorContext) : MetaphorContext option =
        match command with
        | Statement s -> 
            match s with
            | Quit -> 
                None
            | Turn turn ->
                context
                |> MetaphorContext.transformState (MetaphorState.executeTurn turn)
                |> MetaphorContext.doSideEffect (MetaphorContext.reportTurn turn)
                |> Some
            | _ -> 
                context |> processInvalidCommand
        | Question q -> 
            match q with
            | Status ->
                context 
                |> MetaphorContext.doSideEffect MetaphorContext.showStatus
                |> Some
            | _ ->
                context |> processInvalidCommand
        | _ -> 
            context |> processInvalidCommand
    type MetaphorContext with
        static member internal processInput 
                (input: string) 
                (context: MetaphorContext) 
                : MetaphorContext option =
            match input |> Command.parse with
            | Some command -> 
                context
                |> processCommand command
            | _ -> 
                context 
                |> processInvalidCommand
