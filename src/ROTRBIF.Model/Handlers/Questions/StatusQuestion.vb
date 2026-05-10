Friend Module StatusQuestion
    Friend Sub Handle(context As IModelContext)
        context.TerminalDispatch(
            Sub(x)
                Dim avatar = x.World.Avatar
                x.Output($"{avatar.GetName()} is {avatar.GetAliveStatus()}.")
                If avatar.IsDead Then
                    Return
                End If
                x.Output($"{avatar.GetName()} is facing {avatar.GetFacing().GetName()}.")
                If avatar.IsBentOver Then
                    x.Output($"{avatar.GetName()} is bent over.")
                End If
                If avatar.HasCheckedIt Then
                    x.Output($"{avatar.GetName()} has checked it {avatar.GetCheckCount()} times.")
                End If
                Dim location = avatar.Location
                x.Output($"{avatar.GetName()} is in {location.GetName()}.")
                If location.HasItems() Then
                    x.Output($"{avatar.GetName()} sees things on the ground.")
                End If
                If location.HasRoutes Then
                    x.Output($"{avatar.GetName()} sees exits.")
                End If
                If location.HasFeatures Then
                    x.Output($"{avatar.GetName()} sees features.")
                End If
                If location.HasOthers(avatar) Then
                    x.Output($"{avatar.GetName()} sees others.")
                End If
                If avatar.HasJools() Then
                    x.Output($"{avatar.GetName} has {avatar.GetJools} jools.")
                End If
            End Sub,
            AddressOf HandleInvalidCommand)
    End Sub

End Module
