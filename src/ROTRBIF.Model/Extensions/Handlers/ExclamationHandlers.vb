Friend Module ExclamationHandlers

    Friend Sub HandleExclamation(context As IModelContext)
        If context.HasTokens Then
            Select Case context.ReadToken()
                Case Else
                    HandleInvalidCommand(context)
            End Select
        Else
            HandleInvalidCommand(context)
        End If
    End Sub
End Module
