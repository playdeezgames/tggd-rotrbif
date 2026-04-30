Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Public Module InventoryEntityExtensions
    <Extension>
    Function HasItems(entity As IInventoryEntity) As Boolean
        Return entity.Inventory.HasItems
    End Function
End Module
