Imports ROTRBIFOS.Business

Friend Module StatementHandlers
    Const Quit = "Quit"
    Const Turn = "Turn"
    Const left = "left"
    Const right = "right"
    Const around = "around"


    Friend Sub HandleStatement(world As IWorld, tokens As IEnumerable(Of String), quit As Action, outputter As Action(Of String))
        If tokens.Any Then
            Dim remaining = tokens.Skip(1)
            Select Case tokens.First
                Case StatementHandlers.Quit
                    HandleQuitStatement(world, remaining, quit, outputter)
                Case Turn
                    HandleTurnStatement(world, remaining, quit, outputter)
                Case Else
                    HandleInvalidCommand(outputter)
            End Select
        Else
            HandleInvalidCommand(outputter)
        End If
    End Sub

    Private Sub HandleTurnStatement(world As IWorld, remaining As IEnumerable(Of String), quit As Action, outputter As Action(Of String))
        If remaining.Any Then
            Dim token = remaining.First
            remaining = remaining.Skip(1)
            Select Case token
                Case left
                    HandleTurnStatment(world, Model.Turn.Left, remaining, quit, outputter)
                Case right
                    HandleTurnStatment(world, Model.Turn.Right, remaining, quit, outputter)
                Case around
                    HandleTurnStatment(world, Model.Turn.Around, remaining, quit, outputter)
                Case Else
                    HandleInvalidCommand(outputter)
            End Select
        Else
            HandleInvalidCommand(outputter)
        End If
    End Sub

    Private Sub HandleTurnStatment(world As IWorld, turn As Turn, remaining As IEnumerable(Of String), quit As Action, outputter As Action(Of String))
        Dim avatar = world.Avatar
        outputter($"{avatar.GetName()} turns {turn.GetName()}.")
        avatar.Turn(turn)
        outputter($"{avatar.GetName()} now faces {avatar.GetFacing().GetName()}.")
    End Sub

    Private Sub HandleQuitStatement(world As IWorld, remaining As IEnumerable(Of String), quit As Action, outputter As Action(Of String))
        If remaining.Any() Then
            HandleInvalidCommand(outputter)
        Else
            quit()
        End If
    End Sub

End Module
