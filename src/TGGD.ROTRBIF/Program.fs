open Repl
open Spectre.Console
open System
open TGGD.ROTRBIF

Console.BackgroundColor <- ConsoleColor.Yellow
AnsiConsole.Clear()

let figlet = FigletText("ROTRBIF# of SPLORR!!")
figlet.Color <- Color.Fuchsia

figlet
|> AnsiConsole.Write

let inputter() : string =
    "[olive]>[/]"
    |> AnsiConsole.Ask

(inputter, AnsiConsole.MarkupLine, MetaphorState.create())
|||> MetaphorContext.create 
|> Some
|> mutateMetaphor
|> ignore