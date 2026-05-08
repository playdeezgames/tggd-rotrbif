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
    Function CreateFeature(Optional featureInitializer As Action(Of IFeature) = Nothing) As IFeature
    Sub AddFeature(feature As IFeature)
    ReadOnly Property HasRoutes As Boolean
    ReadOnly Property HasFeatures As Boolean
    ReadOnly Property Features As IEnumerable(Of IFeature)
    Function HasOthers(character As ICharacter) As Boolean
    ReadOnly Property Characters As IEnumerable(Of ICharacter)
End Interface
