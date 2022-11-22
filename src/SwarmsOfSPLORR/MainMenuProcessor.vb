Module MainMenuProcessor
    Friend Sub Run()
StartOfLoop:
        AnsiConsole.Clear()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Main Menu:[/]"}
        prompt.AddChoices(QuitText)
        Select Case AnsiConsole.Prompt(prompt)
            Case QuitText
                GoTo DoneWithLoop
        End Select
        GoTo StartOfLoop

DoneWithLoop:
    End Sub
End Module
