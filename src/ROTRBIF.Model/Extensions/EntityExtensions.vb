Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Public Module EntityExtensions
    <Extension>
    Sub SetName(character As IEntity, name As String)
        character.SetMetadata(Metadatas.NAME, name)
    End Sub
    <Extension>
    Function GetName(character As IEntity) As String
        Return character.GetMetadata(Metadatas.NAME)
    End Function
    <Extension>
    Function HasJools(entity As IEntity) As Boolean
        Return entity.HasStatistic(Statistics.JOOLS) AndAlso entity.GetStatistic(Statistics.JOOLS) > 0
    End Function
    <Extension>
    Function GetJools(entity As IEntity) As Integer
        Return entity.GetStatistic(Statistics.JOOLS)
    End Function
End Module
