<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GenerateForm
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
        Me.EntryPanel = New System.Windows.Forms.Panel
        Me.RulesTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.ClearListCheckBox = New System.Windows.Forms.CheckBox
        Me.MyOkButton = New System.Windows.Forms.Button
        Me.MyCancelButton = New System.Windows.Forms.Button
        Me.EntryPanel.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'EntryPanel
        '
        Me.EntryPanel.AutoScroll = True
        Me.EntryPanel.BackColor = System.Drawing.SystemColors.Control
        Me.EntryPanel.Controls.Add(Me.RulesTableLayoutPanel)
        Me.EntryPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EntryPanel.Location = New System.Drawing.Point(0, 0)
        Me.EntryPanel.Name = "EntryPanel"
        Me.EntryPanel.Size = New System.Drawing.Size(395, 203)
        Me.EntryPanel.TabIndex = 0
        '
        'RulesTableLayoutPanel
        '
        Me.RulesTableLayoutPanel.AutoSize = True
        Me.RulesTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.RulesTableLayoutPanel.ColumnCount = 2
        Me.RulesTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.RulesTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.RulesTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.RulesTableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.RulesTableLayoutPanel.Name = "RulesTableLayoutPanel"
        Me.RulesTableLayoutPanel.RowCount = 2
        Me.RulesTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.RulesTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.RulesTableLayoutPanel.Size = New System.Drawing.Size(395, 0)
        Me.RulesTableLayoutPanel.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.ClearListCheckBox)
        Me.Panel2.Controls.Add(Me.MyOkButton)
        Me.Panel2.Controls.Add(Me.MyCancelButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 203)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(5)
        Me.Panel2.Size = New System.Drawing.Size(395, 36)
        Me.Panel2.TabIndex = 2
        '
        'ClearListCheckBox
        '
        Me.ClearListCheckBox.AutoSize = True
        Me.ClearListCheckBox.Dock = System.Windows.Forms.DockStyle.Left
        Me.ClearListCheckBox.Location = New System.Drawing.Point(5, 5)
        Me.ClearListCheckBox.Name = "ClearListCheckBox"
        Me.ClearListCheckBox.Size = New System.Drawing.Size(65, 26)
        Me.ClearListCheckBox.TabIndex = 2
        Me.ClearListCheckBox.Text = "Clear list"
        Me.ClearListCheckBox.UseVisualStyleBackColor = True
        '
        'MyOkButton
        '
        Me.MyOkButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.MyOkButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.MyOkButton.Location = New System.Drawing.Point(240, 5)
        Me.MyOkButton.Name = "MyOkButton"
        Me.MyOkButton.Size = New System.Drawing.Size(75, 26)
        Me.MyOkButton.TabIndex = 1
        Me.MyOkButton.Text = "&Generate"
        Me.MyOkButton.UseVisualStyleBackColor = True
        '
        'MyCancelButton
        '
        Me.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.MyCancelButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.MyCancelButton.Location = New System.Drawing.Point(315, 5)
        Me.MyCancelButton.Name = "MyCancelButton"
        Me.MyCancelButton.Size = New System.Drawing.Size(75, 26)
        Me.MyCancelButton.TabIndex = 0
        Me.MyCancelButton.Text = "&Cancel"
        Me.MyCancelButton.UseVisualStyleBackColor = True
        '
        'GenerateForm
        '
        Me.AcceptButton = Me.MyOkButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.MyCancelButton
        Me.ClientSize = New System.Drawing.Size(395, 239)
        Me.Controls.Add(Me.EntryPanel)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "GenerateForm"
        Me.Text = "Generate"
        Me.EntryPanel.ResumeLayout(False)
        Me.EntryPanel.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents EntryPanel As System.Windows.Forms.Panel
    Friend WithEvents RulesTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents MyCancelButton As System.Windows.Forms.Button
    Friend WithEvents MyOkButton As System.Windows.Forms.Button
    Friend WithEvents ClearListCheckBox As System.Windows.Forms.CheckBox
End Class
