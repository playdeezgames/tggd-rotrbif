Friend Module SearchCellarStatement
    Friend Sub Handle(context As IModelContext)
        Dim avatar = context.World.Avatar
        Dim location = avatar.Location
        Dim rat = location.GetOthers(avatar).FirstOrDefault(Function(x) x.GetMetadata(Metadatas.ENEMY_TYPE) = EnemyTypes.RAT)
        If rat IsNot Nothing Then
            context.Output($"{avatar.GetName} is engaged in combat with {rat.GetName} and cannot search now.")
        Else
            rat = location.CreateCharacter(AddressOf CreateRat)
            context.Output($"{avatar.GetName} finds {rat.GetName}!")
        End If
    End Sub

    Private Sub CreateRat(character As ROTRBIFOS.Business.ICharacter)
        character.SetName(Names.RAT)
        character.SetMetadata(Metadatas.ENEMY_TYPE, EnemyTypes.RAT)
    End Sub
End Module
