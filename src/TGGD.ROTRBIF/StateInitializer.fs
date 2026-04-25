namespace TGGD.ROTRBIF

open System

module internal StateInitializer =
    let initialize (state:MetaphorState) : MetaphorState =
        let avatarCharacter = CharacterState.create true CardinalDirection.North
        let avatarId = Guid.NewGuid()
        state
        |> MetaphorState.setCharacter avatarId avatarCharacter
        |> MetaphorState.setAvatarId avatarId