Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Friend Module LocationExtensions
    <Extension>
    Private Function GetText(Of TItem)(items As IEnumerable(Of TItem), toText As Func(Of TItem, String), noneText As String) As String
        Select Case items.Count
            Case 0
                Return noneText
            Case 1
                Return toText(items.Single)
            Case 2
                Return $"{toText(items.First)} and {toText(items.Last)}"
            Case Else
                Return $"{String.Join(", ", items.Take(items.Count - 1).Select(toText))} And {toText(items.Last)}"
        End Select
    End Function
    <Extension>
    Friend Function GetExitsText(location As ILocation) As String
        Return location.Routes.GetText(Function(x) x.Direction, "nowhere")
    End Function
    <Extension>
    Friend Function GetFeaturesText(location As ILocation) As String
        Return location.Features.GetText(Function(x) x.GetName(), "nothing")
    End Function
    <Extension>
    Friend Function GetOthersText(location As ILocation, character As ICharacter) As String
        Return location.GetOthers(character).GetText(Function(x) x.GetName(), "nobody")
    End Function
    <Extension>
    Friend Function FindFeatureByName(location As ILocation, name As String) As IFeature
        Return location.Features.FirstOrDefault(Function(x) x.GetName() = name)
    End Function
    <Extension>
    Friend Function FindOtherByName(location As ILocation, character As ICharacter, name As String) As ICharacter
        Return location.GetOthers(character).FirstOrDefault(Function(x) x.GetName() = name)
    End Function
    <Extension>
    Friend Sub SetWanderingMonsters(location As ILocation, wanderingMonsters As WanderingMonsters)
        location.SetStatistic(Statistics.WANDERING_MONSTERS, CInt(wanderingMonsters))
    End Sub
    <Extension>
    Friend Sub ClearWanderingMonsters(location As ILocation)
        location.ClearStatistic(Statistics.WANDERING_MONSTERS)
    End Sub
    <Extension>
    Friend Function GetWanderingMonsters(location As ILocation) As WanderingMonsters?
        If location.HasStatistic(Statistics.WANDERING_MONSTERS) Then
            Return CType(location.GetStatistic(Statistics.WANDERING_MONSTERS), WanderingMonsters)
        End If
        Return Nothing
    End Function
End Module
