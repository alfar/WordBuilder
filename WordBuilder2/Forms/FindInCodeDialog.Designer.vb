<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FindInCodeDialog
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.FindTextBox = New System.Windows.Forms.TextBox
        Me.FindLabel = New System.Windows.Forms.Label
        Me.SearchUpCheckBox = New System.Windows.Forms.CheckBox
        Me.WholeWordCheckBox = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 45)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'FindTextBox
        '
        Me.FindTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FindTextBox.Location = New System.Drawing.Point(48, 12)
        Me.FindTextBox.Name = "FindTextBox"
        Me.FindTextBox.Size = New System.Drawing.Size(375, 20)
        Me.FindTextBox.TabIndex = 1
        '
        'FindLabel
        '
        Me.FindLabel.AutoSize = True
        Me.FindLabel.Location = New System.Drawing.Point(12, 15)
        Me.FindLabel.Name = "FindLabel"
        Me.FindLabel.Size = New System.Drawing.Size(30, 13)
        Me.FindLabel.TabIndex = 0
        Me.FindLabel.Text = "Find:"
        '
        'SearchUpCheckBox
        '
        Me.SearchUpCheckBox.AutoSize = True
        Me.SearchUpCheckBox.Location = New System.Drawing.Point(15, 45)
        Me.SearchUpCheckBox.Name = "SearchUpCheckBox"
        Me.SearchUpCheckBox.Size = New System.Drawing.Size(75, 17)
        Me.SearchUpCheckBox.TabIndex = 3
        Me.SearchUpCheckBox.Text = "Search up"
        Me.SearchUpCheckBox.UseVisualStyleBackColor = True
        '
        'WholeWordCheckBox
        '
        Me.WholeWordCheckBox.AutoSize = True
        Me.WholeWordCheckBox.Location = New System.Drawing.Point(96, 45)
        Me.WholeWordCheckBox.Name = "WholeWordCheckBox"
        Me.WholeWordCheckBox.Size = New System.Drawing.Size(83, 17)
        Me.WholeWordCheckBox.TabIndex = 4
        Me.WholeWordCheckBox.Text = "Whole word"
        Me.WholeWordCheckBox.UseVisualStyleBackColor = True
        '
        'FindInCodeDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 86)
        Me.Controls.Add(Me.WholeWordCheckBox)
        Me.Controls.Add(Me.SearchUpCheckBox)
        Me.Controls.Add(Me.FindLabel)
        Me.Controls.Add(Me.FindTextBox)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FindInCodeDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Find in code"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents FindTextBox As System.Windows.Forms.TextBox
    Friend WithEvents FindLabel As System.Windows.Forms.Label
    Friend WithEvents SearchUpCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents WholeWordCheckBox As System.Windows.Forms.CheckBox

End Class
