Imports ROTRBIFOS.Data

Friend MustInherit Class InventoryEntity(Of TEntity As InventoryEntityData)
    Inherits Entity(Of TEntity)
    Implements IInventoryEntity

    Protected Sub New(worldData As WorldData)
        MyBase.New(worldData)
    End Sub

    Public ReadOnly Property Inventory As IInventory Implements IInventoryEntity.Inventory
        Get
            Dim inventoryId = EntityData.InventoryId
            If Not inventoryId.HasValue Then
                inventoryId = Guid.NewGuid
                worldData.Inventories(inventoryId.Value) = New InventoryData
                EntityData.InventoryId = inventoryId.Value
            End If
            Return Business.Inventory.TryFind(worldData, inventoryId.Value)
        End Get
    End Property
End Class
