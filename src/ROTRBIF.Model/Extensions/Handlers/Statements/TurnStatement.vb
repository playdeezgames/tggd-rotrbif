Friend Module TurnStatement
    Const LeftToken = "left"
    Const RightToken = "right"
    Const AroundToken = "around"


    Private ReadOnly turnTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {LeftToken, DoTurn(Turn.Left)},
            {RightToken, DoTurn(Turn.Right)},
            {AroundToken, DoTurn(Turn.Around)}
        }

    Friend Sub Handle(context As IModelContext)
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

End Module
