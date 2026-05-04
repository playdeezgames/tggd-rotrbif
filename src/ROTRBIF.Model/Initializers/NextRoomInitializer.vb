Imports ROTRBIFOS.Business

Friend Module NextRoomInitializer
    Friend Function Initialize(blueRoom As ILocation) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName(Names.THE_NEXT_ROOM)
                   blueRoom.CreateRoute(
                        Direction.Out.GetName(),
                        location,
                        Sub(r)
                            r.SetTag(Tags.IS_LOCKED)
                            r.SetMetadata(Metadatas.KEY_TYPE, KeyTypes.ASS_KEY)
                        End Sub)
                   location.CreateRoute(Direction.In.GetName(), blueRoom)
               End Sub
    End Function
End Module
