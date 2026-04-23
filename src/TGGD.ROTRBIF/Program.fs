open Spectre.Console
open System

type State = {
        Alive: bool
    }

type Outputter = string -> unit
type Inputter = unit -> string

let showStatus 
        (outputter: Outputter) 
        (state:State) : unit =
    if state.Alive then "Alive" else "Dead"
    |> sprintf "Status: %s"
    |> outputter

let processInput 
        (outputter: Outputter) 
        (input: string) 
        (state: State) : State option =
    match input with
    | "Quit." -> None
    | "Status?" -> 
        (outputter, state)
        ||> showStatus 
        state
        |> Some
    | _ ->
        "[red]INVALID INPUT![/]"
        |> outputter
        state
        |> Some
    

let rec repl (outputter: Outputter) (inputter: Inputter) (state:State option) : State option =
    match state with
    | Some s -> 
        (inputter(), s)
        ||> processInput outputter
        |> repl outputter inputter
    | None -> None

/////////////////////////////////////////////////
// After this line, Console and AnsiConsole may be used!
// AVAST! here be side effects!

Console.BackgroundColor <- ConsoleColor.Yellow
AnsiConsole.Clear()

let figlet = FigletText("ROTRBIF# of SPLORR!!")
figlet.Color <- Color.Fuchsia

figlet
|> AnsiConsole.Write


let inputter() : string =
    "[olive]>[/]"
    |> AnsiConsole.Ask

{ Alive = true }
|> Some
|> repl AnsiConsole.MarkupLine inputter
|> ignore