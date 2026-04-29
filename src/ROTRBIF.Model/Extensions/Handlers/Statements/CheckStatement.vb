Friend Module CheckStatement
    Const ItToken = "it"

    Private ReadOnly checkTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {ItToken, AddressOf HandleCheckItCommand}
        }

    Private Sub HandleCheckItCommand(context As IModelContext)
        context.TerminalDispatch(
            Sub(x)
                Dim avatar = context.World.Avatar
                If avatar.IsBentOver Then
                    context.Output($"{avatar.GetName()} checks it.")
                    context.Output($"It seems fine.")
                Else
                    context.Output($"{avatar.GetName()} cannot check it without bending over.")
                End If
            End Sub,
            AddressOf HandleInvalidCommand)
    End Sub

    Friend Sub Handle(context As IModelContext)
        context.Dispatch(checkTable, AddressOf HandleInvalidCommand)
    End Sub
End Module
