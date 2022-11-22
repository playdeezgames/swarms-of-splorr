Module ChangeSpeedProcessor
    Friend Sub Run(world As IWorld, entity As IEntity)
        entity.Speed = AnsiConsole.Ask(Of Double)("[olive]New Speed: (0.0-1.0)[/]")
    End Sub
End Module
