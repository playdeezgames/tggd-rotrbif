Friend Module SearchLoftCrateStatement
    Friend Sub Handle(context As IModelContext)
        Const JOOLS_COUNT = 5
        Dim avatar = context.World.Avatar
        avatar.ChangeStatistic(Statistics.JOOLS, JOOLS_COUNT)
        context.Output($"{avatar.GetName} finds {JOOLS_COUNT} jools.")
    End Sub
End Module
