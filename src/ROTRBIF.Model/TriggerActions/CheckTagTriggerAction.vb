Imports ROTRBIFOS.Business

Friend Module CheckTagTriggerAction
    Friend Function Fire(trigger As ITrigger, context As IModelContext) As ITrigger
        Dim triggerTagType = trigger.GetTriggerTag()
        If trigger.GetTag(triggerTagType) Then
            Return trigger.GetTrigger(Triggers.CONDITION_MET)
        Else
            Return trigger.GetTrigger(Triggers.CONDITION_UNMET)
        End If
    End Function
End Module
