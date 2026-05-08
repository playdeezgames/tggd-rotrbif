Friend Module TakeStatement
    Friend Sub Handle(context As IModelContext)
        Dim itemName = context.ReadRemainingTokens()
        If String.IsNullOrWhiteSpace(itemName) Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim avatar = context.World.Avatar
        'You can't reach the ground from this height.
        If Not avatar.GetTag(Tags.BENT_OVER) Then
            context.Output($"{avatar.GetName} cannot reach the ground from this height.")
            Return
        End If
        Dim item = avatar.Location.Inventory.TryFindItemByName(itemName)
        If item Is Nothing Then
            HandleInvalidCommand(context)
            Return
        End If
        avatar.Location.Inventory.RemoveItem(item)
        item.Inventory = avatar.Inventory
        avatar.Inventory.AddItem(item)
        context.Output($"{avatar.GetName()} takes {itemName}.")
    End Sub
End Module
