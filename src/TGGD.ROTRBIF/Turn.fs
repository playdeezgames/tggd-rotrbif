[<RequireQualifiedAccess>]
module internal Turn

type Turn =
    | Left
    | Right
    | Around

let internal getName(turn:Turn) : string =
    match turn with
    | Left   -> "left"
    | Right  -> "right"
    | Around -> "around"
