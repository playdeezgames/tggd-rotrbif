Friend Module Questions
    Const StatusCommand = "Status"

    Private ReadOnly questionTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {StatusCommand, AddressOf HandleStatusQuestion}
        }

    Friend Sub Handle(context As IModelContext)
        context.Dispatch(questionTable, AddressOf HandleInvalidCommand)
    End Sub

    Private Sub HandleStatusQuestion(context As IModelContext)
        context.TerminalDispatch(
            Sub(x)
                Dim avatar = x.World.Avatar
                x.Output($"{avatar.GetName()} is {avatar.GetAliveStatus()}.")
                x.Output($"{avatar.GetName()} is facing {avatar.GetFacing().GetName()}.")
                If avatar.IsBentOver Then
                    x.Output($"{avatar.GetName()} is bent over.")
                End If
                x.Output($"{avatar.GetName()} is in {avatar.Location.GetName()}.")
            End Sub,
            AddressOf HandleInvalidCommand)
    End Sub
End Module
