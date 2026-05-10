Friend Module OthersQuestion
    Friend Sub Handle(context As IModelContext)
        context.TerminalDispatchAlive(AddressOf ShowOthers, AddressOf HandleInvalidCommand)
    End Sub

    Private Sub ShowOthers(context As IModelContext)
        Dim avatar = context.World.Avatar
        Dim location = avatar.Location
        context.Output($"{avatar.GetName()} sees {location.GetOthersText(avatar)}.")
    End Sub
End Module
