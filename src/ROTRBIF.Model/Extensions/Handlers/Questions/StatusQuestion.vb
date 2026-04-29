Friend Module StatusQuestion
    Friend Sub Handle(context As IModelContext)
        context.TerminalDispatch(
            Sub(x)
                Dim avatar = x.World.Avatar
                x.Output($"{avatar.GetName()} is {avatar.GetAliveStatus()}.")
                x.Output($"{avatar.GetName()} is facing {avatar.GetFacing().GetName()}.")
                If avatar.IsBentOver Then
                    x.Output($"{avatar.GetName()} is bent over.")
                End If
                If avatar.HasCheckedIt Then
                    x.Output($"{avatar.GetName()} has checked it {avatar.GetCheckCount()} times.")
                End If
                x.Output($"{avatar.GetName()} is in {avatar.Location.GetName()}.")
            End Sub,
            AddressOf HandleInvalidCommand)
    End Sub

End Module
