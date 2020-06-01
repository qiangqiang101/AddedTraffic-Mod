<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Downtown", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Vinewood", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("South Los Santos", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Port of South Los Santos", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup5 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("East Los Santos", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup6 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Vespucci", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup7 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Los Santos", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup8 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Grand Senora Desert", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup9 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("San Chianski Mountain Range", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup10 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Blaine County", System.Windows.Forms.HorizontalAlignment.Left)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbRoadType = New System.Windows.Forms.ComboBox()
        Me.cbRdColor = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbDrivingStyle = New System.Windows.Forms.ComboBox()
        Me.nudSpawnDistance = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.nudCruiseSpeed = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.cbBlip = New System.Windows.Forms.CheckBox()
        Me.cbNotify = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.nudUpgradeChance = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cbVehicleUpgrade = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.nudMidnight = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.nudNight = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.nudEvening = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.nudAfternoon = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.nudMorning = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.lvVehicleSwap = New System.Windows.Forms.ListView()
        Me.chNo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chOldVehicle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chNewVehicle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmsVehicleSwap = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiSwapNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSwapEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiSwapDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.cmsVehicleList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiListNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiListEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiCopyTo = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiDT = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiVW = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSLS = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiPSLS = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiELS = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiVPC = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiLS = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiGSD = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSCMR = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiBC = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiListDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.tlpTheTwoList = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.tlpTheThreeGroups = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lvModelList = New AddedTrafficSettings.ListViewX()
        Me.chListNo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chListModel = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.nudSwapChance = New System.Windows.Forms.NumericUpDown()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.nudSwapDistance = New System.Windows.Forms.NumericUpDown()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nudSpawnDistance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCruiseSpeed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.nudUpgradeChance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.nudMidnight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudNight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudEvening, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudAfternoon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudMorning, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.cmsVehicleSwap.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.cmsVehicleList.SuspendLayout()
        Me.tlpTheTwoList.SuspendLayout()
        Me.tlpTheThreeGroups.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.nudSwapChance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudSwapDistance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cmbRoadType)
        Me.GroupBox1.Controls.Add(Me.cbRdColor)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cmbDrivingStyle)
        Me.GroupBox1.Controls.Add(Me.nudSpawnDistance)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.nudCruiseSpeed)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(526, 100)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "General"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(173, 54)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(61, 15)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Road Type"
        '
        'cmbRoadType
        '
        Me.cmbRoadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRoadType.FormattingEnabled = True
        Me.cmbRoadType.Items.AddRange(New Object() {"AsphaltRoad", "SimplePath", "UnderTheMap", "Water"})
        Me.cmbRoadType.Location = New System.Drawing.Point(252, 51)
        Me.cmbRoadType.Name = "cmbRoadType"
        Me.cmbRoadType.Size = New System.Drawing.Size(163, 23)
        Me.cmbRoadType.TabIndex = 4
        '
        'cbRdColor
        '
        Me.cbRdColor.AutoSize = True
        Me.cbRdColor.Location = New System.Drawing.Point(421, 24)
        Me.cbRdColor.Name = "cbRdColor"
        Me.cbRdColor.Size = New System.Drawing.Size(103, 19)
        Me.cbRdColor.TabIndex = 5
        Me.cbRdColor.Text = "Random Color"
        Me.cbRdColor.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(173, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 15)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Driving Style"
        '
        'cmbDrivingStyle
        '
        Me.cmbDrivingStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDrivingStyle.FormattingEnabled = True
        Me.cmbDrivingStyle.Items.AddRange(New Object() {"Normal", "IgnoreLights", "SometimesOvertakeTraffic", "Rushed", "AvoidTraffic", "AvoidTrafficExtremely"})
        Me.cmbDrivingStyle.Location = New System.Drawing.Point(252, 22)
        Me.cmbDrivingStyle.Name = "cmbDrivingStyle"
        Me.cmbDrivingStyle.Size = New System.Drawing.Size(163, 23)
        Me.cmbDrivingStyle.TabIndex = 3
        '
        'nudSpawnDistance
        '
        Me.nudSpawnDistance.DecimalPlaces = 2
        Me.nudSpawnDistance.Location = New System.Drawing.Point(102, 51)
        Me.nudSpawnDistance.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudSpawnDistance.Name = "nudSpawnDistance"
        Me.nudSpawnDistance.Size = New System.Drawing.Size(65, 23)
        Me.nudSpawnDistance.TabIndex = 2
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 54)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 15)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Spawn Distance"
        '
        'nudCruiseSpeed
        '
        Me.nudCruiseSpeed.DecimalPlaces = 2
        Me.nudCruiseSpeed.Location = New System.Drawing.Point(102, 22)
        Me.nudCruiseSpeed.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudCruiseSpeed.Name = "nudCruiseSpeed"
        Me.nudCruiseSpeed.Size = New System.Drawing.Size(65, 23)
        Me.nudCruiseSpeed.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 15)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Cruise Speed"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.cbBlip)
        Me.GroupBox5.Controls.Add(Me.cbNotify)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox5.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(89, 100)
        Me.GroupBox5.TabIndex = 3
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Debug"
        '
        'cbBlip
        '
        Me.cbBlip.AutoSize = True
        Me.cbBlip.Location = New System.Drawing.Point(6, 47)
        Me.cbBlip.Name = "cbBlip"
        Me.cbBlip.Size = New System.Drawing.Size(78, 19)
        Me.cbBlip.TabIndex = 2
        Me.cbBlip.Text = "Show Blip"
        Me.cbBlip.UseVisualStyleBackColor = True
        '
        'cbNotify
        '
        Me.cbNotify.AutoSize = True
        Me.cbNotify.Location = New System.Drawing.Point(6, 22)
        Me.cbNotify.Name = "cbNotify"
        Me.cbNotify.Size = New System.Drawing.Size(59, 19)
        Me.cbNotify.TabIndex = 1
        Me.cbNotify.Text = "Notify"
        Me.cbNotify.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.nudUpgradeChance)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.cbVehicleUpgrade)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(153, 100)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Vehicle Upgrade"
        '
        'nudUpgradeChance
        '
        Me.nudUpgradeChance.Location = New System.Drawing.Point(80, 46)
        Me.nudUpgradeChance.Name = "nudUpgradeChance"
        Me.nudUpgradeChance.Size = New System.Drawing.Size(65, 23)
        Me.nudUpgradeChance.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 49)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(68, 15)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "Chance (%)"
        '
        'cbVehicleUpgrade
        '
        Me.cbVehicleUpgrade.AutoSize = True
        Me.cbVehicleUpgrade.Location = New System.Drawing.Point(6, 22)
        Me.cbVehicleUpgrade.Name = "cbVehicleUpgrade"
        Me.cbVehicleUpgrade.Size = New System.Drawing.Size(61, 19)
        Me.cbVehicleUpgrade.TabIndex = 1
        Me.cbVehicleUpgrade.Text = "Enable"
        Me.cbVehicleUpgrade.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.nudMidnight)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.nudNight)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.nudEvening)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.nudAfternoon)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.nudMorning)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(780, 53)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Wait Time (seconds)"
        '
        'nudMidnight
        '
        Me.nudMidnight.Location = New System.Drawing.Point(576, 22)
        Me.nudMidnight.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudMidnight.Name = "nudMidnight"
        Me.nudMidnight.Size = New System.Drawing.Size(65, 23)
        Me.nudMidnight.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(514, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 15)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Midnight"
        '
        'nudNight
        '
        Me.nudNight.Location = New System.Drawing.Point(443, 22)
        Me.nudNight.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudNight.Name = "nudNight"
        Me.nudNight.Size = New System.Drawing.Size(65, 23)
        Me.nudNight.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(400, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Night"
        '
        'nudEvening
        '
        Me.nudEvening.Location = New System.Drawing.Point(329, 22)
        Me.nudEvening.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudEvening.Name = "nudEvening"
        Me.nudEvening.Size = New System.Drawing.Size(65, 23)
        Me.nudEvening.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(274, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Evening"
        '
        'nudAfternoon
        '
        Me.nudAfternoon.Location = New System.Drawing.Point(203, 22)
        Me.nudAfternoon.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudAfternoon.Name = "nudAfternoon"
        Me.nudAfternoon.Size = New System.Drawing.Size(65, 23)
        Me.nudAfternoon.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(136, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Afternoon"
        '
        'nudMorning
        '
        Me.nudMorning.Location = New System.Drawing.Point(65, 22)
        Me.nudMorning.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudMorning.Name = "nudMorning"
        Me.nudMorning.Size = New System.Drawing.Size(65, 23)
        Me.nudMorning.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Morning"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.nudSwapDistance)
        Me.GroupBox6.Controls.Add(Me.Label12)
        Me.GroupBox6.Controls.Add(Me.nudSwapChance)
        Me.GroupBox6.Controls.Add(Me.Label11)
        Me.GroupBox6.Controls.Add(Me.lvVehicleSwap)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox6.Location = New System.Drawing.Point(2, 2)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox6.Size = New System.Drawing.Size(506, 336)
        Me.GroupBox6.TabIndex = 1
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Model Swap (If New Model is blank, it will take random model from Model List)"
        '
        'lvVehicleSwap
        '
        Me.lvVehicleSwap.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvVehicleSwap.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chNo, Me.chOldVehicle, Me.chNewVehicle})
        Me.lvVehicleSwap.ContextMenuStrip = Me.cmsVehicleSwap
        Me.lvVehicleSwap.FullRowSelect = True
        Me.lvVehicleSwap.Location = New System.Drawing.Point(3, 19)
        Me.lvVehicleSwap.Name = "lvVehicleSwap"
        Me.lvVehicleSwap.Size = New System.Drawing.Size(500, 285)
        Me.lvVehicleSwap.TabIndex = 1
        Me.lvVehicleSwap.UseCompatibleStateImageBehavior = False
        Me.lvVehicleSwap.View = System.Windows.Forms.View.Details
        '
        'chNo
        '
        Me.chNo.Text = ""
        Me.chNo.Width = 0
        '
        'chOldVehicle
        '
        Me.chOldVehicle.Text = "Old Model"
        Me.chOldVehicle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.chOldVehicle.Width = 200
        '
        'chNewVehicle
        '
        Me.chNewVehicle.Text = "New Model"
        Me.chNewVehicle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.chNewVehicle.Width = 200
        '
        'cmsVehicleSwap
        '
        Me.cmsVehicleSwap.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiSwapNew, Me.tsmiSwapEdit, Me.ToolStripSeparator1, Me.tsmiSwapDelete})
        Me.cmsVehicleSwap.Name = "cmsVehicleSwap"
        Me.cmsVehicleSwap.Size = New System.Drawing.Size(108, 76)
        '
        'tsmiSwapNew
        '
        Me.tsmiSwapNew.Name = "tsmiSwapNew"
        Me.tsmiSwapNew.Size = New System.Drawing.Size(107, 22)
        Me.tsmiSwapNew.Text = "New"
        '
        'tsmiSwapEdit
        '
        Me.tsmiSwapEdit.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsmiSwapEdit.Name = "tsmiSwapEdit"
        Me.tsmiSwapEdit.Size = New System.Drawing.Size(107, 22)
        Me.tsmiSwapEdit.Text = "Edit"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(104, 6)
        '
        'tsmiSwapDelete
        '
        Me.tsmiSwapDelete.Name = "tsmiSwapDelete"
        Me.tsmiSwapDelete.Size = New System.Drawing.Size(107, 22)
        Me.tsmiSwapDelete.Text = "Delete"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.lvModelList)
        Me.GroupBox7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox7.Location = New System.Drawing.Point(512, 2)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox7.Size = New System.Drawing.Size(272, 336)
        Me.GroupBox7.TabIndex = 1
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Model List"
        '
        'cmsVehicleList
        '
        Me.cmsVehicleList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiListNew, Me.tsmiListEdit, Me.ToolStripSeparator2, Me.tsmiCopyTo, Me.ToolStripSeparator3, Me.tsmiListDelete})
        Me.cmsVehicleList.Name = "cmsVehicleSwap"
        Me.cmsVehicleList.Size = New System.Drawing.Size(118, 104)
        '
        'tsmiListNew
        '
        Me.tsmiListNew.Name = "tsmiListNew"
        Me.tsmiListNew.Size = New System.Drawing.Size(117, 22)
        Me.tsmiListNew.Text = "New"
        '
        'tsmiListEdit
        '
        Me.tsmiListEdit.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsmiListEdit.Name = "tsmiListEdit"
        Me.tsmiListEdit.Size = New System.Drawing.Size(117, 22)
        Me.tsmiListEdit.Text = "Edit"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(114, 6)
        '
        'tsmiCopyTo
        '
        Me.tsmiCopyTo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiDT, Me.tsmiVW, Me.tsmiSLS, Me.tsmiPSLS, Me.tsmiELS, Me.tsmiVPC, Me.tsmiLS, Me.tsmiGSD, Me.tsmiSCMR, Me.tsmiBC})
        Me.tsmiCopyTo.Name = "tsmiCopyTo"
        Me.tsmiCopyTo.Size = New System.Drawing.Size(117, 22)
        Me.tsmiCopyTo.Text = "Copy To"
        '
        'tsmiDT
        '
        Me.tsmiDT.Name = "tsmiDT"
        Me.tsmiDT.Size = New System.Drawing.Size(232, 22)
        Me.tsmiDT.Text = "Downtown"
        '
        'tsmiVW
        '
        Me.tsmiVW.Name = "tsmiVW"
        Me.tsmiVW.Size = New System.Drawing.Size(232, 22)
        Me.tsmiVW.Text = "Vinewood"
        '
        'tsmiSLS
        '
        Me.tsmiSLS.Name = "tsmiSLS"
        Me.tsmiSLS.Size = New System.Drawing.Size(232, 22)
        Me.tsmiSLS.Text = "South Los Santos"
        '
        'tsmiPSLS
        '
        Me.tsmiPSLS.Name = "tsmiPSLS"
        Me.tsmiPSLS.Size = New System.Drawing.Size(232, 22)
        Me.tsmiPSLS.Text = "Port of South Los Santos"
        '
        'tsmiELS
        '
        Me.tsmiELS.Name = "tsmiELS"
        Me.tsmiELS.Size = New System.Drawing.Size(232, 22)
        Me.tsmiELS.Text = "East Los Santos"
        '
        'tsmiVPC
        '
        Me.tsmiVPC.Name = "tsmiVPC"
        Me.tsmiVPC.Size = New System.Drawing.Size(232, 22)
        Me.tsmiVPC.Text = "Vespucci"
        '
        'tsmiLS
        '
        Me.tsmiLS.Name = "tsmiLS"
        Me.tsmiLS.Size = New System.Drawing.Size(232, 22)
        Me.tsmiLS.Text = "Los Santos"
        '
        'tsmiGSD
        '
        Me.tsmiGSD.Name = "tsmiGSD"
        Me.tsmiGSD.Size = New System.Drawing.Size(232, 22)
        Me.tsmiGSD.Text = "Grand Senora Desert"
        '
        'tsmiSCMR
        '
        Me.tsmiSCMR.Name = "tsmiSCMR"
        Me.tsmiSCMR.Size = New System.Drawing.Size(232, 22)
        Me.tsmiSCMR.Text = "San Chianski Mountain Range"
        '
        'tsmiBC
        '
        Me.tsmiBC.Name = "tsmiBC"
        Me.tsmiBC.Size = New System.Drawing.Size(232, 22)
        Me.tsmiBC.Text = "Blaine County"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(114, 6)
        '
        'tsmiListDelete
        '
        Me.tsmiListDelete.Name = "tsmiListDelete"
        Me.tsmiListDelete.Size = New System.Drawing.Size(117, 22)
        Me.tsmiListDelete.Text = "Delete"
        '
        'tlpTheTwoList
        '
        Me.tlpTheTwoList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpTheTwoList.ColumnCount = 2
        Me.tlpTheTwoList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.0!))
        Me.tlpTheTwoList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.tlpTheTwoList.Controls.Add(Me.GroupBox6, 0, 0)
        Me.tlpTheTwoList.Controls.Add(Me.GroupBox7, 1, 0)
        Me.tlpTheTwoList.Location = New System.Drawing.Point(9, 183)
        Me.tlpTheTwoList.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpTheTwoList.Name = "tlpTheTwoList"
        Me.tlpTheTwoList.RowCount = 1
        Me.tlpTheTwoList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTheTwoList.Size = New System.Drawing.Size(786, 340)
        Me.tlpTheTwoList.TabIndex = 5
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(717, 526)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'tlpTheThreeGroups
        '
        Me.tlpTheThreeGroups.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpTheThreeGroups.ColumnCount = 3
        Me.tlpTheThreeGroups.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.0!))
        Me.tlpTheThreeGroups.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        Me.tlpTheThreeGroups.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.tlpTheThreeGroups.Controls.Add(Me.Panel1, 0, 0)
        Me.tlpTheThreeGroups.Controls.Add(Me.Panel2, 1, 0)
        Me.tlpTheThreeGroups.Controls.Add(Me.Panel3, 2, 0)
        Me.tlpTheThreeGroups.Location = New System.Drawing.Point(9, 71)
        Me.tlpTheThreeGroups.Name = "tlpTheThreeGroups"
        Me.tlpTheThreeGroups.Padding = New System.Windows.Forms.Padding(3)
        Me.tlpTheThreeGroups.RowCount = 1
        Me.tlpTheThreeGroups.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpTheThreeGroups.Size = New System.Drawing.Size(786, 109)
        Me.tlpTheThreeGroups.TabIndex = 7
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(526, 100)
        Me.Panel1.TabIndex = 5
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox5)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(535, 5)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(89, 100)
        Me.Panel2.TabIndex = 6
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.GroupBox4)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(628, 5)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(153, 100)
        Me.Panel3.TabIndex = 7
        '
        'lvModelList
        '
        Me.lvModelList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvModelList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chListNo, Me.chListModel})
        Me.lvModelList.ContextMenuStrip = Me.cmsVehicleList
        Me.lvModelList.FullRowSelect = True
        ListViewGroup1.Header = "Downtown"
        ListViewGroup1.Name = "Downtown"
        ListViewGroup2.Header = "Vinewood"
        ListViewGroup2.Name = "Vinewood"
        ListViewGroup3.Header = "South Los Santos"
        ListViewGroup3.Name = "South Los Santos"
        ListViewGroup4.Header = "Port of South Los Santos"
        ListViewGroup4.Name = "Port of South Los Santos"
        ListViewGroup5.Header = "East Los Santos"
        ListViewGroup5.Name = "East Los Santos"
        ListViewGroup6.Header = "Vespucci"
        ListViewGroup6.Name = "Vespucci"
        ListViewGroup7.Header = "Los Santos"
        ListViewGroup7.Name = "Los Santos"
        ListViewGroup8.Header = "Grand Senora Desert"
        ListViewGroup8.Name = "Grand Senora Desert"
        ListViewGroup9.Header = "San Chianski Mountain Range"
        ListViewGroup9.Name = "San Chianski Mountain Range"
        ListViewGroup10.Header = "Blaine County"
        ListViewGroup10.Name = "Blaine County"
        Me.lvModelList.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3, ListViewGroup4, ListViewGroup5, ListViewGroup6, ListViewGroup7, ListViewGroup8, ListViewGroup9, ListViewGroup10})
        Me.lvModelList.Location = New System.Drawing.Point(3, 19)
        Me.lvModelList.Name = "lvModelList"
        Me.lvModelList.Size = New System.Drawing.Size(266, 314)
        Me.lvModelList.TabIndex = 1
        Me.lvModelList.UseCompatibleStateImageBehavior = False
        Me.lvModelList.View = System.Windows.Forms.View.Details
        '
        'chListNo
        '
        Me.chListNo.Text = ""
        Me.chListNo.Width = 0
        '
        'chListModel
        '
        Me.chListModel.Text = "Model"
        Me.chListModel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.chListModel.Width = 200
        '
        'nudSwapChance
        '
        Me.nudSwapChance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.nudSwapChance.Location = New System.Drawing.Point(108, 310)
        Me.nudSwapChance.Name = "nudSwapChance"
        Me.nudSwapChance.Size = New System.Drawing.Size(65, 23)
        Me.nudSwapChance.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 312)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(99, 15)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "Swap Chance (%)"
        '
        'nudSwapDistance
        '
        Me.nudSwapDistance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.nudSwapDistance.DecimalPlaces = 2
        Me.nudSwapDistance.Location = New System.Drawing.Point(268, 310)
        Me.nudSwapDistance.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudSwapDistance.Name = "nudSwapDistance"
        Me.nudSwapDistance.Size = New System.Drawing.Size(65, 23)
        Me.nudSwapDistance.TabIndex = 3
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(179, 312)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(83, 15)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "Swap Distance"
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(804, 561)
        Me.Controls.Add(Me.tlpTheThreeGroups)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.tlpTheTwoList)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MinimumSize = New System.Drawing.Size(820, 600)
        Me.Name = "frmSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Added Traffic GUI Settings by I'm Not MentaL"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nudSpawnDistance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCruiseSpeed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.nudUpgradeChance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.nudMidnight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudNight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudEvening, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudAfternoon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudMorning, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.cmsVehicleSwap.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.cmsVehicleList.ResumeLayout(False)
        Me.tlpTheTwoList.ResumeLayout(False)
        Me.tlpTheThreeGroups.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.nudSwapChance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudSwapDistance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents nudMorning As NumericUpDown
    Friend WithEvents Label1 As Label
    Friend WithEvents nudMidnight As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents nudNight As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents nudEvening As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents nudAfternoon As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents cmbRoadType As ComboBox
    Friend WithEvents cbRdColor As CheckBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents cbBlip As CheckBox
    Friend WithEvents cbNotify As CheckBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents nudUpgradeChance As NumericUpDown
    Friend WithEvents Label9 As Label
    Friend WithEvents cbVehicleUpgrade As CheckBox
    Friend WithEvents Label8 As Label
    Friend WithEvents cmbDrivingStyle As ComboBox
    Friend WithEvents nudSpawnDistance As NumericUpDown
    Friend WithEvents Label7 As Label
    Friend WithEvents nudCruiseSpeed As NumericUpDown
    Friend WithEvents Label6 As Label
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents lvVehicleSwap As ListView
    Friend WithEvents chNo As ColumnHeader
    Friend WithEvents chOldVehicle As ColumnHeader
    Friend WithEvents chNewVehicle As ColumnHeader
    Friend WithEvents cmsVehicleSwap As ContextMenuStrip
    Friend WithEvents tsmiSwapNew As ToolStripMenuItem
    Friend WithEvents tsmiSwapEdit As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsmiSwapDelete As ToolStripMenuItem
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents tlpTheTwoList As TableLayoutPanel
    Friend WithEvents cmsVehicleList As ContextMenuStrip
    Friend WithEvents tsmiListNew As ToolStripMenuItem
    Friend WithEvents tsmiListEdit As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents tsmiListDelete As ToolStripMenuItem
    Friend WithEvents tsmiCopyTo As ToolStripMenuItem
    Friend WithEvents tsmiDT As ToolStripMenuItem
    Friend WithEvents tsmiVW As ToolStripMenuItem
    Friend WithEvents tsmiSLS As ToolStripMenuItem
    Friend WithEvents tsmiPSLS As ToolStripMenuItem
    Friend WithEvents tsmiELS As ToolStripMenuItem
    Friend WithEvents tsmiVPC As ToolStripMenuItem
    Friend WithEvents tsmiLS As ToolStripMenuItem
    Friend WithEvents tsmiGSD As ToolStripMenuItem
    Friend WithEvents tsmiSCMR As ToolStripMenuItem
    Friend WithEvents tsmiBC As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents lvModelList As ListViewX
    Friend WithEvents chListNo As ColumnHeader
    Friend WithEvents chListModel As ColumnHeader
    Friend WithEvents btnSave As Button
    Friend WithEvents tlpTheThreeGroups As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents nudSwapDistance As NumericUpDown
    Friend WithEvents Label12 As Label
    Friend WithEvents nudSwapChance As NumericUpDown
    Friend WithEvents Label11 As Label
End Class
