namespace TGGD.ROTRBIF

[<AutoOpen>]
module internal InputProcessor =
    let private processInvalidCommand =
        MetaphorContext.sideEffect MetaphorContext.invalidCommand 
        >> Some

    let private processQuitCommand (_:MetaphorContext) : MetaphorContext option =
        None

    let private processTurnCommand (turn:Turn) =
        MetaphorContext.mutateState (MetaphorState.executeTurn turn)
        >> MetaphorContext.sideEffect (MetaphorContext.reportTurn turn)
        >> Some

    let private processStatement = function
        | Quit -> 
            processQuitCommand
        | Turn turn ->
            processTurnCommand turn
        | _ -> 
            processInvalidCommand

    let private processQuery = function
        | Status ->
            MetaphorContext.sideEffect MetaphorContext.showStatus
            >> Some
        | _ ->
            processInvalidCommand

    let private processCommand = function
        | Statement s -> 
            processStatement s
        | Question q -> 
            processQuery q
        | _ -> 
            processInvalidCommand

    type MetaphorContext with
        static member internal processInput 
                (input: string) =
            match input |> Command.parse with
            | Some command -> 
                processCommand command
            | _ -> 
                processInvalidCommand
