Friend Module StatementHandlers
    Const QuitCommand = "Quit"
    Const TurnCommand = "Turn"
    Const LeftToken = "left"
    Const RightToken = "right"
    Const AroundToken = "around"

    Private ReadOnly statementTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {QuitCommand, AddressOf HandleQuitStatement},
            {TurnCommand, AddressOf HandleTurnStatement}
        }

    Friend Sub HandleStatement(context As IModelContext)
        context.Dispatch(statementTable, AddressOf HandleInvalidCommand)
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
