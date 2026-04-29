Friend Module StatementHandlers
    Const QuitCommand = "Quit"
    Const TurnCommand = "Turn"
    Const BendCommand = "Bend"

    Const LeftToken = "left"
    Const RightToken = "right"
    Const AroundToken = "around"
    Const OverToken = "over"

    Private ReadOnly statementTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {QuitCommand, AddressOf HandleQuitStatement},
            {TurnCommand, AddressOf HandleTurnStatement},
            {BendCommand, AddressOf HandleBendCommand}
        }

    Friend Sub HandleStatement(context As IModelContext)
        context.Dispatch(statementTable, AddressOf HandleInvalidCommand)
    End Sub

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

    Private Sub HandleBendCommand(context As IModelContext)
        context.Dispatch(bendTable, AddressOf HandleInvalidCommand)
    End Sub

    Private ReadOnly turnTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {LeftToken, DoTurn(Turn.Left)},
            {RightToken, DoTurn(Turn.Right)},
            {AroundToken, DoTurn(Turn.Around)}
        }

    Private Sub HandleTurnStatement(context As IModelContext)
        context.Dispatch(turnTable, AddressOf HandleInvalidCommand)
    End Sub

    Private Function DoTurn(turn As Turn) As Action(Of IModelContext)
        Return Sub(context)
                   Dim avatar = context.World.Avatar
                   context.Output($"{avatar.GetName()} turns {turn.GetName()}.")
                   avatar.Turn(turn)
                   context.Output($"{avatar.GetName()} now faces {avatar.GetFacing().GetName()}.")
               End Sub
    End Function

    Private Sub HandleQuitStatement(context As IModelContext)
        context.TerminalDispatch(Sub(x) x.Quit(), AddressOf HandleInvalidCommand)
    End Sub

End Module
