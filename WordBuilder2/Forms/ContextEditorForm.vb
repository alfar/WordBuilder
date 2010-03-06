Imports DictionaryProject

Public Class ContextEditorForm

    Private _context As Context
    Private _project As Project

    Public Sub New(ByVal context As Context, ByVal project As Project)
        InitializeComponent()
        _context = context
        _project = project

        If _project IsNot Nothing Then
            RuleComboBox.Items.AddRange(_project.Rules.Select(Function(r As Rule) r.Name).Distinct().ToArray())
        Else
            RuleComboBox.Enabled = False
            ApplyRuleButton.Enabled = False
        End If

        PopulateTree()
    End Sub

    Private Sub PopulateTree()
        Dim n As New TreeNode(_context.ToString())

        ContextTreeView.Nodes.Clear()
        ContextTreeView.Nodes.Add(n)
        n.ImageKey = "Root"

        PopulateTreeNode(n, _context)

        ContextTreeView.SelectedNode = n
    End Sub

    Private Sub PopulateTreeNode(ByVal node As TreeNode, ByVal context As Context)
        node.Nodes.Clear()
        node.Tag = context

        For Each b As String In context.Branches.Keys
            Dim n As New TreeNode(b & ": " & context.Branches(b).ToString())
            n.ImageKey = "Branch"
            n.Name = node.Name & "\" & b
            node.Nodes.Add(n)
            PopulateTreeNode(n, context.Branches(b))
        Next

        For Each m As String In context.Marks.Keys
            Dim n As New TreeNode(m & ": " & context.Marks(m))
            n.Name = node.Name & "\!" & m
            n.Tag = context
            n.ImageKey = "Mark"
            node.Nodes.Add(n)
        Next
    End Sub

    Public Function GetContext() As Context
        Return _context
    End Function

    Private Sub ContextTreeView_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles ContextTreeView.AfterSelect
        Dim k As String = ContextTreeView.SelectedNode.ImageKey
        Dim c As Context = TryCast(ContextTreeView.SelectedNode.Tag, Context)
        If c IsNot Nothing Then
            LoadingContext = True
            TokensTextBox.Text = ProjectSerializer.SecureList(c.Tokens).TrimEnd()
            If k = "Branch" Then
                BranchNameTextBox.Text = ContextTreeView.SelectedNode.Text.Split(":")(0)
                MarkNameTextBox.Text = ""
                MarkValueTextBox.Text = ""

                RenameBranchButton.Enabled = True
                BranchButton.Enabled = True
                DeleteButton.Enabled = True
                UnmarkButton.Enabled = False
                ApplyRuleButton.Enabled = _project IsNot Nothing
                TokensTextBox.Enabled = True
            ElseIf k = "Mark" Then
                BranchNameTextBox.Text = ""
                Dim parts() As String = ContextTreeView.SelectedNode.Text.Split(":")
                MarkNameTextBox.Text = parts(0)
                MarkValueTextBox.Text = parts(1).Trim()

                RenameBranchButton.Enabled = False
                BranchButton.Enabled = False
                DeleteButton.Enabled = False
                UnmarkButton.Enabled = True
                ApplyRuleButton.Enabled = False
                TokensTextBox.Text = ""
                TokensTextBox.Enabled = False
            Else
                BranchNameTextBox.Text = ""
                MarkNameTextBox.Text = ""
                MarkValueTextBox.Text = ""

                RenameBranchButton.Enabled = False
                BranchButton.Enabled = True
                DeleteButton.Enabled = False
                UnmarkButton.Enabled = False
                ApplyRuleButton.Enabled = _project IsNot Nothing
                TokensTextBox.Enabled = True
            End If
            LoadingContext = False
        End If
    End Sub

    Private Sub RenameBranchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameBranchButton.Click
        Dim k As String = ContextTreeView.SelectedNode.ImageKey
        Dim c As Context = TryCast(ContextTreeView.SelectedNode.Tag, Context)
        Dim parentC As Context = TryCast(ContextTreeView.SelectedNode.Parent.Tag, Context)

        Dim current As String = ContextTreeView.SelectedNode.Text.Split(":")(0)

        parentC.Branches.Remove(current)
        parentC.Branches.Add(BranchNameTextBox.Text, c)

        UpdateNode(ContextTreeView.SelectedNode.Parent, ContextTreeView.SelectedNode.Parent.Name & "\" & BranchNameTextBox.Text)
    End Sub

    Private Sub UpdateNode(ByVal node As TreeNode, Optional ByVal selectPath As String = Nothing)
        Dim p As String = node.Name

        UpdateNodeName(node, TryCast(node.Tag, Context))
        PopulateTreeNode(node, TryCast(node.Tag, Context))

        If Not String.IsNullOrEmpty(selectPath) Then
            SelectNode(selectPath)
        Else
            SelectNode(p)
        End If
    End Sub

    Private Sub SelectNode(ByVal path As String)
        Dim nodes() As TreeNode = ContextTreeView.Nodes.Find(path, True)
        If nodes.Length > 0 Then
            ContextTreeView.SelectedNode = nodes(0)
        End If
    End Sub

    Private Sub BranchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BranchButton.Click
        Dim c As Context = TryCast(ContextTreeView.SelectedNode.Tag, Context)
        Dim childC As Context = c.Branch(BranchNameTextBox.Text)

        PopulateTreeNode(ContextTreeView.SelectedNode, c)
        SelectNode(ContextTreeView.SelectedNode.Name & "\" & BranchNameTextBox.Text)
    End Sub

    Private LoadingContext As Boolean = False

    Private Sub TokensTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TokensTextBox.TextChanged
    End Sub

    Private Sub TokensTextBox_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TokensTextBox.Validated
        If Not LoadingContext Then
            Dim c As Context = TryCast(ContextTreeView.SelectedNode.Tag, Context)

            c.Tokens.Clear()
            If Not String.IsNullOrEmpty(TokensTextBox.Text.Trim()) Then
                c.Tokens.AddRange(ProjectSerializer.ReadTokens(TokensTextBox.Text.Trim()))
            End If

            UpdateNodeName(ContextTreeView.SelectedNode, c)
        End If
    End Sub

    Private Sub UpdateNodeName(ByVal node As TreeNode, ByVal context As Context)
        If ContextTreeView.SelectedNode.ImageKey = "Branch" Then
            Dim current As String = ContextTreeView.SelectedNode.Text.Split(":")(0)
            ContextTreeView.SelectedNode.Text = current & ": " & context.ToString()
        Else
            ContextTreeView.SelectedNode.Text = context.ToString()
        End If
    End Sub

    Private Sub ApplyRuleButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApplyRuleButton.Click
        Dim c As Context = TryCast(ContextTreeView.SelectedNode.Tag, Context)

        If ClearBeforeApplyingCheckBox.Checked Then
            c = New Context()
            If ContextTreeView.SelectedNode.Parent Is Nothing Then
                _context = c
            End If
            ContextTreeView.SelectedNode.Tag = c
        End If

        Dim r As Rule = _project.Rules.GetRuleByName(RuleComboBox.SelectedItem)
        r.Execute(c)

        TokensTextBox.Text = ProjectSerializer.SecureList(c.Tokens).TrimEnd()

        UpdateNode(ContextTreeView.SelectedNode)
    End Sub

    Private Sub DeleteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        Dim c As Context = TryCast(ContextTreeView.SelectedNode.Tag, Context)
        Dim parentC As Context = TryCast(ContextTreeView.SelectedNode.Parent.Tag, Context)

        Dim current As String = ContextTreeView.SelectedNode.Text.Split(":")(0)

        parentC.Branches.Remove(current)

        UpdateNode(ContextTreeView.SelectedNode.Parent, ContextTreeView.SelectedNode.Parent.Name)
    End Sub

    Private Sub MarkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarkButton.Click
        Dim k As String = ContextTreeView.SelectedNode.ImageKey
        Dim c As Context = TryCast(ContextTreeView.SelectedNode.Tag, Context)

        Dim nodeToUpdate As TreeNode = ContextTreeView.SelectedNode

        If k = "Mark" Then
            Dim current As String = ContextTreeView.SelectedNode.Text.Split(":")(0)
            c.Marks.Remove(current)
            nodeToUpdate = ContextTreeView.SelectedNode.Parent
        End If

        c.Mark(MarkNameTextBox.Text, MarkValueTextBox.Text)

        UpdateNode(nodeToUpdate, nodeToUpdate.Name & "\!" & MarkNameTextBox.Text)
    End Sub

    Private Sub UnmarkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnmarkButton.Click
        Dim c As Context = TryCast(ContextTreeView.SelectedNode.Tag, Context)

        Dim nodeToUpdate As TreeNode = ContextTreeView.SelectedNode.Parent

        Dim current As String = ContextTreeView.SelectedNode.Text.Split(":")(0)
        c.Marks.Remove(current)

        UpdateNode(nodeToUpdate, nodeToUpdate.Name)
    End Sub

    Private Sub ContextEditorForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class