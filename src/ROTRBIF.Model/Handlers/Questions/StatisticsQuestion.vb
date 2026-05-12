Friend Module StatisticsQuestion
    Friend Sub Handle(context As IModelContext)
        context.TerminalDispatchAlive(
            AddressOf ShowStatistics,
            AddressOf HandleInvalidCommand)
    End Sub

    Private Sub ShowStatistics(context As IModelContext)
        Dim avatar = context.World.Avatar
        context.Output($"Health: {avatar.GetHealth()}/{avatar.GetMaximumHealth()}")
        context.Output($"XP: {avatar.GetXP()}/{avatar.GetMaximumXP()}")
        context.Output($"XP Level: {avatar.GetXPLevel()}")
        context.Output($"Skill Points: {avatar.GetSkillPoints()}")
    End Sub
End Module
