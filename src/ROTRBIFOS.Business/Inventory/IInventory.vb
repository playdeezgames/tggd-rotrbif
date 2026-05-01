Public Interface IInventory
    Function CreateItem(Optional itemInitializer As Action(Of IItem) = Nothing) As IItem
    Sub AddItem(item As IItem)
    ReadOnly Property Items As IEnumerable(Of IItem)
    Sub RemoveItem(item As IItem)
    ReadOnly Property InventoryId As Guid
    ReadOnly Property HasItems As Boolean
    Function FindItems(predicate As Func(Of IItem, Boolean)) As IEnumerable(Of IItem)
End Interface
