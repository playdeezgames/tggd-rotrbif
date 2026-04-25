namespace TGGD.ROTRBIF

[<AutoOpen>]
module private CommandHandler =
    type MetaphorContext with
        static member internal showStatus 
                (context:MetaphorContext) 
                : unit =
            if context.State.Alive then "Alive" else "Dead"
            |> sprintf "Status: %s"
            |> context.Outputter

            context.State.Facing.Name
            |> sprintf "Facing: %s"
            |> context.Outputter
        static member internal reportTurn 
                (turn:Turn) 
                (context:MetaphorContext) 
                : unit =
            turn.Name
            |> sprintf "You turn %s."
            |> context.Outputter

            context.State.Facing.Name
            |> sprintf "You are now facing %s."
            |> context.Outputter

        static member internal invalidCommand 
                (context:MetaphorContext)
                : unit =
            "[red]INVALID INPUT![/]"
            |> context.Outputter

    type MetaphorState with
        static member internal executeTurn 
                (turn:Turn) 
                (state:MetaphorState) 
                : MetaphorState =
            {state with Facing = state.Facing |> CardinalDirection.turn turn}

