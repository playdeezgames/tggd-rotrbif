module internal Turn

[<RequireQualifiedAccess>]
type Turn =
    | Left
    | Right
    | Around
    member internal this.Name = 
        match this with
        | Left -> "left"
        | Right -> "right"
        | Around -> "around"
