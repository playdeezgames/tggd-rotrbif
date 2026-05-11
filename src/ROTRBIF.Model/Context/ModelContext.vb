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
        outputter(
            If(
                String.IsNullOrEmpty(text),
                text,
                Char.ToUpper(text(0)) + text.Substring(1)))
    End Sub

    Public Sub Quit() Implements IModelContext.Quit
        quitter()
    End Sub

    Public Sub DiscardToken() Implements IModelContext.DiscardToken
        If HasTokens Then
            _tokens = _tokens.Skip(1)
        End If
    End Sub

    Public Function ReadToken() As String Implements IModelContext.ReadToken
        Dim result = Tokens.First
        _tokens = _tokens.Skip(1)
        Return result
    End Function

    Public Function ReadRemainingTokens() As String Implements IModelContext.ReadRemainingTokens
        Dim result = String.Join(" ", _tokens)
        _tokens = Array.Empty(Of String)
        Return result
    End Function

    Public Function HasToken(token As String) As Boolean Implements IModelContext.HasToken
        Return _tokens.Any(Function(x) x = token)
    End Function

    Public Function ReadUntilToken(token As String) As String Implements IModelContext.ReadUntilToken
        Dim tokenIndex = _tokens.ToList.IndexOf(token)
        Dim result = String.Join(" ", _tokens.Take(tokenIndex))
        _tokens = _tokens.Skip(tokenIndex)
        Return result
    End Function
End Class
