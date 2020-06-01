Imports System.IO
Imports GTA
Imports Metadata

Public Class Traffic
    Inherits Script

    Public Sub New()
        LoadSettings()
        Game.Globals(GetGlobalValue).SetInt(1)
    End Sub

    Private Sub Traffic_Tick(sender As Object, e As EventArgs) Handles Me.Tick
        If Not Decor.Registered(modDecor, Decor.eDecorType.Bool) Then
            Decor.Unlock()
            Decor.Register(modDecor, Decor.eDecorType.Bool)
            Decor.Lock()
        End If

        If Not GetPlayerZoneVehicleList.Count = 0 Then
            If (Now - Sekarang).TotalSeconds > GetCurrentWaitTime() AndAlso Not Game.IsLoading AndAlso Not Game.IsPaused Then
                SpawnVehicle()
                SpawnVehicle()
                Sekarang = Now
            End If

            'World.GetAllVehicles.ClearVehicles
            'World.GetAllPeds.ClearPeds
        End If

        If Not vehicleSwaps.Count = 0 Then
            If (Now - XianZai).TotalSeconds > GetCurrentWaitTime() AndAlso Not Game.IsLoading AndAlso Not Game.IsPaused Then
                SwapVehicleHaveDriver()
                SwapParkedVehicle()
                XianZai = Now
            End If
        End If

        If File.GetLastWriteTime(".\scripts\AddedTraffic.xml") <> XMLFileDate Then
            UI.Notify($"Added Traffic settings refreshed.")
            config = New Settings(".\scripts\AddedTraffic.xml").Instance
            LoadSettings()
            XMLFileDate = File.GetLastWriteTime(".\scripts\AddedTraffic.xml")
        End If
    End Sub

    Private Sub Traffic_Aborted(sender As Object, e As EventArgs) Handles Me.Aborted
        Dim spawnVehicles = World.GetAllVehicles
        For v As Integer = 0 To spawnVehicles.Count - 1
            If spawnVehicles(v).IsVehicleSpawnByMod AndAlso Not Game.Player.Character.CurrentVehicle = spawnVehicles(v) Then
                For p As Integer = 0 To spawnVehicles(v).PassengerCount - 1
                    spawnVehicles(v).Passengers(p).Delete()
                Next
                spawnVehicles(v).Delete()
            End If
        Next v
        Dim spawnPeds = World.GetAllPeds
        For p As Integer = 0 To spawnPeds.Count - 1
            Dim ped As Ped = spawnPeds(p)
            If ped.IsPedSpawnByMod Then
                spawnPeds(p).Delete()
            End If
        Next p
    End Sub

End Class
