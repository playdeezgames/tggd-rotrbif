Friend Module Exclamations
    Private Const AttackCommand = "Attack"

    Private ReadOnly exclamationTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {AttackCommand, AddressOf AttackExclamation.Handle}
        }

    Friend Sub Handle(context As IModelContext)
        context.Dispatch(exclamationTable, AddressOf HandleInvalidCommand)
    End Sub
End Module
