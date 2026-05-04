Imports ROTRBIFOS.Business

Friend Module LoftInitializer

    Friend Function Initialize(blueRoom As ILocation) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName(Names.THE_LOFT)
                   location.CreateRoute("down", blueRoom)
                   blueRoom.CreateRoute("up", location)
                   location.CreateFeature(AddressOf InitializeLoftCrate)
               End Sub
    End Function

    Private Sub InitializeLoftCrate(feature As IFeature)
        feature.SetName(Names.CRATE)
        feature.SetObjectIdentifier(ObjectIdentifier.LOFT_CRATE)
    End Sub

End Module
