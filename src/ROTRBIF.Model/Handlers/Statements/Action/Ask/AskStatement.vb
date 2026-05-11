Imports ROTRBIFOS.Business

Friend Module AskStatement
    Const AboutToken As String = "about"
    Const WorkTopic As String = "work"
    Const HealingTopic As String = "healing"
    Private ReadOnly topicObjectTable As IReadOnlyDictionary(Of String, IReadOnlyDictionary(Of ObjectIdentifier, Action(Of IModelContext, ICharacter, ICharacter))) =
        New Dictionary(Of String, IReadOnlyDictionary(Of ObjectIdentifier, Action(Of IModelContext, ICharacter, ICharacter))) From
        {
            {
                WorkTopic,
                New Dictionary(Of ObjectIdentifier, Action(Of IModelContext, ICharacter, ICharacter)) From
                {
                    {ObjectIdentifier.GORACHAN, AddressOf CellarQuest.AskGorachanAboutWork}
                }
            },
            {
                HealingTopic,
                New Dictionary(Of ObjectIdentifier, Action(Of IModelContext, ICharacter, ICharacter)) From
                {
                    {ObjectIdentifier.HEALER, AddressOf AskHealerAboutHealing}
                }
            }
        }

    Private Sub AskHealerAboutHealing(
                                     context As IModelContext,
                                     interrogator As ICharacter,
                                     deponent As ICharacter)
        context.Output($"{deponent.GetName} says:")
        context.Output($"""Nothing matters.""")
        context.Output($"{deponent.GetName} fully heals {interrogator.GetName}.")
        interrogator.SetStatistic(Statistics.HEALTH, interrogator.GetStatisticMaximum(Statistics.HEALTH))
    End Sub

    Friend Sub Handle(context As IModelContext)
        Dim avatar = context.World.Avatar
        If avatar.IsDead Then
            HandleInvalidCommand(context)
            Return
        End If
        If Not context.HasToken(AboutToken) Then
            HandleInvalidCommand(context)
            Return
        End If
        Dim otherName = context.ReadUntilToken(AboutToken)
        Dim other = avatar.Location.FindOtherByName(avatar, otherName)
        If other Is Nothing Then
            HandleInvalidCommand(context)
            Return
        End If
        If Not context.HasTokens Then
            HandleInvalidCommand(context)
            Return
        End If
        context.DiscardToken()
        Dim topic As String = context.ReadRemainingTokens
        If topic Is Nothing Then
            HandleInvalidCommand(context)
            Return
        End If
        If Not topicObjectTable.ContainsKey(topic) Then
            context.Output($"{other.GetName} has nothing to say to {avatar.GetName} about {topic}.")
            Return
        End If
        context.Output($"{avatar.GetName} asks {other.GetName} about {topic}.")
        Dim objectIdentifier = other.GetObjectIdentifier()
        If Not objectIdentifier.HasValue Then
            context.Output($"{other.GetName} has nothing to say to {avatar.GetName} about {topic}.")
            Return
        End If
        Dim handler As Action(Of IModelContext, ICharacter, ICharacter) = Nothing
        If Not topicObjectTable(topic).TryGetValue(objectIdentifier.Value, handler) Then
            context.Output($"{other.GetName} has nothing to say to {avatar.GetName} about {topic}.")
            Return
        End If
        handler(context, avatar, other)
    End Sub
End Module
