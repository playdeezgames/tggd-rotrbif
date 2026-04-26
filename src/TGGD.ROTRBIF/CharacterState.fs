namespace TGGD.ROTRBIF

open System

[<RequireQualifiedAccess>]
type internal CharacterState =
    {
        Alive: bool
        Facing: CardinalDirection
    }
    with
        static member create (alive:bool) (facing:CardinalDirection) : CharacterState =
            {
                Alive = alive
                Facing = facing
            }

        static member mutateFacing (mutator : CardinalDirection -> CardinalDirection) (characterState:CharacterState) : CharacterState =
            {characterState with Facing = characterState.Facing |> mutator}