Module PlayerTurnProcessor
    Friend Sub Run(world As IWorld, entity As IEntity)
        Do
            AnsiConsole.Clear()
            Dim messages = entity.Messages
            If messages.Any Then
                AnsiConsole.MarkupLine("[blue]Messages:[/]")
                For Each message In messages
                    AnsiConsole.MarkupLine($"- {message}")
                Next
                AnsiConsole.WriteLine()
                entity.ClearMessages()
            End If
            AnsiConsole.MarkupLine($"Name: {entity.Name}")
            AnsiConsole.MarkupLine($"Location: ({entity.X},{entity.Y})")
            AnsiConsole.MarkupLine($"Heading: {entity.Heading}")
            AnsiConsole.MarkupLine($"Speed: {entity.Speed}")
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
End Module
