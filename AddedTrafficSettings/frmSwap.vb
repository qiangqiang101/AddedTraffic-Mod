Public Class frmSwap

    Public Property ListViewItem As ListViewItem
    Public Property VehSwap As VehicleSwap
    Public Property IsEdit As Boolean

    Private Sub frmSwap_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IsEdit Then
            txtOldVeh.Text = VehSwap.OldVehicle
            txtNewVeh.Text = VehSwap.NewVehicle
            btnAction.Text = "Edit"
        End If
    End Sub

    Private Sub btnAction_Click(sender As Object, e As EventArgs) Handles btnAction.Click
        If IsEdit Then
            ListViewItem.Tag = New VehicleSwap(txtOldVeh.Text, txtNewVeh.Text)
            ListViewItem.SubItems(1).Text = txtOldVeh.Text
            ListViewItem.SubItems(2).Text = txtNewVeh.Text
        Else
            Dim num As Integer = frmSettings.lvVehicleSwap.Items.Count + 1
            Dim newLVI As New ListViewItem(num)
            With newLVI
                .SubItems.Add(txtOldVeh.Text)
                .SubItems.Add(txtNewVeh.Text)
                .Tag = New VehicleSwap(txtOldVeh.Text, txtNewVeh.Text)
            End With
            frmSettings.lvVehicleSwap.Items.Add(newLVI)
        End If
        Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub
End Class