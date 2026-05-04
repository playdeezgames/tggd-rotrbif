Friend Module SearchStatement
    Private ReadOnly searchTargets As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {Names.CRATE, AddressOf SearchCrateStatement.Handle}
        }
    Friend Sub Handle(context As IModelContext)
        context.DispatchRemaining(searchTargets, AddressOf HandleInvalidCommand)
    End Sub
End Module
