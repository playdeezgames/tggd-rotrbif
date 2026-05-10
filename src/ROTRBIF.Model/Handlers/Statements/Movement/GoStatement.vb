Friend Module GoStatement
    Friend Sub Handle(context As IModelContext)
        Dim avatar = context.World.Avatar
        If avatar.IsDead Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim direction = context.ReadRemainingTokens()
        Dim route = avatar.Location.GetRoute(direction)
        If route Is Nothing Then
            HandleInvalidCommand(context)
            Return
        End If
        If route.GetTag(Tags.IS_LOCKED) Then
            context.Output($"{avatar.GetName()} cannot go that way for it is locked.")
            Return
        End If
        context.Output($"{avatar.GetName} goes {direction}.")
        avatar.Location = route.Destination
    End Sub
End Module
