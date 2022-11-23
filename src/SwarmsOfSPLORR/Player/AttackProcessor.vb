Module AttackProcessor
    Friend Sub Run(world As IWorld, entity As IEntity)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Attack Whom?[/]"}
        Dim table = world.AttackableEnemiesOf(entity).ToDictionary(Function(x) x.Name, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim enemy = table(AnsiConsole.Prompt(prompt))
        entity.Attack(enemy)
    End Sub
End Module
