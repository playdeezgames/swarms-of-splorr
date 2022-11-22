Module PlayProcessor
    Friend Sub Run(world As IWorld)
        Dim playerCharacter As ICharacter = world.PlayerCharacter
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Name: {playerCharacter.Name}")
            AnsiConsole.MarkupLine($"Location: ({playerCharacter.X},{playerCharacter.Y})")
            AnsiConsole.MarkupLine($"Heading: {playerCharacter.Heading}")
            AnsiConsole.MarkupLine($"Speed: {playerCharacter.Speed}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(AbandonGameText)
            Select Case AnsiConsole.Prompt(prompt)
                Case AbandonGameText
                    If ConfirmProcessor.Run("[red]Are you sure you want to abandon this game?[/]") Then
                        Exit Do
                    End If
            End Select
        Loop
    End Sub
End Module
