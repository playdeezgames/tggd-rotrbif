Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Friend Module InventoryExtensions
    <Extension>
    Friend Function TryFindItemByName(inventory As IInventory, itemName As String) As IItem
        Return inventory.Items.FirstOrDefault(Function(x) x.GetName() = itemName)
    End Function
End Module
