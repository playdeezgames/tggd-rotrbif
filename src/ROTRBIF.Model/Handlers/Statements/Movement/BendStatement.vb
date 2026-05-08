Friend Module BendStatement
    Const OverToken = "over"

    Private ReadOnly bendTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {OverToken, AddressOf HandleBendOverCommand}
        }

    Private Sub HandleBendOverCommand(context As IModelContext)
        context.TerminalDispatch(
            Sub(x)
                Dim avatar = x.World.Avatar
                If avatar.IsBentOver Then
                    x.Output($"{avatar.GetName} is already bent over!")
                Else
                    avatar.BendOver()
                    x.Output($"{avatar.GetName} bends over.")
                End If
            End Sub,
            AddressOf HandleInvalidCommand)
    End Sub

    Friend Sub Handle(context As IModelContext)
        context.Dispatch(bendTable, AddressOf HandleInvalidCommand)
    End Sub

End Module
