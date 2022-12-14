Module MainMenuProcessor
    Friend Sub Run()
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Main Menu:[/]"}
            prompt.AddChoices(StartText, QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case StartText
                    Dim world As IWorld = New World
                    world.Start()
                    PlayProcessor.Run(world)
                    GameOverProcessor.Run(world)
                Case QuitText
                    If ConfirmProcessor.Run("[red]Are you sure you want to quit?[/]") Then
                        Exit Do
                    End If
            End Select
        Loop
    End Sub
End Module
