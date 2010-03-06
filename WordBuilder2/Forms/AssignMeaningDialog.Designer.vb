<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AssignMeaningDialog
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
        Me.WordGroupBox = New System.Windows.Forms.GroupBox
        Me.WordTextBox = New System.Windows.Forms.TextBox
        Me.MeaningGroupBox = New System.Windows.Forms.GroupBox
        Me.MeaningTextBox = New System.Windows.Forms.TextBox
        Me.SynonymsWebBrowser = New System.Windows.Forms.WebBrowser
        Me.TableLayoutPanel1.SuspendLayout()
        Me.WordGroupBox.SuspendLayout()
        Me.MeaningGroupBox.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(276, 322)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
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
        'WordGroupBox
        '
        Me.WordGroupBox.Controls.Add(Me.WordTextBox)
        Me.WordGroupBox.Location = New System.Drawing.Point(12, 12)
        Me.WordGroupBox.Name = "WordGroupBox"
        Me.WordGroupBox.Size = New System.Drawing.Size(410, 134)
        Me.WordGroupBox.TabIndex = 1
        Me.WordGroupBox.TabStop = False
        Me.WordGroupBox.Text = "Word"
        '
        'WordTextBox
        '
        Me.WordTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WordTextBox.Location = New System.Drawing.Point(6, 19)
        Me.WordTextBox.Multiline = True
        Me.WordTextBox.Name = "WordTextBox"
        Me.WordTextBox.ReadOnly = True
        Me.WordTextBox.Size = New System.Drawing.Size(398, 109)
        Me.WordTextBox.TabIndex = 0
        '
        'MeaningGroupBox
        '
        Me.MeaningGroupBox.Controls.Add(Me.SynonymsWebBrowser)
        Me.MeaningGroupBox.Controls.Add(Me.MeaningTextBox)
        Me.MeaningGroupBox.Location = New System.Drawing.Point(11, 152)
        Me.MeaningGroupBox.Name = "MeaningGroupBox"
        Me.MeaningGroupBox.Size = New System.Drawing.Size(411, 164)
        Me.MeaningGroupBox.TabIndex = 2
        Me.MeaningGroupBox.TabStop = False
        Me.MeaningGroupBox.Text = "Meaning:"
        '
        'MeaningTextBox
        '
        Me.MeaningTextBox.Location = New System.Drawing.Point(6, 19)
        Me.MeaningTextBox.Name = "MeaningTextBox"
        Me.MeaningTextBox.Size = New System.Drawing.Size(399, 20)
        Me.MeaningTextBox.TabIndex = 0
        '
        'SynonymsWebBrowser
        '
        Me.SynonymsWebBrowser.AllowWebBrowserDrop = False
        Me.SynonymsWebBrowser.IsWebBrowserContextMenuEnabled = False
        Me.SynonymsWebBrowser.Location = New System.Drawing.Point(7, 45)
        Me.SynonymsWebBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.SynonymsWebBrowser.Name = "SynonymsWebBrowser"
        Me.SynonymsWebBrowser.ScriptErrorsSuppressed = True
        Me.SynonymsWebBrowser.Size = New System.Drawing.Size(398, 113)
        Me.SynonymsWebBrowser.TabIndex = 3
        Me.SynonymsWebBrowser.WebBrowserShortcutsEnabled = False
        '
        'AssignMeaningDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(434, 363)
        Me.Controls.Add(Me.MeaningGroupBox)
        Me.Controls.Add(Me.WordGroupBox)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AssignMeaningDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Assign meaning"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.WordGroupBox.ResumeLayout(False)
        Me.WordGroupBox.PerformLayout()
        Me.MeaningGroupBox.ResumeLayout(False)
        Me.MeaningGroupBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents WordGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents WordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents MeaningGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents MeaningTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SynonymsWebBrowser As System.Windows.Forms.WebBrowser

End Class
