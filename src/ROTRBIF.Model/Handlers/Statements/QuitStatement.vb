Friend Module QuitStatement

    Friend Sub Handle(context As IModelContext)
        context.TerminalDispatch(Sub(x) x.Quit(), AddressOf HandleInvalidCommand)
    End Sub

End Module
