Public Interface ILocation
    Inherits IInventoryEntity
    ReadOnly Property LocationId As Guid
    Sub AddCharacter(character As ICharacter)
    Function CreateCharacter(Optional characterInitializer As Action(Of ICharacter) = Nothing) As ICharacter
    ReadOnly Property Routes As IEnumerable(Of IRoute)
    Function CreateRoute(direction As String, destination As ILocation, Optional routeInitializer As Action(Of IRoute) = Nothing) As IRoute
    Sub AddRoute(direction As String, route As IRoute)
    Function GetRoute(direction As String) As IRoute
    Sub RemoveCharacter(character As ICharacter)
End Interface
