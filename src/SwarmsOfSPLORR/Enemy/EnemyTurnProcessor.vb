Module EnemyTurnProcessor
    Friend Sub Run(world As IWorld, entity As IEntity)
        Dim target = world.PlayerEntity
        If target Is Nothing Then Return
        entity.Heading = entity.HeadingTo(target)
        entity.Speed = entity.MaximumSpeed
        If entity.DistanceFrom(target) < entity.AttackRadius Then
            entity.Attack(target)
        Else
            entity.Move()
        End If
    End Sub
End Module
