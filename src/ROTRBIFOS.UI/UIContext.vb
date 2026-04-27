Imports ROTRBIF.Model
Imports ROTRBIFOS.Business
Imports ROTRBIFOS.Data

Public Class UIContext
    Implements IUIContext
    Private ReadOnly inputter As Func(Of String)
    Private ReadOnly outputter As Action(Of String)
    Private ReadOnly worldData As WorldData
    Private ReadOnly Property World As IWorld
        Get
            Return Business.World.Create(worldData)
        End Get
    End Property
    Private Sub New(
                  worldData As WorldData,
                  inputter As Func(Of String),
                  outputter As Action(Of String))
        Me.worldData = worldData
        Me.outputter = outputter
        Me.inputter = inputter
    End Sub

    Private Function ReadCommand() As ICommand
        Return Command.Parse(inputter())
    End Function

    Private Sub HandleCommand(command As ICommand, quit As Action)
        World.HandleCommand(command, quit, outputter)
    End Sub

    Public Sub Run() Implements IUIContext.Run
        World.Initialize()
        Dim done = False
        While Not done
            HandleCommand(ReadCommand(), Sub() done = True)
        End While
    End Sub
    Public Shared Function Create(metaphorState As WorldData,
                  inputter As Func(Of String),
                  outputter As Action(Of String)) As IUIContext
        Return New UIContext(metaphorState, inputter, outputter)
    End Function
End Class
