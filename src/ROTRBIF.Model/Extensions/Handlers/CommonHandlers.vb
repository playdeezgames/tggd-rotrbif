Friend Module CommonHandlers

    Friend Sub HandleInvalidCommand(outputter As Action(Of String))
        outputter("[red]INVALID COMMAND![/]")
    End Sub

End Module
