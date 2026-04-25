namespace TGGD.ROTRBIF

module internal InputProcessor =
    let internal processInput 
            (input: string) 
            (context: MetaphorContext) 
            : MetaphorContext option =
        match input |> Command.parse with
        | Some command -> 
            match command with
            | Statement s -> 
                match s with
                | Quit -> 
                    None
                | Turn turn ->
                    context
                    |> MetaphorContext.transformState (CommandHandler.executeTurn turn)
                    |> MetaphorContext.doSideEffect (CommandHandler.reportTurn turn)
                    |> Some
                | _ -> 
                    context 
                    |> MetaphorContext.doSideEffect CommandHandler.invalidCommand
                    |> Some
            | Question q -> 
                match q with
                | Status ->
                    context 
                    |> MetaphorContext.doSideEffect CommandHandler.showStatus
                    |> Some
                | _ ->
                    context
                    |> MetaphorContext.doSideEffect CommandHandler.invalidCommand
                    |> Some
            | _ -> 
                context 
                |> MetaphorContext.doSideEffect CommandHandler.invalidCommand
                |> Some
        | _ -> 
            context 
            |> MetaphorContext.doSideEffect CommandHandler.invalidCommand
            |> Some
