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
End Module
