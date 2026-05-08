Imports ROTRBIFOS.Business
Imports TGGD.Business

Friend Module CheckStatement
    Const ItToken = "it"

    Private ReadOnly checkTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {ItToken, AddressOf HandleCheckItCommand}
        }

    Private Sub HandleCheckItCommand(context As IModelContext)
        context.TerminalDispatch(
            Sub(x)
                Dim avatar = context.World.Avatar
                If Not avatar.IsBentOver Then
                    context.Output($"{avatar.GetName()} cannot check it without bending over.")
                Else
                    DoCheckIt(context, avatar)
                End If
            End Sub,
            AddressOf HandleInvalidCommand)
    End Sub

    Private Sub DoCheckIt(context As IModelContext, avatar As ICharacter)
        avatar.IncrementCheckCount()
        context.Output($"{avatar.GetName()} checks it.")
        If avatar.HasAssKey() AndAlso RNG.FromRange(1, 6) = 1 Then
            avatar.ClearTag(Tags.HAS_ASS_KEY)
            Dim item = avatar.Inventory.CreateItem(AddressOf InitializeAssKey)
            context.Output($"{avatar.GetName()} finds {item.GetName()}.")
        Else
            context.Output($"It seems fine.")
        End If
    End Sub

    Private Sub InitializeAssKey(item As IItem)
        item.SetName(Names.ASS_KEY)
        item.SetMetadata(Metadatas.KEY_TYPE, KeyTypes.ASS_KEY)
    End Sub

    Friend Sub Handle(context As IModelContext)
        context.Dispatch(checkTable, AddressOf HandleInvalidCommand)
    End Sub
End Module
