Public Class World
    Implements IWorld
    Private _worldData As WorldData

    Public ReadOnly Property PlayerEntity As IEntity Implements IWorld.PlayerEntity
        Get
            Return Entity.FromId(_worldData, _worldData.PlayerCharacterId)
        End Get
    End Property

    Public Sub Start() Implements IWorld.Start
        _worldData = New WorldData
        _worldData.PlayerCharacterId = _worldData.Characters.Count
        _worldData.Characters.Add(New EntityData() With {.Name = "Yer Swarm", .X = 0.0, .Y = 0.0, .Heading = 0.0, .Speed = 1.0})
    End Sub
End Class
