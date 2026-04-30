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
End Module
