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
                If entity IsNot Nothing Then
                    result.Add(entity)
                End If
            Next
            Return result
        End Get
    End Property

    Public Sub Start() Implements IWorld.Start
        _worldData = New WorldData
        _worldData.PlayerCharacterId = _worldData.Entities.Count
        _worldData.Entities.Add(New EntityData() With {.Name = "Yer Swarm", .X = 0.0, .Y = 0.0, .Heading = 0.0, .Speed = 1.0})
    End Sub

    Public Sub Abandon() Implements IWorld.Abandon
        _worldData.PlayerCharacterId = Nothing
    End Sub
End Class
