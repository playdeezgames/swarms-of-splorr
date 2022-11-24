Public Class WorldData
    Public Property PlayerCharacterId As Integer?
    Public Property Entities As New List(Of EntityData)
    Public Property Messages As New List(Of String)
    Public Property Statistics As New Dictionary(Of StatisticType, Double)
End Class
