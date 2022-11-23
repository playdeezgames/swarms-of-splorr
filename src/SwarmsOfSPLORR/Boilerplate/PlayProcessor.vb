Module PlayProcessor
    Friend Sub Run(world As IWorld)
        While world.PlayerEntity IsNot Nothing
            For Each entity In world.Entities
                If entity.IsPlayer Then
                    PlayerTurnProcessor.Run(world, entity)
                Else
                    TurnProcessor.Run(world, entity)
                End If
            Next
        End While
    End Sub
End Module
