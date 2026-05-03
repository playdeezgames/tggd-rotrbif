Imports ROTRBIFOS.Data

Friend Class Trigger
    Inherits Entity(Of TriggerData)
    Implements ITrigger

    Private Sub New(worldData As WorldData, triggerId As Guid)
        MyBase.New(worldData)
        Me.TriggerId = triggerId
    End Sub

    Public ReadOnly Property TriggerId As Guid Implements ITrigger.TriggerId

    Protected Overrides ReadOnly Property EntityData As TriggerData
        Get
            Return worldData.Triggers(TriggerId)
        End Get
    End Property

    Friend Shared Function TryFind(worldData As WorldData, triggerId As Guid?) As ITrigger
        Return If(
            triggerId.HasValue AndAlso worldData.Triggers.ContainsKey(triggerId.Value),
            New Trigger(worldData, triggerId.Value),
            Nothing)
    End Function
End Class
