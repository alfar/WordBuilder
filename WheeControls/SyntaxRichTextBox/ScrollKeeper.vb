Imports System.Runtime.InteropServices

Public Class ScrollKeeper
    Implements IDisposable

    Private Const SB_HORZ = &H0
    Private Const SB_VERT = &H1
    Private Const WM_HSCROLL = &H114
    Private Const WM_VSCROLL = &H115
    Private Const SB_THUMBPOSITION = 4

    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function GetScrollPos(ByVal hWnd As Integer, ByVal nBar As Integer) As Integer
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function SetScrollPos(ByVal hWnd As IntPtr, ByVal nBar As Integer, ByVal nPos As Integer, ByVal bRedraw As Boolean) As Integer
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function PostMessageA(ByVal hWnd As IntPtr, ByVal nBar As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Boolean
    End Function

    Private _control As Control
    Private _H As Integer
    Private _V As Integer
    Private _capture As Boolean

    Public Sub New(ByVal ctrl As Control, Optional ByVal capture As Boolean = True)
        _control = ctrl
        _H = HScrollPos
        _V = VScrollPos
        _capture = capture
    End Sub

    Public Property HScrollPos() As Integer
        Get
            Return GetScrollPos(_control.Handle, SB_HORZ)
        End Get
        Set(ByVal value As Integer)
            SetScrollPos(_control.Handle, SB_HORZ, value, True)
            PostMessageA(_control.Handle, WM_HSCROLL, SB_THUMBPOSITION + &H10000 * value, 0)
        End Set
    End Property

    Public Property VScrollPos() As Integer
        Get
            Return GetScrollPos(_control.Handle, SB_VERT)
        End Get
        Set(ByVal value As Integer)
            SetScrollPos(_control.Handle, SB_VERT, value, True)
            PostMessageA(_control.Handle, WM_VSCROLL, SB_THUMBPOSITION + &H10000 * value, 0)
        End Set
    End Property

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
            End If

            If _capture Then
                HScrollPos = _H
                VScrollPos = _V
            End If
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
