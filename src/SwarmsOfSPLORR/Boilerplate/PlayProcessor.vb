Module PlayProcessor
    Private ReadOnly table As IReadOnlyDictionary(Of EntityType, Action(Of IWorld, IEntity)) =
        New Dictionary(Of EntityType, Action(Of IWorld, IEntity)) From
        {
            {EntityType.Player, AddressOf PlayerTurnProcessor.Run},
            {EntityType.Enemy, AddressOf EnemyTurnProcessor.Run},
            {EntityType.XP, Sub(w, e) Return}
        }
    Friend Sub Run(world As IWorld)
        While world.PlayerEntity IsNot Nothing
            For Each entity In world.Entities
                If entity.Exists Then
                    table(entity.EntityType)(world, entity)
                End If
            Next
        End While
    End Sub
End Module
