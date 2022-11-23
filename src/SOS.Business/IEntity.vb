Public Interface IEntity
    ReadOnly Property Id As Integer
    ReadOnly Property Name As String
    ReadOnly Property X As Double
    ReadOnly Property Y As Double
    Property Heading As Double
    Property Speed As Double
    ReadOnly Property IsPlayer As Boolean
    Sub Move()
    Sub AddMessage(text As String)
    Sub ClearMessages()
    ReadOnly Property Messages As IEnumerable(Of String)
End Interface
