namespace TGGD.ROTRBIF

open System

type internal Character =
    {
        CharacterId: Guid
        MetaphorState: MetaphorState
    }
    with
        member private this.CharacterState = this.MetaphorState.Characters.[this.CharacterId]
        member private this.IsAlive = this.CharacterState.Alive
        member private this.Facing = this.CharacterState.Facing
        static member isAlive (character: Character) = character.IsAlive
        static member getAliveStatus (character: Character) =
            if character.IsAlive then
                "Alive"
            else
                "Dead"
        static member getFacing (character: Character) = character.Facing
