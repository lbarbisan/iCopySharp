'iCopy - Simple Photocopier
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
Imports System.Globalization
Imports WIA

Public Class SettingsDialog
    Dim localeRootStr As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        SaveSettings()
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Sub SaveSettings()
        If Not (My.Settings.Culture.LCID = cboLanguage.SelectedItem.LCID) Then
            My.Settings.Culture = cboLanguage.SelectedItem
            My.Settings.CustomCulture = True
            MsgBoxWrap(appControl.GetLocalizedString("Msg_Language"), MsgBoxStyle.Information, "iCopy")
        End If

        My.Settings.RememberSettings = chkRememberScanSettings.Checked
        My.Settings.StoreLocation = chkRememberWindowPos.Checked
        My.Settings.CheckForUpdates = chkUpdates.Checked

        My.Settings.PDFAskWhereToSave = rbAskPDF.Checked
        My.Settings.PDFOpenAfterAcquisition = chkOpenPDF.Checked

        My.Settings.FileAskWhereToSave = rbAskFile.Checked
        My.Settings.FileOpenAfterAcquisition = chkOpenFile.Checked

        If txtPathFile.Text.EndsWith("\") Then
            My.Settings.FileSavePath = txtPathFile.Text
        Else
            My.Settings.FileSavePath = txtPathFile.Text + "\"
        End If

        If txtPathPDF.Text.EndsWith("\") Then
            My.Settings.PDFSavePath = txtPathPDF.Text
        Else
            My.Settings.PDFSavePath = txtPathPDF.Text + "\"
        End If

        If cboBitDepth.Text = "Auto" Or cboBitDepth.Text = "" Then
            My.Settings.LastScanSettings.BitDepth = 0
        Else
            My.Settings.LastScanSettings.BitDepth = CInt(cboBitDepth.Text)
        End If

        My.Settings.LastScanSettings.RotateDuplex = chkRotateDuplex.Checked
    End Sub

    Sub LoadSettings()
        localeRootStr = Me.Name & "_"
        Me.Text = appControl.GetLocalizedString(Me.Name)
        'Applies localized strings to the controls
        For Each control As System.Windows.Forms.Control In Me.Controls

            Dim text As String = appControl.GetLocalizedString(localeRootStr & control.Name)
            If text <> "" Then control.Text = text
            ToolTip1.SetToolTip(control, appControl.GetLocalizedString(localeRootStr & control.Name & "ToolTip"))
            For Each subcontrol As System.Windows.Forms.Control In control.Controls
                text = appControl.GetLocalizedString(localeRootStr & subcontrol.Name)
                If text <> "" Then subcontrol.Text = text
                ToolTip1.SetToolTip(subcontrol, appControl.GetLocalizedString(localeRootStr & subcontrol.Name & "ToolTip"))
                For Each subsubcontrol As System.Windows.Forms.Control In subcontrol.Controls
                    text = appControl.GetLocalizedString(localeRootStr & subsubcontrol.Name)
                    If text <> "" Then subsubcontrol.Text = text
                    ToolTip1.SetToolTip(subsubcontrol, appControl.GetLocalizedString(localeRootStr & subsubcontrol.Name & "ToolTip"))
                    For Each subsubsubcontrol As System.Windows.Forms.Control In subsubcontrol.Controls
                        text = appControl.GetLocalizedString(localeRootStr & subsubsubcontrol.Name)
                        If text <> "" Then subsubsubcontrol.Text = text
                        ToolTip1.SetToolTip(subsubsubcontrol, appControl.GetLocalizedString(localeRootStr & subsubsubcontrol.Name & "ToolTip"))
                    Next
                Next
            Next
        Next

        'Populates language combobox

        'Get the available languages
        If appControl.AvailableLanguages.Count = 0 Then appControl.GetAvailableLanguages()
        Dim availableCultures As List(Of CultureInfo) = appControl.AvailableLanguages
        cboLanguage.DataSource = appControl.AvailableLanguages
        cboLanguage.DisplayMember = "EnglishName"


        'Select the current culture from the combo box
        cboLanguage.SelectedItem = Threading.Thread.CurrentThread.CurrentUICulture
        'This happens if iCopy is not translated into the system default culture
        If cboLanguage.SelectedItem Is Nothing Then
            cboLanguage.SelectedItem = CultureInfo.GetCultureInfo("en-US")
        End If

        'Load bit depth settings
        If My.Settings.LastScanSettings.BitDepth = 0 Then
            cboBitDepth.SelectedIndex = 0
        Else
            cboBitDepth.Text = My.Settings.LastScanSettings.BitDepth
        End If


        'Load rotate duplex settings
        chkRotateDuplex.Checked = My.Settings.LastScanSettings.RotateDuplex

        'Load file saving preferences
        rbAskFile.Checked = My.Settings.FileAskWhereToSave
        rbPathFile.Checked = Not My.Settings.FileAskWhereToSave
        rbAskPDF.Checked = My.Settings.PDFAskWhereToSave
        rbPathPDF.Checked = Not My.Settings.PDFAskWhereToSave

        txtPathFile.Text = My.Settings.FileSavePath
        txtPathPDF.Text = My.Settings.PDFSavePath

        chkOpenFile.Checked = My.Settings.FileOpenAfterAcquisition
        chkOpenPDF.Checked = My.Settings.PDFOpenAfterAcquisition


        chkRememberScanSettings.Checked = My.Settings.RememberSettings
        chkRememberWindowPos.Checked = My.Settings.StoreLocation
        chkUpdates.Checked = My.Settings.CheckForUpdates
    End Sub

    Private Sub SettingsDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadSettings()
    End Sub


    Private Sub btnResetScanSettings_Click(sender As System.Object, e As System.EventArgs) Handles btnResetScanSettings.Click
        My.Settings.LastScanSettings = My.Settings.DefaultScanSettings()
    End Sub

    Private Sub btnBrowseFile_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseFile.Click
        Dim fldr As New FolderBrowserDialog
        fldr.SelectedPath = txtPathFile.Text
        Dim res = fldr.ShowDialog()
        If res = Windows.Forms.DialogResult.OK Then
            txtPathFile.Text = fldr.SelectedPath
        End If
    End Sub

    Private Sub btnBrowsePDF_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowsePDF.Click
        Dim fldr As New FolderBrowserDialog
        fldr.SelectedPath = txtPathPDF.Text
        Dim res = fldr.ShowDialog()
        If res = Windows.Forms.DialogResult.OK Then
            txtPathPDF.Text = fldr.SelectedPath
        End If
    End Sub

    Private Sub rbAskFile_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbAskFile.CheckedChanged
        txtPathFile.Enabled = Not rbAskFile.Checked
        btnBrowseFile.Enabled = Not rbAskFile.Checked
    End Sub

    Private Sub rbAskPDF_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbAskPDF.CheckedChanged
        txtPathPDF.Enabled = Not rbAskPDF.Checked
        btnBrowsePDF.Enabled = Not rbAskPDF.Checked
    End Sub

    Private Sub chkRotateDuplex_CheckedChanged(sender As Object, e As EventArgs) Handles chkRotateDuplex.CheckedChanged

    End Sub
End Class

Class WIAEventWrapper
    Implements DeviceEvent

    Dim _ev As WIA.DeviceEvent
    Dim _action As Action

    Sub New(ev As WIA.DeviceEvent)
        _ev = ev
    End Sub

    Public Property currentAction() As Action
        Get
            Return _action
        End Get
        Set(ByVal value As Action)
            _action = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return _ev.Name
    End Function

    Public ReadOnly Property Description As String Implements WIA.IDeviceEvent.Description
        Get
            Return _ev.Description
        End Get
    End Property

    Public ReadOnly Property EventID As String Implements WIA.IDeviceEvent.EventID
        Get
            Return _ev.EventID
        End Get
    End Property

    Public ReadOnly Property Name As String Implements WIA.IDeviceEvent.Name
        Get
            Return _ev.Name
        End Get
    End Property

    Public ReadOnly Property Type As WIA.WiaEventFlag Implements WIA.IDeviceEvent.Type
        Get
            Return _ev.Type
        End Get
    End Property
End Class

Class Action
    Public Sub New(Description As String, Arguments As String)
        _description = Description
        _arguments = Arguments
    End Sub

    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Private _arguments As String
    Public Property Arguments() As String
        Get
            Return _arguments
        End Get
        Set(ByVal value As String)
            _arguments = value
        End Set
    End Property

End Class