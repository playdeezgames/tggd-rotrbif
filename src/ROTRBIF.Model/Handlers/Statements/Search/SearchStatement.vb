Friend Module SearchStatement
    Private ReadOnly objectTable As IReadOnlyDictionary(Of ObjectIdentifier, Action(Of IModelContext)) =
        New Dictionary(Of ObjectIdentifier, Action(Of IModelContext)) From
        {
            {ObjectIdentifier.LOFT_CRATE, AddressOf SearchLoftCrateStatement.Handle}
        }
    Friend Sub Handle(context As IModelContext)
        Dim avatar = context.World.Avatar
        Dim searchTarget = context.ReadRemainingTokens()
        Dim feature = avatar.Location.FindFeatureByName(searchTarget)
        If feature IsNot Nothing Then
            avatar.SetFeature(feature)
            Dim objectIdentifier = feature.GetObjectIdentifier()
            If objectIdentifier.HasValue Then
                objectTable(objectIdentifier.Value).Invoke(context)
            Else
                HandleInvalidCommand(context)
                Return
            End If
            avatar.ClearFeature()
            Return
        Else
            HandleInvalidCommand(context)
        End If
    End Sub
End Module
