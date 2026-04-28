Friend Module QuestionHandlers
    Const StatusCommand = "Status"

    Private ReadOnly questionTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {StatusCommand, AddressOf HandleStatusQuestion}
        }

    Friend Sub HandleQuestion(context As IModelContext)
        Dim handler As Action(Of IModelContext) = Nothing
        If Not context.HasTokens OrElse Not questionTable.TryGetValue(context.ReadToken, handler) Then
            handler = AddressOf HandleInvalidCommand
        End If
        handler.Invoke(context)
    End Sub

    Private Sub HandleStatusQuestion(context As IModelContext)
        If Not context.HasTokens Then
            Dim avatar = context.World.Avatar
            context.Output($"{avatar.GetName()} is {avatar.GetAliveStatus()}.")
            context.Output($"{avatar.GetName()} is facing {avatar.GetFacing().GetName()}.")
        Else
            HandleInvalidCommand(context)
        End If
    End Sub
End Module
