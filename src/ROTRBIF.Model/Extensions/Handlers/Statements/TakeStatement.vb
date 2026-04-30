Friend Module TakeStatement
    Friend Sub Handle(context As IModelContext)
        Dim itemName = context.ReadRemainingTokens()
        If String.IsNullOrWhiteSpace(itemName) Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim avatar = context.World.Avatar
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
