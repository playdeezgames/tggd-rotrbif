Friend Module CommonHandlers
    Friend Sub HandleInvalidCommand(context As IModelContext)
        context.Output("[red]INVALID COMMAND![/]")
    End Sub
End Module
