Friend Module SearchCellarStatement
    Friend Sub Handle(context As IModelContext)
        Dim avatar = context.World.Avatar
        Dim location = avatar.Location
        Dim rat = location.GetOthers(avatar).FirstOrDefault(Function(x) x.GetMetadata(Metadatas.ENEMY_TYPE) = EnemyTypes.RAT)
        If rat IsNot Nothing Then
            context.Output($"{avatar.GetName} is engaged in combat with {rat.GetName} and cannot search now.")
        Else
            rat = location.CreateCharacter(AddressOf CreateRat)
            context.Output($"{avatar.GetName} finds {rat.GetName}!")
        End If
    End Sub

    Private Sub CreateRat(character As ROTRBIFOS.Business.ICharacter)
        character.SetName(Names.RAT)
        character.SetMetadata(Metadatas.ENEMY_TYPE, EnemyTypes.RAT)
        character.SetStatisticMinimum(Statistics.HEALTH, 0)
        character.SetStatistic(Statistics.HEALTH, 1)
        character.SetStatisticMaximum(Statistics.HEALTH, 1)
        character.SetStatistic(Statistics.ATTACK_DICE, 1)
        character.SetStatistic(Statistics.ATTACK_LIMIT, 1)
        character.SetStatistic(Statistics.DEFEND_DICE, 1)
        character.SetStatistic(Statistics.DEFEND_LIMIT, 1)
        character.SetTag(Tags.ENEMY)
        character.Inventory.CreateItem(AddressOf CreateRatTail)
    End Sub

    Private Sub CreateRatTail(item As ROTRBIFOS.Business.IItem)
        item.SetName(Names.RAT_TAIL)
        item.SetObjectIdentifier(ObjectIdentifier.RAT_TAIL)
    End Sub
End Module
