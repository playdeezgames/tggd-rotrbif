Friend Module ExitsQuestion
    Friend Sub Handle(context As IModelContext)
        context.TerminalDispatchAlive(AddressOf ShowExits,
                                AddressOf HandleInvalidCommand)
    End Sub

    Private Sub ShowExits(context As IModelContext)
        Dim avatar = context.World.Avatar
        context.Output($"{avatar.GetName()} sees exit(s) going {avatar.Location.GetExitsText()}.")
    End Sub
End Module
