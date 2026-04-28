Imports ROTRBIFOS.Business

Friend Interface IModelContext
    Sub Output(text As String)
    Sub Quit()
    ReadOnly Property Tokens As IEnumerable(Of String)
    ReadOnly Property World As IWorld
    ReadOnly Property HasTokens As Boolean
    Function ReadToken() As String
End Interface
