Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Friend Module TriggerExtensions
    Private ReadOnly triggerActionTable As IReadOnlyDictionary(Of String, Func(Of ITrigger, IModelContext, ITrigger)) =
        New Dictionary(Of String, Func(Of ITrigger, IModelContext, ITrigger)) From
        {
            {TriggerActions.MESSAGE, AddressOf MessageTriggerAction.Fire},
            {TriggerActions.CHECK_TAG, AddressOf CheckTagTriggerAction.Fire},
            {TriggerActions.SET_TAG, AddressOf SetTagTriggerAction.Fire}
        }
    <Extension>
    Friend Sub Fire(trigger As ITrigger, context As IModelContext)
        While trigger IsNot Nothing
            trigger = triggerActionTable(trigger.GetTriggerAction()).Invoke(trigger, context)
        End While
    End Sub
    <Extension>
    Friend Sub SetTriggerAction(trigger As ITrigger, triggerAction As String)
        trigger.SetMetadata(Metadatas.TRIGGER_ACTION, triggerAction)
    End Sub
    <Extension>
    Friend Function GetTriggerAction(trigger As ITrigger) As String
        Return trigger.GetMetadata(Metadatas.TRIGGER_ACTION)
    End Function
    <Extension>
    Friend Sub SetTriggerMessage(trigger As ITrigger, triggerMessage As String)
        trigger.SetMetadata(Metadatas.TRIGGER_MESSAGE, triggerMessage)
    End Sub
    <Extension>
    Friend Function GetTriggerMessage(trigger As ITrigger) As String
        Return trigger.GetMetadata(Metadatas.TRIGGER_MESSAGE)
    End Function
    <Extension>
    Friend Function GetNextTrigger(trigger As ITrigger) As ITrigger
        If Not trigger.HasTrigger(Triggers.NEXT) Then
            Return Nothing
        End If
        Return trigger.GetTrigger(Triggers.NEXT)
    End Function
    <Extension>
    Friend Sub SetTriggerTag(trigger As ITrigger, tagType As String)
        trigger.SetMetadata(Metadatas.TRIGGER_TAG_TYPE, tagType)
    End Sub
    <Extension>
    Friend Function GetTriggerTag(trigger As ITrigger) As String
        Return trigger.GetMetadata(Metadatas.TRIGGER_TAG_TYPE)
    End Function
End Module
