Imports ROTRBIFOS.Business

Friend Module ExclamationHandlers

    Friend Sub HandleExclamation(world As IWorld, tokens As IEnumerable(Of String), quit As Action, outputter As Action(Of String))
        If tokens.Any Then
            Dim remaining = tokens.Skip(1)
            Select Case tokens.First
                Case Else
                    HandleInvalidCommand(outputter)
            End Select
        Else
            HandleInvalidCommand(outputter)
        End If
    End Sub
End Module
