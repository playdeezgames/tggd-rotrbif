Friend Module StatisticsQuestion
    Friend Sub Handle(context As IModelContext)
        context.TerminalDispatchAlive(
            AddressOf ShowStatistics,
            AddressOf HandleInvalidCommand)
    End Sub

    Private Sub ShowStatistics(context As IModelContext)
        Dim avatar = context.World.Avatar
        context.Output($"Health: {avatar.GetHealth()}/{avatar.GetMaximumHealth()}")
    End Sub
End Module
