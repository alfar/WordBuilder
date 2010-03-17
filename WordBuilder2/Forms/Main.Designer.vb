Imports WheeControls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.ApplicationStatusStrip = New System.Windows.Forms.StatusStrip
        Me.ProjectTabControl = New System.Windows.Forms.TabControl
        Me.GeneratorTabPage = New System.Windows.Forms.TabPage
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ResultsListBox = New System.Windows.Forms.ListView
        Me.GeneratorResultsContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddToDictionaryToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Splitter3 = New System.Windows.Forms.Splitter
        Me.ResultsListBox_Old = New System.Windows.Forms.ListBox
        Me.ResultsTextBox = New System.Windows.Forms.TextBox
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.CodePanel = New System.Windows.Forms.Panel
        Me.CodeTextBox = New WheeControls.SyntaxRichTextBox
        Me.RulesComboBox = New System.Windows.Forms.ComboBox
        Me.WarningsSplitter = New System.Windows.Forms.Splitter
        Me.WarningsPanel = New System.Windows.Forms.Panel
        Me.WarningsListBox = New System.Windows.Forms.ListBox
        Me.WarningsLabel = New System.Windows.Forms.Label
        Me.DictionaryTabPage = New System.Windows.Forms.TabPage
        Me.DictionaryResultsGroupBox = New System.Windows.Forms.GroupBox
        Me.DictionaryResultsWebBrowser = New System.Windows.Forms.WebBrowser
        Me.DictionaryResultsContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditWordToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.AddAnotherMeaningToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DictionaryResultEditMeaningToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LinkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DictionarySeparatorToolStripMenuItem = New System.Windows.Forms.ToolStripSeparator
        Me.DeleteWordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeleteLinkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DictionarySearchGroupBox = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.DictionarySearchMarksCheckedListBox = New System.Windows.Forms.CheckedListBox
        Me.DictionaryMarksCheckBox = New System.Windows.Forms.CheckBox
        Me.DictionarySearchWordCheckBox = New System.Windows.Forms.CheckBox
        Me.DictionarySearchMeaningCheckBox = New System.Windows.Forms.CheckBox
        Me.DictionarySearchOperationComboBox = New System.Windows.Forms.ComboBox
        Me.DictionaryLookupButton = New System.Windows.Forms.Button
        Me.MeaningTextBox = New System.Windows.Forms.TextBox
        Me.TranslatorTabPage = New System.Windows.Forms.TabPage
        Me.TranslatorResultWebBrowser = New System.Windows.Forms.WebBrowser
        Me.TranslatorResultContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddWordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TranslatorSplitter = New System.Windows.Forms.Splitter
        Me.TranslatorSourceTextBox = New System.Windows.Forms.TextBox
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.TranslateToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.TranslateBackToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.SaveFileDialog2 = New System.Windows.Forms.SaveFileDialog
        Me.ApplicationMenuStrip = New System.Windows.Forms.MenuStrip
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectAllToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator
        Me.FindToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FindAgainToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.WordsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GenerateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.QuickGenerateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.QuickClearAndGenerateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ClearListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.EditwordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyListToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyDetailsToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.AddToDictionaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ExportListToFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DictionaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NewDictionaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenDictionaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator
        Me.StatisticsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FontsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.OpenFileDialog2 = New System.Windows.Forms.OpenFileDialog
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.ProjectTabControl.SuspendLayout()
        Me.GeneratorTabPage.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GeneratorResultsContextMenuStrip.SuspendLayout()
        Me.CodePanel.SuspendLayout()
        Me.WarningsPanel.SuspendLayout()
        Me.DictionaryTabPage.SuspendLayout()
        Me.DictionaryResultsGroupBox.SuspendLayout()
        Me.DictionaryResultsContextMenuStrip.SuspendLayout()
        Me.DictionarySearchGroupBox.SuspendLayout()
        Me.TranslatorTabPage.SuspendLayout()
        Me.TranslatorResultContextMenuStrip.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.ApplicationMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'ApplicationStatusStrip
        '
        Me.ApplicationStatusStrip.Location = New System.Drawing.Point(0, 364)
        Me.ApplicationStatusStrip.Name = "ApplicationStatusStrip"
        Me.ApplicationStatusStrip.Size = New System.Drawing.Size(635, 22)
        Me.ApplicationStatusStrip.TabIndex = 1
        Me.ApplicationStatusStrip.Text = "StatusStrip1"
        '
        'ProjectTabControl
        '
        Me.ProjectTabControl.Controls.Add(Me.GeneratorTabPage)
        Me.ProjectTabControl.Controls.Add(Me.DictionaryTabPage)
        Me.ProjectTabControl.Controls.Add(Me.TranslatorTabPage)
        Me.ProjectTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProjectTabControl.HotTrack = True
        Me.ProjectTabControl.Location = New System.Drawing.Point(0, 24)
        Me.ProjectTabControl.Name = "ProjectTabControl"
        Me.ProjectTabControl.SelectedIndex = 0
        Me.ProjectTabControl.Size = New System.Drawing.Size(635, 340)
        Me.ProjectTabControl.TabIndex = 2
        '
        'GeneratorTabPage
        '
        Me.GeneratorTabPage.BackColor = System.Drawing.Color.Transparent
        Me.GeneratorTabPage.Controls.Add(Me.Panel1)
        Me.GeneratorTabPage.Location = New System.Drawing.Point(4, 22)
        Me.GeneratorTabPage.Name = "GeneratorTabPage"
        Me.GeneratorTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.GeneratorTabPage.Size = New System.Drawing.Size(627, 314)
        Me.GeneratorTabPage.TabIndex = 0
        Me.GeneratorTabPage.Text = "Generator"
        Me.GeneratorTabPage.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ResultsListBox)
        Me.Panel1.Controls.Add(Me.Splitter3)
        Me.Panel1.Controls.Add(Me.ResultsListBox_Old)
        Me.Panel1.Controls.Add(Me.ResultsTextBox)
        Me.Panel1.Controls.Add(Me.Splitter1)
        Me.Panel1.Controls.Add(Me.CodePanel)
        Me.Panel1.Controls.Add(Me.WarningsSplitter)
        Me.Panel1.Controls.Add(Me.WarningsPanel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(5)
        Me.Panel1.Size = New System.Drawing.Size(621, 308)
        Me.Panel1.TabIndex = 2
        '
        'ResultsListBox
        '
        Me.ResultsListBox.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ResultsListBox.ContextMenuStrip = Me.GeneratorResultsContextMenuStrip
        Me.ResultsListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResultsListBox.FullRowSelect = True
        Me.ResultsListBox.GridLines = True
        Me.ResultsListBox.HideSelection = False
        Me.ResultsListBox.Location = New System.Drawing.Point(217, 5)
        Me.ResultsListBox.Name = "ResultsListBox"
        Me.ResultsListBox.Size = New System.Drawing.Size(399, 134)
        Me.ResultsListBox.TabIndex = 9
        Me.ResultsListBox.UseCompatibleStateImageBehavior = False
        Me.ResultsListBox.View = System.Windows.Forms.View.Details
        '
        'GeneratorResultsContextMenuStrip
        '
        Me.GeneratorResultsContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddToDictionaryToolStripMenuItem1, Me.ToolStripMenuItem4, Me.EditToolStripMenuItem})
        Me.GeneratorResultsContextMenuStrip.Name = "GeneratorResultsContextMenuStrip"
        Me.GeneratorResultsContextMenuStrip.Size = New System.Drawing.Size(176, 54)
        '
        'AddToDictionaryToolStripMenuItem1
        '
        Me.AddToDictionaryToolStripMenuItem1.Name = "AddToDictionaryToolStripMenuItem1"
        Me.AddToDictionaryToolStripMenuItem1.Size = New System.Drawing.Size(175, 22)
        Me.AddToDictionaryToolStripMenuItem1.Text = "&Add to dictionary..."
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(172, 6)
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.EditToolStripMenuItem.Text = "&Edit..."
        '
        'Splitter3
        '
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter3.Location = New System.Drawing.Point(217, 139)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(399, 5)
        Me.Splitter3.TabIndex = 5
        Me.Splitter3.TabStop = False
        '
        'ResultsListBox_Old
        '
        Me.ResultsListBox_Old.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResultsListBox_Old.FormattingEnabled = True
        Me.ResultsListBox_Old.IntegralHeight = False
        Me.ResultsListBox_Old.Location = New System.Drawing.Point(217, 5)
        Me.ResultsListBox_Old.Name = "ResultsListBox_Old"
        Me.ResultsListBox_Old.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ResultsListBox_Old.Size = New System.Drawing.Size(399, 139)
        Me.ResultsListBox_Old.TabIndex = 1
        '
        'ResultsTextBox
        '
        Me.ResultsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ResultsTextBox.Location = New System.Drawing.Point(217, 144)
        Me.ResultsTextBox.Multiline = True
        Me.ResultsTextBox.Name = "ResultsTextBox"
        Me.ResultsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.ResultsTextBox.Size = New System.Drawing.Size(399, 54)
        Me.ResultsTextBox.TabIndex = 2
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(212, 5)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(5, 193)
        Me.Splitter1.TabIndex = 4
        Me.Splitter1.TabStop = False
        '
        'CodePanel
        '
        Me.CodePanel.Controls.Add(Me.CodeTextBox)
        Me.CodePanel.Controls.Add(Me.RulesComboBox)
        Me.CodePanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.CodePanel.Location = New System.Drawing.Point(5, 5)
        Me.CodePanel.Name = "CodePanel"
        Me.CodePanel.Size = New System.Drawing.Size(207, 193)
        Me.CodePanel.TabIndex = 8
        '
        'CodeTextBox
        '
        Me.CodeTextBox.AcceptsTab = True
        Me.CodeTextBox.CodeFont = New System.Drawing.Font("Courier New", 11.0!)
        Me.CodeTextBox.DetectUrls = False
        Me.CodeTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CodeTextBox.Location = New System.Drawing.Point(0, 21)
        Me.CodeTextBox.Name = "CodeTextBox"
        Me.CodeTextBox.Size = New System.Drawing.Size(207, 172)
        Me.CodeTextBox.TabIndex = 0
        Me.CodeTextBox.Text = ""
        Me.CodeTextBox.WordWrap = False
        '
        'RulesComboBox
        '
        Me.RulesComboBox.DisplayMember = "Name"
        Me.RulesComboBox.Dock = System.Windows.Forms.DockStyle.Top
        Me.RulesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RulesComboBox.FormattingEnabled = True
        Me.RulesComboBox.Location = New System.Drawing.Point(0, 0)
        Me.RulesComboBox.Name = "RulesComboBox"
        Me.RulesComboBox.Size = New System.Drawing.Size(207, 21)
        Me.RulesComboBox.TabIndex = 1
        '
        'WarningsSplitter
        '
        Me.WarningsSplitter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.WarningsSplitter.Location = New System.Drawing.Point(5, 198)
        Me.WarningsSplitter.Name = "WarningsSplitter"
        Me.WarningsSplitter.Size = New System.Drawing.Size(611, 5)
        Me.WarningsSplitter.TabIndex = 6
        Me.WarningsSplitter.TabStop = False
        Me.WarningsSplitter.Visible = False
        '
        'WarningsPanel
        '
        Me.WarningsPanel.Controls.Add(Me.WarningsListBox)
        Me.WarningsPanel.Controls.Add(Me.WarningsLabel)
        Me.WarningsPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.WarningsPanel.Location = New System.Drawing.Point(5, 203)
        Me.WarningsPanel.Name = "WarningsPanel"
        Me.WarningsPanel.Padding = New System.Windows.Forms.Padding(3)
        Me.WarningsPanel.Size = New System.Drawing.Size(611, 100)
        Me.WarningsPanel.TabIndex = 7
        Me.WarningsPanel.Visible = False
        '
        'WarningsListBox
        '
        Me.WarningsListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WarningsListBox.FormattingEnabled = True
        Me.WarningsListBox.IntegralHeight = False
        Me.WarningsListBox.Location = New System.Drawing.Point(3, 16)
        Me.WarningsListBox.Name = "WarningsListBox"
        Me.WarningsListBox.Size = New System.Drawing.Size(605, 81)
        Me.WarningsListBox.TabIndex = 4
        '
        'WarningsLabel
        '
        Me.WarningsLabel.AutoSize = True
        Me.WarningsLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.WarningsLabel.Location = New System.Drawing.Point(3, 3)
        Me.WarningsLabel.Name = "WarningsLabel"
        Me.WarningsLabel.Size = New System.Drawing.Size(55, 13)
        Me.WarningsLabel.TabIndex = 6
        Me.WarningsLabel.Text = "Warnings:"
        '
        'DictionaryTabPage
        '
        Me.DictionaryTabPage.Controls.Add(Me.DictionaryResultsGroupBox)
        Me.DictionaryTabPage.Controls.Add(Me.DictionarySearchGroupBox)
        Me.DictionaryTabPage.Location = New System.Drawing.Point(4, 22)
        Me.DictionaryTabPage.Name = "DictionaryTabPage"
        Me.DictionaryTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.DictionaryTabPage.Size = New System.Drawing.Size(627, 314)
        Me.DictionaryTabPage.TabIndex = 1
        Me.DictionaryTabPage.Text = "Dictionary"
        Me.DictionaryTabPage.UseVisualStyleBackColor = True
        '
        'DictionaryResultsGroupBox
        '
        Me.DictionaryResultsGroupBox.Controls.Add(Me.DictionaryResultsWebBrowser)
        Me.DictionaryResultsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DictionaryResultsGroupBox.Location = New System.Drawing.Point(3, 142)
        Me.DictionaryResultsGroupBox.Name = "DictionaryResultsGroupBox"
        Me.DictionaryResultsGroupBox.Size = New System.Drawing.Size(621, 169)
        Me.DictionaryResultsGroupBox.TabIndex = 4
        Me.DictionaryResultsGroupBox.TabStop = False
        Me.DictionaryResultsGroupBox.Text = "Results:"
        '
        'DictionaryResultsWebBrowser
        '
        Me.DictionaryResultsWebBrowser.AllowWebBrowserDrop = False
        Me.DictionaryResultsWebBrowser.ContextMenuStrip = Me.DictionaryResultsContextMenuStrip
        Me.DictionaryResultsWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DictionaryResultsWebBrowser.IsWebBrowserContextMenuEnabled = False
        Me.DictionaryResultsWebBrowser.Location = New System.Drawing.Point(3, 16)
        Me.DictionaryResultsWebBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.DictionaryResultsWebBrowser.Name = "DictionaryResultsWebBrowser"
        Me.DictionaryResultsWebBrowser.ScriptErrorsSuppressed = True
        Me.DictionaryResultsWebBrowser.Size = New System.Drawing.Size(615, 150)
        Me.DictionaryResultsWebBrowser.TabIndex = 2
        '
        'DictionaryResultsContextMenuStrip
        '
        Me.DictionaryResultsContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditWordToolStripMenuItem1, Me.AddAnotherMeaningToolStripMenuItem, Me.DictionaryResultEditMeaningToolStripMenuItem, Me.LinkToolStripMenuItem, Me.DictionarySeparatorToolStripMenuItem, Me.DeleteWordToolStripMenuItem, Me.DeleteLinkToolStripMenuItem})
        Me.DictionaryResultsContextMenuStrip.Name = "DictionaryResultsContextMenuStrip"
        Me.DictionaryResultsContextMenuStrip.Size = New System.Drawing.Size(200, 142)
        '
        'EditWordToolStripMenuItem1
        '
        Me.EditWordToolStripMenuItem1.Name = "EditWordToolStripMenuItem1"
        Me.EditWordToolStripMenuItem1.Size = New System.Drawing.Size(199, 22)
        Me.EditWordToolStripMenuItem1.Text = "&Edit word..."
        '
        'AddAnotherMeaningToolStripMenuItem
        '
        Me.AddAnotherMeaningToolStripMenuItem.Name = "AddAnotherMeaningToolStripMenuItem"
        Me.AddAnotherMeaningToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.AddAnotherMeaningToolStripMenuItem.Text = "&Add another meaning..."
        '
        'DictionaryResultEditMeaningToolStripMenuItem
        '
        Me.DictionaryResultEditMeaningToolStripMenuItem.Name = "DictionaryResultEditMeaningToolStripMenuItem"
        Me.DictionaryResultEditMeaningToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.DictionaryResultEditMeaningToolStripMenuItem.Text = "Edit &meaning..."
        '
        'LinkToolStripMenuItem
        '
        Me.LinkToolStripMenuItem.Name = "LinkToolStripMenuItem"
        Me.LinkToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.LinkToolStripMenuItem.Text = "&Link..."
        '
        'DictionarySeparatorToolStripMenuItem
        '
        Me.DictionarySeparatorToolStripMenuItem.Name = "DictionarySeparatorToolStripMenuItem"
        Me.DictionarySeparatorToolStripMenuItem.Size = New System.Drawing.Size(196, 6)
        '
        'DeleteWordToolStripMenuItem
        '
        Me.DeleteWordToolStripMenuItem.Name = "DeleteWordToolStripMenuItem"
        Me.DeleteWordToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.DeleteWordToolStripMenuItem.Text = "&Delete word"
        '
        'DeleteLinkToolStripMenuItem
        '
        Me.DeleteLinkToolStripMenuItem.Name = "DeleteLinkToolStripMenuItem"
        Me.DeleteLinkToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.DeleteLinkToolStripMenuItem.Text = "Dele&te link"
        '
        'DictionarySearchGroupBox
        '
        Me.DictionarySearchGroupBox.Controls.Add(Me.Label2)
        Me.DictionarySearchGroupBox.Controls.Add(Me.Label1)
        Me.DictionarySearchGroupBox.Controls.Add(Me.DictionarySearchMarksCheckedListBox)
        Me.DictionarySearchGroupBox.Controls.Add(Me.DictionaryMarksCheckBox)
        Me.DictionarySearchGroupBox.Controls.Add(Me.DictionarySearchWordCheckBox)
        Me.DictionarySearchGroupBox.Controls.Add(Me.DictionarySearchMeaningCheckBox)
        Me.DictionarySearchGroupBox.Controls.Add(Me.DictionarySearchOperationComboBox)
        Me.DictionarySearchGroupBox.Controls.Add(Me.DictionaryLookupButton)
        Me.DictionarySearchGroupBox.Controls.Add(Me.MeaningTextBox)
        Me.DictionarySearchGroupBox.Dock = System.Windows.Forms.DockStyle.Top
        Me.DictionarySearchGroupBox.Location = New System.Drawing.Point(3, 3)
        Me.DictionarySearchGroupBox.Name = "DictionarySearchGroupBox"
        Me.DictionarySearchGroupBox.Size = New System.Drawing.Size(621, 139)
        Me.DictionarySearchGroupBox.TabIndex = 3
        Me.DictionarySearchGroupBox.TabStop = False
        Me.DictionarySearchGroupBox.Text = "Term:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(182, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Search in:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Comparison:"
        '
        'DictionarySearchMarksCheckedListBox
        '
        Me.DictionarySearchMarksCheckedListBox.CheckOnClick = True
        Me.DictionarySearchMarksCheckedListBox.FormattingEnabled = True
        Me.DictionarySearchMarksCheckedListBox.IntegralHeight = False
        Me.DictionarySearchMarksCheckedListBox.Location = New System.Drawing.Point(438, 48)
        Me.DictionarySearchMarksCheckedListBox.Name = "DictionarySearchMarksCheckedListBox"
        Me.DictionarySearchMarksCheckedListBox.Size = New System.Drawing.Size(177, 83)
        Me.DictionarySearchMarksCheckedListBox.TabIndex = 9
        '
        'DictionaryMarksCheckBox
        '
        Me.DictionaryMarksCheckBox.AutoSize = True
        Me.DictionaryMarksCheckBox.Location = New System.Drawing.Point(374, 50)
        Me.DictionaryMarksCheckBox.Name = "DictionaryMarksCheckBox"
        Me.DictionaryMarksCheckBox.Size = New System.Drawing.Size(58, 17)
        Me.DictionaryMarksCheckBox.TabIndex = 7
        Me.DictionaryMarksCheckBox.Text = "Marks:"
        Me.DictionaryMarksCheckBox.UseVisualStyleBackColor = True
        '
        'DictionarySearchWordCheckBox
        '
        Me.DictionarySearchWordCheckBox.AutoSize = True
        Me.DictionarySearchWordCheckBox.Checked = True
        Me.DictionarySearchWordCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DictionarySearchWordCheckBox.Location = New System.Drawing.Point(316, 50)
        Me.DictionarySearchWordCheckBox.Name = "DictionarySearchWordCheckBox"
        Me.DictionarySearchWordCheckBox.Size = New System.Drawing.Size(52, 17)
        Me.DictionarySearchWordCheckBox.TabIndex = 6
        Me.DictionarySearchWordCheckBox.Text = "Word"
        Me.DictionarySearchWordCheckBox.UseVisualStyleBackColor = True
        '
        'DictionarySearchMeaningCheckBox
        '
        Me.DictionarySearchMeaningCheckBox.AutoSize = True
        Me.DictionarySearchMeaningCheckBox.Checked = True
        Me.DictionarySearchMeaningCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DictionarySearchMeaningCheckBox.Location = New System.Drawing.Point(243, 50)
        Me.DictionarySearchMeaningCheckBox.Name = "DictionarySearchMeaningCheckBox"
        Me.DictionarySearchMeaningCheckBox.Size = New System.Drawing.Size(67, 17)
        Me.DictionarySearchMeaningCheckBox.TabIndex = 5
        Me.DictionarySearchMeaningCheckBox.Text = "Meaning"
        Me.DictionarySearchMeaningCheckBox.UseVisualStyleBackColor = True
        '
        'DictionarySearchOperationComboBox
        '
        Me.DictionarySearchOperationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DictionarySearchOperationComboBox.FormattingEnabled = True
        Me.DictionarySearchOperationComboBox.Items.AddRange(New Object() {"Exact match", "Start with", "End with", "Anywhere"})
        Me.DictionarySearchOperationComboBox.Location = New System.Drawing.Point(74, 48)
        Me.DictionarySearchOperationComboBox.Name = "DictionarySearchOperationComboBox"
        Me.DictionarySearchOperationComboBox.Size = New System.Drawing.Size(102, 21)
        Me.DictionarySearchOperationComboBox.TabIndex = 4
        '
        'DictionaryLookupButton
        '
        Me.DictionaryLookupButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DictionaryLookupButton.Location = New System.Drawing.Point(544, 19)
        Me.DictionaryLookupButton.Name = "DictionaryLookupButton"
        Me.DictionaryLookupButton.Size = New System.Drawing.Size(71, 25)
        Me.DictionaryLookupButton.TabIndex = 3
        Me.DictionaryLookupButton.Text = "&Look up"
        Me.DictionaryLookupButton.UseVisualStyleBackColor = True
        '
        'MeaningTextBox
        '
        Me.MeaningTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MeaningTextBox.Location = New System.Drawing.Point(6, 22)
        Me.MeaningTextBox.Name = "MeaningTextBox"
        Me.MeaningTextBox.Size = New System.Drawing.Size(532, 20)
        Me.MeaningTextBox.TabIndex = 2
        '
        'TranslatorTabPage
        '
        Me.TranslatorTabPage.Controls.Add(Me.TranslatorResultWebBrowser)
        Me.TranslatorTabPage.Controls.Add(Me.TranslatorSplitter)
        Me.TranslatorTabPage.Controls.Add(Me.TranslatorSourceTextBox)
        Me.TranslatorTabPage.Controls.Add(Me.ToolStrip1)
        Me.TranslatorTabPage.Location = New System.Drawing.Point(4, 22)
        Me.TranslatorTabPage.Name = "TranslatorTabPage"
        Me.TranslatorTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.TranslatorTabPage.Size = New System.Drawing.Size(627, 314)
        Me.TranslatorTabPage.TabIndex = 2
        Me.TranslatorTabPage.Text = "Translator"
        Me.TranslatorTabPage.UseVisualStyleBackColor = True
        '
        'TranslatorResultWebBrowser
        '
        Me.TranslatorResultWebBrowser.AllowWebBrowserDrop = False
        Me.TranslatorResultWebBrowser.ContextMenuStrip = Me.TranslatorResultContextMenuStrip
        Me.TranslatorResultWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TranslatorResultWebBrowser.IsWebBrowserContextMenuEnabled = False
        Me.TranslatorResultWebBrowser.Location = New System.Drawing.Point(3, 123)
        Me.TranslatorResultWebBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.TranslatorResultWebBrowser.Name = "TranslatorResultWebBrowser"
        Me.TranslatorResultWebBrowser.ScriptErrorsSuppressed = True
        Me.TranslatorResultWebBrowser.Size = New System.Drawing.Size(621, 188)
        Me.TranslatorResultWebBrowser.TabIndex = 2
        '
        'TranslatorResultContextMenuStrip
        '
        Me.TranslatorResultContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddWordToolStripMenuItem})
        Me.TranslatorResultContextMenuStrip.Name = "TranslatorResultContextMenuStrip"
        Me.TranslatorResultContextMenuStrip.Size = New System.Drawing.Size(136, 26)
        '
        'AddWordToolStripMenuItem
        '
        Me.AddWordToolStripMenuItem.Name = "AddWordToolStripMenuItem"
        Me.AddWordToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.AddWordToolStripMenuItem.Text = "&Add word..."
        '
        'TranslatorSplitter
        '
        Me.TranslatorSplitter.Dock = System.Windows.Forms.DockStyle.Top
        Me.TranslatorSplitter.Location = New System.Drawing.Point(3, 118)
        Me.TranslatorSplitter.Name = "TranslatorSplitter"
        Me.TranslatorSplitter.Size = New System.Drawing.Size(621, 5)
        Me.TranslatorSplitter.TabIndex = 1
        Me.TranslatorSplitter.TabStop = False
        '
        'TranslatorSourceTextBox
        '
        Me.TranslatorSourceTextBox.Dock = System.Windows.Forms.DockStyle.Top
        Me.TranslatorSourceTextBox.Location = New System.Drawing.Point(3, 3)
        Me.TranslatorSourceTextBox.Multiline = True
        Me.TranslatorSourceTextBox.Name = "TranslatorSourceTextBox"
        Me.TranslatorSourceTextBox.Size = New System.Drawing.Size(621, 115)
        Me.TranslatorSourceTextBox.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TranslateToolStripButton, Me.TranslateBackToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 3)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(621, 25)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        Me.ToolStrip1.Visible = False
        '
        'TranslateToolStripButton
        '
        Me.TranslateToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TranslateToolStripButton.Image = CType(resources.GetObject("TranslateToolStripButton.Image"), System.Drawing.Image)
        Me.TranslateToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TranslateToolStripButton.Name = "TranslateToolStripButton"
        Me.TranslateToolStripButton.Size = New System.Drawing.Size(59, 22)
        Me.TranslateToolStripButton.Text = "&Translate"
        '
        'TranslateBackToolStripButton
        '
        Me.TranslateBackToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TranslateBackToolStripButton.Image = CType(resources.GetObject("TranslateBackToolStripButton.Image"), System.Drawing.Image)
        Me.TranslateBackToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TranslateBackToolStripButton.Name = "TranslateBackToolStripButton"
        Me.TranslateBackToolStripButton.Size = New System.Drawing.Size(87, 22)
        Me.TranslateBackToolStripButton.Text = "Translate &back"
        '
        'SaveFileDialog2
        '
        Me.SaveFileDialog2.Filter = "Dictionary files|*.wdic"
        Me.SaveFileDialog2.Title = "Export list"
        '
        'ApplicationMenuStrip
        '
        Me.ApplicationMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.EditToolStripMenuItem1, Me.WordsToolStripMenuItem, Me.DictionaryToolStripMenuItem, Me.OptionsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.ApplicationMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.ApplicationMenuStrip.Name = "ApplicationMenuStrip"
        Me.ApplicationMenuStrip.Size = New System.Drawing.Size(635, 24)
        Me.ApplicationMenuStrip.TabIndex = 3
        Me.ApplicationMenuStrip.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.ToolStripSeparator1, Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveasToolStripMenuItem, Me.ToolStripMenuItem2, Me.ExitToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(37, 20)
        Me.ToolStripMenuItem1.Text = "&File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.NewToolStripMenuItem.Text = "&New"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(190, 6)
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.OpenToolStripMenuItem.Text = "&Open..."
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.SaveToolStripMenuItem.Text = "&Save"
        '
        'SaveasToolStripMenuItem
        '
        Me.SaveasToolStripMenuItem.Name = "SaveasToolStripMenuItem"
        Me.SaveasToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveasToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.SaveasToolStripMenuItem.Text = "Save &as..."
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(190, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'EditToolStripMenuItem1
        '
        Me.EditToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.SelectAllToolStripMenuItem1, Me.ToolStripMenuItem5, Me.FindToolStripMenuItem, Me.FindAgainToolStripMenuItem})
        Me.EditToolStripMenuItem1.Name = "EditToolStripMenuItem1"
        Me.EditToolStripMenuItem1.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem1.Text = "&Edit"
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.CutToolStripMenuItem.Text = "Cu&t"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.CopyToolStripMenuItem.Text = "&Copy"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.PasteToolStripMenuItem.Text = "&Paste"
        '
        'SelectAllToolStripMenuItem1
        '
        Me.SelectAllToolStripMenuItem1.Name = "SelectAllToolStripMenuItem1"
        Me.SelectAllToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.SelectAllToolStripMenuItem1.Size = New System.Drawing.Size(162, 22)
        Me.SelectAllToolStripMenuItem1.Text = "&Select all"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(159, 6)
        '
        'FindToolStripMenuItem
        '
        Me.FindToolStripMenuItem.Name = "FindToolStripMenuItem"
        Me.FindToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.FindToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.FindToolStripMenuItem.Text = "&Find..."
        '
        'FindAgainToolStripMenuItem
        '
        Me.FindAgainToolStripMenuItem.Name = "FindAgainToolStripMenuItem"
        Me.FindAgainToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.FindAgainToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.FindAgainToolStripMenuItem.Text = "F&ind again"
        '
        'WordsToolStripMenuItem
        '
        Me.WordsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GenerateToolStripMenuItem, Me.QuickGenerateToolStripMenuItem, Me.QuickClearAndGenerateToolStripMenuItem, Me.ClearListToolStripMenuItem, Me.ToolStripSeparator4, Me.EditwordToolStripMenuItem, Me.ToolStripSeparator2, Me.SelectAllToolStripMenuItem, Me.CopyListToClipboardToolStripMenuItem, Me.CopyDetailsToClipboardToolStripMenuItem, Me.ToolStripSeparator3, Me.AddToDictionaryToolStripMenuItem, Me.ToolStripSeparator5, Me.ExportListToFileToolStripMenuItem})
        Me.WordsToolStripMenuItem.Name = "WordsToolStripMenuItem"
        Me.WordsToolStripMenuItem.Size = New System.Drawing.Size(53, 20)
        Me.WordsToolStripMenuItem.Text = "&Words"
        '
        'GenerateToolStripMenuItem
        '
        Me.GenerateToolStripMenuItem.Name = "GenerateToolStripMenuItem"
        Me.GenerateToolStripMenuItem.ShortcutKeyDisplayString = ""
        Me.GenerateToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G), System.Windows.Forms.Keys)
        Me.GenerateToolStripMenuItem.Size = New System.Drawing.Size(302, 22)
        Me.GenerateToolStripMenuItem.Text = "&Generate..."
        '
        'QuickGenerateToolStripMenuItem
        '
        Me.QuickGenerateToolStripMenuItem.Name = "QuickGenerateToolStripMenuItem"
        Me.QuickGenerateToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.G), System.Windows.Forms.Keys)
        Me.QuickGenerateToolStripMenuItem.Size = New System.Drawing.Size(302, 22)
        Me.QuickGenerateToolStripMenuItem.Text = "&Quick generate"
        '
        'QuickClearAndGenerateToolStripMenuItem
        '
        Me.QuickClearAndGenerateToolStripMenuItem.Name = "QuickClearAndGenerateToolStripMenuItem"
        Me.QuickClearAndGenerateToolStripMenuItem.ShortcutKeys = CType((((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
                    Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.G), System.Windows.Forms.Keys)
        Me.QuickClearAndGenerateToolStripMenuItem.Size = New System.Drawing.Size(302, 22)
        Me.QuickClearAndGenerateToolStripMenuItem.Text = "Q&uick clear and generate"
        '
        'ClearListToolStripMenuItem
        '
        Me.ClearListToolStripMenuItem.Name = "ClearListToolStripMenuItem"
        Me.ClearListToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.ClearListToolStripMenuItem.Size = New System.Drawing.Size(302, 22)
        Me.ClearListToolStripMenuItem.Text = "C&lear list"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(299, 6)
        '
        'EditwordToolStripMenuItem
        '
        Me.EditwordToolStripMenuItem.Name = "EditwordToolStripMenuItem"
        Me.EditwordToolStripMenuItem.Size = New System.Drawing.Size(302, 22)
        Me.EditwordToolStripMenuItem.Text = "Edit &word..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(299, 6)
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(302, 22)
        Me.SelectAllToolStripMenuItem.Text = "Select &all words"
        '
        'CopyListToClipboardToolStripMenuItem
        '
        Me.CopyListToClipboardToolStripMenuItem.Name = "CopyListToClipboardToolStripMenuItem"
        Me.CopyListToClipboardToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C"
        Me.CopyListToClipboardToolStripMenuItem.Size = New System.Drawing.Size(302, 22)
        Me.CopyListToClipboardToolStripMenuItem.Text = "&Copy list to clipboard"
        '
        'CopyDetailsToClipboardToolStripMenuItem
        '
        Me.CopyDetailsToClipboardToolStripMenuItem.Name = "CopyDetailsToClipboardToolStripMenuItem"
        Me.CopyDetailsToClipboardToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyDetailsToClipboardToolStripMenuItem.Size = New System.Drawing.Size(302, 22)
        Me.CopyDetailsToClipboardToolStripMenuItem.Text = "C&opy details to clipboard"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(299, 6)
        '
        'AddToDictionaryToolStripMenuItem
        '
        Me.AddToDictionaryToolStripMenuItem.Name = "AddToDictionaryToolStripMenuItem"
        Me.AddToDictionaryToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.AddToDictionaryToolStripMenuItem.Size = New System.Drawing.Size(302, 22)
        Me.AddToDictionaryToolStripMenuItem.Text = "Add to &dictionary"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(299, 6)
        '
        'ExportListToFileToolStripMenuItem
        '
        Me.ExportListToFileToolStripMenuItem.Name = "ExportListToFileToolStripMenuItem"
        Me.ExportListToFileToolStripMenuItem.Size = New System.Drawing.Size(302, 22)
        Me.ExportListToFileToolStripMenuItem.Text = "&Export selection to file..."
        '
        'DictionaryToolStripMenuItem
        '
        Me.DictionaryToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewDictionaryToolStripMenuItem, Me.OpenDictionaryToolStripMenuItem, Me.ToolStripMenuItem3, Me.StatisticsToolStripMenuItem})
        Me.DictionaryToolStripMenuItem.Name = "DictionaryToolStripMenuItem"
        Me.DictionaryToolStripMenuItem.Size = New System.Drawing.Size(73, 20)
        Me.DictionaryToolStripMenuItem.Text = "&Dictionary"
        '
        'NewDictionaryToolStripMenuItem
        '
        Me.NewDictionaryToolStripMenuItem.Name = "NewDictionaryToolStripMenuItem"
        Me.NewDictionaryToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.NewDictionaryToolStripMenuItem.Text = "&New..."
        '
        'OpenDictionaryToolStripMenuItem
        '
        Me.OpenDictionaryToolStripMenuItem.Name = "OpenDictionaryToolStripMenuItem"
        Me.OpenDictionaryToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.OpenDictionaryToolStripMenuItem.Text = "&Open..."
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(149, 6)
        '
        'StatisticsToolStripMenuItem
        '
        Me.StatisticsToolStripMenuItem.Name = "StatisticsToolStripMenuItem"
        Me.StatisticsToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.StatisticsToolStripMenuItem.Text = "&Statistics..."
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FontsToolStripMenuItem})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.OptionsToolStripMenuItem.Text = "&Options"
        '
        'FontsToolStripMenuItem
        '
        Me.FontsToolStripMenuItem.Name = "FontsToolStripMenuItem"
        Me.FontsToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.FontsToolStripMenuItem.Text = "&Fonts..."
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.AboutToolStripMenuItem.Text = "&About..."
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "wordo"
        Me.SaveFileDialog1.Filter = "Project file|*.wordo|All files|*.*"
        Me.SaveFileDialog1.Title = "Save WordBuilder project"
        '
        'OpenFileDialog2
        '
        Me.OpenFileDialog2.DefaultExt = "wdic"
        Me.OpenFileDialog2.Filter = "Dictionary files|*.wdic"
        Me.OpenFileDialog2.Title = "Open WordBuilder dictionary"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "wordo"
        Me.OpenFileDialog1.Filter = "Project file|*.wordo|All files|*.*"
        Me.OpenFileDialog1.Title = "Open WordBuilder project"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(635, 386)
        Me.Controls.Add(Me.ProjectTabControl)
        Me.Controls.Add(Me.ApplicationMenuStrip)
        Me.Controls.Add(Me.ApplicationStatusStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main"
        Me.Text = "WordBuilder"
        Me.ProjectTabControl.ResumeLayout(False)
        Me.GeneratorTabPage.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GeneratorResultsContextMenuStrip.ResumeLayout(False)
        Me.CodePanel.ResumeLayout(False)
        Me.WarningsPanel.ResumeLayout(False)
        Me.WarningsPanel.PerformLayout()
        Me.DictionaryTabPage.ResumeLayout(False)
        Me.DictionaryResultsGroupBox.ResumeLayout(False)
        Me.DictionaryResultsContextMenuStrip.ResumeLayout(False)
        Me.DictionarySearchGroupBox.ResumeLayout(False)
        Me.DictionarySearchGroupBox.PerformLayout()
        Me.TranslatorTabPage.ResumeLayout(False)
        Me.TranslatorTabPage.PerformLayout()
        Me.TranslatorResultContextMenuStrip.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ApplicationMenuStrip.ResumeLayout(False)
        Me.ApplicationMenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ApplicationStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ProjectTabControl As System.Windows.Forms.TabControl
    Friend WithEvents GeneratorTabPage As System.Windows.Forms.TabPage
    Friend WithEvents DictionaryTabPage As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ResultsListBox As System.Windows.Forms.ListView
    Friend WithEvents ResultsListBox_Old As System.Windows.Forms.ListBox
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Friend WithEvents ResultsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents WarningsSplitter As System.Windows.Forms.Splitter
    Friend WithEvents CodePanel As System.Windows.Forms.Panel
    Friend WithEvents WarningsPanel As System.Windows.Forms.Panel
    Friend WithEvents WarningsListBox As System.Windows.Forms.ListBox
    Friend WithEvents WarningsLabel As System.Windows.Forms.Label
    Friend WithEvents CodeTextBox As SyntaxRichTextBox
    Friend WithEvents SaveFileDialog2 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ApplicationMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WordsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QuickGenerateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QuickClearAndGenerateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditwordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyListToClipboardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyDetailsToClipboardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExportListToFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OpenFileDialog2 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents DictionarySearchGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents DictionaryLookupButton As System.Windows.Forms.Button
    Friend WithEvents MeaningTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DictionaryResultsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents DictionaryResultsWebBrowser As System.Windows.Forms.WebBrowser
    Friend WithEvents AddToDictionaryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DictionaryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewDictionaryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OpenDictionaryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DictionaryResultsContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EditWordToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LinkToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DictionarySeparatorToolStripMenuItem As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteWordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteLinkToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DictionaryMarksCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents DictionarySearchWordCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents DictionarySearchMeaningCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents DictionarySearchOperationComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DictionarySearchMarksCheckedListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TranslatorTabPage As System.Windows.Forms.TabPage
    Friend WithEvents TranslatorResultWebBrowser As System.Windows.Forms.WebBrowser
    Friend WithEvents TranslatorSplitter As System.Windows.Forms.Splitter
    Friend WithEvents TranslatorSourceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents TranslateToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents TranslatorResultContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddWordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DictionaryResultEditMeaningToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FontsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GeneratorResultsContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddToDictionaryToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RulesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents EditToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FindToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FindAgainToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TranslateBackToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AddAnotherMeaningToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectAllToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatisticsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
