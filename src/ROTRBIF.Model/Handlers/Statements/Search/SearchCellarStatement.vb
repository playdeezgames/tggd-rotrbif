Friend Module SearchCellarStatement
    Friend Sub Handle(context As IModelContext)
        Dim avatar = context.World.Avatar
        If avatar.Location.GetOthers(avatar).Any(Function(x) x.GetMetadata(Metadatas.ENEMY_TYPE) = EnemyTypes.RAT) Then
            context.Output($"RAT!")
        Else
            context.Output($"NO RAT!")
        End If
    End Sub
End Module
