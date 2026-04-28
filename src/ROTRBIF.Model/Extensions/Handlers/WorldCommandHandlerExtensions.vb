Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Public Module WorldCommandHandlerExtensions
    Private ReadOnly commandTypeTable As IReadOnlyDictionary(Of CommandType, Action(Of IModelContext)) =
        New Dictionary(Of CommandType, Action(Of IModelContext)) From
        {
            {CommandType.Exclamation, AddressOf HandleExclamation},
            {CommandType.Statement, AddressOf HandleStatement},
            {CommandType.Question, AddressOf HandleQuestion}
        }
    <Extension>
    Public Sub HandleCommand(world As IWorld, command As ICommand, quit As Action, outputter As Action(Of String))
        Dim handler As Action(Of IModelContext) = Nothing
        If Not commandTypeTable.TryGetValue(command.CommandType, handler) Then
            handler = AddressOf HandleInvalidCommand
        End If
        handler.Invoke(New ModelContext(world, command.Tokens, quit, outputter))
    End Sub
End Module
