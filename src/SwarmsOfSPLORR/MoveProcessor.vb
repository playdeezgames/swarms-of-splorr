Module MoveProcessor
    Friend Sub Run(world As IWorld, entity As IEntity)
        entity.Move()
        entity.AddMessage($"{entity.Name} moves.")
    End Sub
End Module
