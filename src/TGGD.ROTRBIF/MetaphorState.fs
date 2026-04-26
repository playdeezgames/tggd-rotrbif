namespace TGGD.ROTRBIF

open System

[<RequireQualifiedAccess>]
type internal MetaphorState = {
        Characters : Map<Guid, CharacterState>
        AvatarId : Guid option
    }
    with
        static member create () : MetaphorState =
            {
                Characters = Map.empty
                AvatarId = None
            }

        static member setCharacter 
                (characterId    : Guid) 
                (characterState : CharacterState) 
                (state          : MetaphorState) 
                : MetaphorState =
            {state with 
                Characters = 
                    state.Characters 
                    |> Map.add characterId characterState}

        static member setAvatarId
                (avatarId : Guid)
                (state:MetaphorState)
                : MetaphorState =
            {state with 
                AvatarId = 
                    avatarId 
                    |> Some}

        static member getAvatarCharacter (state:MetaphorState) : CharacterState =
            state.Characters.[state.AvatarId.Value]
