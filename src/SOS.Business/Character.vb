Public Class Character
    Implements ICharacter
    Private ReadOnly _id As Integer
    Private ReadOnly _worldData As WorldData
    Sub New(worldData As WorldData, id As Integer)
        _worldData = worldData
        _id = id
    End Sub

    Public ReadOnly Property Id As Integer Implements ICharacter.Id
        Get
            Return _id
        End Get
    End Property

    Public ReadOnly Property Name As String Implements ICharacter.Name
        Get
            Return _worldData.Characters(Id).Name
        End Get
    End Property

    Public ReadOnly Property X As Double Implements ICharacter.X
        Get
            Return _worldData.Characters(Id).X
        End Get
    End Property

    Public ReadOnly Property Y As Double Implements ICharacter.Y
        Get
            Return _worldData.Characters(Id).Y
        End Get
    End Property

    Public Property Heading As Double Implements ICharacter.Heading
        Get
            Return _worldData.Characters(Id).Heading
        End Get
        Set(value As Double)
            _worldData.Characters(Id).Heading = Math.Min(360.0, Math.Max(0.0, value))
        End Set
    End Property

    Public Property Speed As Double Implements ICharacter.Speed
        Get
            Return _worldData.Characters(Id).Speed
        End Get
        Set(value As Double)
            _worldData.Characters(Id).Speed = Math.Min(1.0, Math.Max(0.0, value))
        End Set
    End Property
    Friend Shared Function FromId(worldData As WorldData, id As Integer?) As ICharacter
        Return If(id.HasValue, New Character(worldData, id.Value), Nothing)
    End Function
End Class
