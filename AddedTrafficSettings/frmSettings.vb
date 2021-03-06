﻿Imports System.ComponentModel

Public Class frmSettings

    Private Sub tsmiListEdit_Click(sender As Object, e As EventArgs) Handles tsmiListEdit.Click, btnEditModel.Click
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

    Private Sub tsmiListNew_Click(sender As Object, e As EventArgs) Handles tsmiListNew.Click, btnAddModel.Click
        Dim fl As New frmList
        With fl
            .IsEdit = False
        End With
        fl.Show()
    End Sub

    Private Sub tsmiListDelete_Click(sender As Object, e As EventArgs) Handles tsmiListDelete.Click, btnDelModel.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            lvModelList.Items.Remove(item)
        Next
        lvModelList.Striped
        lvModelList.AddGroupFooter()
    End Sub

    Private Sub tsmiSwapNew_Click(sender As Object, e As EventArgs) Handles tsmiSwapNew.Click, btnAddSwap.Click
        Dim fs As New frmSwap
        With fs
            .VehSwap = New VehicleSwap()
            .IsEdit = False
        End With
        fs.Show()
    End Sub

    Private Sub tsmiSwapEdit_Click(sender As Object, e As EventArgs) Handles tsmiSwapEdit.Click, btnEditSwap.Click
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

    Private Sub tsmiSwapDelete_Click(sender As Object, e As EventArgs) Handles tsmiSwapDelete.Click, btnDelSwap.Click
        For Each item As ListViewItem In lvVehicleSwap.SelectedItems
            lvVehicleSwap.Items.Remove(item)
        Next
        lvVehicleSwap.Striped
    End Sub

    Private Sub lvVehicleSwap_DoubleClick(sender As Object, e As EventArgs) Handles lvVehicleSwap.DoubleClick
        For Each item As ListViewItem In lvVehicleSwap.SelectedItems
            item.Checked = Not item.Checked
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
        lvModelList.Striped
        lvModelList.AddGroupFooter
    End Sub

    Private Sub tsmiVW_Click(sender As Object, e As EventArgs) Handles tsmiVW.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("Vinewood")
            End With
            lvModelList.Items.Add(newItem)
        Next
        lvModelList.Striped
        lvModelList.AddGroupFooter
    End Sub

    Private Sub tsmiSLS_Click(sender As Object, e As EventArgs) Handles tsmiSLS.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("South Los Santos")
            End With
            lvModelList.Items.Add(newItem)
        Next
        lvModelList.Striped
        lvModelList.AddGroupFooter
    End Sub

    Private Sub tsmiPSLS_Click(sender As Object, e As EventArgs) Handles tsmiPSLS.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("Port of South Los Santos")
            End With
            lvModelList.Items.Add(newItem)
        Next
        lvModelList.Striped
        lvModelList.AddGroupFooter
    End Sub

    Private Sub tsmiELS_Click(sender As Object, e As EventArgs) Handles tsmiELS.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("East Los Santos")
            End With
            lvModelList.Items.Add(newItem)
        Next
        lvModelList.Striped
        lvModelList.AddGroupFooter
    End Sub

    Private Sub tsmiVPC_Click(sender As Object, e As EventArgs) Handles tsmiVPC.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("Vespucci")
            End With
            lvModelList.Items.Add(newItem)
        Next
        lvModelList.Striped
        lvModelList.AddGroupFooter
    End Sub

    Private Sub tsmiLS_Click(sender As Object, e As EventArgs) Handles tsmiLS.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("Los Santos")
            End With
            lvModelList.Items.Add(newItem)
        Next
        lvModelList.Striped
        lvModelList.AddGroupFooter
    End Sub

    Private Sub tsmiGSD_Click(sender As Object, e As EventArgs) Handles tsmiGSD.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("Grand Senora Desert")
            End With
            lvModelList.Items.Add(newItem)
        Next
        lvModelList.Striped
        lvModelList.AddGroupFooter
    End Sub

    Private Sub tsmiSCMR_Click(sender As Object, e As EventArgs) Handles tsmiSCMR.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("San Chianski Mountain Range")
            End With
            lvModelList.Items.Add(newItem)
        Next
        lvModelList.Striped
        lvModelList.AddGroupFooter
    End Sub

    Private Sub tsmiBC_Click(sender As Object, e As EventArgs) Handles tsmiBC.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("Blaine County")
            End With
            lvModelList.Items.Add(newItem)
        Next
        lvModelList.Striped
        lvModelList.AddGroupFooter
    End Sub

    Private Sub tsmiFZ_Click(sender As Object, e As EventArgs) Handles tsmiFZ.Click
        For Each item As ListViewItem In lvModelList.SelectedItems
            Dim newItem As ListViewItem = item.Clone
            With newItem
                .Group = lvModelList.Groups("Fort Zancudo")
            End With
            lvModelList.Items.Add(newItem)
        Next
        lvModelList.Striped
        lvModelList.AddGroupFooter
    End Sub

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IO.File.Exists(".\AddedTraffic.xml") Then
            Dim settings = New Settings(".\AddedTraffic.xml").Instance

            nudMorning.Value = settings.WaitTime.Morning
            nudAfternoon.Value = settings.WaitTime.Afternoon
            nudEvening.Value = settings.WaitTime.Evening
            nudNight.Value = settings.WaitTime.Night
            nudMidnight.Value = settings.WaitTime.Midnight

            nudCruiseSpeed.Value = settings.CruiseSpeed
            nudSpawnDistance.Value = settings.SpawnDistance
            cmbDrivingStyle.Text = settings.DrivingStyle
            cmbRoadType.Text = settings.RoadType
            cbSpawnParkedVeh.Checked = settings.SpawnParkedVehicle

            cbNotify.Checked = settings.Notify
            cbBlip.Checked = settings.ShowBlip

            cbVehicleUpgrade.Checked = settings.EnableUpgrade
            cbRdWheels.Checked = settings.RandomizeWheels
            cbRdColor.Checked = settings.RandomizeColor
            nudUpgradeChance.Value = settings.UpgradeChance
            cbNeon.Checked = settings.EnableNeonUpgrade

            nudSwapChance.Value = settings.SwapChance
            nudSwapDistance.Value = settings.SwapDistance
            nudMaxVeh.Value = settings.MaxVehicleAllow
            cbDebugText.Checked = settings.DebugText

            For Each vs In settings.VehicleSwaps
                Dim lvi As New ListViewItem("")
                With lvi
                    .SubItems.Add(vs.OldVehicle)
                    .SubItems.Add(vs.NewVehicle)
                    .Checked = vs.Enable
                    .Tag = vs
                End With
                lvVehicleSwap.Items.Add(lvi)
            Next
            lvVehicleSwap.Striped

            Dim vehList As Vehicles = settings.Vehicles
            For Each ml In vehList.Downtown
                Dim lvi As New ListViewItem("")
                With lvi
                    .SubItems.Add(ml)
                    .Group = lvModelList.Groups("Downtown")
                End With
                lvModelList.Items.Add(lvi)
            Next
            For Each ml In vehList.Vinewood
                Dim lvi As New ListViewItem("")
                With lvi
                    .SubItems.Add(ml)
                    .Group = lvModelList.Groups("Vinewood")
                End With
                lvModelList.Items.Add(lvi)
            Next
            For Each ml In vehList.SouthLosSantos
                Dim lvi As New ListViewItem("")
                With lvi
                    .SubItems.Add(ml)
                    .Group = lvModelList.Groups("South Los Santos")
                End With
                lvModelList.Items.Add(lvi)
            Next
            For Each ml In vehList.PortOfSouthLosSantos
                Dim lvi As New ListViewItem("")
                With lvi
                    .SubItems.Add(ml)
                    .Group = lvModelList.Groups("Port of South Los Santos")
                End With
                lvModelList.Items.Add(lvi)
            Next
            For Each ml In vehList.EastLosSantos
                Dim lvi As New ListViewItem("")
                With lvi
                    .SubItems.Add(ml)
                    .Group = lvModelList.Groups("East Los Santos")
                End With
                lvModelList.Items.Add(lvi)
            Next
            For Each ml In vehList.Vespucci
                Dim lvi As New ListViewItem("")
                With lvi
                    .SubItems.Add(ml)
                    .Group = lvModelList.Groups("Vespucci")
                End With
                lvModelList.Items.Add(lvi)
            Next
            For Each ml In vehList.LosSantos
                Dim lvi As New ListViewItem("")
                With lvi
                    .SubItems.Add(ml)
                    .Group = lvModelList.Groups("Los Santos")
                End With
                lvModelList.Items.Add(lvi)
            Next
            For Each ml In vehList.GrandSenoraDesert
                Dim lvi As New ListViewItem("")
                With lvi
                    .SubItems.Add(ml)
                    .Group = lvModelList.Groups("Grand Senora Desert")
                End With
                lvModelList.Items.Add(lvi)
            Next
            For Each ml In vehList.SanChianskiMountainRange
                Dim lvi As New ListViewItem("")
                With lvi
                    .SubItems.Add(ml)
                    .Group = lvModelList.Groups("San Chianski Mountain Range")
                End With
                lvModelList.Items.Add(lvi)
            Next
            For Each ml In vehList.BlaineCounty
                Dim lvi As New ListViewItem("")
                With lvi
                    .SubItems.Add(ml)
                    .Group = lvModelList.Groups("Blaine County")
                End With
                lvModelList.Items.Add(lvi)
            Next
            For Each ml In vehList.FortZancudo
                Dim lvi As New ListViewItem("")
                With lvi
                    .SubItems.Add(ml)
                    .Group = lvModelList.Groups("Fort Zancudo")
                End With
                lvModelList.Items.Add(lvi)
            Next
            lvModelList.Striped
            lvModelList.AddGroupFooter()
        Else
            Dim result = MessageBox.Show($"AddedTraffic.xml not found, do you want to create one?", "File Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                Dim settings = New Settings(".\AddedTraffic.xml").Instance
                settings.FileName = ".\AddedTraffic.xml"
                settings.Save()

                nudMorning.Value = settings.WaitTime.Morning
                nudAfternoon.Value = settings.WaitTime.Afternoon
                nudEvening.Value = settings.WaitTime.Evening
                nudNight.Value = settings.WaitTime.Night
                nudMidnight.Value = settings.WaitTime.Midnight

                nudCruiseSpeed.Value = settings.CruiseSpeed
                nudSpawnDistance.Value = settings.SpawnDistance
                cmbDrivingStyle.Text = settings.DrivingStyle
                cmbRoadType.Text = settings.RoadType
                cbSpawnParkedVeh.Checked = settings.SpawnParkedVehicle

                cbNotify.Checked = settings.Notify
                cbBlip.Checked = settings.ShowBlip

                cbVehicleUpgrade.Checked = settings.EnableUpgrade
                cbRdWheels.Checked = settings.RandomizeWheels
                cbRdColor.Checked = settings.RandomizeColor
                nudUpgradeChance.Value = settings.UpgradeChance
                cbNeon.Checked = settings.EnableNeonUpgrade

                nudSwapChance.Value = settings.SwapChance
                nudSwapDistance.Value = settings.SwapDistance
                nudMaxVeh.Value = settings.MaxVehicleAllow
                cbDebugText.Checked = settings.DebugText

                For Each vs In settings.VehicleSwaps
                    Dim lvi As New ListViewItem("")
                    With lvi
                        .SubItems.Add(vs.OldVehicle)
                        .SubItems.Add(vs.NewVehicle)
                        .Checked = vs.Enable
                        .Tag = vs
                    End With
                    lvVehicleSwap.Items.Add(lvi)
                Next
                lvVehicleSwap.Striped

                Dim vehList As Vehicles = settings.Vehicles
                For Each ml In vehList.Downtown
                    Dim lvi As New ListViewItem("")
                    With lvi
                        .SubItems.Add(ml)
                        .Group = lvModelList.Groups("Downtown")
                    End With
                    lvModelList.Items.Add(lvi)
                Next
                For Each ml In vehList.Vinewood
                    Dim lvi As New ListViewItem("")
                    With lvi
                        .SubItems.Add(ml)
                        .Group = lvModelList.Groups("Vinewood")
                    End With
                    lvModelList.Items.Add(lvi)
                Next
                For Each ml In vehList.SouthLosSantos
                    Dim lvi As New ListViewItem("")
                    With lvi
                        .SubItems.Add(ml)
                        .Group = lvModelList.Groups("South Los Santos")
                    End With
                    lvModelList.Items.Add(lvi)
                Next
                For Each ml In vehList.PortOfSouthLosSantos
                    Dim lvi As New ListViewItem("")
                    With lvi
                        .SubItems.Add(ml)
                        .Group = lvModelList.Groups("Port of South Los Santos")
                    End With
                    lvModelList.Items.Add(lvi)
                Next
                For Each ml In vehList.EastLosSantos
                    Dim lvi As New ListViewItem("")
                    With lvi
                        .SubItems.Add(ml)
                        .Group = lvModelList.Groups("East Los Santos")
                    End With
                    lvModelList.Items.Add(lvi)
                Next
                For Each ml In vehList.Vespucci
                    Dim lvi As New ListViewItem("")
                    With lvi
                        .SubItems.Add(ml)
                        .Group = lvModelList.Groups("Vespucci")
                    End With
                    lvModelList.Items.Add(lvi)
                Next
                For Each ml In vehList.LosSantos
                    Dim lvi As New ListViewItem("")
                    With lvi
                        .SubItems.Add(ml)
                        .Group = lvModelList.Groups("Los Santos")
                    End With
                    lvModelList.Items.Add(lvi)
                Next
                For Each ml In vehList.GrandSenoraDesert
                    Dim lvi As New ListViewItem("")
                    With lvi
                        .SubItems.Add(ml)
                        .Group = lvModelList.Groups("Grand Senora Desert")
                    End With
                    lvModelList.Items.Add(lvi)
                Next
                For Each ml In vehList.SanChianskiMountainRange
                    Dim lvi As New ListViewItem("")
                    With lvi
                        .SubItems.Add(ml)
                        .Group = lvModelList.Groups("San Chianski Mountain Range")
                    End With
                    lvModelList.Items.Add(lvi)
                Next
                For Each ml In vehList.BlaineCounty
                    Dim lvi As New ListViewItem("")
                    With lvi
                        .SubItems.Add(ml)
                        .Group = lvModelList.Groups("Blaine County")
                    End With
                    lvModelList.Items.Add(lvi)
                Next
                For Each ml In vehList.FortZancudo
                    Dim lvi As New ListViewItem("")
                    With lvi
                        .SubItems.Add(ml)
                        .Group = lvModelList.Groups("Fort Zancudo")
                    End With
                    lvModelList.Items.Add(lvi)
                Next
                lvModelList.Striped
                lvModelList.AddGroupFooter()
            End If
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim newSetting As New Settings(".\AddedTraffic.xml")
        With newSetting
            .WaitTime = New WaitTime(nudMorning.Value, nudAfternoon.Value, nudEvening.Value, nudNight.Value, nudMidnight.Value)
            .CruiseSpeed = nudCruiseSpeed.Value
            .SpawnDistance = nudSpawnDistance.Value
            .DrivingStyle = cmbDrivingStyle.Text
            .RoadType = cmbRoadType.Text
            .RandomizeColor = cbRdColor.Checked
            .RandomizeWheels = cbRdWheels.Checked
            .Notify = cbNotify.Checked
            .ShowBlip = cbBlip.Checked
            .EnableUpgrade = cbVehicleUpgrade.Checked
            .UpgradeChance = nudUpgradeChance.Value
            .SwapChance = nudSwapChance.Value
            .SwapDistance = nudSwapDistance.Value
            .EnableNeonUpgrade = cbNeon.Checked
            .SpawnParkedVehicle = cbSpawnParkedVeh.Checked
            .MaxVehicleAllow = nudMaxVeh.Value
            .DebugText = cbDebugText.Checked

            Dim mSwap As New List(Of VehicleSwap)
            For Each item As ListViewItem In lvVehicleSwap.Items
                mSwap.Add(item.Tag)
            Next
            .VehicleSwaps = mSwap

            Dim dt As New List(Of String)
            Dim vw As New List(Of String)
            Dim sls As New List(Of String)
            Dim psls As New List(Of String)
            Dim els As New List(Of String)
            Dim vpc As New List(Of String)
            Dim ls As New List(Of String)
            Dim gsd As New List(Of String)
            Dim scmr As New List(Of String)
            Dim bc As New List(Of String)
            Dim fz As New List(Of String)

            For Each group As ListViewGroup In lvModelList.Groups
                For Each item As ListViewItem In group.Items
                    Select Case group.Name
                        Case "Downtown"
                            dt.Add(item.SubItems(1).Text)
                        Case "Vinewood"
                            vw.Add(item.SubItems(1).Text)
                        Case "South Los Santos"
                            sls.Add(item.SubItems(1).Text)
                        Case "Port of South Los Santos"
                            psls.Add(item.SubItems(1).Text)
                        Case "East Los Santos"
                            els.Add(item.SubItems(1).Text)
                        Case "Vespucci"
                            vpc.Add(item.SubItems(1).Text)
                        Case "Los Santos"
                            ls.Add(item.SubItems(1).Text)
                        Case "Grand Senora Desert"
                            gsd.Add(item.SubItems(1).Text)
                        Case "San Chianski Mountain Range"
                            scmr.Add(item.SubItems(1).Text)
                        Case "Blaine County"
                            bc.Add(item.SubItems(1).Text)
                        Case "Fort Zancudo"
                            fz.Add(item.SubItems(1).Text)
                    End Select
                Next
            Next

            Dim mList As New Vehicles()
            With mList
                .Downtown = dt
                .Vinewood = vw
                .SouthLosSantos = sls
                .PortOfSouthLosSantos = psls
                .EastLosSantos = els
                .Vespucci = vpc
                .LosSantos = ls
                .GrandSenoraDesert = gsd
                .SanChianskiMountainRange = scmr
                .BlaineCounty = bc
                .FortZancudo = fz
            End With
            .Vehicles = mList
        End With
        newSetting.Save()

        MsgBox("Save successfully.", MsgBoxStyle.Information， “Save”)
    End Sub

    Private Sub llblWeb_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llblWeb.LinkClicked
        Process.Start("https://www.imnotmental.com")
    End Sub

    Private Sub lvVehicleSwap_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles lvVehicleSwap.ItemChecked
        Dim vehSwap As VehicleSwap = e.Item.Tag
        vehSwap.Enable = e.Item.Checked
        e.Item.Tag = vehSwap
    End Sub

    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        Dim helpText As String = "Downtown
Pillbox Hill, Mission Row, Textile City, Legion Square and Downtown.

Vinewood
Vinewood, Downtown Vinewood, East Vinewood, Mirror Park, Vinewood Racetrack, West Vinewood, Alta, Hawick, Richman, Richman Glen, GWC and Golfing Society, Rockford Hills and Vinewood Hills

South Los Santos
Davis, Strawberry, Chamberlain Hills and Rancho

Port of South Los Santos
Banning, Elysian Island, Terminal and Port of South Los Santos

East Los Santos
La Mesa, Cypress Flats, El Burro Heights and Murrieta Heights

Vespucci
Vespucci, Vespucci Beach, Vespucci Canals and Puerto Del Sol

Los Santos
Del Perro Beach, Del Perro, La Puerta, Maze Bank Arena, Little Seoul, L.S.I.A., Morningwood, Pacific Bluffs, Banham Canyon, Chumash, Tongva Hills, Tongva Valley, Great Chaparral, Tataviam Mountains, Land Act Dam, Land Act Reservoir, Palomino Highlands, N.O.O.S.E and Richards Majestic

Grand Senora Desert
Grand Senora Desert, Bolingbroke Penitentiary and Redwood Lights Track

San Chianski Mountain Range
San Chianski Mountain Range, RON Alternates Wind Farm, Palmer-Taylor Power Station, 'Humane Labs and Research' and Davis Quartz

Blaine County
Paleto Cove, Paleto Bay, Paleto Forest, Procopio Beach, Harmony, Sandy Shores, Mount Josiah, Zancudo River, Stab City, Lago Zancudo, Fort Zancudo, North Chumash, Raton Canyon, Cassidy Creek, Calafia Bridge, Chiliad Mountain State Wilderness, Alamo Sea, Grapeseed, Mount Gordo, El Gordo Lighthouse, Braddock Pass, Braddock Tunnel, Mount Chiliad and Galilee

Fort Zancudo
Fort Zancudo Only"

        MsgBox(helpText, MsgBoxStyle.Question, "Help")
    End Sub

End Class
