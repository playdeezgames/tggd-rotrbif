Public Class WorldData
    Public Property AvatarCharacterId As Guid? = Nothing
    Public Property Characters As New Dictionary(Of Guid, CharacterData)
    Public Property Locations As New Dictionary(Of Guid, LocationData)
    Public Property Inventories As New Dictionary(Of Guid, InventoryData)
    Public Property Items As New Dictionary(Of Guid, ItemData)
    Public Property Routes As New Dictionary(Of Guid, RouteData)
    Public Property Features As New Dictionary(Of Guid, FeatureData)
    Public Property Triggers As New Dictionary(Of Guid, TriggerData)
End Class
