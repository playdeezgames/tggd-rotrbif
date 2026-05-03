Imports ROTRBIFOS.Business

Friend Module SetTagTriggerAction
    Friend Function Fire(trigger As ITrigger, context As IModelContext) As ITrigger
        trigger.SetTag(trigger.GetTriggerTag())
        Return trigger.GetNextTrigger()
    End Function
End Module
