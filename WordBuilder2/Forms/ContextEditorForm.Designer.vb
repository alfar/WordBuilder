<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ContextEditorForm
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
        Me.MyOkButton = New System.Windows.Forms.Button
        Me.MyCancelButton = New System.Windows.Forms.Button
        Me.ContextTreeView = New System.Windows.Forms.TreeView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.DeleteButton = New System.Windows.Forms.Button
        Me.RenameBranchButton = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.BranchNameTextBox = New System.Windows.Forms.TextBox
        Me.BranchButton = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.UnmarkButton = New System.Windows.Forms.Button
        Me.MarkButton = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.MarkValueTextBox = New System.Windows.Forms.TextBox
        Me.MarkNameTextBox = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.TokensTextBox = New System.Windows.Forms.TextBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.RuleComboBox = New System.Windows.Forms.ComboBox
        Me.ApplyRuleButton = New System.Windows.Forms.Button
        Me.ClearBeforeApplyingCheckBox = New System.Windows.Forms.CheckBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'MyOkButton
        '
        Me.MyOkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyOkButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.MyOkButton.Location = New System.Drawing.Point(396, 331)
        Me.MyOkButton.Name = "MyOkButton"
        Me.MyOkButton.Size = New System.Drawing.Size(75, 23)
        Me.MyOkButton.TabIndex = 7
        Me.MyOkButton.Text = "&Ok"
        Me.MyOkButton.UseVisualStyleBackColor = True
        '
        'MyCancelButton
        '
        Me.MyCancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.MyCancelButton.Location = New System.Drawing.Point(477, 331)
        Me.MyCancelButton.Name = "MyCancelButton"
        Me.MyCancelButton.Size = New System.Drawing.Size(75, 23)
        Me.MyCancelButton.TabIndex = 6
        Me.MyCancelButton.Text = "&Cancel"
        Me.MyCancelButton.UseVisualStyleBackColor = True
        '
        'ContextTreeView
        '
        Me.ContextTreeView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ContextTreeView.HideSelection = False
        Me.ContextTreeView.Location = New System.Drawing.Point(12, 12)
        Me.ContextTreeView.Name = "ContextTreeView"
        Me.ContextTreeView.Size = New System.Drawing.Size(193, 313)
        Me.ContextTreeView.TabIndex = 8
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DeleteButton)
        Me.GroupBox1.Controls.Add(Me.RenameBranchButton)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.BranchNameTextBox)
        Me.GroupBox1.Controls.Add(Me.BranchButton)
        Me.GroupBox1.Location = New System.Drawing.Point(211, 63)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(341, 74)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Branch"
        '
        'DeleteButton
        '
        Me.DeleteButton.Location = New System.Drawing.Point(104, 45)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(75, 23)
        Me.DeleteButton.TabIndex = 15
        Me.DeleteButton.Text = "&Delete"
        Me.DeleteButton.UseVisualStyleBackColor = True
        '
        'RenameBranchButton
        '
        Me.RenameBranchButton.Location = New System.Drawing.Point(185, 45)
        Me.RenameBranchButton.Name = "RenameBranchButton"
        Me.RenameBranchButton.Size = New System.Drawing.Size(75, 23)
        Me.RenameBranchButton.TabIndex = 14
        Me.RenameBranchButton.Text = "&Rename"
        Me.RenameBranchButton.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Branch name:"
        '
        'BranchNameTextBox
        '
        Me.BranchNameTextBox.Location = New System.Drawing.Point(85, 19)
        Me.BranchNameTextBox.Name = "BranchNameTextBox"
        Me.BranchNameTextBox.Size = New System.Drawing.Size(250, 20)
        Me.BranchNameTextBox.TabIndex = 12
        '
        'BranchButton
        '
        Me.BranchButton.Location = New System.Drawing.Point(266, 45)
        Me.BranchButton.Name = "BranchButton"
        Me.BranchButton.Size = New System.Drawing.Size(69, 23)
        Me.BranchButton.TabIndex = 11
        Me.BranchButton.Text = "&Branch"
        Me.BranchButton.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.UnmarkButton)
        Me.GroupBox2.Controls.Add(Me.MarkButton)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.MarkValueTextBox)
        Me.GroupBox2.Controls.Add(Me.MarkNameTextBox)
        Me.GroupBox2.Location = New System.Drawing.Point(211, 143)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(341, 100)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Mark"
        '
        'UnmarkButton
        '
        Me.UnmarkButton.Location = New System.Drawing.Point(185, 71)
        Me.UnmarkButton.Name = "UnmarkButton"
        Me.UnmarkButton.Size = New System.Drawing.Size(75, 23)
        Me.UnmarkButton.TabIndex = 13
        Me.UnmarkButton.Text = "&Unmark"
        Me.UnmarkButton.UseVisualStyleBackColor = True
        '
        'MarkButton
        '
        Me.MarkButton.Location = New System.Drawing.Point(266, 71)
        Me.MarkButton.Name = "MarkButton"
        Me.MarkButton.Size = New System.Drawing.Size(69, 23)
        Me.MarkButton.TabIndex = 12
        Me.MarkButton.Text = "&Mark"
        Me.MarkButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Mark value:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Mark name:"
        '
        'MarkValueTextBox
        '
        Me.MarkValueTextBox.Location = New System.Drawing.Point(75, 45)
        Me.MarkValueTextBox.Name = "MarkValueTextBox"
        Me.MarkValueTextBox.Size = New System.Drawing.Size(260, 20)
        Me.MarkValueTextBox.TabIndex = 1
        '
        'MarkNameTextBox
        '
        Me.MarkNameTextBox.Location = New System.Drawing.Point(75, 19)
        Me.MarkNameTextBox.Name = "MarkNameTextBox"
        Me.MarkNameTextBox.Size = New System.Drawing.Size(260, 20)
        Me.MarkNameTextBox.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TokensTextBox)
        Me.GroupBox3.Location = New System.Drawing.Point(211, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(341, 45)
        Me.GroupBox3.TabIndex = 14
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Tokens"
        '
        'TokensTextBox
        '
        Me.TokensTextBox.Location = New System.Drawing.Point(6, 19)
        Me.TokensTextBox.Name = "TokensTextBox"
        Me.TokensTextBox.Size = New System.Drawing.Size(329, 20)
        Me.TokensTextBox.TabIndex = 12
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.ClearBeforeApplyingCheckBox)
        Me.GroupBox4.Controls.Add(Me.RuleComboBox)
        Me.GroupBox4.Controls.Add(Me.ApplyRuleButton)
        Me.GroupBox4.Location = New System.Drawing.Point(211, 250)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(341, 75)
        Me.GroupBox4.TabIndex = 15
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Rule"
        '
        'RuleComboBox
        '
        Me.RuleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RuleComboBox.FormattingEnabled = True
        Me.RuleComboBox.Location = New System.Drawing.Point(6, 19)
        Me.RuleComboBox.Name = "RuleComboBox"
        Me.RuleComboBox.Size = New System.Drawing.Size(329, 21)
        Me.RuleComboBox.TabIndex = 14
        '
        'ApplyRuleButton
        '
        Me.ApplyRuleButton.Location = New System.Drawing.Point(266, 46)
        Me.ApplyRuleButton.Name = "ApplyRuleButton"
        Me.ApplyRuleButton.Size = New System.Drawing.Size(69, 23)
        Me.ApplyRuleButton.TabIndex = 13
        Me.ApplyRuleButton.Text = "&Apply"
        Me.ApplyRuleButton.UseVisualStyleBackColor = True
        '
        'ClearBeforeApplyingCheckBox
        '
        Me.ClearBeforeApplyingCheckBox.AutoSize = True
        Me.ClearBeforeApplyingCheckBox.Location = New System.Drawing.Point(135, 50)
        Me.ClearBeforeApplyingCheckBox.Name = "ClearBeforeApplyingCheckBox"
        Me.ClearBeforeApplyingCheckBox.Size = New System.Drawing.Size(125, 17)
        Me.ClearBeforeApplyingCheckBox.TabIndex = 15
        Me.ClearBeforeApplyingCheckBox.Text = "Clear before applying"
        Me.ClearBeforeApplyingCheckBox.UseVisualStyleBackColor = True
        '
        'ContextEditorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(564, 366)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ContextTreeView)
        Me.Controls.Add(Me.MyOkButton)
        Me.Controls.Add(Me.MyCancelButton)
        Me.Name = "ContextEditorForm"
        Me.Text = "Edit word"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MyOkButton As System.Windows.Forms.Button
    Friend WithEvents MyCancelButton As System.Windows.Forms.Button
    Friend WithEvents ContextTreeView As System.Windows.Forms.TreeView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BranchNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents BranchButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MarkValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents MarkNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents MarkButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents TokensTextBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents RuleComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ApplyRuleButton As System.Windows.Forms.Button
    Friend WithEvents RenameBranchButton As System.Windows.Forms.Button
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents UnmarkButton As System.Windows.Forms.Button
    Friend WithEvents ClearBeforeApplyingCheckBox As System.Windows.Forms.CheckBox
End Class
