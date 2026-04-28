Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Public Module WorldExtensions

    Private ReadOnly commandTypeTable As IReadOnlyDictionary(Of CommandType, Action(Of IModelContext)) =
        New Dictionary(Of CommandType, Action(Of IModelContext)) From
        {
            {CommandType.Exclamation, AddressOf HandleExclamation},
            {CommandType.Statement, AddressOf HandleStatement},
            {CommandType.Question, AddressOf HandleQuestion}
        }
    <Extension>
    Public Sub HandleCommand(world As IWorld, command As ICommand, quit As Action, outputter As Action(Of String))
        Dim context As IModelContext = New ModelContext(world, command.Tokens, quit, outputter)
        Select Case command.CommandType
            Case CommandType.Exclamation
                HandleExclamation(context)
            Case CommandType.Question
                HandleQuestion(context)
            Case CommandType.Statement
                HandleStatement(context)
            Case Else
                HandleInvalidCommand(context)
        End Select
    End Sub

    <Extension>
    Public Sub Initialize(world As IWorld)
        world.Clear()
        world.Avatar = world.CreateCharacter(AddressOf InitializeN00b)
    End Sub

    Private Sub InitializeN00b(character As ICharacter)
        character.SetName("N00b")
        character.SetAlive()
        character.SetFacing(CardinalDirection.North)
    End Sub
End Module
