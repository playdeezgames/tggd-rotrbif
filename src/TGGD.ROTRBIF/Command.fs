namespace TGGD.ROTRBIF

open System

type private Subcommand =
    | Quit
    | Status
    | Turn of Turn

type private Command =
    | Statement of Subcommand
    | Question of Subcommand
    | Exclamation of Subcommand
    static member private parseTurn (tokens: string list) : Turn option=
        match tokens with
        | ["left"]   -> Turn.Left   |> Some
        | ["right"]  -> Turn.Right  |> Some
        | ["around"] -> Turn.Around |> Some
        | _          -> None

    static member private parseSubcommand(input:string) : Subcommand option =
        let tokens = input.Split([|' '|], StringSplitOptions.RemoveEmptyEntries) |> Array.toList
        match tokens with
        | ["Quit"] -> 
            Subcommand.Quit 
            |> Some
        | ["Status"] -> 
            Subcommand.Status 
            |> Some
        | "Turn" :: subtokens -> 
            subtokens 
            |> Command.parseTurn 
            |> Option.map Subcommand.Turn
        | _ -> 
            None

    static member internal parse (input: string) : Command option =
        let subcommand = 
            input 
            |> Seq.truncate (max 0 (input.Length-1)) 
            |> System.String.Concat 
            |> Command.parseSubcommand
        let ending = 
            input 
            |> Seq.tryLast
        match ending, subcommand with
        | Some '.', Some x -> x |> Command.Statement   |> Some
        | Some '?', Some x -> x |> Command.Question    |> Some
        | Some '!', Some x -> x |> Command.Exclamation |> Some
        | _                -> None
