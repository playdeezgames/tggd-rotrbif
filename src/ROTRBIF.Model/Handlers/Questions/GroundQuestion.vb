Friend Module GroundQuestion
    Friend Sub Handle(context As IModelContext)
        context.TerminalDispatchAlive(
            AddressOf ShowGroundInventory,
            AddressOf HandleInvalidCommand)
    End Sub

    Private Sub ShowGroundInventory(context As IModelContext)
        Dim avatar = context.World.Avatar
        context.Output($"{avatar.GetName()} sees {avatar.Location.Inventory.GetInventoryText()} on the ground.")
    End Sub
End Module
