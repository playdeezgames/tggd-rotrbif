Friend Module SearchBedStatement
    Friend Sub Handle(context As IModelContext)
        Dim avatar = context.World.Avatar
        Dim item = avatar.Inventory.CreateItem(AddressOf InitializeRustyDagger)
        context.Output($"{avatar.GetName} finds {item.GetName}.")
    End Sub

    Private Sub InitializeRustyDagger(item As ROTRBIFOS.Business.IItem)
        item.SetName(Names.RUSTY_DAGGER)
    End Sub
End Module
