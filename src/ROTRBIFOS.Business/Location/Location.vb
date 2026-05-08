Imports ROTRBIFOS.Data

Friend Class Location
    Inherits InventoryEntity(Of LocationData)
    Implements ILocation

    Private Sub New(worldData As WorldData, locationId As Guid)
        MyBase.New(worldData)
        Me.LocationId = locationId
    End Sub

    Public ReadOnly Property LocationId As Guid Implements ILocation.LocationId

    Public ReadOnly Property Routes As IEnumerable(Of IRoute) Implements ILocation.Routes
        Get
            Return EntityData.RouteIds.Select(Function(x) Route.TryFind(worldData, x.Value, x.Key))
        End Get
    End Property

    Public ReadOnly Property HasRoutes As Boolean Implements ILocation.HasRoutes
        Get
            Return EntityData.RouteIds.Count <> 0
        End Get
    End Property

    Public ReadOnly Property HasFeatures As Boolean Implements ILocation.HasFeatures
        Get
            Return EntityData.FeatureIds.Count <> 0
        End Get
    End Property

    Public ReadOnly Property Features As IEnumerable(Of IFeature) Implements ILocation.Features
        Get
            Return EntityData.FeatureIds.Select(Function(x) Feature.TryFind(worldData, x))
        End Get
    End Property

    Public ReadOnly Property Characters As IEnumerable(Of ICharacter) Implements ILocation.Characters
        Get
            Return EntityData.CharacterIds.Select(Function(x) Character.TryFind(worldData, x))
        End Get
    End Property

    Protected Overrides ReadOnly Property EntityData As LocationData
        Get
            Return worldData.Locations(LocationId)
        End Get
    End Property

    Public Sub AddCharacter(character As ICharacter) Implements ILocation.AddCharacter
        EntityData.CharacterIds.Add(character.CharacterId)
    End Sub

    Public Sub AddRoute(direction As String, route As IRoute) Implements ILocation.AddRoute
        EntityData.RouteIds(direction) = route.RouteId
    End Sub

    Public Sub RemoveCharacter(character As ICharacter) Implements ILocation.RemoveCharacter
        EntityData.CharacterIds.Remove(character.CharacterId)
    End Sub

    Public Sub AddFeature(feature As IFeature) Implements ILocation.AddFeature
        EntityData.FeatureIds.Add(feature.FeatureId)
    End Sub

    Friend Shared Function TryFind(worldData As WorldData, locationId As Guid?) As ILocation
        Return If(
            locationId.HasValue AndAlso worldData.Locations.ContainsKey(locationId.Value),
            New Location(worldData, locationId.Value),
            Nothing)
    End Function

    Public Function CreateCharacter(Optional characterInitializer As Action(Of ICharacter) = Nothing) As ICharacter Implements ILocation.CreateCharacter
        Dim characterId = Guid.NewGuid
        worldData.Characters(characterId) = New CharacterData With
            {
                .LocationId = LocationId
            }
        Dim character = Business.Character.TryFind(worldData, characterId)
        AddCharacter(character)
        characterInitializer?.Invoke(character)
        Return character
    End Function

    Public Function CreateRoute(direction As String, destination As ILocation, Optional routeInitializer As Action(Of IRoute) = Nothing) As IRoute Implements ILocation.CreateRoute
        Dim routeId = Guid.NewGuid
        worldData.Routes(routeId) = New RouteData With
            {
                .DestinationLocationId = destination.LocationId
            }
        Dim route = Business.Route.TryFind(worldData, routeId, direction)
        AddRoute(direction, route)
        routeInitializer?.Invoke(route)
        Return route
    End Function

    Public Function GetRoute(direction As String) As IRoute Implements ILocation.GetRoute
        Dim routeId As Guid
        If EntityData.RouteIds.TryGetValue(direction, routeId) Then
            Return Route.TryFind(worldData, routeId, direction)
        End If
        Return Nothing
    End Function

    Public Function CreateFeature(Optional featureInitializer As Action(Of IFeature) = Nothing) As IFeature Implements ILocation.CreateFeature
        Dim featureId = Guid.NewGuid
        worldData.Features(featureId) = New FeatureData With
            {
                .LocationId = LocationId
            }
        Dim feature = Business.Feature.TryFind(worldData, featureId)
        AddFeature(feature)
        featureInitializer?.Invoke(feature)
        Return feature
    End Function

    Public Function HasOthers(character As ICharacter) As Boolean Implements ILocation.HasOthers
        Return Characters.Any(Function(x) x.CharacterId <> character.CharacterId)
    End Function
End Class
