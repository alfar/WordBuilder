<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ManageRelationTypesDialog
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
        Me.RelationTypesListView = New System.Windows.Forms.ListView
        Me.RelationTypeGroupBox = New System.Windows.Forms.GroupBox
        Me.SaveButton = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.InverseTypeComboBox = New System.Windows.Forms.ComboBox
        Me.NameTextBox = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.NameColumnHeader = New System.Windows.Forms.ColumnHeader
        Me.TableLayoutPanel1.SuspendLayout()
        Me.RelationTypeGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 1, 0)
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
        Me.OK_Button.Location = New System.Drawing.Point(76, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'RelationTypesListView
        '
        Me.RelationTypesListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.NameColumnHeader})
        Me.RelationTypesListView.Location = New System.Drawing.Point(12, 12)
        Me.RelationTypesListView.MultiSelect = False
        Me.RelationTypesListView.Name = "RelationTypesListView"
        Me.RelationTypesListView.Size = New System.Drawing.Size(205, 256)
        Me.RelationTypesListView.TabIndex = 1
        Me.RelationTypesListView.UseCompatibleStateImageBehavior = False
        Me.RelationTypesListView.View = System.Windows.Forms.View.Details
        '
        'RelationTypeGroupBox
        '
        Me.RelationTypeGroupBox.Controls.Add(Me.SaveButton)
        Me.RelationTypeGroupBox.Controls.Add(Me.Label2)
        Me.RelationTypeGroupBox.Controls.Add(Me.InverseTypeComboBox)
        Me.RelationTypeGroupBox.Controls.Add(Me.NameTextBox)
        Me.RelationTypeGroupBox.Controls.Add(Me.Label1)
        Me.RelationTypeGroupBox.Location = New System.Drawing.Point(223, 12)
        Me.RelationTypeGroupBox.Name = "RelationTypeGroupBox"
        Me.RelationTypeGroupBox.Size = New System.Drawing.Size(199, 255)
        Me.RelationTypeGroupBox.TabIndex = 2
        Me.RelationTypeGroupBox.TabStop = False
        Me.RelationTypeGroupBox.Text = "Relation type"
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(118, 104)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(75, 23)
        Me.SaveButton.TabIndex = 4
        Me.SaveButton.Text = "&Save"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Inverse relation:"
        '
        'InverseTypeComboBox
        '
        Me.InverseTypeComboBox.FormattingEnabled = True
        Me.InverseTypeComboBox.Location = New System.Drawing.Point(9, 77)
        Me.InverseTypeComboBox.Name = "InverseTypeComboBox"
        Me.InverseTypeComboBox.Size = New System.Drawing.Size(184, 21)
        Me.InverseTypeComboBox.TabIndex = 2
        '
        'NameTextBox
        '
        Me.NameTextBox.Location = New System.Drawing.Point(9, 38)
        Me.NameTextBox.Name = "NameTextBox"
        Me.NameTextBox.Size = New System.Drawing.Size(184, 20)
        Me.NameTextBox.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name:"
        '
        'NameColumnHeader
        '
        Me.NameColumnHeader.Text = "Name"
        Me.NameColumnHeader.Width = 184
        '
        'ManageRelationTypesDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 315)
        Me.Controls.Add(Me.RelationTypeGroupBox)
        Me.Controls.Add(Me.RelationTypesListView)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ManageRelationTypesDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Manage Relation Types"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.RelationTypeGroupBox.ResumeLayout(False)
        Me.RelationTypeGroupBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents RelationTypesListView As System.Windows.Forms.ListView
    Friend WithEvents RelationTypeGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents InverseTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents NameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents NameColumnHeader As System.Windows.Forms.ColumnHeader

End Class
