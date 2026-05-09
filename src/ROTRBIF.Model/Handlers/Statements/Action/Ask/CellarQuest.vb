Imports ROTRBIFOS.Business

Friend Module CellarQuest

    Friend Sub AskGorachanAboutWork(context As IModelContext, interrogator As ICharacter, deponent As ICharacter)
        If interrogator.GetTag(Tags.CELLAR_QUEST) Then
            ShowQuestState(context, interrogator, deponent)
        Else
            GiveQuest(context, interrogator, deponent)
        End If
    End Sub

    Private Sub GiveQuest(context As IModelContext, interrogator As ICharacter, deponent As ICharacter)
        context.Output($"{deponent.GetName} says:")
        context.Output($"""As it turns out, I've got a bunch of rats in my cellar that need some killing.""")
        context.Output($"""I will give you one jools for each rat tail you collect.""")
        context.Output($"""But only ten tails.""")
        context.Output($"""Here's the key to the cellar.""")
        Dim key = interrogator.Inventory.CreateItem(AddressOf CreateCellarKey)
        context.Output($"{deponent.GetName} gives {key.GetName} to {interrogator.GetName}.")
        interrogator.SetTag(Tags.CELLAR_QUEST)
        interrogator.SetStatistic(Statistics.RAT_TAILS_REMAINING, 10)
    End Sub

    Private Sub CreateCellarKey(item As IItem)
        item.SetName(Names.CELLAR_KEY)
        item.SetObjectIdentifier(ObjectIdentifier.CELLAR_KEY)
        item.SetMetadata(Metadatas.KEY_TYPE, KeyTypes.CELLAR_KEY)
    End Sub

    Private Sub ShowQuestState(context As IModelContext, interrogator As ICharacter, deponent As ICharacter)
        Dim ratTailsRemaining = interrogator.GetStatistic(Statistics.RAT_TAILS_REMAINING)
        If ratTailsRemaining > 0 Then
            context.Output($"{deponent.GetName} says:")
            context.Output($"""You still have {ratTailsRemaining} rat tails left to go.""")
        Else
            context.Output($"{deponent.GetName} compliments {interrogator.GetName} on a job well-done.")
        End If
    End Sub
End Module
