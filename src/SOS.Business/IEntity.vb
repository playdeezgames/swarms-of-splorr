Public Interface IEntity
    ReadOnly Property Id As Integer
    ReadOnly Property Name As String
    ReadOnly Property X As Double
    ReadOnly Property Y As Double
    Property Heading As Double
    Property Speed As Double
    Sub Move()
End Interface
