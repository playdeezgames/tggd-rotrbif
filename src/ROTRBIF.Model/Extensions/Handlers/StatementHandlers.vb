Friend Module StatementHandlers
    Const QuitCommand = "Quit"
    Const TurnCommand = "Turn"
    Const LeftToken = "left"
    Const RightToken = "right"
    Const AroundToken = "around"

    Friend Sub HandleStatement(context As IModelContext)
        If context.HasTokens Then
            Select Case context.ReadToken
                Case StatementHandlers.QuitCommand
                    HandleQuitStatement(context)
                Case TurnCommand
                    HandleTurnStatement(context)
                Case Else
                    HandleInvalidCommand(context)
            End Select
        Else
            HandleInvalidCommand(context)
        End If
    End Sub

    Private Sub HandleTurnStatement(context As IModelContext)
        If context.HasTokens Then
            Select Case context.ReadToken
                Case LeftToken
                    HandleTurnStatment(context, Turn.Left)
                Case RightToken
                    HandleTurnStatment(context, Turn.Right)
                Case AroundToken
                    HandleTurnStatment(context, Turn.Around)
                Case Else
                    HandleInvalidCommand(context)
            End Select
        Else
            HandleInvalidCommand(context)
        End If
    End Sub

    Private Sub HandleTurnStatment(context As IModelContext, turn As Turn)
        Dim avatar = context.World.Avatar
        context.Output($"{avatar.GetName()} turns {turn.GetName()}.")
        avatar.Turn(turn)
        context.Output($"{avatar.GetName()} now faces {avatar.GetFacing().GetName()}.")
    End Sub

    Private Sub HandleQuitStatement(context As IModelContext)
        If context.HasTokens Then
            HandleInvalidCommand(context)
        Else
            context.Quit()
        End If
    End Sub

End Module
