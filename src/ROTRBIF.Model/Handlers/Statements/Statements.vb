Friend Module Statements
    Const QuitCommand = "Quit"
    Const TurnCommand = "Turn"
    Const BendCommand = "Bend"
    Const StraightenCommand = "Straighten"
    Const CheckCommand = "Check"
    Const DropCommand = "Drop"
    Const TakeCommand = "Take"
    Const GoCommand = "Go"
    Const UnlockCommand = "Unlock"
    Const SearchCommand = "Search"
    Const GreetCommand = "Greet"

    Private ReadOnly statementTable As IReadOnlyDictionary(Of String, Action(Of IModelContext)) =
        New Dictionary(Of String, Action(Of IModelContext)) From
        {
            {QuitCommand, AddressOf QuitStatement.Handle},
            {TurnCommand, AddressOf TurnStatement.Handle},
            {BendCommand, AddressOf BendStatement.Handle},
            {StraightenCommand, AddressOf StraightenStatement.Handle},
            {CheckCommand, AddressOf CheckStatement.Handle},
            {DropCommand, AddressOf DropStatement.Handle},
            {TakeCommand, AddressOf TakeStatement.Handle},
            {GoCommand, AddressOf GoStatement.Handle},
            {UnlockCommand, AddressOf UnlockStatement.Handle},
            {SearchCommand, AddressOf SearchStatement.Handle},
            {GreetCommand, AddressOf GreetStatement.Handle}
        }

    Friend Sub Handle(context As IModelContext)
        context.Dispatch(statementTable, AddressOf HandleInvalidCommand)
    End Sub

End Module
