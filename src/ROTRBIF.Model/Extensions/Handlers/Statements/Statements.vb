Friend Module Statements
    Const QuitCommand = "Quit"
    Const TurnCommand = "Turn"
    Const BendCommand = "Bend"
    Const StraightenCommand = "Straighten"

    Private ReadOnly statementTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {QuitCommand, AddressOf QuitStatement.Handle},
            {TurnCommand, AddressOf TurnStatement.Handle},
            {BendCommand, AddressOf BendStatement.Handle},
            {StraightenCommand, AddressOf StraightenStatement.Handle}
        }

    Friend Sub Handle(context As IModelContext)
        context.Dispatch(statementTable, AddressOf HandleInvalidCommand)
    End Sub

End Module
