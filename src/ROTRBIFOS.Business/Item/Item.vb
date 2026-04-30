Imports ROTRBIFOS.Data

Friend Class Item
    Inherits Entity(Of ItemData)
    Implements IItem

    Private Sub New(worldData As WorldData, itemId As Guid)
        MyBase.New(worldData)
        Me.itemId = itemId
    End Sub

    Public ReadOnly Property ItemId As Guid Implements IItem.ItemId

    Public Property Inventory As IInventory Implements IItem.Inventory
        Get
            Return Business.Inventory.TryFind(worldData, EntityData.InventoryId)
        End Get
        Set(value As IInventory)
            EntityData.InventoryId = value.InventoryId
        End Set
    End Property

    Protected Overrides ReadOnly Property EntityData As ItemData
        Get
            Return worldData.Items(ItemId)
        End Get
    End Property

    Friend Shared Function TryFind(worldData As WorldData, itemId As Guid) As IItem
        Return New Item(worldData, itemId)
    End Function
End Class
