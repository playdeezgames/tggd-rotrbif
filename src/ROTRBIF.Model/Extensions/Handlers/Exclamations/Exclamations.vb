Friend Module Exclamations
    Private ReadOnly exclamationTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext))

    Friend Sub Handle(context As IModelContext)
        context.Dispatch(exclamationTable, AddressOf HandleInvalidCommand)
    End Sub
End Module
