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

    Private Sub HandleExclamation(world As IWorld, tokens As IEnumerable(Of String), quit As Action, outputter As Action(Of String))
        If tokens.Any Then
            Dim remaining = tokens.Skip(1)
            Select Case tokens.First
                Case Else
                    HandleInvalidCommand(outputter)
            End Select
        Else
            HandleInvalidCommand(outputter)
        End If
    End Sub

    Private Sub HandleQuestion(world As IWorld, tokens As IEnumerable(Of String), quit As Action, outputter As Action(Of String))
        If tokens.Any Then
            Dim remaining = tokens.Skip(1)
            Select Case tokens.First
                Case "Status"
                    HandleStatusQuestion(world, remaining, quit, outputter)
                Case Else
                    HandleInvalidCommand(outputter)
            End Select
        Else
            HandleInvalidCommand(outputter)
        End If
    End Sub

    Private Sub HandleStatusQuestion(world As IWorld, remaining As IEnumerable(Of String), quit As Action, outputter As Action(Of String))
        If Not remaining.Any Then
            Dim avatar = world.Avatar
            outputter($"{avatar.GetName()} is {avatar.GetAliveStatus()}.")
            outputter($"{avatar.GetName()} is facing {avatar.GetFacing().GetName()}.")
        Else
            HandleInvalidCommand(outputter)
        End If
    End Sub

    Private Sub HandleStatement(world As IWorld, tokens As IEnumerable(Of String), quit As Action, outputter As Action(Of String))
        If tokens.Any Then
            Dim remaining = tokens.Skip(1)
            Select Case tokens.First
                Case "Quit"
                    HandleQuitStatement(world, remaining, quit, outputter)
                Case "Turn"
                    HandleTurnStatement(world, remaining, quit, outputter)
                Case Else
                    HandleInvalidCommand(outputter)
            End Select
        Else
            HandleInvalidCommand(outputter)
        End If
    End Sub

    Private Sub HandleTurnStatement(world As IWorld, remaining As IEnumerable(Of String), quit As Action, outputter As Action(Of String))
        If remaining.Any Then
            Dim token = remaining.First
            remaining = remaining.Skip(1)
            Select Case token
                Case "left"
                    HandleTurnStatment(world, Turn.Left, remaining, quit, outputter)
                Case "right"
                    HandleTurnStatment(world, Turn.Right, remaining, quit, outputter)
                Case "around"
                    HandleTurnStatment(world, Turn.Around, remaining, quit, outputter)
                Case Else
                    HandleInvalidCommand(outputter)
            End Select
        Else
            HandleInvalidCommand(outputter)
        End If
    End Sub

    Private Sub HandleTurnStatment(world As IWorld, turn As Turn, remaining As IEnumerable(Of String), quit As Action, outputter As Action(Of String))
        Dim avatar = world.Avatar
        outputter($"{avatar.GetName()} turns {turn.GetName()}.")
        avatar.Turn(turn)
        outputter($"{avatar.GetName()} now faces {avatar.GetFacing().GetName()}.")
    End Sub

    Private Sub HandleQuitStatement(world As IWorld, remaining As IEnumerable(Of String), quit As Action, outputter As Action(Of String))
        If remaining.Any() Then
            HandleInvalidCommand(outputter)
        Else
            quit()
        End If
    End Sub

    Private Sub HandleInvalidCommand(outputter As Action(Of String))
        outputter("[red]INVALID COMMAND![/]")
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
