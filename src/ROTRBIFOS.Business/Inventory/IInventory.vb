Public Interface IInventory
    Function CreateItem(Optional itemInitializer As Action(Of IItem) = Nothing) As IItem
    Sub AddItem(item As IItem)
End Interface
