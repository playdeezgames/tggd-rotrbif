Public Interface IItem
    Inherits IEntity
    ReadOnly Property ItemId As Guid
    Property Inventory As IInventory
End Interface
