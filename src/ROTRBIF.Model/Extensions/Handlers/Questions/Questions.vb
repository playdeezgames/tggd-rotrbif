Friend Module Questions
    Const StatusCommand = "Status"
    Const InventoryCommand = "Inventory"
    Const GroundCommand = "Ground"

    Private ReadOnly questionTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {StatusCommand, AddressOf StatusQuestion.Handle},
            {InventoryCommand, AddressOf InventoryQuestion.Handle},
            {GroundCommand, AddressOf GroundQuestion.Handle}
        }

    Friend Sub Handle(context As IModelContext)
        context.Dispatch(questionTable, AddressOf HandleInvalidCommand)
    End Sub
End Module
