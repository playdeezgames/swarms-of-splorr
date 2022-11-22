﻿Module MainMenuProcessor
    Friend Sub Run()
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Main Menu:[/]"}
            prompt.AddChoices(QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case QuitText
                    done = True
            End Select
        End While
    End Sub
End Module
