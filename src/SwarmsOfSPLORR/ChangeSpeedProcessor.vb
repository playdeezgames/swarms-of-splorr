Module ChangeSpeedProcessor
    Friend Sub Run(world As IWorld, entity As IEntity)
        entity.Speed = AnsiConsole.Ask(Of Double)($"[olive]New Speed: (0-{entity.MaximumSpeed})[/]")
        entity.AddMessage($"{entity.Name} sets speed to {entity.Speed}.")
    End Sub
End Module
