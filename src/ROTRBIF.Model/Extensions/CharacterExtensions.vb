Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Public Module CharacterExtensions
    <Extension>
    Function GetAliveStatus(character As ICharacter) As String
        Return If(character.GetTag(Tags.ALIVE), "alive", "dead")
    End Function
    <Extension>
    Function GetFacing(character As ICharacter) As CardinalDirection
        Return CType(character.GetStatistic(Statistics.FACING), CardinalDirection)
    End Function
    <Extension>
    Sub Turn(character As ICharacter, turn As Turn)
        character.SetFacing(turn.NextFacing(character.GetFacing()))
    End Sub
    <Extension>
    Sub SetName(character As IEntity, name As String)
        character.SetMetadata(Metadatas.NAME, name)
    End Sub
    <Extension>
    Function GetName(character As IEntity) As String
        Return character.GetMetadata(Metadatas.NAME)
    End Function
    <Extension>
    Sub SetAlive(character As ICharacter)
        character.SetTag(Tags.ALIVE)
    End Sub
    <Extension>
    Sub SetFacing(character As ICharacter, direction As CardinalDirection)
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
End Module
