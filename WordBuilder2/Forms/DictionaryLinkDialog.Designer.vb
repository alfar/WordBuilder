<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DictionaryLinkDialog
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
        Me.SearchLabel = New System.Windows.Forms.Label
        Me.SearchTextBox = New System.Windows.Forms.TextBox
        Me.DestinationWordCheckedListBox = New System.Windows.Forms.CheckedListBox
        Me.RelationTypeLabel = New System.Windows.Forms.Label
        Me.RelationTypeComboBox = New System.Windows.Forms.ComboBox
        Me.SearchButton = New System.Windows.Forms.Button
        Me.ManageRelationTypesButton = New System.Windows.Forms.Button
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 274)
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
        'SearchLabel
        '
        Me.SearchLabel.AutoSize = True
        Me.SearchLabel.Location = New System.Drawing.Point(12, 42)
        Me.SearchLabel.Name = "SearchLabel"
        Me.SearchLabel.Size = New System.Drawing.Size(56, 13)
        Me.SearchLabel.TabIndex = 9
        Me.SearchLabel.Text = "Find word:"
        '
        'SearchTextBox
        '
        Me.SearchTextBox.Location = New System.Drawing.Point(90, 39)
        Me.SearchTextBox.Name = "SearchTextBox"
        Me.SearchTextBox.Size = New System.Drawing.Size(257, 20)
        Me.SearchTextBox.TabIndex = 8
        '
        'DestinationWordCheckedListBox
        '
        Me.DestinationWordCheckedListBox.CheckOnClick = True
        Me.DestinationWordCheckedListBox.FormattingEnabled = True
        Me.DestinationWordCheckedListBox.Location = New System.Drawing.Point(12, 65)
        Me.DestinationWordCheckedListBox.Name = "DestinationWordCheckedListBox"
        Me.DestinationWordCheckedListBox.Size = New System.Drawing.Size(411, 199)
        Me.DestinationWordCheckedListBox.TabIndex = 7
        '
        'RelationTypeLabel
        '
        Me.RelationTypeLabel.AutoSize = True
        Me.RelationTypeLabel.Location = New System.Drawing.Point(12, 15)
        Me.RelationTypeLabel.Name = "RelationTypeLabel"
        Me.RelationTypeLabel.Size = New System.Drawing.Size(72, 13)
        Me.RelationTypeLabel.TabIndex = 6
        Me.RelationTypeLabel.Text = "Relation type:"
        '
        'RelationTypeComboBox
        '
        Me.RelationTypeComboBox.FormattingEnabled = True
        Me.RelationTypeComboBox.Location = New System.Drawing.Point(90, 12)
        Me.RelationTypeComboBox.Name = "RelationTypeComboBox"
        Me.RelationTypeComboBox.Size = New System.Drawing.Size(257, 21)
        Me.RelationTypeComboBox.TabIndex = 5
        '
        'SearchButton
        '
        Me.SearchButton.Location = New System.Drawing.Point(353, 39)
        Me.SearchButton.Name = "SearchButton"
        Me.SearchButton.Size = New System.Drawing.Size(70, 20)
        Me.SearchButton.TabIndex = 10
        Me.SearchButton.Text = "&Search"
        Me.SearchButton.UseVisualStyleBackColor = True
        '
        'ManageRelationTypesButton
        '
        Me.ManageRelationTypesButton.Location = New System.Drawing.Point(353, 12)
        Me.ManageRelationTypesButton.Name = "ManageRelationTypesButton"
        Me.ManageRelationTypesButton.Size = New System.Drawing.Size(70, 21)
        Me.ManageRelationTypesButton.TabIndex = 11
        Me.ManageRelationTypesButton.Text = "&Manage..."
        Me.ManageRelationTypesButton.UseVisualStyleBackColor = True
        '
        'DictionaryLinkDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 315)
        Me.Controls.Add(Me.ManageRelationTypesButton)
        Me.Controls.Add(Me.SearchButton)
        Me.Controls.Add(Me.SearchLabel)
        Me.Controls.Add(Me.SearchTextBox)
        Me.Controls.Add(Me.DestinationWordCheckedListBox)
        Me.Controls.Add(Me.RelationTypeLabel)
        Me.Controls.Add(Me.RelationTypeComboBox)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DictionaryLinkDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Create link"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents SearchLabel As System.Windows.Forms.Label
    Friend WithEvents SearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DestinationWordCheckedListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents RelationTypeLabel As System.Windows.Forms.Label
    Friend WithEvents RelationTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents SearchButton As System.Windows.Forms.Button
    Friend WithEvents ManageRelationTypesButton As System.Windows.Forms.Button

End Class
