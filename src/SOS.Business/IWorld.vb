Public Interface IWorld
    ReadOnly Property PlayerEntity As IEntity
    Sub Start()
    Sub Abandon()
    ReadOnly Property Entities As IEnumerable(Of IEntity)
    ReadOnly Property EntitiesOfType(entityType As EntityType) As IEnumerable(Of IEntity)
    ReadOnly Property VisibleEnemiesOf(entity As IEntity) As IEnumerable(Of IEntity)
    ReadOnly Property AttackableEnemiesOf(entity As IEntity) As IEnumerable(Of IEntity)
    ReadOnly Property EnemiesOf(entity As IEntity) As IEnumerable(Of IEntity)
    ReadOnly Property VisiblePowerUps(entity As IEntity) As IEnumerable(Of IEntity)
    ReadOnly Property TakeablePowerUps(entity As IEntity) As IEnumerable(Of IEntity)
    Function CreateEntity(entityData As EntityData) As Integer
    ReadOnly Property Statistic(statisticType As StatisticType) As Double
End Interface
