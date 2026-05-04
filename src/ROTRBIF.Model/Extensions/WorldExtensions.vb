Imports System.Runtime.CompilerServices
Imports ROTRBIFOS.Business

Public Module WorldExtensions
    <Extension>
    Public Sub Initialize(world As IWorld)
        world.Clear()
        WorldInitializer.Initialize(world)
    End Sub
End Module
