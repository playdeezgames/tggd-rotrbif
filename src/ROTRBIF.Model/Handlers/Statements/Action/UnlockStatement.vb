Friend Module UnlockStatement
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
        If Not route.GetTag(Tags.IS_LOCKED) Then
            context.Output($"{avatar.GetName} cannot unlock something that is not locked.")
            Return
        End If
        Dim keyType = route.GetMetadata(Metadatas.KEY_TYPE)
        Dim candidateItems = avatar.Inventory.FindItems(Function(i) i.GetMetadata(Metadatas.KEY_TYPE) = keyType)
        If Not candidateItems.Any Then
            context.Output($"{avatar.GetName} cannot unlock that.")
            Return
        End If
        route.ClearTag(Tags.IS_LOCKED)
        context.Output($"{avatar.GetName} unlocks {direction}.")
    End Sub
End Module
