Friend Module QuestionHandlers
    Const StatusCommand = "Status"

    Friend Sub HandleQuestion(context As IModelContext)
        If context.HasTokens Then
            Select Case context.ReadToken
                Case StatusCommand
                    HandleStatusQuestion(context)
                Case Else
                    HandleInvalidCommand(context)
            End Select
        Else
            HandleInvalidCommand(context)
        End If
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
