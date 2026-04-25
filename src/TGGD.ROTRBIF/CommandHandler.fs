namespace TGGD.ROTRBIF

[<AutoOpen>]
module private CommandHandler =
    type MetaphorContext with
        static member internal showStatus 
                (context:MetaphorContext) 
                : unit =
            let avatar = context.State |> MetaphorState.getAvatarCharacter

            if avatar.Alive then "Alive" else "Dead"
            |> sprintf "Status: %s"
            |> context.Outputter

            avatar.Facing.Name
            |> sprintf "Facing: %s"
            |> context.Outputter

        static member internal reportTurn 
                (turn:Turn) 
                (context:MetaphorContext) 
                : unit =
            turn.Name
            |> sprintf "You turn %s."
            |> context.Outputter

            let avatar = context.State |> MetaphorState.getAvatarCharacter
            avatar.Facing.Name
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
            {state with 
                Characters = 
                    state.Characters
                    |> Map.add 
                        state.AvatarId.Value {
                            state.Characters.[state.AvatarId.Value] with 
                                Facing = 
                                    state.Characters.[state.AvatarId.Value].Facing 
                                    |> CardinalDirection.turn turn}}

