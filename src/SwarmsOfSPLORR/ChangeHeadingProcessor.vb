Module ChangeHeadingProcessor
    Friend Sub Run(world As IWorld, playerCharacter As ICharacter)
        playerCharacter.Heading = AnsiConsole.Ask(Of Double)("[olive]New Heading: (0-360)[/]", playerCharacter.Heading)
    End Sub
End Module
