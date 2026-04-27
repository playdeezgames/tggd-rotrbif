Public Interface ICommand
    ReadOnly Property CommandType As CommandType
    ReadOnly Property Tokens As IEnumerable(Of String)
End Interface
