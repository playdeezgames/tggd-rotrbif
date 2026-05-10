Friend Module StraightenStatement
    Const UpToken = "up"

    Private ReadOnly straightenTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {UpToken, AddressOf HandleStraightenUpCommand}
        }

    Private Sub HandleStraightenUpCommand(context As IModelContext)
        context.TerminalDispatch(
            Sub(x)
                Dim avatar = x.World.Avatar
                If Not avatar.IsBentOver Then
                    x.Output($"{avatar.GetName} is not bent over!")
                Else
                    avatar.StraightenUp()
                    x.Output($"{avatar.GetName} straightens up.")
                End If
            End Sub,
            AddressOf HandleInvalidCommand)
    End Sub

    Friend Sub Handle(context As IModelContext)
        context.DispatchAlive(straightenTable, AddressOf HandleInvalidCommand)
    End Sub
End Module
