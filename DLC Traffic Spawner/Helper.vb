Imports System.Drawing
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Threading
Imports GTA
Imports GTA.Math
Imports GTA.Native
Imports Metadata

Module Helper

    Public modDecor As String = "inm_traffic"
    Public rdVehicle, rdColor, rdMod, rdSpawn, rdSwap, rdWheel As Random
    Public Sekarang As Date = Now
    Public XianZai As Date = Now
    Public XMLFileDate As Date = File.GetLastWriteTime(".\scripts\AddedTraffic.xml")

    'Settings
    Public config As Settings = New Settings(".\scripts\AddedTraffic.xml").Instance
    Public waitTime As WaitTime = New WaitTime(15, 10, 15, 10, 5)
    Public cruiseSpeed As Single = 20.0F
    Public spawnDistance As Single = 150.0F
    Public drivingStyle As DrivingStyle = DrivingStyle.Normal
    Public enableUpgrade As Boolean = True
    Public upgradeChance As Integer = 20
    Public randomizeColor As Boolean = True
    Public randomizeWheels As Boolean = True
    Public enableNeon As Boolean = True
    Public swapChance As Integer = 50
    Public swapDistance As Single = 100.0F
    Public notify As Boolean = True
    Public showBlip As Boolean = True
    Public roadType As eNodeType = eNodeType.AsphaltRoad
    Public vehicles As Vehicles
    Public vehicleSwaps As New List(Of VehicleSwap)
    Public vehicleSwaps2 As List(Of Model)
    Public spawnParkedVehicles As Boolean

    Public Sub LoadSettings()
        CreateConfig()
        waitTime = config.WaitTime
        cruiseSpeed = config.CruiseSpeed
        spawnDistance = config.SpawnDistance
        drivingStyle = config.DrivingStyle
        enableUpgrade = config.EnableUpgrade
        upgradeChance = config.UpgradeChance
        randomizeColor = config.RandomizeColor
        randomizeWheels = config.RandomizeWheels
        enableNeon = config.EnableNeonUpgrade
        swapChance = config.SwapChance
        swapDistance = config.SwapDistance
        notify = config.Notify
        showBlip = config.ShowBlip
        roadType = config.RoadType
        vehicles = config.Vehicles
        vehicleSwaps = config.VehicleSwaps
        vehicleSwaps2 = New List(Of Model)
        For Each veh In vehicleSwaps
            If veh.Enable Then vehicleSwaps2.Add(New Model(veh.OldVehicle))
        Next
        spawnParkedVehicles = config.SpawnParkedVehicle
    End Sub

    Public Sub CreateConfig()
        If Not File.Exists(".\scripts\AddedTraffic.xml") Then
            config.FileName = ".\scripts\AddedTraffic.xml"
            config.Save()
        End If
    End Sub

    <Extension>
    Public Function IsVehicleSpawnByMod(veh As Vehicle) As Boolean
        If veh.GetBool(modDecor) = Nothing Then Return False
        Return veh.GetBool(modDecor)
    End Function

    <Extension>
    Public Function IsPedSpawnByMod(ped As Ped) As Boolean
        Return ped.GetBool(modDecor)
    End Function

    <Extension>
    Public Sub SetVehicleIsSpawnByMod(veh As Vehicle)
        veh.SetBool(modDecor, True)
    End Sub

    <Extension>
    Public Sub SetPedIsSpawnByMod(ped As Ped)
        ped.SetBool(modDecor, True)
    End Sub

    <Extension>
    Public Sub SetAsCop(ped As Ped)
        Native.Function.Call(Hash.SET_PED_AS_COP, ped, True)
    End Sub

    Public Sub SwapVehicleHaveDriverOffScreen()
        Dim gpclv = Game.Player.Character.LastVehicle
        Dim gpcp = Game.Player.Character.Position

        Dim query = (From v In World.GetAllVehicles Where v.IsPersistent = False And Not v.Model.IsBoat And Not v.Model.IsHelicopter And Not v.Model.IsTrain And Not v.Model.IsPlane And
                     Not v.IsVehicleSpawnByMod And Not v = gpclv And Not v.IsSeatFree(VehicleSeat.Driver) And v.Position.DistanceTo(gpcp) <= swapDistance And Not v.IsOnScreen And
                     vehicleSwaps2.Contains(v.Model) Select v)

        If query.Count <> 0 Then
            Dim veh As Vehicle = query.FirstOrDefault

            If veh = Nothing Then
                SwapVehicleHaveDriverOffScreen()
                Exit Sub
            Else
                rdSwap = New Random
                Dim schance As Integer = rdSwap.Next(0, 100)
                If schance >= 0 AndAlso schance <= swapChance Then
                    Dim modelString As String = vehicleSwaps.Where(Function(x) New Model(x.OldVehicle) = veh.Model).FirstOrDefault.NewVehicle
                    If String.IsNullOrEmpty(modelString) Then
                        If Not GetPlayerZoneVehicleList.Count = 0 Then
                            rdVehicle = New Random
                            modelString = GetPlayerZoneVehicleList(rdVehicle.Next(0, GetPlayerZoneVehicleList.Count))
                        Else
                            Exit Sub
                        End If
                    End If

                    Dim model As Model = New Model(modelString)
                    model.Request(250)
                    If model.IsInCdImage AndAlso model.IsValid Then
                        While Not model.IsLoaded
                            Script.Wait(50)
                        End While

                        Dim vehFriendlyName As String = If(veh.FriendlyName = "NULL", veh.DisplayName, veh.FriendlyName)

                        Dim driver As Ped = veh.Driver
                        driver.IsPersistent = True
                        Dim passengers As Ped() = veh.Passengers
                        Dim isCop As Boolean = driver.IsInPoliceVehicle
                        driver.AlwaysKeepTask = False

                        Dim newveh As Vehicle = World.CreateVehicle(model, veh.Position, veh.Heading)
                        veh.SetNoCollision(newveh, True)

                        newveh.EngineRunning = True
                        driver.SetIntoVehicle(newveh, VehicleSeat.Driver)
                        driver.SetPedIsSpawnByMod
                        driver.IsPersistent = False
                        driver.Task.CruiseWithVehicle(newveh, cruiseSpeed, drivingStyle)
                        driver.MarkAsNoLongerNeeded()

                        For Each pass As Ped In passengers
                            pass.SetIntoVehicle(newveh, pass.SeatIndex)
                            pass.MarkAsNoLongerNeeded()
                        Next

                        newveh.IsPersistent = veh.IsPersistent
                        newveh.SetVehicleIsSpawnByMod
                        newveh.LockStatus = veh.LockStatus
                        newveh.ForwardSpeed(veh.Speed)
                        newveh.Velocity = veh.Velocity

                        veh.Delete()

                        If Not randomizeColor Then newveh.PaintVehicle
                        If enableUpgrade Then
                            rdMod = New Random
                            Dim chance As Integer = rdMod.Next(0, 100)
                            If chance >= 0 AndAlso chance <= upgradeChance Then
                                Dim fullOrRandom As Integer = rdMod.Next(0, 100)
                                Select Case fullOrRandom
                                    Case 0 To 50
                                        newveh.FullyUpgradeVehicle
                                    Case Else
                                        newveh.RandomlyUpgradeVehicle
                                End Select
                            End If
                        End If
                        If showBlip Then
                            newveh.AddBlip()
                            newveh.CurrentBlip.Color = BlipColor.YellowDark
                            newveh.CurrentBlip.Name = If(newveh.FriendlyName = "NULL", newveh.DisplayName, newveh.FriendlyName)
                        End If
                        If notify Then UI.Notify($"~b~{vehFriendlyName}~w~ was swapped with ~y~{If(newveh.FriendlyName = "NULL", newveh.DisplayName, newveh.FriendlyName)}~w~ at {World.GetStreetName(newveh.Position)}.")
                        model.MarkAsNoLongerNeeded()
                        newveh.MarkAsNoLongerNeeded()
                    Else
                        UI.Notify($"{modelString} is not a valid model.")
                    End If
                Else
                    veh.SetVehicleIsSpawnByMod
                End If
            End If
        End If
    End Sub

    Public Sub SwapVehicleHaveDriver()
        Dim gpclv = Game.Player.Character.LastVehicle
        Dim gpcp = Game.Player.Character.Position

        Dim query = (From v In World.GetAllVehicles Where v.IsPersistent = False And Not v.Model.IsBoat And Not v.Model.IsHelicopter And Not v.Model.IsTrain And Not v.Model.IsPlane And
                     Not v.IsVehicleSpawnByMod And Not v = gpclv And Not v.IsSeatFree(VehicleSeat.Driver) And v.Position.DistanceTo(gpcp) <= swapDistance And v.Position.DistanceTo(gpcp) >= (swapDistance / 2) And
                     vehicleSwaps2.Contains(v.Model) Select v)

        If query.Count <> 0 Then
            Dim veh As Vehicle = query.FirstOrDefault

            If veh = Nothing Then
                SwapVehicleHaveDriver()
                Exit Sub
            Else
                rdSwap = New Random
                Dim schance As Integer = rdSwap.Next(0, 100)
                If schance >= 0 AndAlso schance <= swapChance Then
                    Dim modelString As String = vehicleSwaps.Where(Function(x) New Model(x.OldVehicle) = veh.Model).FirstOrDefault.NewVehicle
                    If String.IsNullOrEmpty(modelString) Then
                        If Not GetPlayerZoneVehicleList.Count = 0 Then
                            rdVehicle = New Random
                            modelString = GetPlayerZoneVehicleList(rdVehicle.Next(0, GetPlayerZoneVehicleList.Count))
                        Else
                            Exit Sub
                        End If
                    End If

                    Dim model As Model = New Model(modelString)
                    model.Request(250)
                    If model.IsInCdImage AndAlso model.IsValid Then
                        While Not model.IsLoaded
                            Script.Wait(50)
                        End While

                        Dim vehFriendlyName As String = If(veh.FriendlyName = "NULL", veh.DisplayName, veh.FriendlyName)

                        Dim driver As Ped = veh.Driver
                        driver.IsPersistent = True
                        Dim passengers As Ped() = veh.Passengers
                        Dim isCop As Boolean = driver.IsInPoliceVehicle
                        driver.AlwaysKeepTask = False

                        Dim newveh As Vehicle = World.CreateVehicle(model, veh.Position, veh.Heading)
                        veh.SetNoCollision(newveh, True)

                        newveh.EngineRunning = True
                        driver.SetIntoVehicle(newveh, VehicleSeat.Driver)
                        driver.SetPedIsSpawnByMod
                        driver.IsPersistent = False
                        driver.Task.CruiseWithVehicle(newveh, cruiseSpeed, drivingStyle)
                        driver.MarkAsNoLongerNeeded()

                        For Each pass As Ped In passengers
                            pass.SetIntoVehicle(newveh, pass.SeatIndex)
                            pass.MarkAsNoLongerNeeded()
                        Next

                        newveh.IsPersistent = veh.IsPersistent
                        newveh.SetVehicleIsSpawnByMod
                        newveh.LockStatus = veh.LockStatus
                        newveh.ForwardSpeed(veh.Speed)
                        newveh.Velocity = veh.Velocity

                        veh.Delete()

                        If Not randomizeColor Then newveh.PaintVehicle
                        If enableUpgrade Then
                            rdMod = New Random
                            Dim chance As Integer = rdMod.Next(0, 100)
                            If chance >= 0 AndAlso chance <= upgradeChance Then
                                Dim fullOrRandom As Integer = rdMod.Next(0, 100)
                                Select Case fullOrRandom
                                    Case 0 To 50
                                        newveh.FullyUpgradeVehicle
                                    Case Else
                                        newveh.RandomlyUpgradeVehicle
                                End Select
                            End If
                        End If
                        If showBlip Then
                            newveh.AddBlip()
                            newveh.CurrentBlip.Color = BlipColor.YellowDark
                            newveh.CurrentBlip.Name = If(newveh.FriendlyName = "NULL", newveh.DisplayName, newveh.FriendlyName)
                        End If
                        If notify Then UI.Notify($"~b~{vehFriendlyName}~w~ was swapped with ~y~{If(newveh.FriendlyName = "NULL", newveh.DisplayName, newveh.FriendlyName)}~w~ at {World.GetStreetName(newveh.Position)}.")
                        model.MarkAsNoLongerNeeded()
                        newveh.MarkAsNoLongerNeeded()
                    Else
                        UI.Notify($"{modelString} is not a valid model.")
                    End If
                Else
                    veh.SetVehicleIsSpawnByMod
                End If
            End If
        Else
            SwapVehicleHaveDriverOffScreen()
            Exit Sub
        End If
    End Sub

    Public Sub SwapParkedVehicle()
        Dim gpclv = Game.Player.Character.LastVehicle
        Dim gpcp = Game.Player.Character.Position

        Dim query = (From v In World.GetAllVehicles Where v.IsPersistent = False And Not v.Model.IsBoat And Not v.Model.IsHelicopter And Not v.Model.IsTrain And Not v.Model.IsPlane And
                     Not v.IsVehicleSpawnByMod And Not v = gpclv And v.IsSeatFree(VehicleSeat.Driver) And v.IsStopped And Not v.EngineRunning And Not v.IsOnScreen And
                     vehicleSwaps2.Contains(v.Model) Select v)

        If query.Count <> 0 Then
            Dim veh As Vehicle = query.FirstOrDefault

            If veh = Nothing Then
                SwapParkedVehicle()
                Exit Sub
            Else
                rdSwap = New Random
                Dim schance As Integer = rdSwap.Next(0, 100)
                If schance >= 0 AndAlso schance <= swapChance Then
                    Dim modelString As String = vehicleSwaps.Where(Function(x) New Model(x.OldVehicle) = veh.Model).FirstOrDefault.NewVehicle
                    If String.IsNullOrEmpty(modelString) Then
                        If Not GetPlayerZoneVehicleList.Count = 0 Then
                            rdVehicle = New Random
                            modelString = GetPlayerZoneVehicleList(rdVehicle.Next(0, GetPlayerZoneVehicleList.Count))
                        Else
                            Exit Sub
                        End If
                    End If

                    Dim model As Model = New Model(modelString)
                    model.Request(250)
                    If model.IsInCdImage AndAlso model.IsValid Then
                        While Not model.IsLoaded
                            Script.Wait(50)
                        End While

                        Dim vehFriendlyName As String = If(veh.FriendlyName = "NULL", veh.DisplayName, veh.FriendlyName)

                        Dim newveh As Vehicle = World.CreateVehicle(model, veh.Position, veh.Heading)
                        veh.SetNoCollision(newveh, True)

                        newveh.IsPersistent = veh.IsPersistent
                        newveh.SetVehicleIsSpawnByMod
                        veh.HasAlarm = True
                        veh.LockStatus = VehicleLockStatus.CanBeBrokenInto
                        veh.NeedsToBeHotwired = True

                        veh.Delete()

                        If Not randomizeColor Then newveh.PaintVehicle
                        If enableUpgrade Then
                            rdMod = New Random
                            Dim chance As Integer = rdMod.Next(0, 100)
                            If chance >= 0 AndAlso chance <= upgradeChance Then
                                Dim fullOrRandom As Integer = rdMod.Next(0, 100)
                                Select Case fullOrRandom
                                    Case 0 To 50
                                        newveh.FullyUpgradeVehicle
                                    Case Else
                                        newveh.RandomlyUpgradeVehicle
                                End Select
                            End If
                        End If
                        If showBlip Then
                            newveh.AddBlip()
                            newveh.CurrentBlip.Color = BlipColor.Yellow
                            newveh.CurrentBlip.Name = If(newveh.FriendlyName = "NULL", newveh.DisplayName, newveh.FriendlyName)
                        End If
                        If notify Then UI.Notify($"~b~{vehFriendlyName}~w~ was swapped with ~y~{If(newveh.FriendlyName = "NULL", newveh.DisplayName, newveh.FriendlyName)}(P)~w~ at {World.GetStreetName(veh.Position)}.")
                        model.MarkAsNoLongerNeeded()
                        newveh.MarkAsNoLongerNeeded()
                    Else
                        UI.Notify($"{modelString} is not a valid model.")
                    End If
                Else
                    veh.SetVehicleIsSpawnByMod
                End If
            End If
        End If
    End Sub

    Public Sub SpawnVehicle()
        rdVehicle = New Random
        Dim modelString As String = GetPlayerZoneVehicleList(rdVehicle.Next(0, GetPlayerZoneVehicleList.Count))
        Dim model As Model = New Model(modelString)
        model.Request(250)
        If model.IsInCdImage AndAlso model.IsValid Then
            While Not model.IsLoaded
                Script.Wait(50)
            End While

            Dim forward As Vector3 = Game.Player.Character.Position + (Game.Player.Character.ForwardVector * spawnDistance)
            Dim left As Vector3 = Game.Player.Character.Position - (Game.Player.Character.RightVector * spawnDistance)
            Dim right As Vector3 = Game.Player.Character.Position + (Game.Player.Character.RightVector * spawnDistance)
            Dim coords As Vector3

            rdSpawn = New Random()
            Select Case rdSpawn.Next(0, 100)
                Case 0 To 33
                    coords = World.GetNextPositionOnStreet(forward, False)
                Case 34 To 66
                    coords = World.GetNextPositionOnStreet(left, False)
                Case 67 To 100
                    coords = World.GetNextPositionOnStreet(right, False)
            End Select

            Dim closestVehicleNodeCoords As Vector3 = Vector3.Zero
            Dim roadHeading As Single = 0F
            Dim tempCoords, tempRoadHeading As New OutputArgument

            Native.Function.Call(Of Vector3)(Hash.GET_CLOSEST_VEHICLE_NODE_WITH_HEADING, coords.X, coords.Y, coords.Z, tempCoords, tempRoadHeading, roadType, 3.0F, 0)
            closestVehicleNodeCoords = tempCoords.GetResult(Of Vector3)
            roadHeading = tempRoadHeading.GetResult(Of Single)

            If closestVehicleNodeCoords.DistanceTo(Game.Player.Character.Position) < (spawnDistance / 2) Then
                SpawnVehicle()
                Exit Sub
            End If

            Dim veh As Vehicle = World.CreateVehicle(model, closestVehicleNodeCoords, roadHeading)
            Dim driver As Ped = Nothing
            Try
                veh.Position = (veh.Position + veh.RightVector * 3)
                driver = veh.CreateRandomPedOnSeat(VehicleSeat.Driver)
                If Not driver.IsHuman Then driver = veh.CreateRandomPedOnSeat(VehicleSeat.Driver)
                If driver.IsInPoliceVehicle Then
                    driver = World.CreatePed(PedHash.Cop01SMY, veh.Position)
                    driver.Task.WarpIntoVehicle(veh, VehicleSeat.Driver)
                    driver.SetAsCop
                    driver.Weapons.Give(WeaponHash.Pistol, 9999, True, True)
                    driver.Weapons.Give(WeaponHash.PumpShotgun, 9999, False, True)
                    driver.Weapons.Give(WeaponHash.CarbineRifle, 9999, False, True)
                    driver.Weapons.Give(WeaponHash.MicroSMG, 9999, False, True)
                    driver.Weapons.Give(WeaponHash.AssaultShotgun, 9999, False, True)
                    driver.Armor = 100
                End If
                driver.SetPedIsSpawnByMod
                driver.IsPersistent = False
                driver.Task.CruiseWithVehicle(veh, cruiseSpeed, drivingStyle)
            Catch ex As Exception
                If Not veh = Nothing Then veh.Delete()
                If Not driver = Nothing Then driver.Delete()
                SpawnVehicle()
                Exit Sub
            End Try

            veh.EngineRunning = True
            veh.IsPersistent = False
            veh.SetVehicleIsSpawnByMod
            If Not randomizeColor Then veh.PaintVehicle
            If enableUpgrade Then
                rdMod = New Random
                Dim chance As Integer = rdMod.Next(0, 100)
                If chance >= 0 AndAlso chance <= upgradeChance Then
                    Dim fullOrRandom As Integer = rdMod.Next(0, 100)
                    Select Case fullOrRandom
                        Case 0 To 50
                            veh.FullyUpgradeVehicle
                        Case Else
                            veh.RandomlyUpgradeVehicle
                    End Select
                End If
            End If
            If showBlip Then
                veh.AddBlip()
                veh.CurrentBlip.Color = BlipColor.GreenDark
                veh.CurrentBlip.Name = If(veh.FriendlyName = "NULL", veh.DisplayName, veh.FriendlyName)
            End If
            If notify Then UI.Notify($"~g~{If(veh.FriendlyName = "NULL", veh.DisplayName, veh.FriendlyName)}~w~ was spawned at {World.GetStreetName(veh.Position)}.")
            model.MarkAsNoLongerNeeded()
            veh.MarkAsNoLongerNeeded()
            driver.MarkAsNoLongerNeeded()
        Else
            UI.Notify($"{modelString} is not a valid model.")
        End If
    End Sub

    Public Sub SpawnParkedVehicle()
        rdVehicle = New Random
        Dim modelString As String = GetPlayerZoneVehicleList(rdVehicle.Next(0, GetPlayerZoneVehicleList.Count))
        Dim model As Model = New Model(modelString)
        model.Request(250)
        If model.IsInCdImage AndAlso model.IsValid Then
            While Not model.IsLoaded
                Script.Wait(50)
            End While

            Dim forward As Vector3 = Game.Player.Character.Position + (Game.Player.Character.ForwardVector * spawnDistance)
            Dim left As Vector3 = Game.Player.Character.Position - (Game.Player.Character.RightVector * spawnDistance)
            Dim right As Vector3 = Game.Player.Character.Position + (Game.Player.Character.RightVector * spawnDistance)
            Dim coords As Vector3

            rdSpawn = New Random()
            Select Case rdSpawn.Next(0, 100)
                Case 0 To 33
                    coords = World.GetNextPositionOnStreet(forward, True)
                Case 34 To 66
                    coords = World.GetNextPositionOnStreet(left, True)
                Case 67 To 100
                    coords = World.GetNextPositionOnStreet(right, True)
            End Select

            Dim parkingSpotPos = coords.GetNearestParkingSpot

            If parkingSpotPos Is Vector5.Zero Then
                model.MarkAsNoLongerNeeded()
                SpawnVehicle()
                Exit Sub
            End If

            If parkingSpotPos.Vector3.IsPositionOccupied(5.0F) Then
                model.MarkAsNoLongerNeeded()
                SpawnVehicle()
                Exit Sub
            End If

            If parkingSpotPos.Vector3.DistanceTo(Game.Player.Character.Position) < (spawnDistance / 2) Then
                model.MarkAsNoLongerNeeded()
                SpawnVehicle()
                Exit Sub
            End If

            Dim veh As Vehicle = World.CreateVehicle(model, parkingSpotPos.Vector3, parkingSpotPos.Vector2.ToHeading)

            veh.EngineRunning = False
            veh.IsPersistent = False
            veh.HasAlarm = True
            veh.LockStatus = VehicleLockStatus.CanBeBrokenInto
            veh.NeedsToBeHotwired = True
            veh.SetVehicleIsSpawnByMod
            If Not randomizeColor Then veh.PaintVehicle
            If enableUpgrade Then
                rdMod = New Random
                Dim chance As Integer = rdMod.Next(0, 100)
                If chance >= 0 AndAlso chance <= upgradeChance Then
                    Dim fullOrRandom As Integer = rdMod.Next(0, 100)
                    Select Case fullOrRandom
                        Case 0 To 50
                            veh.FullyUpgradeVehicle
                        Case Else
                            veh.RandomlyUpgradeVehicle
                    End Select
                End If
            End If
            If showBlip Then
                veh.AddBlip()
                veh.CurrentBlip.Color = BlipColor.Green
                veh.CurrentBlip.Name = If(veh.FriendlyName = "NULL", veh.DisplayName, veh.FriendlyName)
            End If
            If notify Then UI.Notify($"~g~{If(veh.FriendlyName = "NULL", veh.DisplayName, veh.FriendlyName)}(P)~w~ was spawned at {World.GetStreetName(veh.Position)}.")
            model.MarkAsNoLongerNeeded()
            veh.MarkAsNoLongerNeeded()
        Else
            UI.Notify($"{modelString} is not a valid model.")
        End If
    End Sub

    <Extension>
    Public Sub FullyUpgradeVehicle(veh As Vehicle)
        rdWheel = New Random

        veh.InstallModKit()
        veh.SetMod(43, veh.GetModCount(43) - 1, True)
        veh.SetMod(40, veh.GetModCount(40) - 1, True)
        veh.SetMod(42, veh.GetModCount(42) - 1, True)
        veh.SetMod(16, veh.GetModCount(16) - 1, True)
        veh.SetMod(24, rdWheel.Next(-1, veh.GetModCount(24) - 1), CBool(rdWheel.Next(0, 2) > 0))
        veh.SetMod(12, veh.GetModCount(12) - 1, True)
        veh.SetMod(34, veh.GetModCount(34) - 1, True)
        veh.SetMod(29, veh.GetModCount(29) - 1, True)
        veh.SetMod(30, veh.GetModCount(30) - 1, True)
        veh.SetMod(31, veh.GetModCount(31) - 1, True)
        veh.SetMod(11, veh.GetModCount(11) - 1, True)
        veh.SetMod(39, veh.GetModCount(39) - 1, True)
        veh.SetMod(4, veh.GetModCount(4) - 1, True)
        veh.SetMod(8, veh.GetModCount(8) - 1, True)
        veh.SetMod(5, veh.GetModCount(5) - 1, True)
        veh.SetMod(1, veh.GetModCount(1) - 1, True)
        veh.SetMod(23, rdWheel.Next(-1, veh.GetModCount(23) - 1), CBool(rdWheel.Next(0, 2) > 0))
        veh.SetMod(6, veh.GetModCount(6) - 1, True)
        veh.SetMod(7, veh.GetModCount(7) - 1, True)
        veh.SetMod(14, veh.GetModCount(14) - 1, True)
        veh.SetMod(38, veh.GetModCount(38) - 1, True)
        veh.SetMod(48, veh.GetModCount(48) - 1, True)
        veh.SetMod(28, veh.GetModCount(28) - 1, True)
        veh.SetMod(35, veh.GetModCount(35) - 1, True)
        veh.SetMod(25, veh.GetModCount(25) - 1, True)
        veh.SetMod(2, veh.GetModCount(2) - 1, True)
        veh.SetMod(9, veh.GetModCount(9) - 1, True)
        veh.SetMod(10, veh.GetModCount(10) - 1, True)
        veh.SetMod(32, veh.GetModCount(32) - 1, True)
        veh.SetMod(3, veh.GetModCount(3) - 1, True)
        veh.SetMod(36, veh.GetModCount(36) - 1, True)
        veh.SetMod(0, veh.GetModCount(0) - 1, True)
        veh.SetMod(33, veh.GetModCount(33) - 1, True)
        veh.SetMod(41, veh.GetModCount(41) - 1, True)
        veh.SetMod(15, veh.GetModCount(15) - 1, True)
        veh.SetMod(45, veh.GetModCount(45) - 1, True)
        veh.SetMod(13, veh.GetModCount(13) - 1, True)
        veh.SetMod(44, veh.GetModCount(44) - 1, True)
        veh.SetMod(27, veh.GetModCount(27) - 1, True)
        veh.SetMod(37, veh.GetModCount(37) - 1, True)
        veh.SetMod(26, veh.GetModCount(26) - 1, True)
        veh.SetMod(46, veh.GetModCount(46) - 1, True)
        veh.ToggleMod(21, True)
        veh.ToggleMod(17, True)
        veh.ToggleMod(19, True)
        veh.SetNeonLightsOn(3, enableNeon)
        veh.SetNeonLightsOn(2, enableNeon)
        veh.SetNeonLightsOn(0, enableNeon)
        veh.SetNeonLightsOn(1, enableNeon)
        rdColor = New Random()
        veh.NeonLightsColor = Color.FromArgb(rdColor.Next(0, 255), rdColor.Next(0, 255), rdColor.Next(0, 255))
        veh.WindowTint = VehicleWindowTint.None
        veh.XenonLightsColor(rdColor.Next(0, 11))
    End Sub

    <Extension>
    Public Sub RandomlyUpgradeVehicle(veh As Vehicle)
        veh.InstallModKit()
        rdMod = New Random()
        veh.SetMod(43, rdMod.Next(-1, veh.GetModCount(43) - 1), True)
        veh.SetMod(40, rdMod.Next(-1, veh.GetModCount(40) - 1), True)
        veh.SetMod(42, rdMod.Next(-1, veh.GetModCount(42) - 1), True)
        veh.SetMod(16, rdMod.Next(-1, veh.GetModCount(16) - 1), True)
        veh.SetMod(24, rdMod.Next(-1, veh.GetModCount(24) - 1), True)
        veh.SetMod(12, rdMod.Next(-1, veh.GetModCount(12) - 1), True)
        veh.SetMod(34, rdMod.Next(-1, veh.GetModCount(34) - 1), True)
        veh.SetMod(29, rdMod.Next(-1, veh.GetModCount(29) - 1), True)
        veh.SetMod(30, rdMod.Next(-1, veh.GetModCount(30) - 1), True)
        veh.SetMod(31, rdMod.Next(-1, veh.GetModCount(31) - 1), True)
        veh.SetMod(11, rdMod.Next(-1, veh.GetModCount(11) - 1), True)
        veh.SetMod(39, rdMod.Next(-1, veh.GetModCount(39) - 1), True)
        veh.SetMod(4, rdMod.Next(-1, veh.GetModCount(4) - 1), True)
        veh.SetMod(8, rdMod.Next(-1, veh.GetModCount(8) - 1), True)
        veh.SetMod(5, rdMod.Next(-1, veh.GetModCount(5) - 1), True)
        veh.SetMod(1, rdMod.Next(-1, veh.GetModCount(1) - 1), True)
        veh.SetMod(23, rdMod.Next(-1, veh.GetModCount(23) - 1), True)
        veh.SetMod(6, rdMod.Next(-1, veh.GetModCount(6) - 1), True)
        veh.SetMod(7, rdMod.Next(-1, veh.GetModCount(7) - 1), True)
        veh.SetMod(14, rdMod.Next(-1, veh.GetModCount(14) - 1), True)
        veh.SetMod(38, rdMod.Next(-1, veh.GetModCount(38) - 1), True)
        veh.SetMod(48, rdMod.Next(-1, veh.GetModCount(48) - 1), True)
        veh.SetMod(28, rdMod.Next(-1, veh.GetModCount(28) - 1), True)
        veh.SetMod(35, rdMod.Next(-1, veh.GetModCount(35) - 1), True)
        veh.SetMod(25, rdMod.Next(-1, veh.GetModCount(25) - 1), True)
        veh.SetMod(2, rdMod.Next(-1, veh.GetModCount(2) - 1), True)
        veh.SetMod(9, rdMod.Next(-1, veh.GetModCount(9) - 1), True)
        veh.SetMod(10, rdMod.Next(-1, veh.GetModCount(10) - 1), True)
        veh.SetMod(32, rdMod.Next(-1, veh.GetModCount(32) - 1), True)
        veh.SetMod(3, rdMod.Next(-1, veh.GetModCount(3) - 1), True)
        veh.SetMod(36, rdMod.Next(-1, veh.GetModCount(36) - 1), True)
        veh.SetMod(0, rdMod.Next(-1, veh.GetModCount(0) - 1), True)
        veh.SetMod(33, rdMod.Next(-1, veh.GetModCount(33) - 1), True)
        veh.SetMod(41, rdMod.Next(-1, veh.GetModCount(41) - 1), True)
        veh.SetMod(15, rdMod.Next(-1, veh.GetModCount(15) - 1), True)
        veh.SetMod(45, rdMod.Next(-1, veh.GetModCount(45) - 1), True)
        veh.SetMod(13, rdMod.Next(-1, veh.GetModCount(13) - 1), True)
        veh.SetMod(44, rdMod.Next(-1, veh.GetModCount(44) - 1), True)
        veh.SetMod(27, rdMod.Next(-1, veh.GetModCount(27) - 1), True)
        veh.SetMod(37, rdMod.Next(-1, veh.GetModCount(37) - 1), True)
        veh.SetMod(26, rdMod.Next(-1, veh.GetModCount(26) - 1), True)
        veh.SetMod(46, rdMod.Next(-1, veh.GetModCount(46) - 1), True)
        veh.ToggleMod(21, True)
        veh.ToggleMod(17, True)
        veh.ToggleMod(19, True)
        veh.SetNeonLightsOn(3, enableNeon)
        veh.SetNeonLightsOn(2, enableNeon)
        veh.SetNeonLightsOn(0, enableNeon)
        veh.SetNeonLightsOn(1, enableNeon)
        rdColor = New Random()
        veh.NeonLightsColor = Color.FromArgb(rdColor.Next(0, 255), rdColor.Next(0, 255), rdColor.Next(0, 255))
        veh.WindowTint = VehicleWindowTint.None
        veh.XenonLightsColor(rdColor.Next(0, 11))
    End Sub

    <Extension>
    Public Sub PaintVehicle(veh As Vehicle)
        rdColor = New Random
        veh.PrimaryColor = rdColor.Next(0, 160)
        veh.SecondaryColor = veh.PrimaryColor
        rdMod = New Random
        Dim chance As Integer = rdMod.Next(0, 100)
        If chance >= 0 AndAlso chance <= upgradeChance Then
            veh.SecondaryColor = rdColor.Next(0, 160)
        End If
        veh.PearlescentColor = rdColor.Next(0, 160)
        veh.DashboardColor = rdColor.Next(0, 160)
        veh.TrimColor = rdColor.Next(0, 160)
        veh.RimColor = rdColor.Next(0, 160)
    End Sub

    <Extension>
    Public Sub ForwardSpeed(veh As Vehicle, value As Single)
        Native.Function.Call(Hash.SET_VEHICLE_FORWARD_SPEED, veh, value)
    End Sub

    'open shop_controller.ysc and search for "!= 999"
    Public Enum GlobalValue
        b1_0_757_4 = &H271803
        b1_0_791_2 = &H272A34
        b1_0_877_1 = &H2750BD
        b1_0_944_2 = &H279476
        'b1_0_1011_1 = ?
        b1_0_1032_1 = 2593970
        b1_0_1103_2 = 2599337
        b1_0_1180_2 = 2606794
        'b1_0_1290_1 = ?
        b1_0_1365_1 = 4265719
        'b1_0_1493_0 = ?
        b1_0_1493_1 = 4266042
        b1_0_1604_1 = 4266905
        b1_0_1737_0 = 4267883
        b1_0_1868_0 = 4268190
    End Enum

    Public Function GetGlobalValue() As GlobalValue
        Select Case Game.Version
            Case GameVersion.VER_1_0_757_4_NOSTEAM
                Return GlobalValue.b1_0_757_4
            Case GameVersion.VER_1_0_791_2_NOSTEAM, GameVersion.VER_1_0_791_2_STEAM
                Return GlobalValue.b1_0_791_2
            Case GameVersion.VER_1_0_877_1_NOSTEAM, GameVersion.VER_1_0_877_1_STEAM
                Return GlobalValue.b1_0_877_1
            Case GameVersion.VER_1_0_944_2_NOSTEAM, GameVersion.VER_1_0_944_2_STEAM
                Return GlobalValue.b1_0_944_2
            Case GameVersion.VER_1_0_1032_1_NOSTEAM, GameVersion.VER_1_0_1032_1_STEAM
                Return GlobalValue.b1_0_1032_1
            Case GameVersion.VER_1_0_1103_2_NOSTEAM, GameVersion.VER_1_0_1103_2_STEAM
                Return GlobalValue.b1_0_1103_2
            Case GameVersion.VER_1_0_1180_2_NOSTEAM, GameVersion.VER_1_0_1180_2_STEAM
                Return GlobalValue.b1_0_1180_2
            Case GameVersion.VER_1_0_1365_1_NOSTEAM, GameVersion.VER_1_0_1365_1_STEAM
                Return GlobalValue.b1_0_1365_1
            Case GameVersion.VER_1_0_1493_1_NOSTEAM, GameVersion.VER_1_0_1493_1_STEAM
                Return GlobalValue.b1_0_1493_1
            Case GameVersion.VER_1_0_1604_0_NOSTEAM, GameVersion.VER_1_0_1604_0_STEAM, GameVersion.VER_1_0_1604_1_NOSTEAM, GameVersion.VER_1_0_1604_1_STEAM
                Return GlobalValue.b1_0_1604_1
            Case GameVersion.VER_1_0_1737_0_NOSTEAM, GameVersion.VER_1_0_1737_0_STEAM, GameVersion.VER_1_0_1737_6_NOSTEAM, GameVersion.VER_1_0_1737_6_STEAM
                Return GlobalValue.b1_0_1737_0
            Case GameVersion.VER_1_0_1868_0_NOSTEAM, GameVersion.VER_1_0_1868_0_STEAM, 57, 58
                Return GlobalValue.b1_0_1868_0
            Case Else
                Return GlobalValue.b1_0_1868_0
        End Select
    End Function

    Public Enum eZone
        Downtown
        Vinewood
        SouthLosSantos
        PortOfSouthLosSantos
        EastLosSantos
        Vespucci
        LosSantos
        GrandSenoraDesert
        SanChianskiMountainRange
        BlaineCounty
        FortZancudo
    End Enum

    Public Function GetPlayerZoneVehicleList() As List(Of String)
        Dim pp = Game.Player.Character.Position
        Dim zone = Native.Function.Call(Of String)(Hash.GET_NAME_OF_ZONE, pp.X, pp.Y, pp.Z)
        Select Case zone
            Case "PBOX", "SKID", "TEXTI", "LEGSQU", "DOWNT"
                Return vehicles.Downtown
            Case "DTVINE", "EAST_V", "MIRR", "HORS", "WVINE", "ALTA", "HAWICK", "VINE", "RICHM", "golf", "ROCKF", "CHIL", "RGLEN", "OBSERV", "GALLI"
                Return vehicles.Vinewood
            Case "DAVIS", "STRAW", "CHAMH", "RANCHO"
                Return vehicles.SouthLosSantos
            Case "BANNING", "ELYSIAN", "TERMINA", "ZP_ORT"
                Return vehicles.PortOfSouthLosSantos
            Case "LMESA", "CYPRE", "EBURO", "MURRI"
                Return vehicles.EastLosSantos
            Case "VESP", "BEACH", "VCANA", "DELSOL"
                Return vehicles.Vespucci
            Case "DELBE", "DELPE", "LOSPUER", "STAD", "KOREAT", "AIRP", "MORN", "PBLUFF", "BHAMCA", "CHU", "TONGVAH", "TONGVAV", "GREATC", "TATAMO", "LDAM",
                 "LACT", "PALHIGH", "NOOSE", "MOVIE", "SanAnd"
                Return vehicles.LosSantos
            Case "DESRT", "JAIL", "RTRAK"
                Return vehicles.GrandSenoraDesert
            Case "SANCHIA", "WINDF", "PALMPOW", "HUMLAB", "ZQ_UAR"
                Return vehicles.SanChianskiMountainRange
            Case "PALETO", "PALFOR", "PALCOV", "PROCOB", "HARMO", "SANDY", "MTJOSE", "ZANCUDO", "SLAB", "NCHU", "CANNY", "CCREAK", "CALAFB", "CMSW", "ALAMO", "GRAPES", "MTGORDO",
                 "ELGORL", "BRADP", "MTCHIL", "GALFISH", "BRADT", "LAGO"
                Return vehicles.BlaineCounty
            Case "ARMYB"
                Return vehicles.FortZancudo
            Case Else
                Return New List(Of String)
        End Select
    End Function

    Public Function GetCurrentWaitTime() As Integer
        Select Case World.CurrentDayTime.Hours
            Case 0 To 5
                Return waitTime.Midnight
            Case 6 To 11
                Return waitTime.Morning
            Case 12 To 17
                Return waitTime.Afternoon
            Case 18 To 21
                Return waitTime.Evening
            Case Else
                Return waitTime.Night
        End Select
    End Function

    <Extension>
    Public Function IsOnRoad(veh As Vehicle) As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.IS_POINT_ON_ROAD, veh.Position.X, veh.Position.Y, veh.Position.Z, veh)
    End Function

    <Extension>
    Public Function GetVehicleNodeProperties(pos As Vector3) As Tuple(Of Integer, Integer)
        Dim outD, outF As New OutputArgument
        Native.Function.Call(Of Boolean)(Hash.GET_VEHICLE_NODE_PROPERTIES, pos.X, pos.Y, pos.Z, outD, outF)
        Return New Tuple(Of Integer, Integer)(outD.GetResult(Of Integer)(), outF.GetResult(Of Integer)())
    End Function

    Public Function GetSafeZoneSize() As Point
        Dim t As Single = Native.Function.Call(Of Single)(Hash.GET_SAFE_ZONE_SIZE)
        Dim g As Double = System.Math.Round(CDbl(t), 2)
        g = (g * 100) - 90
        g = 10 - g

        Dim sw = Game.ScreenResolution.Width
        Dim sh = Game.ScreenResolution.Height

        Dim r = CSng(sw / sh)
        Dim wmp = r * 5.4F
        Return New Point(CInt(System.Math.Round(g * wmp)), CInt(System.Math.Round(g * 5.4F)))
    End Function

    <Extension>
    Public Function GetParkingSpotByZone(pp As Vector3) As List(Of Vector5)
        Dim zone = Native.Function.Call(Of String)(Hash.GET_NAME_OF_ZONE, pp.X, pp.Y, pp.Z)
        Select Case zone
            Case "DOWNT", "VINE"
                Return DOWNT
            Case "GALLI", "CHIL"
                Return CHIL
            Case "DESRT", "GREATC"
                Return DESRT
            Case "CMSW", "PALCOV"
                Return CMSW
            Case "PBOX"
                Return PBOX
            Case "SKID"
                Return SKID
            Case "TEXTI"
                Return TEXTI
            Case "LEGSQU"
                Return LEGSQU
            Case "CANNY"
                Return CANNY
            Case "DTVINE"
                Return DTVINE
            Case "EAST_V"
                Return EAST_V
            Case "MIRR"
                Return MIRR
            Case "WVINE"
                Return WVINE
            Case "ALTA"
                Return ALTA
            Case "HAWICK"
                Return HAWICK
            Case "RICHM"
                Return RICHM
            Case "golf"
                Return golf
            Case "ROCKF"
                Return ROCKF
            Case "CCREAK"
                Return CCREAK
            Case "RGLEN"
                Return RGLEN
            Case "OBSERV"
                Return OBSERV
            Case "DAVIS"
                Return DAVIS
            Case "STRAW"
                Return STRAW
            Case "CHAMH"
                Return CHAMH
            Case "RANCHO"
                Return RANCHO
            Case "BANNING"
                Return BANNING
            Case "ELYSIAN"
                Return ELYSIAN
            Case "TERMINA"
                Return TERMINA
            Case "ZP_ORT"
                Return ZP_ORT
            Case "LMESA"
                Return LMESA
            Case "CYPRE"
                Return CYPRE
            Case "EBURO"
                Return EBURO
            Case "MURRI"
                Return MURRI
            Case "VESP"
                Return VESP
            Case "BEACH"
                Return BEACH
            Case "VCANA"
                Return VCANA
            Case "DELSOL"
                Return DELSOL
            Case "DELBE"
                Return DELBE
            Case "DELPE"
                Return DELPE
            Case "LOSPUER"
                Return LOSPUER
            Case "STAD"
                Return STAD
            Case "KOREAT"
                Return KOREAT
            Case "AIRP"
                Return AIRP
            Case "MORN"
                Return MORN
            Case "PBLUFF"
                Return PBLUFF
            Case "BHAMCA"
                Return BHAMCA
            Case "CHU"
                Return CHU
            Case "TONGVAH"
                Return TONGVAH
            Case "TONGVAV"
                Return TONGVAV
            Case "TATAMO"
                Return TATAMO
            Case "PALHIGH"
                Return PALHIGH
            Case "NOOSE"
                Return NOOSE
            Case "MOVIE"
                Return MOVIE
            Case "SanAnd"
                Return SanAnd
            Case "ALAMO"
                Return ALAMO
            Case "JAIL"
                Return JAIL
            Case "RTRAK"
                Return RTRAK
            Case "SANCHIA"
                Return SANCHIA
            Case "WINDF"
                Return WINDF
            Case "PALMPOW"
                Return PALMPOW
            Case "HUMLAB"
                Return HUMLAB
            Case "ZQ_UAR"
                Return ZQ_UAR
            Case "PALETO"
                Return PALETO
            Case "PALFOR"
                Return PALFOR
            Case "PROCOB"
                Return PROCOB
            Case "HARMO"
                Return HARMO
            Case "SANDY"
                Return SANDY
            Case "ZANCUDO"
                Return ZANCUDO
            Case "SLAB"
                Return SLAB
            Case "NCHU"
                Return NCHU
            Case "GRAPES"
                Return GRAPES
            Case "MTGORDO"
                Return MTGORDO
            Case "MTCHIL"
                Return MTCHIL
            Case "GALFISH"
                Return GALFISH
            Case "LAGO"
                Return LAGO
            Case "ARMYB"
                Return ARMYB
            Case "BURTON"
                Return BURTON
            Case Else
                Return New List(Of Vector5)
        End Select
    End Function

    <Extension>
    Public Function GetNearestParkingSpot(pos As Vector3) As Vector5
        Dim result = pos.GetParkingSpotByZone.OrderBy(Function(x) System.Math.Abs(x.Vector3.DistanceToSquared(pos)))
        If result.Count <> 0 Then
            Return result.FirstOrDefault
        Else
            Return Vector5.Zero
        End If
    End Function

    <Extension>
    Public Function IsPositionOccupied(pos As Vector3, range As Single) As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.IS_POSITION_OCCUPIED, pos.X, pos.Y, pos.Z, range, False, True, False, False, False, 0, False)
    End Function

    Public Function GetPlayerZone() As String
        Dim pos As Vector3 = Game.Player.Character.Position
        Return Native.Function.Call(Of String)(Hash.GET_NAME_OF_ZONE, pos.X, pos.Y, pos.Z)
    End Function

End Module
