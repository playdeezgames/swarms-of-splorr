Public Interface IEntity
    ReadOnly Property Id As Integer
    ReadOnly Property Name As String
    ReadOnly Property X As Double
    ReadOnly Property Y As Double
    Property Heading As Double
    Property Speed As Double
    ReadOnly Property MaximumSpeed As Double
    ReadOnly Property IsPlayer As Boolean
    Sub Move()
    Sub AddMessage(text As String)
    Sub ClearMessages()
    ReadOnly Property Messages As IEnumerable(Of String)
    ReadOnly Property SightRadius As Double
    Function DistanceFrom(other As IEntity) As Double
    Function HeadingTo(other As IEntity) As Double
    ReadOnly Property EntityType As EntityType
    ReadOnly Property AttackRadius As Double
    Sub Attack(enemy As IEntity)
    Sub Destroy()
    Function Exists() As Boolean
    Sub AddWounds(wounds As Double)
    ReadOnly Property MaximumDamage As Double
    ReadOnly Property IsDead As Boolean
    ReadOnly Property Health As Double
    ReadOnly Property MaximumHealth As Double
    ReadOnly Property XPValue As Double
    ReadOnly Property XPGoal As Double
    ReadOnly Property XPLevel As Integer
    Sub Take(powerUp As IEntity)
    Sub Rest()
    ReadOnly Property RestBenefit As Double
End Interface
