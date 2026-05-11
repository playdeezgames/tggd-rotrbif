Imports ROTRBIFOS.Business

Module GiveGorachanStatement
    Private ReadOnly table As IReadOnlyDictionary(Of ObjectIdentifier, Action(Of IModelContext, ICharacter, IItem, ICharacter)) =
        New Dictionary(Of ObjectIdentifier, Action(Of IModelContext, ICharacter, IItem, ICharacter)) From
        {
            {ObjectIdentifier.RAT_TAIL, AddressOf AcceptRatTail}
        }

    Private Sub AcceptRatTail(context As IModelContext, giver As ICharacter, gift As IItem, recipient As ICharacter)
        If giver.GetStatistic(Statistics.RAT_TAILS_REMAINING) <= 0 Then
            context.Output($"{recipient.GetName} won't accept another {gift.GetName} from {giver.GetName}.")
            Return
        End If
        context.Output($"{recipient.GetName} trades 1 jools to {giver.GetName} for the {gift.GetName}.")
        giver.Inventory.RemoveItem(gift)
        gift.Destroy()
        giver.ChangeStatistic(Statistics.JOOLS, 1)
        giver.ChangeStatistic(Statistics.RAT_TAILS_REMAINING, -1)
    End Sub

    Friend Sub Handle(context As IModelContext, giver As ICharacter, gift As IItem, recipient As ICharacter)
        table(gift.GetObjectIdentifier.Value).Invoke(context, giver, gift, recipient)
    End Sub
End Module
