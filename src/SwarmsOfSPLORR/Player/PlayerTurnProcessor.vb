Module PlayerTurnProcessor
    Friend Sub Run(world As IWorld, entity As IEntity)
        Do
            AnsiConsole.Clear()
            ShowMessages(entity)
            ShowVisibleEnemies(world, entity)
            ShowAttackableEnemies(world, entity)
            AnsiConsole.MarkupLine($"Name: {entity.Name}")
            AnsiConsole.MarkupLine($"Location: ({entity.X.ToString("0.00")},{entity.Y.ToString("0.00")})")
            AnsiConsole.MarkupLine($"Heading: {entity.Heading.ToString("0.00")}")
            AnsiConsole.MarkupLine($"Speed: {entity.Speed.ToString("0.00")}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoices(MoveText, ChangeHeadingText, ChangeSpeedText, AbandonGameText)
            Select Case AnsiConsole.Prompt(prompt)
                Case AbandonGameText
                    If ConfirmProcessor.Run("[red]Are you sure you want to abandon this game?[/]") Then
                        world.Abandon()
                        Exit Do
                    End If
                Case ChangeHeadingText
                    ChangeHeadingProcessor.Run(world, entity)
                Case ChangeSpeedText
                    ChangeSpeedProcessor.Run(world, entity)
                Case MoveText
                    MoveProcessor.Run(world, entity)
                    Exit Do
            End Select
        Loop
    End Sub

    Private Sub ShowVisibleEnemies(world As IWorld, entity As IEntity)
        Dim visibleEnemies = world.VisibleEnemiesOf(entity)
        If visibleEnemies.Any Then
            AnsiConsole.MarkupLine("[red]Visible Enemies:[/]")
            For Each enemy In visibleEnemies
                AnsiConsole.MarkupLine($"- {enemy.Name} (Distance: {enemy.DistanceFrom(entity).ToString("0.00")}, Heading: {entity.HeadingTo(enemy).ToString("0.00")})")
            Next
            AnsiConsole.WriteLine()
        End If
    End Sub

    Private Sub ShowAttackableEnemies(world As IWorld, entity As IEntity)
        Dim attackableEnemies = world.AttackableEnemiesOf(entity)
        If attackableEnemies.Any Then
            AnsiConsole.MarkupLine("[red]Attackable Enemies:[/]")
            For Each enemy In attackableEnemies
                AnsiConsole.MarkupLine($"- {enemy.Name} (Distance: {enemy.DistanceFrom(entity).ToString("0.00")}, Heading: {entity.HeadingTo(enemy).ToString("0.00")})")
            Next
            AnsiConsole.WriteLine()
        End If
    End Sub

    Private Sub ShowMessages(entity As IEntity)
        Dim messages = entity.Messages
        If messages.Any Then
            AnsiConsole.MarkupLine("[blue]Messages:[/]")
            For Each message In messages
                AnsiConsole.MarkupLine($"- {message}")
            Next
            AnsiConsole.WriteLine()
            entity.ClearMessages()
        End If
    End Sub
End Module
