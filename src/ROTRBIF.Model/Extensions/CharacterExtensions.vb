Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Public Module CharacterExtensions
    <Extension>
    Function GetAliveStatus(character As ICharacter) As String
        Return If(character.GetTag(Tags.ALIVE), "alive", "dead")
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
    Sub SetAlive(character As ICharacter)
        character.SetTag(Tags.ALIVE)
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
End Module
