Imports ROTRBIFOS.Data

Friend Class Item
    Inherits Entity(Of ItemData)
    Implements IItem

    Private Sub New(worldData As WorldData, itemId As Guid)
        MyBase.New(worldData)
        Me.itemId = itemId
    End Sub

    Public ReadOnly Property ItemId As Guid Implements IItem.ItemId

    Protected Overrides ReadOnly Property EntityData As ItemData
        Get
            Return worldData.Items(ItemId)
        End Get
    End Property

    Friend Shared Function TryFind(worldData As WorldData, itemId As Guid) As IItem
        Return New Item(worldData, itemId)
    End Function
End Class
