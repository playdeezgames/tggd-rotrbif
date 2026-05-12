Imports ROTRBIFOS.Business

Friend Module GoStatement
    Friend Sub Handle(context As IModelContext)
        Dim avatar = context.World.Avatar
        If avatar.IsDead Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim direction = context.ReadRemainingTokens()
        Dim route = avatar.Location.GetRoute(direction)
        If route Is Nothing Then
            HandleInvalidCommand(context)
            Return
        End If
        If route.GetTag(Tags.IS_LOCKED) Then
            context.Output($"{avatar.GetName()} cannot go that way for it is locked.")
            Return
        End If
        context.Output($"{avatar.GetName} goes {direction}.")
        avatar.Location = route.Destination
        CheckWanderingMonsters(context, avatar.Location)
    End Sub

    Private ReadOnly wanderingMonsterGenerators As IReadOnlyDictionary(Of WanderingMonsters, Action(Of IModelContext, ILocation)) =
        New Dictionary(Of WanderingMonsters, Action(Of IModelContext, ILocation)) From
        {
            {WanderingMonsters.WILDERNESS, AddressOf GenerateWildernessWanderingMonster}
        }

    Private Sub GenerateWildernessWanderingMonster(context As IModelContext, location As ILocation)
        Dim enemy = location.CreateCharacter(AddressOf InitializePea)
        context.Output($"{context.World.Avatar.GetName} is waylaid by {enemy.GetName}!")
    End Sub

    Private Sub InitializePea(character As ICharacter)
        character.SetName("pea")
        character.SetMetadata(Metadatas.ENEMY_TYPE, EnemyTypes.PEA)
        character.SetStatisticMinimum(Statistics.HEALTH, 0)
        character.SetStatistic(Statistics.HEALTH, 1)
        character.SetStatisticMaximum(Statistics.HEALTH, 1)
        character.SetStatistic(Statistics.ATTACK_DICE, 1)
        character.SetStatistic(Statistics.ATTACK_LIMIT, 1)
        character.SetStatistic(Statistics.DEFEND_DICE, 1)
        character.SetStatistic(Statistics.DEFEND_LIMIT, 1)
        character.SetStatistic(Statistics.XP_VALUE, 1)
        character.SetTag(Tags.ENEMY)
    End Sub

    Private Sub CheckWanderingMonsters(context As IModelContext, location As ILocation)
        Dim wanderingMonsters = location.GetWanderingMonsters()
        If Not wanderingMonsters.HasValue OrElse location.HasOthers(context.World.Avatar) Then
            Return
        End If
        wanderingMonsterGenerators(wanderingMonsters.Value).Invoke(context, location)
    End Sub
End Module
