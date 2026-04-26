namespace TGGD.ROTRBIF

[<AutoOpen>]
module private CommandHandler =
    type MetaphorContext with
        static member internal showStatus 
                (context:MetaphorContext) 
                : unit =
            context.Metaphor 
            |> Metaphor.getAvatarCharacter
            |> Option.iter 
                (fun avatar -> 
                    avatar 
                    |> Character.getAliveStatus
                    |> sprintf "Status: %s"
                    |> context.Outputter

                    avatar 
                    |> Character.getFacing
                    |> CardinalDirection.getName 
                    |> sprintf "Facing: %s"
                    |> context.Outputter)

        static member internal reportTurn 
                (turn:Turn) 
                (context:MetaphorContext) 
                : unit =
            turn.Name
            |> sprintf "You turn %s."
            |> context.Outputter

            context.Metaphor 
            |> Metaphor.getAvatarCharacter
            |> Option.iter (
                Character.getFacing
                >> CardinalDirection.getName
                >> sprintf "You are now facing %s."
                >> context.Outputter)

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
            let avatarId = state.AvatarId.Value
            let avatarCharacter = state.Characters.[avatarId]
            {state with 
                Characters = 
                    state.Characters
                    |> Map.add 
                        avatarId ((CardinalDirection.turn turn, avatarCharacter) ||> CharacterState.mutateFacing)}
