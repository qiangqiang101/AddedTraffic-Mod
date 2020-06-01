Imports System.Runtime.CompilerServices

Module Helper

    <Extension>
    Public Sub Striped(listview As ListView, Optional color1 As Color = Nothing, Optional color2 As Color = Nothing)
        If color2 = Nothing Then color2 = SystemColors.ButtonFace
        If color1 = Nothing Then color1 = SystemColors.Window

        Dim alternator As Integer = 0
        For Each lvi As ListViewItem In listview.Items
            If lvi.Group Is Nothing Then
                If alternator Mod 2 = 0 Then
                    For i As Integer = 0 To lvi.SubItems.Count - 1
                        If Not lvi.SubItems(i).BackColor = Color.LightSalmon Then lvi.SubItems(i).BackColor = color1
                    Next
                Else
                    For i As Integer = 0 To lvi.SubItems.Count - 1
                        If Not lvi.SubItems(i).BackColor = Color.LightSalmon Then lvi.SubItems(i).BackColor = color2
                    Next
                End If
                alternator += 1
            End If
        Next
        For Each gp As ListViewGroup In listview.Groups
            For Each lvi As ListViewItem In gp.Items
                If alternator Mod 2 = 0 Then
                    For i As Integer = 0 To lvi.SubItems.Count - 1
                        If Not lvi.SubItems(i).BackColor = Color.LightSalmon Then lvi.SubItems(i).BackColor = color1
                    Next
                Else
                    For i As Integer = 0 To lvi.SubItems.Count - 1
                        If Not lvi.SubItems(i).BackColor = Color.LightSalmon Then lvi.SubItems(i).BackColor = color2
                    Next
                End If
                alternator += 1
            Next
        Next
    End Sub

End Module
