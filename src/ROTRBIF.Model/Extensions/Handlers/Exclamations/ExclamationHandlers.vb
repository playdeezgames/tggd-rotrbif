Friend Module ExclamationHandlers
    Private ReadOnly exclamationTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext))

    Friend Sub HandleExclamation(context As IModelContext)
        context.Dispatch(exclamationTable, AddressOf HandleInvalidCommand)
    End Sub
End Module
