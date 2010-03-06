Imports System.Runtime.InteropServices

Public Class RedrawPauser
    Implements IDisposable

    Private Const WM_SETREDRAW = &HB

    Private Const WM_USER = &H400
    Private Const EM_GETEVENTMASK = (WM_USER + 59)
    Private Const EM_SETEVENTMASK = (WM_USER + 69)

    <DllImport("user32", CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As IntPtr
    End Function

    Public Sub New(ByVal ctrl As Control)
        _control = ctrl
        SendMessage(ctrl.Handle, WM_SETREDRAW, 0, IntPtr.Zero)
        eventMask = SendMessage(ctrl.Handle, EM_GETEVENTMASK, 0, IntPtr.Zero)
    End Sub

    Private eventMask As IntPtr = IntPtr.Zero
    Private _control As Control
    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
            End If

            SendMessage(_control.Handle, EM_SETEVENTMASK, 0, eventMask)
            SendMessage(_control.Handle, WM_SETREDRAW, 1, IntPtr.Zero)
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
