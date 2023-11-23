﻿'iCopy - Simple Photocopier
'Copyright (C) 2007-2018 Matteo Rossi

'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.

'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.

'You should have received a copy of the GNU General Public License
'along with this program.  If not, see <http://www.gnu.org/licenses/>.

Imports System.Windows.Forms

Public Class dlgScanMorePages
    Dim localizedRootStr As String

    ''' <summary>
    ''' Shows the dlgScanMorePages as a dialog
    ''' </summary>
    ''' <param name="acquiredPages">The number of pages already acquired. It is shown in the dialog as a reference</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shadows Function ShowDialog(acquiredPages As Integer, owner As IWin32Window) As DialogResult
        lblAcquiredPagesN.Text = acquiredPages
        Return MyBase.ShowDialog(owner)
    End Function
    Public Shadows Function ShowDialog(acquiredPages As Integer) As DialogResult
        lblAcquiredPagesN.Text = acquiredPages
        Return MyBase.ShowDialog(owner)
    End Function

    Private Sub DlgScanMorePages_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        localizedRootStr = Me.Name & "_"
        Me.Text = appControl.GetLocalizedString(Me.Name)
        For Each control As System.Windows.Forms.Control In Me.Controls
            Dim str As String = appControl.GetLocalizedString(localizedRootStr & control.Name)
            If str IsNot Nothing Then
                control.Text = str
            End If

            ToolTip1.SetToolTip(control, appControl.GetLocalizedString(localizedRootStr & control.Name & "ToolTip"))
        Next
    End Sub
End Class
