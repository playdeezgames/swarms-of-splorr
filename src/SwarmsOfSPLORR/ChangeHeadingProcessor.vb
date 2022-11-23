Module ChangeHeadingProcessor
    Friend Sub Run(world As IWorld, entity As IEntity)
        entity.Heading = AnsiConsole.Ask(Of Double)("[olive]New Heading: (0-360)[/]", entity.Heading)
        entity.AddMessage($"{entity.Name} sets heading to {entity.Heading}.")
    End Sub
End Module
