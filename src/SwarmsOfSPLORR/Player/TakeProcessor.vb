Module TakeProcessor
    Friend Sub Run(world As IWorld, entity As IEntity)
        Dim piles = world.TakeablePowerUps(entity).GroupBy(Function(x) x.Name).ToDictionary(Function(x) x.Key)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[green]Take What?[/]"}
        prompt.AddChoices(piles.Select(Function(x) x.Key))
        Dim answer = AnsiConsole.Prompt(prompt)
        Dim pile = piles(answer)
        For Each powerUp In pile
            entity.Take(powerUp)
        Next
    End Sub
End Module
