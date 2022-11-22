Module PlayProcessor
    Friend Sub Run(world As IWorld)
        Dim entity As IEntity = world.PlayerEntity
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Name: {entity.Name}")
            AnsiConsole.MarkupLine($"Location: ({entity.X},{entity.Y})")
            AnsiConsole.MarkupLine($"Heading: {entity.Heading}")
            AnsiConsole.MarkupLine($"Speed: {entity.Speed}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoices(MoveText, ChangeHeadingText, ChangeSpeedText, AbandonGameText)
            Select Case AnsiConsole.Prompt(prompt)
                Case AbandonGameText
                    If ConfirmProcessor.Run("[red]Are you sure you want to abandon this game?[/]") Then
                        Exit Do
                    End If
                Case ChangeHeadingText
                    ChangeHeadingProcessor.Run(world, entity)
                Case ChangeSpeedText
                    ChangeSpeedProcessor.Run(world, entity)
                Case MoveText
                    MoveProcessor.Run(world, entity)
            End Select
        Loop
    End Sub
End Module
