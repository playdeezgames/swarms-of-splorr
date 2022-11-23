Public Interface IWorld
    ReadOnly Property PlayerEntity As IEntity
    Sub Start()
    Sub Abandon()
    ReadOnly Property Entities As IEnumerable(Of IEntity)
End Interface
