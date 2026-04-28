Public Class WorldData
    Public Property AvatarCharacterId As Guid? = Nothing
    Public Property Characters As New Dictionary(Of Guid, CharacterData)
    Public Property Locations As New Dictionary(Of Guid, LocationData)
End Class
