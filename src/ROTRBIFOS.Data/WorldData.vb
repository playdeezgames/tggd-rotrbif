Public Class WorldData
    Public Property AvatarCharacterId As Guid? = Nothing
    Public Property Characters As New Dictionary(Of Guid, CharacterData)
End Class
