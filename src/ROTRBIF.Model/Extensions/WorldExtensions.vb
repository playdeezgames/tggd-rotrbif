Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Public Module WorldExtensions
    <Extension>
    Public Sub HandleCommand(world As IWorld, command As ICommand, quit As Action, outputter As Action(Of String))
        Select Case command.CommandType
            Case CommandType.Exclamation
                HandleExclamation(world, command.Tokens, quit, outputter)
            Case CommandType.Question
                HandleQuestion(world, command.Tokens, quit, outputter)
            Case CommandType.Statement
                HandleStatement(world, command.Tokens, quit, outputter)
            Case Else
                HandleInvalidCommand(outputter)
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
