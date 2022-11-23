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
            Return _worldData.Entities(Id).Name
        End Get
    End Property

    Public ReadOnly Property X As Double Implements IEntity.X
        Get
            Return _worldData.Entities(Id).X
        End Get
    End Property

    Public ReadOnly Property Y As Double Implements IEntity.Y
        Get
            Return _worldData.Entities(Id).Y
        End Get
    End Property

    Public Property Heading As Double Implements IEntity.Heading
        Get
            Return _worldData.Entities(Id).Heading
        End Get
        Set(value As Double)
            _worldData.Entities(Id).Heading = Math.Min(360.0, Math.Max(0.0, value))
        End Set
    End Property

    Public Property Speed As Double Implements IEntity.Speed
        Get
            Return _worldData.Entities(Id).Speed
        End Get
        Set(value As Double)
            _worldData.Entities(Id).Speed = Math.Min(MaximumSpeed, Math.Max(0.0, value))
        End Set
    End Property

    Public ReadOnly Property IsPlayer As Boolean Implements IEntity.IsPlayer
        Get
            Return _worldData.PlayerCharacterId.HasValue AndAlso _worldData.PlayerCharacterId.Value = _id
        End Get
    End Property

    Public ReadOnly Property Messages As IEnumerable(Of String) Implements IEntity.Messages
        Get
            If IsPlayer Then
                Return _worldData.Messages
            End If
            Return Array.Empty(Of String)
        End Get
    End Property

    Public ReadOnly Property MaximumSpeed As Double Implements IEntity.MaximumSpeed
        Get
            Return _worldData.Entities(Id).MaximumSpeed
        End Get
    End Property

    Public Sub Move() Implements IEntity.Move
        _worldData.Entities(Id).X += Speed * Math.Cos(Heading * Math.PI / 180.0)
        _worldData.Entities(Id).Y += Speed * Math.Sin(Heading * Math.PI / 180.0)
    End Sub

    Public Sub AddMessage(text As String) Implements IEntity.AddMessage
        If IsPlayer Then
            _worldData.Messages.Add(text)
        End If
    End Sub

    Public Sub ClearMessages() Implements IEntity.ClearMessages
        If IsPlayer Then
            _worldData.Messages.Clear()
        End If
    End Sub

    Friend Shared Function FromId(worldData As WorldData, id As Integer?) As IEntity
        Return If(id.HasValue, New Entity(worldData, id.Value), Nothing)
    End Function
End Class
