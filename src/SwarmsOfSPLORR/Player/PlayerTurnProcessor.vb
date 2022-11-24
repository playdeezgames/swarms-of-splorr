Module PlayerTurnProcessor
    Friend Sub Run(world As IWorld, entity As IEntity)
        Do
            AnsiConsole.Clear()
            ShowMessages(entity)
            ShowVisibleEnemies(world, entity)
            Dim canAttack = ShowAttackableEnemies(world, entity)
            ShowVisiblePowerUps(world, entity)
            Dim canTake = ShowTakeablePowerUps(world, entity)
            AnsiConsole.MarkupLine($"Name: {entity.Name}")
            AnsiConsole.MarkupLine($"Location: ({entity.X.ToString("0.00")},{entity.Y.ToString("0.00")})")
            AnsiConsole.MarkupLine($"Heading: {entity.Heading.ToString("0.00")}")
            AnsiConsole.MarkupLine($"Speed: {entity.Speed.ToString("0.00")}")
            AnsiConsole.MarkupLine($"Health: {entity.Health.ToString("0.00")}/{entity.MaximumHealth.ToString("0.00")}")
            AnsiConsole.MarkupLine($"XP: {entity.XPValue.ToString("0.00")}/{entity.XPGoal.ToString("0.00")}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            If entity.Speed > 0.0 Then
                prompt.AddChoice(NextTurnText)
            End If
            If canAttack Then
                prompt.AddChoice(AttackText)
            End If
            If canTake Then
                prompt.AddChoice(TakeText)
            End If
            prompt.AddChoices(RestText, ChangeHeadingText, ChangeSpeedText, AbandonGameText)
            Select Case AnsiConsole.Prompt(prompt)
                Case AbandonGameText
                    If ConfirmProcessor.Run("[red]Are you sure you want to abandon this game?[/]") Then
                        world.Abandon()
                        Exit Do
                    End If
                Case AttackText
                    AttackProcessor.Run(world, entity)
                    Exit Do
                Case ChangeHeadingText
                    ChangeHeadingProcessor.Run(world, entity)
                Case ChangeSpeedText
                    ChangeSpeedProcessor.Run(world, entity)
                Case NextTurnText
                    MoveProcessor.Run(world, entity)
                    Exit Do
                Case RestText
                    RestProcessor.Run(world, entity)
                    Exit Do
                Case TakeText
                    TakeProcessor.Run(world, entity)
            End Select
        Loop
    End Sub

    Private Function ShowTakeablePowerUps(world As IWorld, entity As IEntity) As Boolean
        Dim powerUps = world.TakeablePowerUps(entity)
        If powerUps.Any Then
            AnsiConsole.MarkupLine("[green]Takeable Power-Ups:[/]")
            For Each powerUp In powerUps
                AnsiConsole.MarkupLine($"- {powerUp.Name} (Distance: {powerUp.DistanceFrom(entity).ToString("0.00")}, Heading: {entity.HeadingTo(powerUp).ToString("0.00")})")
            Next
            AnsiConsole.WriteLine()
            Return True
        End If
        Return False
    End Function

    Private Sub ShowVisiblePowerUps(world As IWorld, entity As IEntity)
        Dim powerUps = world.VisiblePowerUps(entity)
        If powerUps.Any Then
            AnsiConsole.MarkupLine("[green]Visible Power-Ups:[/]")
            For Each powerUp In powerUps
                AnsiConsole.MarkupLine($"- {powerUp.Name} (Distance: {powerUp.DistanceFrom(entity).ToString("0.00")}, Heading: {entity.HeadingTo(powerUp).ToString("0.00")})")
            Next
            AnsiConsole.WriteLine()
        End If
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

    Private Function ShowAttackableEnemies(world As IWorld, entity As IEntity) As Boolean
        Dim attackableEnemies = world.AttackableEnemiesOf(entity)
        If attackableEnemies.Any Then
            AnsiConsole.MarkupLine("[red]Attackable Enemies:[/]")
            For Each enemy In attackableEnemies
                AnsiConsole.MarkupLine($"- {enemy.Name} (Distance: {enemy.DistanceFrom(entity).ToString("0.00")}, Heading: {entity.HeadingTo(enemy).ToString("0.00")})")
            Next
            AnsiConsole.WriteLine()
            Return True
        End If
        Return False
    End Function

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
