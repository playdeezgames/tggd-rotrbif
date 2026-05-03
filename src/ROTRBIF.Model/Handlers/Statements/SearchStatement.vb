Friend Module SearchStatement
    Friend Sub Handle(context As IModelContext)
        Dim searchTarget = context.ReadRemainingTokens()
        Dim avatar = context.World.Avatar
        Dim feature = avatar.Location.FindFeatureByName(searchTarget)
        If feature Is Nothing Then
            HandleInvalidCommand(context)
            Return
        End If
        If Not feature.HasTrigger(Triggers.SEARCH) Then
            HandleInvalidCommand(context)
            Return
        End If
        feature.GetTrigger(Triggers.SEARCH).Fire(context)
    End Sub
End Module
