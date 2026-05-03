Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Friend Module TriggerExtensions
    <Extension>
    Friend Sub Fire(trigger As ITrigger, context As IModelContext)
        context.Output("TRIGGER FIRED!")
    End Sub
End Module
