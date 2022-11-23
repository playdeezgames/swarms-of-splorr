Module ConfirmProcessor
    Friend Function Run(text As String) As Boolean
        AnsiConsole.Clear()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = text}
        prompt.AddChoices(NoText, YesText)
        Return AnsiConsole.Prompt(prompt) = YesText
    End Function
End Module
