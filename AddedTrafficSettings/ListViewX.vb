Imports System.ComponentModel
Imports System.Reflection
Imports System.Runtime.InteropServices

Public Class ListViewX
    Inherits ListView

    Private Const LVM_FIRST As Integer = &H1000
    Private Const LVM_SETGROUPINFO As Integer = (LVM_FIRST + 147)
    Private Const WM_LBUTTONUP As Integer = &H202
    Private Delegate Sub CallBackSetGroupState(ByVal lstvwgrp As ListViewGroup, ByVal state As ListViewGroupState)
    Private Delegate Sub CallbackSetGroupString(ByVal lstvwgrp As ListViewGroup, ByVal value As String)

    <DllImport("User32.dll"), Description("Sends the specified message to a window or windows. The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message. To send a message and return immediately, use the SendMessageCallback or SendNotifyMessage function. To post a message to a thread's message queue and return immediately, use the PostMessage or PostThreadMessage function.")>
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As LVGROUP) As Integer
    End Function

    Private Shared Function GetGroupID(ByVal lstvwgrp As ListViewGroup) As Integer?
        Dim rtnval As Integer? = Nothing
        Dim GrpTp As Type = lstvwgrp.[GetType]()

        If GrpTp IsNot Nothing Then
            Dim pi As PropertyInfo = GrpTp.GetProperty("ID", BindingFlags.NonPublic Or BindingFlags.Instance)

            If pi IsNot Nothing Then
                Dim tmprtnval As Object = pi.GetValue(lstvwgrp, Nothing)

                If tmprtnval IsNot Nothing Then
                    rtnval = CType(tmprtnval, Integer?)
                End If
            End If
        End If

        Return rtnval
    End Function

    Private Shared Sub setGrpState(ByVal lstvwgrp As ListViewGroup, ByVal state As ListViewGroupState)
        If Environment.OSVersion.Version.Major < 6 Then Return
        If lstvwgrp Is Nothing OrElse lstvwgrp.ListView Is Nothing Then Return

        If lstvwgrp.ListView.InvokeRequired Then
            lstvwgrp.ListView.Invoke(New CallBackSetGroupState(AddressOf setGrpState), lstvwgrp, state)
        Else
            Dim GrpId As Integer? = GetGroupID(lstvwgrp)
            Dim gIndex As Integer = lstvwgrp.ListView.Groups.IndexOf(lstvwgrp)
            Dim group As LVGROUP = New LVGROUP()
            group.CbSize = Marshal.SizeOf(group)
            group.State = state
            group.Mask = ListViewGroupMask.State

            If GrpId IsNot Nothing Then
                group.IGroupId = GrpId.Value
                SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, GrpId.Value, group)
                SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, GrpId.Value, group)
            Else
                group.IGroupId = gIndex
                SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, gIndex, group)
                SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, gIndex, group)
            End If

            lstvwgrp.ListView.Refresh()
        End If
    End Sub

    Private Shared Sub setGrpFooter(ByVal lstvwgrp As ListViewGroup, ByVal footer As String)
        If Environment.OSVersion.Version.Major < 6 Then Return
        If lstvwgrp Is Nothing OrElse lstvwgrp.ListView Is Nothing Then Return

        If lstvwgrp.ListView.InvokeRequired Then
            lstvwgrp.ListView.Invoke(New CallbackSetGroupString(AddressOf setGrpFooter), lstvwgrp, footer)
        Else
            Dim GrpId As Integer? = GetGroupID(lstvwgrp)
            Dim gIndex As Integer = lstvwgrp.ListView.Groups.IndexOf(lstvwgrp)
            Dim group As LVGROUP = New LVGROUP()
            group.CbSize = Marshal.SizeOf(group)
            group.PszFooter = footer
            group.Mask = ListViewGroupMask.Footer

            If GrpId IsNot Nothing Then
                group.IGroupId = GrpId.Value
                SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, GrpId.Value, group)
            Else
                group.IGroupId = gIndex
                SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, gIndex, group)
            End If
        End If
    End Sub

    Public Sub SetGroupState(ByVal state As ListViewGroupState)
        For Each lvg As ListViewGroup In Me.Groups
            setGrpState(lvg, state)
        Next
    End Sub

    Public Sub SetGroupFooter(ByVal lvg As ListViewGroup, ByVal footerText As String)
        setGrpFooter(lvg, footerText)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_LBUTTONUP Then MyBase.DefWndProc(m)
        MyBase.WndProc(m)
    End Sub

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode), Description("LVGROUP StructureUsed to set and retrieve groups.")>
    Public Structure LVGROUP
        <Description("Size of this structure, in bytes.")>
        Public CbSize As Integer
        <Description("Mask that specifies which members of the structure are valid input. One or more of the following values:LVGF_NONE No other items are valid.")>
        Public Mask As ListViewGroupMask
        <Description("Pointer to a null-terminated string that contains the header text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the header text.")>
        <MarshalAs(UnmanagedType.LPWStr)>
        Public PszHeader As String
        <Description("Size in TCHARs of the buffer pointed to by the pszHeader member. If the structure is not receiving information about a group, this member is ignored.")>
        Public CchHeader As Integer
        <Description("Pointer to a null-terminated string that contains the footer text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the footer text.")>
        <MarshalAs(UnmanagedType.LPWStr)>
        Public PszFooter As String
        <Description("Size in TCHARs of the buffer pointed to by the pszFooter member. If the structure is not receiving information about a group, this member is ignored.")>
        Public CchFooter As Integer
        <Description("ID of the group.")>
        Public IGroupId As Integer
        <Description("Mask used with LVM_GETGROUPINFO (Microsoft Windows XP and Windows Vista) and LVM_SETGROUPINFO (Windows Vista only) to specify which flags in the state value are being retrieved or set.")>
        Public StateMask As Integer
        <Description("Flag that can have one of the following values:LVGS_NORMAL Groups are expanded, the group name is displayed, and all items in the group are displayed.")>
        Public State As ListViewGroupState
        <Description("Indicates the alignment of the header or footer text for the group. It can have one or more of the following values. Use one of the header flags. Footer flags are optional. Windows XP: Footer flags are reserved.LVGA_FOOTER_CENTERReserved.")>
        Public UAlign As UInteger
        <Description("Windows Vista. Pointer to a null-terminated string that contains the subtitle text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the subtitle text. This element is drawn under the header text.")>
        Public PszSubtitle As IntPtr
        <Description("Windows Vista. Size, in TCHARs, of the buffer pointed to by the pszSubtitle member. If the structure is not receiving information about a group, this member is ignored.")>
        Public CchSubtitle As UInteger
        <Description("Windows Vista. Pointer to a null-terminated string that contains the text for a task link when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the task text. This item is drawn right-aligned opposite the header text. When clicked by the user, the task link generates an LVN_LINKCLICK notification.")>
        <MarshalAs(UnmanagedType.LPWStr)>
        Public PszTask As String
        <Description("Windows Vista. Size in TCHARs of the buffer pointed to by the pszTask member. If the structure is not receiving information about a group, this member is ignored.")>
        Public CchTask As UInteger
        <Description("Windows Vista. Pointer to a null-terminated string that contains the top description text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the top description text. This item is drawn opposite the title image when there is a title image, no extended image, and uAlign==LVGA_HEADER_CENTER.")>
        <MarshalAs(UnmanagedType.LPWStr)>
        Public PszDescriptionTop As String
        <Description("Windows Vista. Size in TCHARs of the buffer pointed to by the pszDescriptionTop member. If the structure is not receiving information about a group, this member is ignored.")>
        Public CchDescriptionTop As UInteger
        <Description("Windows Vista. Pointer to a null-terminated string that contains the bottom description text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the bottom description text. This item is drawn under the top description text when there is a title image, no extended image, and uAlign==LVGA_HEADER_CENTER.")>
        <MarshalAs(UnmanagedType.LPWStr)>
        Public PszDescriptionBottom As String
        <Description("Windows Vista. Size in TCHARs of the buffer pointed to by the pszDescriptionBottom member. If the structure is not receiving information about a group, this member is ignored.")>
        Public CchDescriptionBottom As UInteger
        <Description("Windows Vista. Index of the title image in the control imagelist.")>
        Public ITitleImage As Integer
        <Description("Windows Vista. Index of the extended image in the control imagelist.")>
        Public IExtendedImage As Integer
        <Description("Windows Vista. Read-only.")>
        Public IFirstItem As Integer
        <Description("Windows Vista. Read-only in non-owner data mode.")>
        Public CItems As IntPtr
        <Description("Windows Vista. NULL if group is not a subset. Pointer to a null-terminated string that contains the subset title text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the subset title text.")>
        Public PszSubsetTitle As IntPtr
        <Description("Windows Vista. Size in TCHARs of the buffer pointed to by the pszSubsetTitle member. If the structure is not receiving information about a group, this member is ignored.")>
        Public CchSubsetTitle As IntPtr
    End Structure

    Public Enum ListViewGroupMask
        None = &H0
        Header = &H1
        Footer = &H2
        State = &H4
        Align = &H8
        GroupId = &H10
        SubTitle = &H100
        Task = &H200
        DescriptionTop = &H400
        DescriptionBottom = &H800
        TitleImage = &H1000
        ExtendedImage = &H2000
        Items = &H4000
        Subset = &H8000
        SubsetItems = &H10000
    End Enum

    Public Enum ListViewGroupState
        Normal = 0
        Collapsed = 1
        Hidden = 2
        NoHeader = 4
        Collapsible = 8
        Focused = 16
        Selected = 32
        SubSeted = 64
        SubSetLinkFocused = 128
    End Enum
End Class
