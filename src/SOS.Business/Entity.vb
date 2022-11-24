Public Class Entity
    Implements IEntity
    Private ReadOnly _id As Integer
    Private ReadOnly _worldData As WorldData
    Private ReadOnly _world As IWorld
    Sub New(world As IWorld, worldData As WorldData, id As Integer)
        _world = world
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
    Public ReadOnly Property SightRadius As Double Implements IEntity.SightRadius
        Get
            Return _worldData.Entities(Id).SightRadius
        End Get
    End Property

    Public ReadOnly Property EntityType As EntityType Implements IEntity.EntityType
        Get
            Return _worldData.Entities(Id).EntityType
        End Get
    End Property

    Public ReadOnly Property AttackRadius As Double Implements IEntity.AttackRadius
        Get
            Return _worldData.Entities(Id).AttackRadius
        End Get
    End Property

    Public ReadOnly Property MaximumDamage As Double Implements IEntity.MaximumDamage
        Get
            Return _worldData.Entities(Id).MaximumDamage
        End Get
    End Property

    Public ReadOnly Property IsDead As Boolean Implements IEntity.IsDead
        Get
            Return Health = 0.0
        End Get
    End Property

    Public ReadOnly Property Health As Double Implements IEntity.Health
        Get
            Return Math.Max(MaximumHealth - _worldData.Entities(Id).Wounds, 0.0)
        End Get
    End Property

    Public ReadOnly Property MaximumHealth As Double Implements IEntity.MaximumHealth
        Get
            Return _worldData.Entities(Id).MaximumHealth
        End Get
    End Property

    Public ReadOnly Property XPValue As Double Implements IEntity.XPValue
        Get
            Return _worldData.Entities(Id).XPValue
        End Get
    End Property

    Const XPLevelMultiplier = 10.0
    Public ReadOnly Property XPGoal As Double Implements IEntity.XPGoal
        Get
            Return XPLevelMultiplier * (XPLevel + 1)
        End Get
    End Property

    Public ReadOnly Property XPLevel As Integer Implements IEntity.XPLevel
        Get
            Return _worldData.Entities(Id).XPLevel
        End Get
    End Property

    Public ReadOnly Property RestBenefit As Double Implements IEntity.RestBenefit
        Get
            Return _worldData.Entities(Id).RestBenefit
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

    Public Sub Attack(enemy As IEntity) Implements IEntity.Attack
        AddMessage($"{Name} attacks {enemy.Name}!")
        enemy.AddMessage($"{enemy.Name} is attacked by {Name}!")
        Dim damage = RollDamage()
        AddMessage($"{Name} does {damage.ToString("0.00")} damage to {enemy.Name}")
        enemy.AddMessage($"{enemy.Name} takes {damage.ToString("0.00")} damage from {Name}")
        enemy.AddWounds(damage)
        If enemy.IsDead Then
            AddMessage($"{Name} destroys {enemy.Name}.")
            enemy.Destroy()
        Else
            AddMessage($"{enemy.Name} has {enemy.Health.ToString("0.00")} health remaining.")
            enemy.AddMessage($"{enemy.Name} has {enemy.Health.ToString("0.00")} health remaining.")
        End If
    End Sub

    Private Function RollDamage() As Double
        Return RNG.Roll(MaximumDamage)
    End Function

    Public Sub Destroy() Implements IEntity.Destroy
        Select Case _worldData.Entities(Id).EntityType
            Case EntityType.Enemy
                _world.CreateEntity(New EntityData() With
                            {
                                .Name = "XP",
                                .EntityType = EntityType.XP,
                                .XPValue = RNG.Roll(XPValue),
                                .X = X,
                                .Y = Y
                            })
        End Select
        _worldData.Entities(Id) = Nothing
        If _worldData.PlayerCharacterId.HasValue AndAlso _worldData.PlayerCharacterId.Value = Id Then
            _worldData.PlayerCharacterId = Nothing
        End If
    End Sub

    Friend Shared Function FromId(world As IWorld, worldData As WorldData, id As Integer?) As IEntity
        Return If(id.HasValue, New Entity(world, worldData, id.Value), Nothing)
    End Function
    Public Function DistanceFrom(other As IEntity) As Double Implements IEntity.DistanceFrom
        Return Math.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y))
    End Function

    Public Function HeadingTo(other As IEntity) As Double Implements IEntity.HeadingTo
        Dim deltaX = other.X - X
        Dim deltaY = other.Y - Y
        Dim result = Math.Atan2(deltaY, deltaX) * 180.0 / Math.PI
        Return If(result < 0.0, result + 360.0, result)
    End Function

    Public Function Exists() As Boolean Implements IEntity.Exists
        Return _worldData.Entities(Id) IsNot Nothing
    End Function

    Public Sub AddWounds(wounds As Double) Implements IEntity.AddWounds
        _worldData.Entities(Id).Wounds = _worldData.Entities(Id).Wounds + wounds
    End Sub

    Public Sub Take(powerUp As IEntity) Implements IEntity.Take
        Select Case powerUp.EntityType
            Case EntityType.XP
                AddXP(powerUp.XPValue)
                powerUp.Destroy()
        End Select
    End Sub

    Private Sub AddXP(xpValue As Double)
        _worldData.Entities(Id).XPValue += xpValue
        AddMessage($"{Name} gains {xpValue.ToString("0.00")} xp!")
        While Me.XPValue >= XPGoal
            AddXPLevel()
        End While
    End Sub

    Private Sub AddXPLevel()
        _worldData.Entities(Id).XPValue -= XPGoal
        AddMessage($"{Name} gains a level!")
        _worldData.Entities(Id).Wounds = 0.0
    End Sub

    Public Sub Rest() Implements IEntity.Rest
        Dim benefit = RNG.Roll(RestBenefit)
        AddMessage($"{Name} rests and gains (up to){benefit.ToString("0.00")} health!")
        _worldData.Entities(Id).Wounds = Math.Max(0.0, _worldData.Entities(Id).Wounds - benefit)
    End Sub
End Class
