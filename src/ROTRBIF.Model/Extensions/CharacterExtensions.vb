Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business
Imports TGGD.Business

Public Module CharacterExtensions
    <Extension>
    Function GetAliveStatus(character As ICharacter) As String
        Return If(Not character.IsDead, "alive", "dead")
    End Function
    <Extension>
    Function GetFacing(character As ICharacter) As Direction
        Return CType(character.GetStatistic(Statistics.FACING), Direction)
    End Function
    <Extension>
    Sub Turn(character As ICharacter, turn As Turn)
        character.SetFacing(turn.NextFacing(character.GetFacing()))
    End Sub
    <Extension>
    Sub SetFacing(character As ICharacter, direction As Direction)
        character.SetStatistic(Statistics.FACING, CInt(direction))
    End Sub
    <Extension>
    Function IsBentOver(character As ICharacter) As Boolean
        Return character.GetTag(Tags.BENT_OVER)
    End Function
    <Extension>
    Sub BendOver(character As ICharacter)
        character.SetTag(Tags.BENT_OVER)
    End Sub
    <Extension>
    Sub StraightenUp(character As ICharacter)
        character.ClearTag(Tags.BENT_OVER)
    End Sub
    <Extension>
    Sub SetCheckCount(character As ICharacter, checkCount As Integer)
        character.SetStatistic(Statistics.CHECK_COUNT, checkCount)
    End Sub
    <Extension>
    Function HasCheckedIt(character As ICharacter) As Boolean
        Return character.GetStatistic(Statistics.CHECK_COUNT) > 0
    End Function
    <Extension>
    Function GetCheckCount(character As ICharacter) As Integer
        Return character.GetStatistic(Statistics.CHECK_COUNT)
    End Function
    <Extension>
    Sub IncrementCheckCount(character As ICharacter)
        character.SetCheckCount(character.GetCheckCount() + 1)
    End Sub
    <Extension>
    Function HasAssKey(character As ICharacter) As Boolean
        Return character.GetTag(Tags.HAS_ASS_KEY)
    End Function
    <Extension>
    Sub SetFeature(character As ICharacter, feature As IFeature)
        character.SetYoke(Yokes.FEATURE, feature.FeatureId)
    End Sub
    <Extension>
    Sub ClearFeature(character As ICharacter)
        character.ClearYoke(Yokes.FEATURE)
    End Sub
    <Extension>
    Function GetFeature(character As ICharacter) As IFeature
        If character.HasYoke(Yokes.FEATURE) Then
            Return character.World.GetFeature(character.GetYoke(Yokes.FEATURE))
        End If
        Return Nothing
    End Function
    <Extension>
    Function IsDead(character As ICharacter) As Boolean
        Return character.GetStatistic(Statistics.HEALTH) <= character.GetStatisticMinimum(Statistics.HEALTH)
    End Function
    Const DIE_SIZE = 6
    <Extension>
    Function RollAttack(character As ICharacter) As Integer
        Dim attackDice = character.GetStatistic(Statistics.ATTACK_DICE)
        Dim attackLimit = character.GetStatistic(Statistics.ATTACK_LIMIT)
        Return Math.Min(
            attackLimit,
            Enumerable.
                Range(0, attackDice).
                Select(Function(x) RNG.FromRange(1, DIE_SIZE) = DIE_SIZE).
                Count(Function(x) x))
    End Function
    <Extension>
    Function RollDefend(character As ICharacter) As Integer
        Dim attackDice = character.GetStatistic(Statistics.DEFEND_DICE)
        Dim attackLimit = character.GetStatistic(Statistics.DEFEND_LIMIT)
        Return Math.Min(
            attackLimit,
            Enumerable.
                Range(0, attackDice).
                Select(Function(x) RNG.FromRange(1, DIE_SIZE) = DIE_SIZE).
                Count(Function(x) x))
    End Function
    <Extension>
    Sub DoDamage(character As ICharacter, damage As Integer)
        character.ChangeStatistic(Statistics.HEALTH, -damage)
    End Sub
    <Extension>
    Sub Kill(character As ICharacter)
        If character.IsAvatar Then
            Return
        End If
        Dim locationInventory = character.Location.Inventory
        For Each item In character.Inventory.Items
            item.Inventory = locationInventory
            locationInventory.AddItem(item)
        Next
        character.Destroy()
    End Sub
    <Extension>
    Function GetHealth(character As ICharacter) As Integer
        Return character.GetStatistic(Statistics.HEALTH)
    End Function
    <Extension>
    Function GetMaximumHealth(character As ICharacter) As Integer
        Return character.GetStatisticMaximum(Statistics.HEALTH)
    End Function
End Module
