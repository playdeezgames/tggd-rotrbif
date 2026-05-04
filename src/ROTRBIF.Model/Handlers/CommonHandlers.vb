Imports System.Runtime.CompilerServices

Friend Module CommonHandlers
    Friend Sub HandleInvalidCommand(context As IModelContext)
        context.Output("[red]INVALID COMMAND![/]")
    End Sub
    <Extension>
    Friend Sub Dispatch(context As IModelContext, tokenTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)), invalidHandler As Action(Of IModelContext))
        Dim handler As Action(Of IModelContext) = Nothing
        If Not context.HasTokens OrElse Not tokenTable.TryGetValue(context.ReadToken, handler) Then
            handler = invalidHandler
        End If
        handler.Invoke(context)
    End Sub
    <Extension>
    Friend Sub DispatchRemaining(context As IModelContext, tokenTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)), invalidHandler As Action(Of IModelContext))
        Dim handler As Action(Of IModelContext) = Nothing
        If Not context.HasTokens OrElse Not tokenTable.TryGetValue(context.ReadRemainingTokens, handler) Then
            handler = invalidHandler
        End If
        handler.Invoke(context)
    End Sub
    <Extension>
    Friend Sub TerminalDispatch(context As IModelContext, validHandler As Action(Of IModelContext), invalidHandler As Action(Of IModelContext))
        If Not context.HasTokens Then
            validHandler.Invoke(context)
        Else
            invalidHandler.Invoke(context)
        End If
    End Sub
End Module
