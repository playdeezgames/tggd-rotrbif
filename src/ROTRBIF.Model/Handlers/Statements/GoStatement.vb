Friend Module GoStatement
    Friend Sub Handle(context As IModelContext)
        Dim direction = context.ReadRemainingTokens()
        Dim avatar = context.World.Avatar
        Dim route = avatar.Location.GetRoute(direction)
        If route Is Nothing Then
            HandleInvalidCommand(context)
            Return
        End If
        context.Output($"{avatar.GetName} goes {direction}.")
        avatar.Location = route.Destination
    End Sub
End Module
