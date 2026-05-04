Friend Module SearchStatement
    Private ReadOnly objectTable As IReadOnlyDictionary(Of ObjectIdentifier, Action(Of IModelContext)) =
        New Dictionary(Of ObjectIdentifier, Action(Of IModelContext)) From
        {
            {ObjectIdentifier.LOFT_CRATE, AddressOf SearchLoftCrateStatement.Handle},
            {ObjectIdentifier.BED, AddressOf SearchBedStatement.Handle}
        }
    Friend Sub Handle(context As IModelContext)
        Dim avatar = context.World.Avatar
        Dim searchTarget = context.ReadRemainingTokens()
        Dim feature = avatar.Location.FindFeatureByName(searchTarget)
        If feature IsNot Nothing Then
            avatar.SetFeature(feature)
            If feature.GetTag(Tags.SEARCHED) Then
                context.Output($"{avatar.GetName} has already searched {feature.GetName}.")
            Else
                context.Output($"{avatar.GetName} searches {feature.GetName}.")
                Dim objectIdentifier = feature.GetObjectIdentifier()
                If objectIdentifier.HasValue Then
                    objectTable(objectIdentifier.Value).Invoke(context)
                Else
                    context.Output($"{avatar.GetName} finds nothing.")
                End If
                feature.SetTag(Tags.SEARCHED)
            End If
            avatar.ClearFeature()
            Return
        Else
            HandleInvalidCommand(context)
        End If
    End Sub
End Module
