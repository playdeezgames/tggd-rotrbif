Public Interface IRoute
    Inherits IEntity
    ReadOnly Property Destination As ILocation
    ReadOnly Property Direction As String
    ReadOnly Property RouteId As Guid
End Interface
