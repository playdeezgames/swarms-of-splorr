Module ChangeHeadingProcessor
    Friend Sub Run(world As IWorld, playerCharacter As ICharacter)
        playerCharacter.Heading = AnsiConsole.Ask(Of Double)("[olive]New Heading: [/]", playerCharacter.Heading)
    End Sub
End Module
