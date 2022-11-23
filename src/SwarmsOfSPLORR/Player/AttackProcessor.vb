Module AttackProcessor
    Friend Sub Run(world As IWorld, entity As IEntity)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Attack Whom?[/]"}
        Dim table = world.AttackableEnemiesOf(entity).ToDictionary(Function(x) x.Name, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim enemy = table(AnsiConsole.Prompt(prompt))
        Dim enemyName = enemy.Name
        entity.AddMessage($"{entity.Name} attacks {enemyName}.")
        entity.Attack(enemy)
        If Not enemy.Exists Then
            entity.AddMessage($"{entity.Name} destroys {enemyName}.")
        End If
    End Sub
End Module
