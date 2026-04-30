Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Friend Module InventoryExtensions
    <Extension>
    Friend Function TryFindItemByName(inventory As IInventory, itemName As String) As IItem
        Return inventory.Items.FirstOrDefault(Function(x) x.GetName() = itemName)
    End Function
    <Extension>
    Friend Function GetInventoryText(inventory As IInventory) As String
        Dim items = inventory.Items
        Select Case items.Count
            Case 0
                Return "nothing"
            Case 1
                Return items.Single.GetName()
            Case 2
                Return $"{items.First.GetName()} and {items.Last.GetName()}"
            Case Else
                Return $"{String.Join(", ", items.Take(items.Count - 1).Select(Function(y) y.GetName()))} And {items.Last.GetName()}"
        End Select
    End Function
End Module
