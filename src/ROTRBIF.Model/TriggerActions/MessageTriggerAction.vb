Imports ROTRBIFOS.Business

Friend Module MessageTriggerAction
    Friend Function Fire(trigger As ITrigger, context As IModelContext) As ITrigger
        context.Output(trigger.GetTriggerMessage())
        Return trigger.GetNextTrigger()
    End Function
End Module
