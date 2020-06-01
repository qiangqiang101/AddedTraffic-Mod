Public Class frmSwap

    Public Property ListViewItem As ListViewItem
    Public Property VehSwap As VehicleSwap
    Public Property IsEdit As Boolean

    Private Sub frmSwap_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IsEdit Then
            txtOldVeh.Text = VehSwap.OldVehicle
            txtNewVeh.Text = VehSwap.NewVehicle
            cbEnable.Checked = VehSwap.Enable
            btnAction.Text = "Edit"
        End If
    End Sub

    Private Sub btnAction_Click(sender As Object, e As EventArgs) Handles btnAction.Click
        If IsEdit Then
            ListViewItem.Tag = New VehicleSwap(txtOldVeh.Text, txtNewVeh.Text)
            ListViewItem.SubItems(1).Text = txtOldVeh.Text
            ListViewItem.SubItems(2).Text = txtNewVeh.Text
            ListViewItem.Checked = cbEnable.Checked
        Else
            Dim newLVI As New ListViewItem("")
            With newLVI
                .SubItems.Add(txtOldVeh.Text)
                .SubItems.Add(txtNewVeh.Text)
                .Checked = cbEnable.Checked
                .Tag = If(txtNewVeh.Text.Length = 0, New VehicleSwap(txtOldVeh.Text, cbEnable.Checked), New VehicleSwap(txtOldVeh.Text, txtNewVeh.Text, cbEnable.Checked))
            End With
            frmSettings.lvVehicleSwap.Items.Add(newLVI)
            frmSettings.lvVehicleSwap.Striped
        End If
        Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub
End Class