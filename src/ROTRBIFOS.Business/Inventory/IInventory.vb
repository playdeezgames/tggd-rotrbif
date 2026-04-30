Public Interface IInventory
    Function CreateItem(Optional itemInitializer As Action(Of IItem) = Nothing) As IItem
    Sub AddItem(item As IItem)
    ReadOnly Property Items As IEnumerable(Of IItem)
    Sub RemoveItem(item As IItem)
    ReadOnly Property InventoryId As Guid
End Interface
