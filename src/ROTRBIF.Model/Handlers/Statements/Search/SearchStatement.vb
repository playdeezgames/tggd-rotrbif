Imports ROTRBIFOS.Business

Friend Module SearchStatement
    Private ReadOnly objectTable As IReadOnlyDictionary(Of ObjectIdentifier, Action(Of IModelContext)) =
        New Dictionary(Of ObjectIdentifier, Action(Of IModelContext)) From
        {
            {ObjectIdentifier.LOFT_CRATE, AddressOf SearchLoftCrateStatement.Handle},
            {ObjectIdentifier.BED, AddressOf SearchBedStatement.Handle},
            {ObjectIdentifier.CELLAR, AddressOf SearchCellarStatement.Handle}
        }
    Friend Function HandleSearchFeature(context As IModelContext, avatar As ICharacter, searchTarget As String) As Boolean
        Dim feature = avatar.Location.FindFeatureByName(searchTarget)
        If feature Is Nothing Then
            Return False
        End If
        avatar.SetFeature(feature)
        If feature.GetTag(Tags.SEARCHED) Then
            context.Output($"{avatar.GetName} has already searched {feature.GetName}.")
        Else
            context.Output($"{avatar.GetName} searches {feature.GetName}.")
            SearchObject(context, avatar, feature.GetObjectIdentifier())
            feature.SetTag(Tags.SEARCHED)
        End If
        avatar.ClearFeature()
        Return True
    End Function

    Private Sub SearchObject(context As IModelContext, avatar As ICharacter, objectIdentifier As ObjectIdentifier?)
        If objectIdentifier.HasValue Then
            objectTable(objectIdentifier.Value).Invoke(context)
        Else
            context.Output($"{avatar.GetName} finds nothing.")
        End If
    End Sub

    Friend Sub Handle(context As IModelContext)
        Dim avatar = context.World.Avatar
        Dim searchTarget = context.ReadRemainingTokens()
        If HandleSearchFeature(context, avatar, searchTarget) Then
        ElseIf HandleSearchLocation(context, avatar, searchTarget) Then
        Else
            HandleInvalidCommand(context)
        End If
    End Sub

    Private Function HandleSearchLocation(context As IModelContext, avatar As ICharacter, searchTarget As String) As Boolean
        Dim location = avatar.Location
        If location.GetName <> searchTarget Then
            Return False
        End If
        context.Output($"{avatar.GetName} searches {location.GetName}.")
        SearchObject(context, avatar, location.GetObjectIdentifier())
        Return True
    End Function
End Module
