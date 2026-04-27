Imports ROTRBIFOS.Business

Friend Module QuestionHandlers
    Const Status = "Status"

    Friend Sub HandleQuestion(world As IWorld, tokens As IEnumerable(Of String), quit As Action, outputter As Action(Of String))
        If tokens.Any Then
            Dim remaining = tokens.Skip(1)
            Select Case tokens.First
                Case Status
                    HandleStatusQuestion(world, remaining, quit, outputter)
                Case Else
                    HandleInvalidCommand(outputter)
            End Select
        Else
            HandleInvalidCommand(outputter)
        End If
    End Sub

    Private Sub HandleStatusQuestion(world As IWorld, remaining As IEnumerable(Of String), quit As Action, outputter As Action(Of String))
        If Not remaining.Any Then
            Dim avatar = world.Avatar
            outputter($"{avatar.GetName()} is {avatar.GetAliveStatus()}.")
            outputter($"{avatar.GetName()} is facing {avatar.GetFacing().GetName()}.")
        Else
            HandleInvalidCommand(outputter)
        End If
    End Sub
End Module
