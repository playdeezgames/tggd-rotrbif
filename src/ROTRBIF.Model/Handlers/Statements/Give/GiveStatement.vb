Imports ROTRBIFOS.Business

Friend Module GiveStatement
    Const ToToken As String = "to"
    Friend Sub Handle(context As IModelContext)
        If Not context.HasToken(ToToken) Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim itemName = context.ReadUntilToken(ToToken)
        context.DiscardToken()
        Dim avatar = context.World.Avatar
        Dim item = avatar.Inventory.FindItemByName(itemName)
        If item Is Nothing Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim characterName = context.ReadRemainingTokens()
        Dim character = avatar.Location.FindOtherByName(avatar, characterName)
        If character Is Nothing Then
            HandleInvalidCommand(context)
            Return
        End If
        GiveItem(context, avatar, item, character)
    End Sub

    Private ReadOnly giveTable As IReadOnlyDictionary(Of ObjectIdentifier, Action(Of IModelContext, ICharacter, IItem, ICharacter)) =
        New Dictionary(Of ObjectIdentifier, Action(Of IModelContext, ICharacter, IItem, ICharacter)) From
        {
            {ObjectIdentifier.GORACHAN, AddressOf GiveGorachanStatement.Handle}
        }

    Private Sub GiveItem(
                        context As IModelContext,
                        giver As ICharacter,
                        gift As IItem,
                        recipient As ICharacter)
        If Not WillAccept(recipient, gift) Then
            context.Output($"{recipient.GetName} refuses to take {gift.GetName}.")
            Return
        End If
        giveTable(recipient.GetObjectIdentifier.Value).Invoke(context, giver, gift, recipient)
    End Sub

    Private ReadOnly acceptTable As IReadOnlyDictionary(Of ObjectIdentifier, HashSet(Of ObjectIdentifier)) =
        New Dictionary(Of ObjectIdentifier, HashSet(Of ObjectIdentifier)) From
        {
            {ObjectIdentifier.GORACHAN, New HashSet(Of ObjectIdentifier) From {ObjectIdentifier.RAT_TAIL}}
        }

    Private Function WillAccept(recipient As ICharacter, gift As IItem) As Boolean
        Dim recipientObjectIdentifier = recipient.GetObjectIdentifier()
        Dim giftObjectIdentifier = gift.GetObjectIdentifier()
        Return recipientObjectIdentifier.HasValue AndAlso
            giftObjectIdentifier.HasValue AndAlso
            acceptTable(recipientObjectIdentifier.Value).
                Contains(giftObjectIdentifier.Value)
    End Function
End Module
