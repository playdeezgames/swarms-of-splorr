Module TurnProcessor
    Friend Sub Run(world As IWorld, entity As IEntity)
        Dim target = world.PlayerEntity
        If target Is Nothing Then Return
        entity.Heading = entity.HeadingTo(target)
        entity.Speed = entity.MaximumSpeed
        entity.Move()
    End Sub
End Module
