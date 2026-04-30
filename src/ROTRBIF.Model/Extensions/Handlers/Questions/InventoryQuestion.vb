Friend Module InventoryQuestion
    Friend Sub Handle(context As IModelContext)
        context.TerminalDispatch(
            Sub(x)
                Dim avatar = x.World.Avatar
                Dim items = avatar.Inventory.Items
                Dim inventoryText As String = Nothing
                Select Case items.Count
                    Case 0
                        inventoryText = "nothing"
                    Case 1
                        inventoryText = items.Single.GetName()
                    Case 2
                        inventoryText = $"{items.First.GetName()} and {items.Last.GetName()}"
                    Case Else
                        inventoryText = $"{String.Join(", ", items.Take(items.Count - 1).Select(Function(y) y.GetName()))} And {items.Last.GetName()}"
                End Select
                context.Output($"{avatar.GetName()} is carrying {inventoryText}.")
            End Sub,
            AddressOf HandleInvalidCommand)
    End Sub
End Module
