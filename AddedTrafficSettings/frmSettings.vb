Imports System.ComponentModel

Public Class frmSettings

    Private Sub tsmiListEdit_Click(sender As Object, e As EventArgs) Handles tsmiListEdit.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim fl As New frmList
            With fl
                .ListViewItem = item
                .Category = item.Group.Header
                .Model = item.SubItems(1).Text
                .IsEdit = True
            End With
            fl.Show()
        Next
    End Sub

    Private Sub tsmiListNew_Click(sender As Object, e As EventArgs) Handles tsmiListNew.Click
        Dim fl As New frmList
        With fl
            .IsEdit = False
        End With
        fl.Show()
    End Sub

    Private Sub tsmiListDelete_Click(sender As Object, e As EventArgs) Handles tsmiListDelete.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            lvModelList.Items.Remove(item)
        Next
    End Sub

    Private Sub tsmiSwapNew_Click(sender As Object, e As EventArgs) Handles tsmiSwapNew.Click
        Dim fs As New frmSwap
        With fs
            .VehSwap = New VehicleSwap()
            .IsEdit = False
        End With
        fs.Show()
    End Sub

    Private Sub tsmiSwapEdit_Click(sender As Object, e As EventArgs) Handles tsmiSwapEdit.Click
        For Each item As ListViewItem In lvVehicleSwap.SelectedItems
            Dim fs As New frmSwap
            With fs
                .ListViewItem = item
                .VehSwap = item.Tag
                .IsEdit = True
            End With
            fs.Show()
        Next
    End Sub

    Private Sub tsmiSwapDelete_Click(sender As Object, e As EventArgs) Handles tsmiSwapDelete.Click
        For Each item As ListViewItem In lvVehicleSwap.SelectedItems
            lvVehicleSwap.Items.Remove(item)
        Next
    End Sub

    Private Sub lvVehicleSwap_DoubleClick(sender As Object, e As EventArgs) Handles lvVehicleSwap.DoubleClick
        For Each item As ListViewItem In lvVehicleSwap.SelectedItems
            Dim fs As New frmSwap
            With fs
                .ListViewItem = item
                .VehSwap = item.Tag
                .IsEdit = True
            End With
            fs.Show()
        Next
    End Sub

    Private Sub lvModelList_DoubleClick(sender As Object, e As EventArgs) Handles lvModelList.DoubleClick
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim fl As New frmList
            With fl
                .ListViewItem = item
                .Category = item.Group.Header
                .Model = item.SubItems(1).Text
                .IsEdit = True
            End With
            fl.Show()
        Next
    End Sub

    Private Sub tsmiDT_Click(sender As Object, e As EventArgs) Handles tsmiDT.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("Downtown")
            End With
            lvModelList.Items.Add(newItem)
        Next
    End Sub

    Private Sub tsmiVW_Click(sender As Object, e As EventArgs) Handles tsmiVW.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("Vinewood")
            End With
            lvModelList.Items.Add(newItem)
        Next
    End Sub

    Private Sub tsmiSLS_Click(sender As Object, e As EventArgs) Handles tsmiSLS.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("South Los Santos")
            End With
            lvModelList.Items.Add(newItem)
        Next
    End Sub

    Private Sub tsmiPSLS_Click(sender As Object, e As EventArgs) Handles tsmiPSLS.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("Port of South Los Santos")
            End With
            lvModelList.Items.Add(newItem)
        Next
    End Sub

    Private Sub tsmiELS_Click(sender As Object, e As EventArgs) Handles tsmiELS.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("East Los Santos")
            End With
            lvModelList.Items.Add(newItem)
        Next
    End Sub

    Private Sub tsmiVPC_Click(sender As Object, e As EventArgs) Handles tsmiVPC.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("Vespucci")
            End With
            lvModelList.Items.Add(newItem)
        Next
    End Sub

    Private Sub tsmiLS_Click(sender As Object, e As EventArgs) Handles tsmiLS.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("Los Santos")
            End With
            lvModelList.Items.Add(newItem)
        Next
    End Sub

    Private Sub tsmiGSD_Click(sender As Object, e As EventArgs) Handles tsmiGSD.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("Grand Senora Desert")
            End With
            lvModelList.Items.Add(newItem)
        Next
    End Sub

    Private Sub tsmiSCMR_Click(sender As Object, e As EventArgs) Handles tsmiSCMR.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("San Chianski Mountain Range")
            End With
            lvModelList.Items.Add(newItem)
        Next
    End Sub

    Private Sub tsmiBC_Click(sender As Object, e As EventArgs) Handles tsmiBC.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("Blaine County")
            End With
            lvModelList.Items.Add(newItem)
        Next
    End Sub

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IO.File.Exists(".\AddedTraffic.xml") Then
            Dim settings = New Settings(".\AddedTraffic.xml").Instance

            nudMorning.Value = settings.WaitTime.Morning
            nudAfternoon.Value = settings.WaitTime.Afternoon
            nudEvening.Value = settings.WaitTime.Evening
            nudNight.Value = settings.WaitTime.Night
            nudMidnight.Value = settings.WaitTime.Midnight


        End If

    End Sub
End Class
