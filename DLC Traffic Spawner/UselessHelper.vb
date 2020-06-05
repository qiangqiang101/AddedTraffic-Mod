Imports System.Runtime.CompilerServices
Imports GTA
Imports GTA.Math
Imports GTA.Native

Module UselessHelper

    Public Function IsVehicleSwapEnabled() As Boolean
        Return vehicleSwaps.Count = 0
    End Function

    <Extension>
    Public Function GetVehicleClassFromName(model As Model) As VehicleClass
        Return Native.Function.Call(Of VehicleClass)(Hash.GET_VEHICLE_CLASS_FROM_NAME, model.GetHashCode)
    End Function

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

    Public Function GetPlayerZone() As String
        Dim pos As Vector3 = Game.Player.Character.Position
        Return Native.Function.Call(Of String)(Hash.GET_NAME_OF_ZONE, pos.X, pos.Y, pos.Z)
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

    <Extension>
    Public Function GetRoadSidePointWithHeading(pos As Vector3) As Vector4
        Dim outV, outH As New OutputArgument()
        Native.Function.Call(Of Boolean)(&HA0F8A7517A273C05UL, pos.X, pos.Y, pos.Z, outH, outV)
        Return New Vector4(outV.GetResult(Of Vector3)(), outH.GetResult(Of Single)())
    End Function

    <Extension>
    Public Function GetPointOnRoadSide(pos As Vector3) As Vector3
        Dim out As New OutputArgument()
        Native.Function.Call(Of Boolean)(&H16F46FB18C8009E4, pos.X, pos.Y, pos.Z, -1, out)
        Return out.GetResult(Of Vector3)()
    End Function

    <Extension>
    Public Function GetCorrectRoadCoords(coords As Vector3, roadtype As eNodeType) As Vector4
        Dim closestVehicleNodeCoords As Vector3 = Vector3.Zero
        Dim roadHeading As Single = 0F
        Dim tempCoords, tempRoadHeading As New OutputArgument

        Native.Function.Call(Of Vector3)(Hash.GET_CLOSEST_VEHICLE_NODE_WITH_HEADING, coords.X, coords.Y, coords.Z, tempCoords, tempRoadHeading, roadtype, 3.0F, 0)
        closestVehicleNodeCoords = tempCoords.GetResult(Of Vector3)
        roadHeading = tempRoadHeading.GetResult(Of Single)
        Return New Vector4(closestVehicleNodeCoords, roadHeading)
    End Function

    <Extension>
    Public Function FindSpawnPointInDirection(pos As Vector3) As Vector3
        Dim out As New OutputArgument()
        Dim rot = Game.Player.Character.Rotation
        Native.Function.Call(Of Boolean)(Hash.FIND_SPAWN_POINT_IN_DIRECTION, pos.X, pos.Y, pos.Z, rot.X, rot.Y, rot.Z, spawnDistance, out)
        Return out.GetResult(Of Vector3)()
    End Function

    <Extension>
    Public Function GetNextPositionOnStreetWithHeading(pos As Vector3) As Vector4
        Dim outV, outH, outU As New OutputArgument()
        Native.Function.Call(Of Boolean)(Hash.GET_NTH_CLOSEST_VEHICLE_NODE_WITH_HEADING, pos.X, pos.Y, pos.Z, 1, outV, outH, outU, 9, 3.0F, 2.5F)
        Return New Vector4(outV.GetResult(Of Vector3)(), outH.GetResult(Of Single)())
    End Function

End Module
