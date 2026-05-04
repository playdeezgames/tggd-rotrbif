Friend Module SearchCrateStatement
    Friend Sub Handle(context As IModelContext)
        Const JOOLS_COUNT = 5
        Dim avatar = context.World.Avatar
        Dim crate = avatar.Location.FindFeatureByName(Names.CRATE)
        If crate.GetTag(Tags.SEARCHED) Then
            context.Output($"{avatar.GetName} has already searched that.")
        Else
            context.Output($"{avatar.GetName} searches {Names.CRATE}.")
            avatar.ChangeStatistic(Statistics.JOOLS, JOOLS_COUNT)
            context.Output($"{avatar.GetName} finds {JOOLS_COUNT} jools.")
            crate.SetTag(Tags.SEARCHED)
        End If
    End Sub
End Module
