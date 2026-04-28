Imports ROTRBIFOS.Business

Friend Class ModelContext
    Implements IModelContext

    Private ReadOnly outputter As Action(Of String)
    Private ReadOnly quitter As Action
    Private _tokens As IEnumerable(Of String)

    Friend Sub New(world As IWorld, tokens As IEnumerable(Of String), quitter As Action, outputter As Action(Of String))
        Me.outputter = outputter
        Me.World = world
        Me._tokens = tokens
        Me.quitter = quitter
    End Sub

    Public ReadOnly Property Tokens As IEnumerable(Of String) Implements IModelContext.Tokens
        Get
            Return _tokens
        End Get
    End Property

    Public ReadOnly Property World As IWorld Implements IModelContext.World

    Public ReadOnly Property HasTokens As Boolean Implements IModelContext.HasTokens
        Get
            Return Tokens.Any()
        End Get
    End Property

    Public Sub Output(text As String) Implements IModelContext.Output
        outputter(text)
    End Sub

    Public Sub Quit() Implements IModelContext.Quit
        quitter()
    End Sub

    Public Function ReadToken() As String Implements IModelContext.ReadToken
        Dim result = Tokens.First
        _tokens = _tokens.Skip(1)
        Return result
    End Function
End Class
