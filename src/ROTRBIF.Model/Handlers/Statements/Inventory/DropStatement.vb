Friend Module DropStatement
    Friend Sub Handle(context As IModelContext)
        Dim itemName = context.ReadRemainingTokens()
        If String.IsNullOrWhiteSpace(itemName) Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim avatar = context.World.Avatar
        Dim item = avatar.Inventory.TryFindItemByName(itemName)
        If item Is Nothing Then
            HandleInvalidCommand(context)
            Return
        End If
        avatar.Inventory.RemoveItem(item)
        item.Inventory = avatar.Location.Inventory
        avatar.Location.Inventory.AddItem(item)
        context.Output($"{avatar.GetName()} drops {itemName}.")
    End Sub
End Module
