Imports ROTRBIF.Model

Friend Class Command
    Implements ICommand
    Private Sub New(commandType As CommandType, tokens As IEnumerable(Of String))
        Me.CommandType = commandType
        Me.Tokens = tokens
    End Sub

    Public ReadOnly Property CommandType As CommandType Implements ICommand.CommandType

    Public ReadOnly Property Tokens As IEnumerable(Of String) Implements ICommand.Tokens

    Private Shared ReadOnly punctuationTable As IReadOnlyDictionary(Of Char, CommandType) =
        New Dictionary(Of Char, CommandType) From
        {
            {"."c, CommandType.Statement},
            {"?"c, CommandType.Question},
            {"!"c, CommandType.Exclamation}
        }

    Friend Shared Function Parse(input As String) As ICommand
        If String.IsNullOrWhiteSpace(input) Then
            Return New Command(CommandType.Invalid, Array.Empty(Of String)())
        End If
        Dim ct As CommandType = CommandType.Invalid
        If punctuationTable.TryGetValue(input.Last(), ct) Then
            Return New Command(ct, input.Substring(0, input.Length - 1).Split(" "c))
        Else
            Return New Command(CommandType.Invalid, Array.Empty(Of String)())
        End If
    End Function
End Class
