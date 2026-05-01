Friend Module InventoryQuestion
    Friend Sub Handle(context As IModelContext)
        context.TerminalDispatch(
            AddressOf ShowInventory,
            AddressOf HandleInvalidCommand)
    End Sub

    Private Sub ShowInventory(context As IModelContext)
        Dim avatar = context.World.Avatar
        context.Output($"{avatar.GetName()} is carrying {avatar.Inventory.GetInventoryText()}.")
    End Sub
End Module
