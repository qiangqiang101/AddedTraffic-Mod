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

    <Extension>
    Public Function GetClosestVehicleNodeID(pos As Vector3) As Integer
        Return Native.Function.Call(Of Integer)(Hash.GET_NTH_CLOSEST_VEHICLE_NODE_ID, pos.X, pos.Y, pos.Z, 1, roadType, 1077936128, 0F)
    End Function

    'Public DowntownPS, VinewoodPS, SouthLSPS, PortSLSPS, EastLSPS, VespucciPS, LosSantosPS, GSDesertPS, SCMRPS, BlaineCPS, OceanaPS As New List(Of Vector5)

    'Public Sub RunThread()
    '    UI.Notify("Start spliting Generators")
    '    For Each v5 In ParkingSpots
    '        GetPositionZone(v5)
    '    Next
    '    UI.Notify("Saving Generators")
    '    Dim genText As String = Nothing
    '    genText = WriteText(genText, "DowntownParkingSpots", DowntownPS)
    '    genText = WriteText(genText, "VinewoodParkingSpots", VinewoodPS)
    '    genText = WriteText(genText, "SouthLosSantosParkingSpots", SouthLSPS)
    '    genText = WriteText(genText, "PortOfSouthLosSantosParkingSpots", PortSLSPS)
    '    genText = WriteText(genText, "EastLosSantosParkingSpots", EastLSPS)
    '    genText = WriteText(genText, "VespucciParkingSpots", VespucciPS)
    '    genText = WriteText(genText, "LosSantosParkingSpots", LosSantosPS)
    '    genText = WriteText(genText, "GrandSenoraDesertParkingSpots", GSDesertPS)
    '    genText = WriteText(genText, "SanChainskiMountainRangeParkingRange", SCMRPS)
    '    genText = WriteText(genText, "BlaineCountyParkingSpots", BlaineCPS)
    '    genText = WriteText(genText, "SanAndreasParkingSpots", OceanaPS)
    '    UI.Notify("Finalizing")
    '    File.WriteAllText("gens.vb", genText)
    '    UI.Notify("Generators saved!")
    'End Sub

    'Public Function WriteText(genText As String, listName As String, list As List(Of Vector5)) As String
    '    genText &= "Public " & listName & " As New List(Of Vector5) From {" & vbNewLine
    '    For Each l In list
    '        genText &= $"        New Vector5(New Vector3({l.Vector3.X}F, {l.Vector3.Y}F, {l.Vector3.Z}F), New Vector2({l.Vector2.X}F, {l.Vector2.Y}F)),{vbNewLine}"
    '    Next
    '    genText = genText.TrimEnd(",")
    '    genText &= vbNewLine & "}" & vbNewLine & vbNewLine
    '    Return genText
    'End Function

    'Public Sub GetPositionZone(pp As Vector5)
    '    Dim zone = Native.Function.Call(Of String)(Hash.GET_NAME_OF_ZONE, pp.Vector3.X, pp.Vector3.Y, pp.Vector3.Z)
    '    Select Case zone
    '        Case "PBOX", "SKID", "TEXTI", "LEGSQU", "DOWNT"
    '            DowntownPS.Add(pp)
    '        Case "DTVINE", "EAST_V", "MIRR", "HORS", "WVINE", "ALTA", "HAWICK", "VINE", "RICHM", "golf", "ROCKF", "CHIL", "RGLEN", "BURTON", "OBSERV", "GALLI"
    '            VinewoodPS.Add(pp)
    '        Case "DAVIS", "STRAW", "CHAMH", "RANCHO"
    '            SouthLSPS.Add(pp)
    '        Case "BANNING", "ELYSIAN", "TERMINA", "ZP_ORT"
    '            PortSLSPS.Add(pp)
    '        Case "LMESA", "CYPRE", "EBURO", "MURRI"
    '            EastLSPS.Add(pp)
    '        Case "VESP", "BEACH", "VCANA", "DELSOL"
    '            VespucciPS.Add(pp)
    '        Case "DELBE", "DELPE", "LOSPUER", "STAD", "KOREAT", "AIRP", "MORN", "PBLUFF", "BHAMCA", "CHU", "TONGVAH", "TONGVAV", "GREATC", "TATAMO", "LDAM",
    '             "LACT", "PALHIGH", "NOOSE", "MOVIE", "SanAnd"
    '            LosSantosPS.Add(pp)
    '        Case "DESRT", "JAIL", "RTRAK"
    '            GSDesertPS.Add(pp)
    '        Case "SANCHIA", "WINDF", "PALMPOW", "HUMLAB", "ZQ_UAR"
    '            SCMRPS.Add(pp)
    '        Case "PALETO", "PALFOR", "PALCOV", "PROCOB", "HARMO", "SANDY", "MTJOSE", "ZANCUDO", "SLAB", "LAGO", "ARMYB", "NCHU", "CANNY", "CCREAK", "CALAFB", "CMSW", "ALAMO", "GRAPES", "MTGORDO",
    '             "ELGORL", "BRADP", "MTCHIL", "GALFISH", "BRADT"
    '            BlaineCPS.Add(pp)
    '        Case Else
    '            OceanaPS.Add(pp)
    '    End Select
    'End Sub

End Module
