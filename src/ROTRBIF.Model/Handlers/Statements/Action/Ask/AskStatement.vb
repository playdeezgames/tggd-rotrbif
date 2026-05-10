Imports ROTRBIFOS.Business

Friend Module AskStatement
    Const AboutToken As String = "about"
    Const WorkTopic As String = "work"
    Private ReadOnly topicTable As IReadOnlyDictionary(Of String, Action(Of IModelContext, ICharacter, ICharacter)) =
        New Dictionary(Of String, Action(Of IModelContext, ICharacter, ICharacter)) From
        {
            {WorkTopic, AddressOf AskAboutWorkTopic.Handle}
        }
    Friend Sub Handle(context As IModelContext)
        Dim avatar = context.World.Avatar
        If avatar.IsDead Then
            HandleInvalidCommand(context)
            Return
        End If
        If Not context.HasTokens Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim otherName = context.ReadToken
        Dim other = avatar.Location.FindOtherByName(avatar, otherName)
        If other Is Nothing Then
            HandleInvalidCommand(context)
            Return
        End If
        If Not context.HasTokens Then
            HandleInvalidCommand(context)
            Return
        End If
        If context.ReadToken <> AboutToken Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim topic As String = context.ReadRemainingTokens
        If topic Is Nothing Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim handler As Action(Of IModelContext, ICharacter, ICharacter) = Nothing
        If Not topicTable.TryGetValue(topic, handler) Then
            HandleInvalidCommand(context)
            Return
        End If
        handler.Invoke(context, avatar, other)
    End Sub
End Module
