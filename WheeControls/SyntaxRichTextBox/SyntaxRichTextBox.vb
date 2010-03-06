Imports System.Text.RegularExpressions

Public Class SyntaxRichTextBox
    Inherits RichTextBox

    Private numberLabel As PictureBox

    Public Sub New()
        Me.WordWrap = False
        Me.DetectUrls = False
        numberLabel = New PictureBox
        numberLabel.Width = 40
        numberLabel.Top = 0
        numberLabel.Left = 0
        numberLabel.BackColor = Color.Gray
        Controls.Add(numberLabel)
    End Sub

    Private _CodeFont As Font = New Font("Courier New", 11)
    Public Property CodeFont() As Font
        Get
            Return _CodeFont
        End Get
        Set(ByVal value As Font)
            _CodeFont = value
            Highlight()
            UpdateNumbering()
        End Set
    End Property

    Private _lines As New List(Of String)
    Private _lineLevels As New List(Of Integer)

    Private _Keywords As New Dictionary(Of Integer, List(Of KeywordDefinition))
    Private ReadOnly Property Keywords() As Dictionary(Of Integer, List(Of KeywordDefinition))
        Get
            If _Keywords.Count = 0 Then
                _Keywords.Add(0, New List(Of KeywordDefinition)(New KeywordDefinition() {New KeywordDefinition("^rule ", Color.Blue) _
                                 , New KeywordDefinition("^tokens ", Color.Blue) _
                                 , New KeywordDefinition("^startingrule ", Color.Blue) _
                                 , New KeywordDefinition("^column ", Color.Blue) _
                                 , New KeywordDefinition("\\\{", Color.Red, False, 1) _
                                 , New KeywordDefinition("\/\*", Color.Chocolate, True, 99) _
                                 , New KeywordDefinition("\/\/.*$", Color.Chocolate) _
                                 }))

                _Keywords.Add(1, New List(Of KeywordDefinition)(New KeywordDefinition() {New KeywordDefinition("^\\\}", Color.Red, False, -2) _
                                 , New KeywordDefinition("\/\/.*$", Color.Chocolate) _
                                 , New KeywordDefinition("^\/\*", Color.Chocolate, True, 98)}))

                _Keywords.Add(98, New List(Of KeywordDefinition)(New KeywordDefinition() {New KeywordDefinition("^\*\/", Color.Chocolate, False, -2), New KeywordDefinition(".*", Color.Chocolate)}))
                _Keywords.Add(99, New List(Of KeywordDefinition)(New KeywordDefinition() {New KeywordDefinition("^\*\/", Color.Chocolate, False, -2), New KeywordDefinition(".*", Color.Chocolate)}))
            End If

            Return _Keywords
        End Get
    End Property

    Public Sub CloneKeywords(ByVal source As Integer, ByVal destination As Integer)
        If Keywords.ContainsKey(source) Then
            Dim dest As List(Of KeywordDefinition)
            If Keywords.ContainsKey(destination) Then
                dest = Keywords(destination)
            Else
                dest = New List(Of KeywordDefinition)()
                Keywords.Add(destination, dest)
            End If
            dest.AddRange(Keywords(source))
        End If
    End Sub

    Public Sub AddKeyword(ByVal level As Integer, ByVal regex As String, ByVal color As Color, ByVal nextLevel As Integer)
        Dim dest As List(Of KeywordDefinition)
        If Keywords.ContainsKey(level) Then
            dest = Keywords(level)
        Else
            dest = New List(Of KeywordDefinition)()
            Keywords.Add(level, dest)
        End If
        dest.Add(New KeywordDefinition(regex, color, False, nextLevel))
    End Sub

    Private Function GetColorIndex(ByVal colors As List(Of Color), ByVal color As Color) As String
        Dim pos As Integer = colors.IndexOf(color)
        If pos = -1 Then
            colors.Add(color)
            pos = colors.Count - 1
        End If

        Return String.Format("\cf{0} ", pos + 1)
    End Function

    Private UnicodeRegex As New Regex("[^\x00-\x7f]", RegexOptions.Compiled)

    Private Function FixUnicode(ByVal match As Match) As String
        Dim c As Char = match.Value(0)

        Return String.Format("\u{0}?", AscW(c))
    End Function

    Private Function RTFLine(ByVal line As String, ByRef currentLevel As Integer, ByVal levels As Stack(Of Integer), ByVal colors As List(Of Color))
        line = UnicodeRegex.Replace(line.Replace("\", "\\").Replace("{", "\{").Replace("}", "\}"), AddressOf FixUnicode)

        For Each r As KeywordDefinition In _Keywords(currentLevel)
            Dim m As Match = r.RegEx.Match(line)
            If m.Success Then
                Dim col As String = GetColorIndex(colors, r.Color)
                line = line.Insert(m.Index, col)
                line = line.Insert(m.Index + col.Length + m.Length, "\cf0 ")
                If r.Level >= 0 Then
                    levels.Push(currentLevel)
                    currentLevel = r.Level
                ElseIf r.Level < -1 Then
                    currentLevel = levels.Pop()
                End If
            End If
        Next

        Return "{\pard\li720 " & line
    End Function

    Private LineLevels As New List(Of Integer())
    Private LineCache As New List(Of String)()
    Private LineColors As New List(Of Color())()

    Private Function GetRTF(ByVal startingLine As Integer) As String
        Dim result As New System.Text.StringBuilder()
        Dim colors As New List(Of Color)

        result.Append("{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 " & CodeFont.Name & ";}}\fs" & Math.Floor(CodeFont.SizeInPoints * 2).ToString(System.Globalization.CultureInfo.InvariantCulture) & "\li720")

        Dim outlines As New List(Of String)()
        If startingLine > 0 Then
            outlines.AddRange(LineCache.Take(startingLine))
            colors.AddRange(LineColors(startingLine - 1))
        End If

        Dim levelStack As New Stack(Of Integer)()
        Dim currentLevel As Integer

        If LineLevels.Count > startingLine Then
            For Each lev As Integer In LineLevels(startingLine)
                levelStack.Push(lev)
            Next
            currentLevel = levelStack.Pop()

            LineLevels.RemoveRange(startingLine, LineLevels.Count - startingLine)
            LineCache.RemoveRange(startingLine, LineCache.Count - startingLine)
            LineColors.RemoveRange(startingLine, LineColors.Count - startingLine)
        End If

        For Each line As String In Lines.Skip(startingLine)
            levelStack.Push(currentLevel)
            LineLevels.Add(levelStack.Reverse().ToArray())
            levelStack.Pop()
            Dim rtf As String = RTFLine(line, currentLevel, levelStack, colors)
            LineCache.Add(rtf)
            LineColors.Add(colors.ToArray())
            outlines.Add(rtf)
        Next

        levelStack.Push(currentLevel)
        LineLevels.Add(levelStack.Reverse().ToArray())

        result.Append("{\colortbl ;")
        For Each c As Color In colors
            result.AppendFormat("\red{0}\green{1}\blue{2};", c.R, c.G, c.B)
        Next
        result.Append("}")

        result.Append(String.Join("\par}" & vbCrLf, outlines.ToArray()))
        result.Append("}")
        Return result.ToString()
    End Function

    Private _Highlighting As Boolean = False

    Public Event Highlighted As EventHandler

    Private Sub Highlight()
        _Highlighting = True
        Using New ScrollKeeper(Me)
            Using New RedrawPauser(Me)
                Dim pos As Integer = Me.SelectionStart
                Dim len As Integer = Me.SelectionLength
                If _highlightingStart = -1 Then
                    Me.Rtf = GetRTF(Me.GetLineFromCharIndex(pos))
                Else
                    Me.Rtf = GetRTF(Me.GetLineFromCharIndex(_highlightingStart))
                    _highlightingStart = -1
                End If
                Me.Select(pos, len)
            End Using
        End Using

        Me.Invalidate()
        _Highlighting = False
        RaiseEvent Highlighted(Me, EventArgs.Empty)
    End Sub

    Private _highlightingStart As Integer = -1

    Private Sub SyntaxRichTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            _highlightingStart = Me.GetLineFromCharIndex(Me.SelectionStart)
        End If
    End Sub

    Private Sub SyntaxRichTextBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        numberLabel.Height = Me.Height
    End Sub

    Private Sub SyntaxRichTextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.TextChanged
        If Not _Highlighting Then
            If Lines.Count <> LineCache.Count Then
                _highlightingStart = 0
            End If
            Highlight()
        End If
    End Sub

    Private BackBuffer As Bitmap

    Private Sub UpdateNumbering()
        Dim sk As New ScrollKeeper(Me, False)

        Dim vscroll As Integer = sk.VScrollPos Mod 16
        numberLabel.Top = 0
        numberLabel.Left = 0

        Dim pos As New Point(0, 0)
        Dim firstIndex As Integer = Me.GetCharIndexFromPosition(pos)
        Dim firstLine As Integer = Me.GetLineFromCharIndex(firstIndex)

        pos.X = ClientRectangle.Width
        pos.Y = ClientRectangle.Height
        Dim lastIndex As Integer = Me.GetCharIndexFromPosition(pos)
        Dim lastLine As Integer = Me.GetLineFromCharIndex(lastIndex)

        pos = Me.GetPositionFromCharIndex(lastIndex)

        BackBuffer = New Bitmap(numberLabel.Width, numberLabel.Height)

        Dim g As Graphics = Graphics.FromImage(BackBuffer)
        g.FillRectangle(Brushes.Gray, 0, 0, numberLabel.Width, numberLabel.Height)

        For i As Integer = firstLine To lastLine + 1
            If i < Me.Lines.Count Then
                g.DrawString((i + 1).ToString(), CodeFont, Brushes.Black, 0, Me.GetPositionFromCharIndex(Me.GetFirstCharIndexFromLine(i)).Y)
            End If
        Next

        numberLabel.Image = BackBuffer
    End Sub

    Private Sub SyntaxRichTextBox_VScroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VScroll
        UpdateNumbering()
    End Sub
End Class
