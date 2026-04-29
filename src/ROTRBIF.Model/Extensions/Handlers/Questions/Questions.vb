Friend Module Questions
    Const StatusCommand = "Status"

    Private ReadOnly questionTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {StatusCommand, AddressOf StatusQuestion.Handle}
        }

    Friend Sub Handle(context As IModelContext)
        context.Dispatch(questionTable, AddressOf HandleInvalidCommand)
    End Sub
End Module
