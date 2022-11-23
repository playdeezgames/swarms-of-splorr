Public Interface IWorld
    ReadOnly Property PlayerEntity As IEntity
    Sub Start()
    Sub Abandon()
    ReadOnly Property Entities As IEnumerable(Of IEntity)
    ReadOnly Property EntitiesOfType(entityType As EntityType) As IEnumerable(Of IEntity)
    ReadOnly Property VisibleEnemiesOf(entity As IEntity) As IEnumerable(Of IEntity)
    ReadOnly Property EnemiesOf(entity As IEntity) As IEnumerable(Of IEntity)
End Interface
