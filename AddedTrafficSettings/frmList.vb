Public Class frmList

    Public Property ListViewItem As ListViewItem
    Public Property Model As String
    Public Property Category As String
    Public Property IsEdit As Boolean

    Private Sub frmList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IsEdit Then
            cmbCategory.Text = Category
            txtNewVeh.Text = Model
            btnAction.Text = "Edit"
        Else
            cmbCategory.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnAction_Click(sender As Object, e As EventArgs) Handles btnAction.Click
        If IsEdit Then
            ListViewItem.SubItems(1).Text = txtNewVeh.Text
            ListViewItem.Group = frmSettings.lvModelList.Groups(cmbCategory.Text)
        Else
            Dim newLVI As New ListViewItem("")
            With newLVI
                .SubItems.Add(txtNewVeh.Text)
                .Group = frmSettings.lvModelList.Groups(cmbCategory.Text)
            End With
            frmSettings.lvModelList.Items.Add(newLVI)
            frmSettings.lvModelList.Striped
        End If
        Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub
End Class