Imports ROTRBIFOS.Data

Friend Class Inventory
    Implements IInventory

    Private ReadOnly worldData As WorldData
    Private ReadOnly inventoryId As Guid

    Private ReadOnly Property InventoryData As InventoryData
        Get
            Return worldData.Inventories(inventoryId)
        End Get
    End Property

    Public ReadOnly Property Items As IEnumerable(Of IItem) Implements IInventory.Items
        Get
            Return InventoryData.ItemIds.Select(Function(x) Item.TryFind(worldData, x))
        End Get
    End Property

    Sub New(
           worldData As WorldData,
           inventoryId As Guid)
        Me.worldData = worldData
        Me.inventoryId = inventoryId
    End Sub

    Public Sub AddItem(item As IItem) Implements IInventory.AddItem
        InventoryData.ItemIds.Add(item.ItemId)
    End Sub

    Friend Shared Function TryFind(worldData As WorldData, inventoryId As Guid?) As IInventory
        Return If(
            inventoryId.HasValue AndAlso worldData.Inventories.ContainsKey(inventoryId.Value),
            New Inventory(worldData, inventoryId.Value),
            Nothing)
    End Function

    Public Function CreateItem(Optional itemInitializer As Action(Of IItem) = Nothing) As IItem Implements IInventory.CreateItem
        Dim itemId = Guid.NewGuid
        worldData.Items(itemId) = New ItemData With
            {
                .inventoryId = inventoryId
            }
        Dim item = Business.Item.TryFind(worldData, itemId)
        AddItem(item)
        itemInitializer?.Invoke(item)
        Return item
    End Function
End Class
