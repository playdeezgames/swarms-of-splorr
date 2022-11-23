Public Class Entity
    Implements IEntity
    Private ReadOnly _id As Integer
    Private ReadOnly _worldData As WorldData
    Sub New(worldData As WorldData, id As Integer)
        _worldData = worldData
        _id = id
    End Sub

    Public ReadOnly Property Id As Integer Implements IEntity.Id
        Get
            Return _id
        End Get
    End Property

    Public ReadOnly Property Name As String Implements IEntity.Name
        Get
            Return _worldData.Characters(Id).Name
        End Get
    End Property

    Public ReadOnly Property X As Double Implements IEntity.X
        Get
            Return _worldData.Characters(Id).X
        End Get
    End Property

    Public ReadOnly Property Y As Double Implements IEntity.Y
        Get
            Return _worldData.Characters(Id).Y
        End Get
    End Property

    Public Property Heading As Double Implements IEntity.Heading
        Get
            Return _worldData.Characters(Id).Heading
        End Get
        Set(value As Double)
            _worldData.Characters(Id).Heading = Math.Min(360.0, Math.Max(0.0, value))
        End Set
    End Property

    Public Property Speed As Double Implements IEntity.Speed
        Get
            Return _worldData.Characters(Id).Speed
        End Get
        Set(value As Double)
            _worldData.Characters(Id).Speed = Math.Min(1.0, Math.Max(0.0, value))
        End Set
    End Property

    Public ReadOnly Property IsPlayer As Boolean Implements IEntity.IsPlayer
        Get
            Return _worldData.PlayerCharacterId.HasValue AndAlso _worldData.PlayerCharacterId.Value = _id
        End Get
    End Property

    Public Sub Move() Implements IEntity.Move
        _worldData.Characters(Id).X += Speed * Math.Cos(Heading * Math.PI / 180.0)
        _worldData.Characters(Id).Y += Speed * Math.Sin(Heading * Math.PI / 180.0)
    End Sub

    Friend Shared Function FromId(worldData As WorldData, id As Integer?) As IEntity
        Return If(id.HasValue, New Entity(worldData, id.Value), Nothing)
    End Function
End Class
