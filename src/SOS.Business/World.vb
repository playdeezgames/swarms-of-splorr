Public Class World
    Implements IWorld
    Private _worldData As WorldData

    Public ReadOnly Property PlayerEntity As IEntity Implements IWorld.PlayerEntity
        Get
            Return Entity.FromId(_worldData, _worldData.PlayerCharacterId)
        End Get
    End Property

    Public ReadOnly Property Entities As IEnumerable(Of IEntity) Implements IWorld.Entities
        Get
            Dim result As New List(Of IEntity)
            For entityId = 0 To _worldData.Entities.Count - 1
                Dim entity = Business.Entity.FromId(_worldData, entityId)
                If entity.Exists Then
                    result.Add(entity)
                End If
            Next
            Return result
        End Get
    End Property

    Public ReadOnly Property VisibleEnemiesOf(entity As IEntity) As IEnumerable(Of IEntity) Implements IWorld.VisibleEnemiesOf
        Get
            Return EnemiesOf(entity).Where(Function(x) x.DistanceFrom(entity) <= entity.SightRadius)
        End Get
    End Property

    Public ReadOnly Property EnemiesOf(entity As IEntity) As IEnumerable(Of IEntity) Implements IWorld.EnemiesOf
        Get
            Select Case entity.EntityType
                Case EntityType.Player
                    Return EntitiesOfType(EntityType.Enemy)
                Case EntityType.Enemy
                    Return EntitiesOfType(EntityType.Player)
                Case Else
                    Return Array.Empty(Of IEntity)
            End Select
        End Get
    End Property

    Public ReadOnly Property EntitiesOfType(entityType As EntityType) As IEnumerable(Of IEntity) Implements IWorld.EntitiesOfType
        Get
            Return Entities.Where(Function(x) x.EntityType = entityType)
        End Get
    End Property

    Public ReadOnly Property AttackableEnemiesOf(entity As IEntity) As IEnumerable(Of IEntity) Implements IWorld.AttackableEnemiesOf
        Get
            Return EnemiesOf(entity).Where(Function(x) x.DistanceFrom(entity) <= entity.AttackRadius)
        End Get
    End Property

    Public Sub Start() Implements IWorld.Start
        _worldData = New WorldData
        CreatePlayerEntity()
        CreateEnemyEntities()
    End Sub

    Private Sub CreateEnemyEntities()
        Const distanceStep = 10.0
        Dim distance As Double = distanceStep
        Dim random As New Random
        For index = 1 To 100
            Dim heading = random.NextDouble * Math.PI * 2
            _worldData.Entities.Add(
            New EntityData() With
            {
                .Name = $"Enemy #{index}",
                .X = Math.Cos(heading) * distance,
                .Y = Math.Sin(heading) * distance,
                .Heading = 0.0,
                .Speed = 0.0,
                .MaximumSpeed = 1.0,
                .SightRadius = Double.MaxValue,
                .EntityType = EntityType.Enemy,
                .AttackRadius = 1.0,
                .MaximumDamage = 1.0,
                .MaximumHealth = 10.0,
                .Wounds = 0.0
            })
            distance += distanceStep
        Next
    End Sub

    Private Sub CreatePlayerEntity()
        _worldData.PlayerCharacterId = _worldData.Entities.Count
        _worldData.Entities.Add(
            New EntityData() With
            {
                .Name = "Yer Character",
                .X = 0.0,
                .Y = 0.0,
                .Heading = 0.0,
                .Speed = 1.0,
                .MaximumSpeed = 1.0,
                .SightRadius = 10.0,
                .EntityType = EntityType.Player,
                .AttackRadius = 1.0,
                .MaximumDamage = 1.0,
                .MaximumHealth = 10.0,
                .Wounds = 0.0
            })
    End Sub

    Public Sub Abandon() Implements IWorld.Abandon
        _worldData.PlayerCharacterId = Nothing
    End Sub
End Class
