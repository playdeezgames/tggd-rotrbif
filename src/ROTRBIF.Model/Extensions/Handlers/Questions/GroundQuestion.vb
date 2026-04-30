Friend Module GroundQuestion
    Friend Sub Handle(context As IModelContext)
        context.TerminalDispatch(
            Sub(x)
                Dim avatar = x.World.Avatar
                context.Output($"{avatar.GetName()} sees {avatar.Location.Inventory.GetInventoryText()} on the ground.")
            End Sub,
            AddressOf HandleInvalidCommand)
    End Sub
End Module
