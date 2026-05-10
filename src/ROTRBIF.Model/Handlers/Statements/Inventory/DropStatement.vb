Friend Module DropStatement
    Friend Sub Handle(context As IModelContext)
        Dim avatar = context.World.Avatar
        If avatar.IsDead Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim itemName = context.ReadRemainingTokens()
        If String.IsNullOrWhiteSpace(itemName) Then
            HandleInvalidCommand(context)
            Return
        End If
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
