<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FontsForm
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
        Me.SelectFontDialog = New System.Windows.Forms.FontDialog
        Me.CodeFontLabel = New System.Windows.Forms.Label
        Me.CodeFontSelectorButton = New System.Windows.Forms.Button
        Me.CodeFontTextBox = New System.Windows.Forms.TextBox
        Me.MyCancelButton = New System.Windows.Forms.Button
        Me.MyOkButton = New System.Windows.Forms.Button
        Me.DictionaryFontTextBox = New System.Windows.Forms.TextBox
        Me.DictionaryFontSelectorButton = New System.Windows.Forms.Button
        Me.DictionaryFontLabel = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'SelectFontDialog
        '
        Me.SelectFontDialog.FontMustExist = True
        Me.SelectFontDialog.ShowColor = True
        Me.SelectFontDialog.ShowEffects = False
        '
        'CodeFontLabel
        '
        Me.CodeFontLabel.AutoSize = True
        Me.CodeFontLabel.Location = New System.Drawing.Point(12, 15)
        Me.CodeFontLabel.Name = "CodeFontLabel"
        Me.CodeFontLabel.Size = New System.Drawing.Size(84, 13)
        Me.CodeFontLabel.TabIndex = 0
        Me.CodeFontLabel.Text = "Generator code:"
        '
        'CodeFontSelectorButton
        '
        Me.CodeFontSelectorButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CodeFontSelectorButton.Location = New System.Drawing.Point(350, 10)
        Me.CodeFontSelectorButton.Name = "CodeFontSelectorButton"
        Me.CodeFontSelectorButton.Size = New System.Drawing.Size(28, 23)
        Me.CodeFontSelectorButton.TabIndex = 2
        Me.CodeFontSelectorButton.Text = "..."
        Me.CodeFontSelectorButton.UseVisualStyleBackColor = True
        '
        'CodeFontTextBox
        '
        Me.CodeFontTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CodeFontTextBox.Location = New System.Drawing.Point(102, 12)
        Me.CodeFontTextBox.Name = "CodeFontTextBox"
        Me.CodeFontTextBox.ReadOnly = True
        Me.CodeFontTextBox.Size = New System.Drawing.Size(242, 20)
        Me.CodeFontTextBox.TabIndex = 3
        '
        'MyCancelButton
        '
        Me.MyCancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.MyCancelButton.Location = New System.Drawing.Point(303, 100)
        Me.MyCancelButton.Name = "MyCancelButton"
        Me.MyCancelButton.Size = New System.Drawing.Size(75, 23)
        Me.MyCancelButton.TabIndex = 4
        Me.MyCancelButton.Text = "&Cancel"
        Me.MyCancelButton.UseVisualStyleBackColor = True
        '
        'MyOkButton
        '
        Me.MyOkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyOkButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.MyOkButton.Location = New System.Drawing.Point(222, 100)
        Me.MyOkButton.Name = "MyOkButton"
        Me.MyOkButton.Size = New System.Drawing.Size(75, 23)
        Me.MyOkButton.TabIndex = 5
        Me.MyOkButton.Text = "&Ok"
        Me.MyOkButton.UseVisualStyleBackColor = True
        '
        'DictionaryFontTextBox
        '
        Me.DictionaryFontTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DictionaryFontTextBox.Location = New System.Drawing.Point(102, 53)
        Me.DictionaryFontTextBox.Name = "DictionaryFontTextBox"
        Me.DictionaryFontTextBox.ReadOnly = True
        Me.DictionaryFontTextBox.Size = New System.Drawing.Size(242, 20)
        Me.DictionaryFontTextBox.TabIndex = 8
        '
        'DictionaryFontSelectorButton
        '
        Me.DictionaryFontSelectorButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DictionaryFontSelectorButton.Location = New System.Drawing.Point(350, 51)
        Me.DictionaryFontSelectorButton.Name = "DictionaryFontSelectorButton"
        Me.DictionaryFontSelectorButton.Size = New System.Drawing.Size(28, 23)
        Me.DictionaryFontSelectorButton.TabIndex = 7
        Me.DictionaryFontSelectorButton.Text = "..."
        Me.DictionaryFontSelectorButton.UseVisualStyleBackColor = True
        '
        'DictionaryFontLabel
        '
        Me.DictionaryFontLabel.AutoSize = True
        Me.DictionaryFontLabel.Location = New System.Drawing.Point(12, 56)
        Me.DictionaryFontLabel.Name = "DictionaryFontLabel"
        Me.DictionaryFontLabel.Size = New System.Drawing.Size(57, 13)
        Me.DictionaryFontLabel.TabIndex = 6
        Me.DictionaryFontLabel.Text = "Dictionary:"
        '
        'FontsForm
        '
        Me.AcceptButton = Me.MyOkButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.MyCancelButton
        Me.ClientSize = New System.Drawing.Size(390, 135)
        Me.Controls.Add(Me.DictionaryFontTextBox)
        Me.Controls.Add(Me.DictionaryFontSelectorButton)
        Me.Controls.Add(Me.DictionaryFontLabel)
        Me.Controls.Add(Me.MyOkButton)
        Me.Controls.Add(Me.MyCancelButton)
        Me.Controls.Add(Me.CodeFontTextBox)
        Me.Controls.Add(Me.CodeFontSelectorButton)
        Me.Controls.Add(Me.CodeFontLabel)
        Me.Name = "FontsForm"
        Me.Text = "Select fonts"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SelectFontDialog As System.Windows.Forms.FontDialog
    Friend WithEvents CodeFontLabel As System.Windows.Forms.Label
    Friend WithEvents CodeFontSelectorButton As System.Windows.Forms.Button
    Friend WithEvents CodeFontTextBox As System.Windows.Forms.TextBox
    Friend WithEvents MyCancelButton As System.Windows.Forms.Button
    Friend WithEvents MyOkButton As System.Windows.Forms.Button
    Friend WithEvents DictionaryFontTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DictionaryFontSelectorButton As System.Windows.Forms.Button
    Friend WithEvents DictionaryFontLabel As System.Windows.Forms.Label
End Class
