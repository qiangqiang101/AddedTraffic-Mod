Imports System.Windows.Forms
Imports GTA

Public Class SplitParking
    Inherits Script

    Public Sub New()

    End Sub

    Private Sub SplitParking_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Delete Then
            RunThread()
        End If
    End Sub
End Class
