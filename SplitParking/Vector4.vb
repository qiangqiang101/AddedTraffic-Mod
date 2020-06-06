Imports GTA.Math

Public Class Vector4

    Public Vector3 As Vector3
    Public Float As Single

    Public Sub New(v3 As Vector3, f As Single)
        Vector3 = v3
        Float = f
    End Sub

End Class

Public Class Vector5

    Public Vector3 As Vector3
    Public Vector2 As Vector2

    Public Sub New(v3 As Vector3, v2 As Vector2)
        Vector3 = v3
        Vector2 = v2
    End Sub

End Class