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
    Public swapChance As Integer = 50
    Public swapDistance As Single = 100.0F
    Public notify As Boolean = True
    Public showBlip As Boolean = True
    Public spawnParkVehicle As Boolean = False
    Public roadType As eNodeType = eNodeType.AsphaltRoad
    Public vehicles As Vehicles
    Public vehicleSwaps As New List(Of VehicleSwap)
    Public vehicleSwaps2 As List(Of Model)

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
        swapChance = config.SwapChance
        swapDistance = config.SwapDistance
        notify = config.Notify
        showBlip = config.ShowBlip
        spawnParkVehicle = config.SpawnParkedVehicle
        roadType = config.RoadType
        vehicles = config.Vehicles
        vehicleSwaps = config.VehicleSwaps
        vehicleSwaps2 = New List(Of Model)
        For Each veh In vehicleSwaps
            If veh.Enable Then vehicleSwaps2.Add(New Model(veh.OldVehicle))
        Next
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

    Public Function IsVehicleSwapEnabled() As Boolean
        Return vehicleSwaps.Count = 0
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
    Public Function GetVehicleClassFromName(model As Model) As VehicleClass
        Return Native.Function.Call(Of VehicleClass)(Hash.GET_VEHICLE_CLASS_FROM_NAME, model.GetHashCode)
    End Function

    <Extension>
    Public Sub SetAsCop(ped As Ped)
        Native.Function.Call(Hash.SET_PED_AS_COP, ped, True)
    End Sub

    <Extension>
    Public Sub ClearVehicles(spawnVehicles As Vehicle())
        For v As Integer = 0 To spawnVehicles.Count - 1
            Dim veh As Vehicle = spawnVehicles(v)
            If veh.IsVehicleSpawnByMod AndAlso veh.Position.DistanceTo(Game.Player.Character.Position) > (spawnDistance * 4) Then
                For p As Integer = 0 To veh.PassengerCount - 1
                    veh.Passengers(p).Delete()
                Next
                If showBlip Then veh.CurrentBlip.Remove()
                If notify Then UI.Notify($"~r~{If(veh.FriendlyName = "NULL", veh.DisplayName, veh.FriendlyName)}~w~ at {World.GetStreetName(veh.Position)} is despawned.")
                veh.Delete()
            End If
        Next v
    End Sub

    <Extension>
    Public Sub ClearPeds(spawnPeds As Ped())
        For p As Integer = 0 To spawnPeds.Count - 1
            Dim ped As Ped = spawnPeds(p)
            If ped.IsPedSpawnByMod AndAlso ped.Position.DistanceTo(Game.Player.Character.Position) > (spawnDistance * 4) Then
                If notify Then UI.Notify($"A ped at {World.GetStreetName(ped.Position)} is despawned.")
                ped.Delete()
            End If
        Next p
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
                        If notify Then UI.Notify($"~b~{vehFriendlyName}~w~ is swapped with ~y~{If(newveh.FriendlyName = "NULL", newveh.DisplayName, newveh.FriendlyName)}~w~ at {World.GetStreetName(newveh.Position)}.")
                        model.MarkAsNoLongerNeeded()
                        newveh.MarkAsNoLongerNeeded()
                    Else
                        UI.Notify($"{modelString} is not a valid model.")
                        SwapVehicleHaveDriverOffScreen()
                        Exit Sub
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
                        If notify Then UI.Notify($"~b~{vehFriendlyName}~w~ is swapped with ~y~{If(newveh.FriendlyName = "NULL", newveh.DisplayName, newveh.FriendlyName)}~w~ at {World.GetStreetName(newveh.Position)}.")
                        model.MarkAsNoLongerNeeded()
                        newveh.MarkAsNoLongerNeeded()
                    Else
                        UI.Notify($"{modelString} is not a valid model.")
                        SwapVehicleHaveDriver()
                        Exit Sub
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
                            newveh.CurrentBlip.Color = BlipColor.YellowDark
                            newveh.CurrentBlip.Name = If(newveh.FriendlyName = "NULL", newveh.DisplayName, newveh.FriendlyName)
                        End If
                        If notify Then UI.Notify($"~b~{vehFriendlyName}~w~ is swapped with ~y~{If(newveh.FriendlyName = "NULL", newveh.DisplayName, newveh.FriendlyName)}(P)~w~ at {World.GetStreetName(veh.Position)}.")
                        model.MarkAsNoLongerNeeded()
                        newveh.MarkAsNoLongerNeeded()
                    Else
                        UI.Notify($"{modelString} is not a valid model.")
                        SwapParkedVehicle()
                        Exit Sub
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
            If notify Then UI.Notify($"~g~{If(veh.FriendlyName = "NULL", veh.DisplayName, veh.FriendlyName)}~w~ is spawned at {World.GetStreetName(veh.Position)}.")
            model.MarkAsNoLongerNeeded()
            veh.MarkAsNoLongerNeeded()
            driver.MarkAsNoLongerNeeded()
        Else
            UI.Notify($"{modelString} is not a valid model.")
            SpawnVehicle()
            Exit Sub
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

            Dim closestVehicleNodeCoords As Vector3 = Vector3.Zero
            Dim roadHeading As Single = 0F
            Dim tempCoords, tempRoadHeading As New OutputArgument

            Native.Function.Call(Of Vector3)(Hash.GET_CLOSEST_VEHICLE_NODE_WITH_HEADING, coords.X, coords.Y, coords.Z, tempCoords, tempRoadHeading, roadType, 3.0F, 0)
            closestVehicleNodeCoords = tempCoords.GetResult(Of Vector3)
            roadHeading = tempRoadHeading.GetResult(Of Single)

            If closestVehicleNodeCoords.DistanceTo(Game.Player.Character.Position) < (spawnDistance / 2) Then
                model.MarkAsNoLongerNeeded()
                SpawnVehicle()
                Exit Sub
            End If

            If closestVehicleNodeCoords.GetVehicleNodeProperties.Item2 > 2 Then
                model.MarkAsNoLongerNeeded()
                SpawnVehicle()
                Exit Sub
            End If

            Dim roadside = GetPointOnRoadSide(closestVehicleNodeCoords)

            Dim veh As Vehicle = World.CreateVehicle(model, roadside, roadHeading)
            veh.Position -= veh.RightVector * 3
            While veh.IsOnRoad
                veh.Position += veh.RightVector * 2
            End While

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
                veh.CurrentBlip.Color = BlipColor.GreenDark
                veh.CurrentBlip.Name = If(veh.FriendlyName = "NULL", veh.DisplayName, veh.FriendlyName)
            End If
            If notify Then UI.Notify($"~g~{If(veh.FriendlyName = "NULL", veh.DisplayName, veh.FriendlyName)}(P)~w~ is spawned at {World.GetStreetName(veh.Position)}.")
            model.MarkAsNoLongerNeeded()
            veh.MarkAsNoLongerNeeded()
        Else
            UI.Notify($"{modelString} is not a valid model.")
            SpawnParkedVehicle()
            Exit Sub
        End If
    End Sub

    Public Function GetPlayerZone() As String
        Dim pos As Vector3 = Game.Player.Character.Position
        Return Native.Function.Call(Of String)(Hash.GET_NAME_OF_ZONE, pos.X, pos.Y, pos.Z)
    End Function

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
        veh.SetNeonLightsOn(3, True)
        veh.SetNeonLightsOn(2, True)
        veh.SetNeonLightsOn(0, True)
        veh.SetNeonLightsOn(1, True)
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
        veh.SetNeonLightsOn(3, True)
        veh.SetNeonLightsOn(2, True)
        veh.SetNeonLightsOn(0, True)
        veh.SetNeonLightsOn(1, True)
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
            Case 50, 49, GameVersion.VER_1_0_1604_0_NOSTEAM, GameVersion.VER_1_0_1604_0_STEAM 'GameVersion.VER_1_0_1604_1_NOSTEAM, GameVersion.VER_1_0_1604_1_STEAM
                Return GlobalValue.b1_0_1604_1
            Case 54, 53, 52, 51 'GameVersion.VER_1_0_1734_0_NOSTEAM, GameVersion.VER_1_0_1734_0_STEAM, GameVersion.VER_1_0_1737_0_NOSTEAM, GameVersion.VER_1_0_1737_0_STEAM
                Return GlobalValue.b1_0_1737_0
            Case Else
                Return GlobalValue.b1_0_1737_0
        End Select
    End Function

    Public Function GetPlayerZone2() As eZone
        Dim pp = Game.Player.Character.Position
        Dim zone = Native.Function.Call(Of String)(Hash.GET_NAME_OF_ZONE, pp.X, pp.Y, pp.Z)
        Select Case zone
            Case "PBOX", "SKID", "TEXTI", "LEGSQU", "DOWNT"
                Return eZone.Downtown
            Case "DTVINE", "EAST_V", "MIRR", "HORS", "WVINE", "ALTA", "HAWICK", "VINE", "RICHM", "GOLF", "ROCKF", "CHIL", "RGLEN"
                Return eZone.Vinewood
            Case "DAVIS", "STRAW", "CHAMH", "RANCHO"
                Return eZone.SouthLosSantos
            Case "BANNING", "ELYSIAN", "TERMINA", "ZP_ORT"
                Return eZone.PortOfSouthLosSantos
            Case "LMESA", "CYPRE", "EBURO", "MURRI"
                Return eZone.EastLosSantos
            Case "VESP", "BEACH", "VCANA", "DELSOL"
                Return eZone.Vespucci
            Case "DELBE", "DELPE", "LOSPUER", "STAD", "KOREAT", "AIRP", "MORN", "PBLUFF", "BHAMCA", "CHU", "TONGVAH", "TONGVAV", "GREATC", "TATAMO", "LDAM",
                 "LACT", "PALHIGH", "NOOSE", "MOVIE"
                Return eZone.LosSantos
            Case "DESRT", "JAIL", "RTRAK"
                Return eZone.GrandSenoraDesert
            Case "SANCHIA", "WINDF", "PALMPOW", "HUMLAB", "ZQ_UAR"
                Return eZone.SanChianskiMountainRange
            Case "PALETO", "PALFOR", "PALCOV", "PROCOB", "HARMO", "SANDY", "MTJOSE", "ZANCUDO", "SLAB", "LAGO", "ARMYB", "NCHU", "CANNY", "CCREAK", "CALAFB", "CMSW", "ALAMO", "GRAPES", "MTGORDO",
                 "ELGORL", "BRADP", "MTCHIL", "GALFISH", "BRADT"
                Return eZone.BlaineCounty
            Case Else
                Return eZone.LosSantos
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
    End Enum

    Public Function GetPlayerZoneVehicleList() As List(Of String)
        Dim pp = Game.Player.Character.Position
        Dim zone = Native.Function.Call(Of String)(Hash.GET_NAME_OF_ZONE, pp.X, pp.Y, pp.Z)
        Select Case zone
            Case "PBOX", "SKID", "TEXTI", "LEGSQU", "DOWNT"
                Return vehicles.Downtown
            Case "DTVINE", "EAST_V", "MIRR", "HORS", "WVINE", "ALTA", "HAWICK", "VINE", "RICHM", "GOLF", "ROCKF", "CHIL", "RGLEN"
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
                         "LACT", "PALHIGH", "NOOSE", "MOVIE"
                Return vehicles.LosSantos
            Case "DESRT", "JAIL", "RTRAK"
                Return vehicles.GrandSenoraDesert
            Case "SANCHIA", "WINDF", "PALMPOW", "HUMLAB", "ZQ_UAR"
                Return vehicles.SanChianskiMountainRange
            Case "PALETO", "PALFOR", "PALCOV", "PROCOB", "HARMO", "SANDY", "MTJOSE", "ZANCUDO", "SLAB", "LAGO", "ARMYB", "NCHU", "CANNY", "CCREAK", "CALAFB", "CMSW", "ALAMO", "GRAPES", "MTGORDO",
                         "ELGORL", "BRADP", "MTCHIL", "GALFISH", "BRADT"
                Return vehicles.BlaineCounty
            Case Else
                Return vehicles.LosSantos
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
    Public Function GetRoadSidePointWithHeading(pos As Vector3) As Quaternion
        Dim outV, outH As New OutputArgument()
        Native.Function.Call(Of Boolean)(&HA0F8A7517A273C05UL, pos.X, pos.Y, pos.Z, outH, outV)
        Return New Quaternion(outV.GetResult(Of Vector3)(), outH.GetResult(Of Single)())
    End Function

    <Extension>
    Public Function GetPointOnRoadSide(pos As Vector3) As Vector3
        Dim out As New OutputArgument()
        Native.Function.Call(Of Boolean)(&H16F46FB18C8009E4, pos.X, pos.Y, pos.Z, -1, out)
        Return out.GetResult(Of Vector3)()
    End Function

    <Extension>
    Public Function GetCorrectRoadCoords(coords As Vector3, roadtype As eNodeType) As Quaternion
        Dim closestVehicleNodeCoords As Vector3 = Vector3.Zero
        Dim roadHeading As Single = 0F
        Dim tempCoords, tempRoadHeading As New OutputArgument

        Native.Function.Call(Of Vector3)(Hash.GET_CLOSEST_VEHICLE_NODE_WITH_HEADING, coords.X, coords.Y, coords.Z, tempCoords, tempRoadHeading, roadtype, 3.0F, 0)
        closestVehicleNodeCoords = tempCoords.GetResult(Of Vector3)
        roadHeading = tempRoadHeading.GetResult(Of Single)
        Return New Quaternion(closestVehicleNodeCoords, roadHeading)
    End Function

    <Extension>
    Public Function FindSpawnPointInDirection(pos As Vector3) As Vector3
        Dim out As New OutputArgument()
        Dim rot = Game.Player.Character.Rotation
        Native.Function.Call(Of Boolean)(Hash.FIND_SPAWN_POINT_IN_DIRECTION, pos.X, pos.Y, pos.Z, rot.X, rot.Y, rot.Z, spawnDistance, out)
        Return out.GetResult(Of Vector3)()
    End Function

    <Extension>
    Public Function GetNextPositionOnStreetWithHeading(pos As Vector3) As Quaternion
        Dim outV, outH, outU As New OutputArgument()
        Native.Function.Call(Of Boolean)(Hash.GET_NTH_CLOSEST_VEHICLE_NODE_WITH_HEADING, pos.X, pos.Y, pos.Z, 1, outV, outH, outU, 9, 3.0F, 2.5F)
        Return New Quaternion(outV.GetResult(Of Vector3)(), outH.GetResult(Of Single)())
    End Function

    <Extension>
    Public Function IsOnRoad(veh As Vehicle) As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.IS_POINT_ON_ROAD, veh.Position.X, veh.Position.Y, veh.Position.Z, veh)
    End Function

    <Extension>
    Public Function GetClosestVehicleNodeID(pos As Vector3) As Integer
        Return Native.Function.Call(Of Integer)(Hash.GET_NTH_CLOSEST_VEHICLE_NODE_ID, pos.X, pos.Y, pos.Z, 1, roadType, 1077936128, 0F)
    End Function

    <Extension>
    Public Function GetVehicleNodeProperties(pos As Vector3) As Tuple(Of Integer, Integer)
        Dim outD, outF As New OutputArgument
        Native.Function.Call(Of Boolean)(Hash.GET_VEHICLE_NODE_PROPERTIES, pos.X, pos.Y, pos.Z, outD, outF)
        Return New Tuple(Of Integer, Integer)(outD.GetResult(Of Integer)(), outF.GetResult(Of Integer)())
    End Function

End Module
