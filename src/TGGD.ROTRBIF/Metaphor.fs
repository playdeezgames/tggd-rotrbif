namespace TGGD.ROTRBIF

open System

type internal Metaphor =
    {
        State : MetaphorState
    }
    with 
        static member mutateState 
                (transformer : MetaphorState -> MetaphorState) 
                (metaphor    : Metaphor) =
            {metaphor with State = metaphor.State |> transformer}
        static member getCharacter (characterId: Guid) (metaphor : Metaphor): Character =
            {
                CharacterId   = characterId
                MetaphorState = metaphor.State
            }
        static member getAvatarCharacter (metaphor : Metaphor) : Character option =
            metaphor.State.AvatarId
            |> Option.map (fun id -> Metaphor.getCharacter id metaphor)