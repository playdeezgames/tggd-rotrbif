open Spectre.Console
open System

type State = {
        Alive: bool
    }

let rec repl (state:State option) : State option =
    if state.IsSome then
        let input : string = 
            "[olive]>[/]"
            |> AnsiConsole.Ask
        match input with
        | "Quit." -> None
        | _ ->
            "[red]INVALID INPUT![/]"
            |> AnsiConsole.MarkupLine
            state
        |> repl
    else
        None

Console.BackgroundColor <- ConsoleColor.Yellow
AnsiConsole.Clear()

let figlet = FigletText("ROTRBIF# of SPLORR!!")
figlet.Color <- Color.Fuchsia

figlet
|> AnsiConsole.Write

{ Alive = true }
|> Some
|> repl
|> ignore