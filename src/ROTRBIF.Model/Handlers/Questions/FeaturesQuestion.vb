Friend Module FeaturesQuestion
    Friend Sub Handle(context As IModelContext)
        context.TerminalDispatch(AddressOf ShowFeatures, AddressOf HandleInvalidCommand)
    End Sub

    Private Sub ShowFeatures(context As IModelContext)
        Dim avatar = context.World.Avatar
        Dim location = avatar.Location
        context.Output($"{avatar.GetName()} sees {location.GetFeaturesText()}.")
    End Sub
End Module
