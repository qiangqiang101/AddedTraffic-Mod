<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSwap
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtOldVeh = New System.Windows.Forms.TextBox()
        Me.txtNewVeh = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnAction = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(44, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Old Model"
        '
        'txtOldVeh
        '
        Me.txtOldVeh.Location = New System.Drawing.Point(118, 18)
        Me.txtOldVeh.Name = "txtOldVeh"
        Me.txtOldVeh.Size = New System.Drawing.Size(159, 23)
        Me.txtOldVeh.TabIndex = 0
        '
        'txtNewVeh
        '
        Me.txtNewVeh.Location = New System.Drawing.Point(118, 47)
        Me.txtNewVeh.Name = "txtNewVeh"
        Me.txtNewVeh.Size = New System.Drawing.Size(159, 23)
        Me.txtNewVeh.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(44, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "New Model"
        '
        'btnAction
        '
        Me.btnAction.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAction.Location = New System.Drawing.Point(98, 76)
        Me.btnAction.Name = "btnAction"
        Me.btnAction.Size = New System.Drawing.Size(75, 23)
        Me.btnAction.TabIndex = 2
        Me.btnAction.Text = "Add"
        Me.btnAction.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(179, 76)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmSwap
        '
        Me.AcceptButton = Me.btnAction
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(354, 111)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAction)
        Me.Controls.Add(Me.txtNewVeh)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtOldVeh)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSwap"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Model Swap"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtOldVeh As TextBox
    Friend WithEvents txtNewVeh As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnAction As Button
    Friend WithEvents btnCancel As Button
End Class
