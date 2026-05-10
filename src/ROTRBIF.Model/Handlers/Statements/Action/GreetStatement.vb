Imports ROTRBIFOS.Business

Friend Module GreetStatement
    Private ReadOnly objectTable As IReadOnlyDictionary(Of ObjectIdentifier, Action(Of IModelContext, ICharacter)) =
        New Dictionary(Of ObjectIdentifier, Action(Of IModelContext, ICharacter)) From
        {
            {ObjectIdentifier.GORACHAN, Sub(context, other) context.Output($"{other.GetName()} says ""Henlo.""")}
        }
    Friend Sub Handle(context As IModelContext)
        Dim avatar = context.World.Avatar
        If avatar.IsDead Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim name = context.ReadRemainingTokens()
        Dim other = avatar.Location.FindOtherByName(avatar, name)
        If other Is Nothing Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim objectIdentifier = other.GetObjectIdentifier()
        If Not objectIdentifier.HasValue Then
            HandleInvalidCommand(context)
            Return
        End If
        context.Output($"{avatar.GetName} greets {other.GetName}.")
        objectTable(objectIdentifier.Value).Invoke(context, other)
    End Sub
End Module
