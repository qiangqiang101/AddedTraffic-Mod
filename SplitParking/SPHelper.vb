Imports System.IO
Imports System.Runtime.CompilerServices
Imports GTA
Imports GTA.Math
Imports GTA.Native

Module SPHelper

    Public PBOX, SKID, TEXTI, LEGSQU, DOWNT, DTVINE, EAST_V, MIRR, HORS, WVINE, ALTA, HAWICK, VINE, RICHM, golf, ROCKF, CHIL, RGLEN, OBSERV, GALLI, DAVIS, STRAW, CHAMH,
           RANCHO, BANNING, ELYSIAN, TERMINA, ZP_ORT, LMESA, CYPRE, EBURO, MURRI, VESP, BEACH, VCANA, DELSOL, DELBE, DELPE, LOSPUER, STAD, KOREAT, AIRP, MORN, PBLUFF,
           BHAMCA, CHU, TONGVAH, TONGVAV, GREATC, TATAMO, LDAM, LACT, PALHIGH, NOOSE, MOVIE, SanAnd, DESRT, JAIL, RTRAK, SANCHIA, WINDF, PALMPOW, HUMLAB, ZQ_UAR, PALETO,
           PALFOR, PALCOV, PROCOB, HARMO, SANDY, MTJOSE, ZANCUDO, SLAB, NCHU, CANNY, CCREAK, CALAFB, CMSW, ALAMO, GRAPES, MTGORDO, ELGORL, BRADP, MTCHIL, GALFISH, BRADT,
           LAGO, ARMYB, OCEANA, NAMENOTFOUND As New List(Of Vector5)
    Public Zones As New List(Of List(Of Vector5)) From {PBOX, SKID, TEXTI, LEGSQU, DOWNT, DTVINE, EAST_V, MIRR, HORS, WVINE, ALTA, HAWICK, VINE, RICHM, golf, ROCKF, CHIL, RGLEN, OBSERV, GALLI, DAVIS, STRAW, CHAMH,
           RANCHO, BANNING, ELYSIAN, TERMINA, ZP_ORT, LMESA, CYPRE, EBURO, MURRI, VESP, BEACH, VCANA, DELSOL, DELBE, DELPE, LOSPUER, STAD, KOREAT, AIRP, MORN, PBLUFF,
           BHAMCA, CHU, TONGVAH, TONGVAV, GREATC, TATAMO, LDAM, LACT, PALHIGH, NOOSE, MOVIE, SanAnd, DESRT, JAIL, RTRAK, SANCHIA, WINDF, PALMPOW, HUMLAB, ZQ_UAR, PALETO,
           PALFOR, PALCOV, PROCOB, HARMO, SANDY, MTJOSE, ZANCUDO, SLAB, NCHU, CANNY, CCREAK, CALAFB, CMSW, ALAMO, GRAPES, MTGORDO, ELGORL, BRADP, MTCHIL, GALFISH, BRADT,
           LAGO, ARMYB, OCEANA, NAMENOTFOUND}

    Public Sub RunThread()
        UI.Notify("Start spliting Generators")
        For Each v5 In ParkingSpots
            GetPositionZone(v5)
        Next
        UI.Notify("Saving Generators")
        Dim genText As String = Nothing

        genText = WriteText(genText, Zones)

        UI.Notify("Finalizing")
        File.WriteAllText("gens.vb", genText)
        UI.Notify("Generators saved!")
    End Sub

    Public Function WriteText(genText As String, list As List(Of List(Of Vector5))) As String
        For Each lv5 In list
            Dim listName = NameOf(lv5)
            genText &= "    Public " & listName & " As New List(Of Vector5) From {" & vbNewLine
            For Each v5 In lv5
                genText &= $"        New Vector5(New Vector3({v5.Vector3.X}F, {v5.Vector3.Y}F, {v5.Vector3.Z}F), New Vector2({v5.Vector2.X}F, {v5.Vector2.Y}F)),{vbNewLine}"
            Next
            genText = genText.Remove(genText.Length - 2)
            genText &= vbNewLine & "    }" & vbNewLine & vbNewLine
        Next
        Return genText
    End Function

    Public Sub GetPositionZone(pp As Vector5)
        Dim zone As Object = Native.Function.Call(Of String)(Hash.GET_NAME_OF_ZONE, pp.Vector3.X, pp.Vector3.Y, pp.Vector3.Z)
        For Each item In Zones
            UI.Notify(NameOf(item))
            If NameOf(item) = zone Then
                item.Add(pp)
            End If
        Next
        'Zones(zone).Add(pp)
    End Sub

    <Extension>
    Public Function GetZoneName(v3 As Vector3) As String
        Return Native.Function.Call(Of String)(Hash.GET_NAME_OF_ZONE, v3.X, v3.Y, v3.Z)
    End Function

End Module
