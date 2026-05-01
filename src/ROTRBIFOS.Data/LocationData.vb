Public Class LocationData
    Inherits InventoryEntityData
    Public Property CharacterIds As New HashSet(Of Guid)
    Public Property RouteIds As New Dictionary(Of String, Guid)
    Public Property FeatureIds As New HashSet(Of Guid)
End Class
