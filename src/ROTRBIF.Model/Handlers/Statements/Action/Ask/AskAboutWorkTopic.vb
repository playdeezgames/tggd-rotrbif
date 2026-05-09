Imports ROTRBIFOS.Business

Friend Module AskAboutWorkTopic
    Private ReadOnly objectTable As IReadOnlyDictionary(Of ObjectIdentifier, Action(Of IModelContext, ICharacter, ICharacter)) =
        New Dictionary(Of ObjectIdentifier, Action(Of IModelContext, ICharacter, ICharacter)) From
        {
            {ObjectIdentifier.GORACHAN, AddressOf CellarQuest.AskGorachanAboutWork}
        }

    Friend Sub Handle(
                     context As IModelContext,
                     interrogator As ICharacter,
                     deponent As ICharacter)
        context.Output($"{interrogator.GetName} asks {deponent.GetName} about work.")
        Dim objectIdentifier = deponent.GetObjectIdentifier()
        If Not objectIdentifier.HasValue Then
            context.Output($"{deponent.GetName} has nothing to say to {interrogator.GetName} about work.")
            Return
        End If
        Dim handler As Action(Of IModelContext, ICharacter, ICharacter) = Nothing
        If Not objectTable.TryGetValue(objectIdentifier.Value, handler) Then
            context.Output($"{deponent.GetName} has nothing to say to {interrogator.GetName} about work.")
            Return
        End If
        handler(context, interrogator, deponent)
    End Sub
End Module
