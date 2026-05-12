Imports ROTRBIFOS.Business

Friend Module AttackExclamation
    Friend Sub Handle(context As IModelContext)
        Dim avatar = context.World.Avatar
        If avatar.IsDead Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim defenderName = context.ReadRemainingTokens
        Dim defender = avatar.Location.GetOthers(avatar).FirstOrDefault(Function(x) x.GetName = defenderName)
        If defender Is Nothing Then
            HandleInvalidCommand(context)
            Return
        End If
        If Not defender.GetTag(Tags.ENEMY) Then
            context.Output($"{avatar.GetName} may not attack {defender.GetName}.")
            Return
        End If
        DoAttack(context, avatar, defender)
        If defender.Exists AndAlso Not defender.IsDead() Then
            DoAttack(context, defender, avatar)
        End If
    End Sub

    Private Sub DoAttack(context As IModelContext, attacker As ICharacter, defender As ICharacter)
        context.Output($"{attacker.GetName} attacks {defender.GetName}!")
        Dim attackRoll = attacker.RollAttack()
        context.Output($"{attacker.GetName} rolls an attack of {attackRoll}!")
        Dim defendRoll = defender.RollDefend()
        context.Output($"{defender.GetName} rolls a defend of {defendRoll}!")
        If attackRoll > defendRoll Then
            Dim damage = attackRoll - defendRoll
            context.Output($"{defender.GetName} takes {damage} damage!")
            defender.DoDamage(damage)
            If defender.IsDead() Then
                context.Output($"{attacker.GetName} kills {defender.GetName}!")
                If defender.HasStatistic(Statistics.XP_VALUE) Then
                    AwardXP(context, attacker, defender.GetStatistic(Statistics.XP_VALUE))
                End If
                defender.Kill()
            Else
                context.Output($"{defender.GetName} has {defender.GetHealth()} health remaining!")
            End If
        Else
            context.Output($"{attacker.GetName} misses!")
        End If
    End Sub

    Private Sub AwardXP(context As IModelContext, recipient As ICharacter, xp As Integer)
        If Not recipient.HasStatistic(Statistics.XP) Then
            Return
        End If
        context.Output($"{recipient.GetName} gains {xp} XP.")
        Dim levelUp As Boolean = False
        While xp > 0
            Dim xpToNextLevel = recipient.GetStatisticMaximum(Statistics.XP) - recipient.GetStatistic(Statistics.XP)
            recipient.ChangeStatistic(Statistics.XP, Math.Min(xp, xpToNextLevel))
            If xp >= xpToNextLevel Then
                recipient.SetStatistic(Statistics.XP, 0)
                recipient.SetStatisticMaximum(Statistics.XP, recipient.GetStatisticMaximum(Statistics.XP) * 2)
                levelUp = True
                recipient.ChangeStatistic(Statistics.XP_LEVEL, 1)
                recipient.ChangeStatistic(Statistics.SKILL_POINTS, recipient.GetStatistic(Statistics.XP_LEVEL))
            End If
            xp -= xpToNextLevel
        End While
        If levelUp Then
            context.Output($"{recipient.GetName} is now level {recipient.GetXPLevel()}.")
            context.Output($"{recipient.GetName} now has {recipient.GetSkillPoints()} skill points.")
        End If
    End Sub
End Module
