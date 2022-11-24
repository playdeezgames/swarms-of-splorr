Module GameOverProcessor
    Friend Sub Run(world As IWorld)
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[aqua]Statistics:[/]")
        AnsiConsole.MarkupLine($"- Distance Moved: {world.Statistic(StatisticType.DistanceMoved):0.00}")
        AnsiConsole.MarkupLine($"- Damage Done: {world.Statistic(StatisticType.DamageDone).ToString("0.00")}")
        AnsiConsole.MarkupLine($"- Damage Taken: {world.Statistic(StatisticType.DamageTaken).ToString("0.00")}")
        Do
        Loop Until AnsiConsole.Ask(Of String)($"[aqua]Type '{YesText}' to return to the main menu[/]") = YesText
    End Sub
End Module
