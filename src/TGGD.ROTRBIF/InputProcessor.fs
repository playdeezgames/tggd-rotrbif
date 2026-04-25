namespace TGGD.ROTRBIF

[<AutoOpen>]
module internal InputProcessor =
    type MetaphorContext with
        static member internal processInput 
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
                        |> MetaphorContext.transformState (MetaphorState.executeTurn turn)
                        |> MetaphorContext.doSideEffect (MetaphorContext.reportTurn turn)
                        |> Some
                    | _ -> 
                        context 
                        |> MetaphorContext.doSideEffect MetaphorContext.invalidCommand
                        |> Some
                | Question q -> 
                    match q with
                    | Status ->
                        context 
                        |> MetaphorContext.doSideEffect MetaphorContext.showStatus
                        |> Some
                    | _ ->
                        context
                        |> MetaphorContext.doSideEffect MetaphorContext.invalidCommand
                        |> Some
                | _ -> 
                    context 
                    |> MetaphorContext.doSideEffect MetaphorContext.invalidCommand
                    |> Some
            | _ -> 
                context 
                |> MetaphorContext.doSideEffect MetaphorContext.invalidCommand
                |> Some
