Friend Module QuestionHandlers
    Const StatusCommand = "Status"

    Private ReadOnly questionTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {StatusCommand, AddressOf HandleStatusQuestion}
        }

    Friend Sub HandleQuestion(context As IModelContext)
        context.Dispatch(questionTable, AddressOf HandleInvalidCommand)
    End Sub

    Private Sub HandleStatusQuestion(context As IModelContext)
        context.TerminalDispatch(
            Sub(x)
                Dim avatar = x.World.Avatar
                x.Output($"{avatar.GetName()} is {avatar.GetAliveStatus()}.")
                x.Output($"{avatar.GetName()} is facing {avatar.GetFacing().GetName()}.")
                x.Output($"{avatar.GetName()} is in {avatar.Location.GetName()}.")
            End Sub,
            AddressOf HandleInvalidCommand)
    End Sub
End Module
