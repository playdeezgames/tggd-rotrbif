Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Friend Module LocationExtensions
    <Extension>
    Friend Function GetExitsText(location As ILocation) As String
        Dim routes = location.Routes
        Select Case routes.Count
            Case 0
                Return "nowhere"
            Case 1
                Return routes.Single.Direction
            Case 2
                Return $"{routes.First.Direction} and {routes.Last.Direction}"
            Case Else
                Return $"{String.Join(", ", routes.Take(routes.Count - 1).Select(Function(y) y.Direction))} And {routes.Last.Direction}"
        End Select
    End Function
    <Extension>
    Friend Function GetFeaturesText(location As ILocation) As String
        Dim features = location.Features
        Select Case features.Count
            Case 0
                Return "nothing"
            Case 1
                Return features.Single.GetName
            Case 2
                Return $"{features.First.GetName} and {features.Last.GetName}"
            Case Else
                Return $"{String.Join(", ", features.Take(features.Count - 1).Select(Function(y) y.GetName))} And {features.Last.GetName}"
        End Select
    End Function
    <Extension>
    Friend Function FindFeatureByName(location As ILocation, name As String) As IFeature
        Return location.Features.FirstOrDefault(Function(x) x.GetName() = name)
    End Function
End Module
