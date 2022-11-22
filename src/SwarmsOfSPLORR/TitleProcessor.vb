Module TitleProcessor
    Friend Sub Run()
        Console.Title = "Swarms of SPLORR!!"
        Dim figlet As New FigletText("Swarms of SPLORR!!") With {.Alignment = Justify.Center, .Color = Color.Aqua}
        AnsiConsole.Write(figlet)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoices(OkText)
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
