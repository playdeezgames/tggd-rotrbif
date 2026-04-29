Imports ROTRBIFOS.Data
Imports ROTRBIFOS.UI
Imports Spectre.Console

Module Program
    Sub Main(args As String())
        Try
            Console.Title = "ROTRBIF# of SPLORR!!"
        Catch ex As PlatformNotSupportedException
            'this doesn't work on the platform. no biggie. eat the exception.
        End Try

        Console.BackgroundColor = ConsoleColor.Yellow
        AnsiConsole.Clear()

        AnsiConsole.Write(
            New FigletText("ROTRBIF# of SPLORR!!") With
            {
                .Color = Color.Fuchsia,
                .Justification = Justify.Center
            })

        UIContext.Create(
            New WorldData(),
            Function() AnsiConsole.Ask(Of String)("[olive]>[/]"),
            Sub(x) AnsiConsole.MarkupLine(x)).Run()
    End Sub
End Module
