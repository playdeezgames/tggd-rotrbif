Imports ROTRBIFOS.Data

Friend Class Route
    Inherits Entity(Of RouteData)
    Implements IRoute

    Public Sub New(
                  worldData As WorldData,
                  routeId As Guid,
                  direction As String)
        MyBase.New(worldData)
        Me.RouteId = routeId
        Me.Direction = direction
    End Sub

    Public ReadOnly Property RouteId As Guid Implements IRoute.RouteId

    Public ReadOnly Property Destination As ILocation Implements IRoute.Destination
        Get
            Return Business.Location.TryFind(
                worldData,
                EntityData.DestinationLocationId)
        End Get
    End Property

    Public ReadOnly Property Direction As String Implements IRoute.Direction

    Protected Overrides ReadOnly Property EntityData As RouteData
        Get
            Return worldData.Routes(RouteId)
        End Get
    End Property

    Friend Shared Function TryFind(
                                  worldData As Data.WorldData,
                                  routeId As Guid?,
                                  direction As String) As IRoute
        Return If(
            routeId.HasValue AndAlso worldData.Routes.ContainsKey(routeId.Value),
            New Route(worldData, routeId.Value, direction),
            Nothing)
    End Function
End Class
