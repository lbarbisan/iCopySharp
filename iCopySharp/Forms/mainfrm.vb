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

Imports WIA
Imports System.Drawing.Printing

Class mainFrm
    Public Shared frmImageSettings As frmImageSettings
    Public Shared AboutBox As AboutBox
    Public Shared frmOptions As SettingsDialog
    Dim splash As SplashScreen

    Dim intent As WiaImageIntent = My.Settings.LastScanSettings.Intent

    Private VersionCheckThread As New Threading.Thread(AddressOf VersionCheck)
    Dim weburl As String
    Dim LocalizedRootStr As String

    Sub VersionCheck()
        Dim reader As Xml.XmlTextReader
        Dim curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
        Dim newVersion = curVersion
        Try
            reader = New Xml.XmlTextReader(My.Resources.VersionCheckURL)
            reader.MoveToContent()
            If reader.NodeType = Xml.XmlNodeType.Element And reader.Name = Application.ProductName Then
                Dim elementName As String = ""
                While reader.Read()
                    If reader.NodeType = Xml.XmlNodeType.Element Then
                        elementName = reader.Name
                    ElseIf reader.NodeType = Xml.XmlNodeType.Text And reader.HasValue Then
                        Select Case elementName
                            Case "version"
                                newVersion = New Version(reader.Value)
                            Case "url"
                                weburl = reader.Value
                        End Select
                    End If
                End While
            End If
            If curVersion.CompareTo(newVersion) < 0 Then
                VersionStatusLabel.Visible = True
            End If
            My.Settings.LastVersionCheck = Today
        Catch ex As Exception 'File is not available, or internet access missing. Just die without any output
            Trace.WriteLine("Couldn't check lastversion: " + ex.Message)
            Exit Sub
        End Try
    End Sub

    Sub LoadSettings()

        AboutBox = New AboutBox()
        frmOptions = New SettingsDialog()
        frmImageSettings = New frmImageSettings()

        If My.Settings.CheckForUpdates Then
            VersionCheckThread.Start() 'Version check
        End If
        'Loads form location if storelocation is true
        If My.Settings.StoreLocation Then
            Me.Location = My.Settings.Location
        Else
            Me.Location = New Point((Screen.GetBounds(Me).Width - Me.Width) / 2, (Screen.GetBounds(Me).Height - Me.Height) / 2)
        End If

        btnCopy.Image = My.Resources.iCopyBig
        Me.Icon = My.Resources.iCopyIco

        'Set frmImageSettings as child
        Me.AddOwnedForm(frmImageSettings)

        'Applies localized strings to the controls
        For Each control As System.Windows.Forms.Control In Me.Controls
            Dim text As String = appControl.GetLocalizedString(LocalizedRootStr & control.Name)
            If text <> "" Then control.Text = text
            ToolTip1.SetToolTip(control, appControl.GetLocalizedString(LocalizedRootStr & control.Name & "ToolTip"))
        Next

        'Applies localized strings to the menustrip
        For Each strip As ToolStripItem In ScanMenuStrip.Items
            strip.Text = appControl.GetLocalizedString(LocalizedRootStr & strip.Name)
        Next

        'Populates comboboxes
        For i As Integer = 0 To 2
            cboScanMode.Items.Add(appControl.GetLocalizedString(LocalizedRootStr & "cboScanModeItem" & i))
        Next

        For i As Integer = 0 To 1
            cboPrintMode.Items.Add(appControl.GetLocalizedString(LocalizedRootStr & "cboPrintModeItem" & i))
        Next

        'Sets default copies number
        nudNCopie.Controls(1).Text = "1"

        'Loads default printer
        Try
            appControl.Printer.Name = My.Settings.DefaultPrinter
        Catch ex As ArgumentException
            If ex.Message = "Printer name is not valid" Then
                Dim sets As New PrinterSettings()
                appControl.Printer.Name = sets.PrinterName
            End If
        End Try
        My.Settings.DefaultPrinter = appControl.Printer.Name
        PrinterStatusLabel.Text = appControl.Printer.Name

        'Statusbar labels
        ScannerStatusLabel.Image = My.Resources.scanner
        ScannerStatusLabel.Text = appControl.ScannerDescription
        PrinterStatusLabel.Image = My.Resources.printer
        If PrinterStatusLabel.Text.Contains("PDF") Then
            PrinterStatusLabel.Image = My.Resources.pdf_icon
        Else
            PrinterStatusLabel.Image = My.Resources.printer
        End If

        'Loads saved intent setting
        If My.Settings.LastScanSettings.Intent = 4 Or My.Settings.LastScanSettings.Intent = 0 Then
            cboScanMode.SelectedIndex = 2
        Else
            cboScanMode.SelectedIndex = My.Settings.LastScanSettings.Intent - 1
        End If

        'Populates paper sizes combo box
        cboPaperSize.DisplayMember = "PaperName" 'Links 
        For Each pSize As PaperSize In appControl.Printer.PrinterSettings.PaperSizes
            cboPaperSize.Items.Add(pSize)
        Next

        cboPaperSize.Text = My.Settings.PrinterSize 'Sets default paper size as stored in settings
        chkADF.Enabled = appControl.CanUseADF()
        chkDuplex.Enabled = chkADF.Checked And appControl.CanDoDuplex()

        chkADF.Checked = My.Settings.LastScanSettings.UseADF
        chkDuplex.Checked = My.Settings.LastScanSettings.Duplex
        chkPDF.Checked = (My.Settings.LastScanSettings.ScanOutput = ScanOutput.PDF)
        chkMultipage.Checked = My.Settings.LastScanSettings.Multipage
        chkSaveToFile.Checked = (My.Settings.LastScanSettings.ScanOutput = ScanOutput.File)

    End Sub

    Private Sub Hotkeys(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        'Shortcuts
        If e.Control Then 'If CTRL is pressed
            Dim ea As New EventArgs()
            Select Case e.KeyCode
                Case Keys.S 'Copy
                    btnCopy_Click(btnCopy, ea)
                Case Keys.M 'Copy Multiple Pages
                    chkMultipage.Checked = Not chkMultipage.Checked
                Case Keys.F 'Scan to File
                    chkSaveToFile.Checked = Not chkSaveToFile.Checked
                Case Keys.I 'Image settings
                    btnImageSettings_Click(btnImageSettings, ea)
                Case Keys.P 'Scan to PDF
                    chkPDF.Checked = Not chkPDF.Checked
            End Select
        End If
    End Sub

    Private Sub StartSplash()
        splash = New SplashScreen()
        Application.Run(splash)
    End Sub

    Private Sub mainFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If My.Settings.CustomCulture And My.Settings.Culture IsNot Nothing Then Threading.Thread.CurrentThread.CurrentUICulture = My.Settings.Culture

        Me.BringToFront()
        Me.Focus()

        LocalizedRootStr = Me.Name & "_"
        Dim SplashThread As New Threading.Thread(AddressOf StartSplash)
        SplashThread.Start()

        LoadSettings() 'Loads stored settings


        splash.Invoke(New EventHandler(AddressOf splash.KillMe))
        splash.Dispose()
        splash = Nothing

        Me.BringToFront()
        Me.Focus()
    End Sub

    Private Sub mainFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Stores form location
        If My.Settings.StoreLocation Then My.Settings.Location = Me.Location
        'Gets the last used settings and saves them
        My.Settings.LastScanSettings = getScanSettings()
    End Sub

    Private Sub mainFrm_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Move
        If frmImageSettings IsNot Nothing Then
            If frmImageSettings.Visible Then 'Moves the image settings form with main form
                Dim tempLocation As New Point(Me.Location.X + Me.Size.Width, Me.Location.Y)
                If tempLocation.X + frmImageSettings.Width >= Screen.PrimaryScreen.WorkingArea.Width Then
                    frmImageSettings.Location = New Point(Me.Location.X - frmImageSettings.Width, Me.Location.Y)
                Else
                    frmImageSettings.Location = tempLocation
                End If
            End If
        End If
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Me.Enabled = False

        'Starts copy process
        Dim settings As ScanSettings = getScanSettings()

        appControl.Copy(settings)

        Me.Enabled = True
    End Sub

    Private Sub SelScanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScannerStatusLabel.Click, btnSelScanner.Click
        Try 'Tries changing the scanner
            Dim newscannerID As String = appControl.changescanner()
            If newscannerID Is Nothing Then Exit Sub

            If My.Settings.DeviceID <> newscannerID Then
                My.Settings.DeviceID = newscannerID 'if a deviceId is returned, store it
            End If

            ScannerStatusLabel.Text = appControl.ScannerDescription
            chkADF.Enabled = appControl.CanUseADF()
            If frmImageSettings.Visible Then btnImageSettings.PerformClick()
            frmImageSettings.Dispose()
            frmImageSettings = New frmImageSettings()
        Catch ex As ExitException
            'Don't change the scanner
        Catch ex As NullReferenceException

        Catch ex As Exception
            Throw
        End Try
    End Sub

    'Show printer settings
    Private Sub PrintSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintSetup.Click, PrinterStatusLabel.Click
        'Disable printer settings when chkPDF is selected (applies to the status bar click, the button gets disabled)
        If Not chkPDF.Checked Then

            ' Address bug #277: some printers, namely Foxit PDF Printer, don't load the PrintDocument.PageDefaultSettings,
            ' thus replacing previously set options such as PaperSize to their default. This is unexpected user behavior.
            ' We replicate the same behavior as Word, i.e. force the settings chosen from iCopy interface:
            '   * Color
            '   * PaperSize
            Dim tmpPaperSize As PaperSize = cboPaperSize.SelectedItem
            Dim tmpColor As Boolean = appControl.Printer.PageSettings.Color

            appControl.Printer.showPreferences()
            cboPaperSize.Items.Clear()

            appControl.Printer.PageSettings.PaperSize = tmpPaperSize
            appControl.Printer.PageSettings.Color = tmpColor

            'Update paper sizes combo box
            cboPaperSize.DisplayMember = "PaperName" 'Links 
            Dim i As Integer
            For Each pSize As PaperSize In appControl.Printer.PrinterSettings.PaperSizes

                i = cboPaperSize.Items.Add(pSize)
                If pSize.Kind = appControl.Printer.PageSettings.PaperSize.Kind Then
                    cboPaperSize.SelectedIndex = i
                End If

            Next


            My.Settings.DefaultPrinter = appControl.Printer.Name
                PrinterStatusLabel.Text = appControl.Printer.Name
                If PrinterStatusLabel.Text.Contains("PDF") Then
                    PrinterStatusLabel.Image = My.Resources.pdf_icon
                Else
                    PrinterStatusLabel.Image = My.Resources.printer
                End If
            End If
    End Sub

    Private Sub cboScanMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboScanMode.SelectedIndexChanged
        'Changes scanning intent and print mode
        Select Case cboScanMode.SelectedIndex
            Case 0
                intent = WiaImageIntent.ColorIntent
                cboPrintMode.SelectedIndex = 0
            Case 1
                intent = WiaImageIntent.GrayscaleIntent
                cboPrintMode.SelectedIndex = 1
            Case 2
                intent = WiaImageIntent.TextIntent
                cboPrintMode.SelectedIndex = 1
        End Select
    End Sub

    Private Sub cboPrintMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPrintMode.SelectedIndexChanged
        'Changes print mode
        appControl.Printer.PageSettings.Color = Not CBool(cboPrintMode.SelectedIndex) 'If index is 0, returns true
        My.Settings.PrintColor = appControl.Printer.PageSettings.Color
    End Sub


    Private Sub btnImageSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImageSettings.Click

        'Shows / hides image settings form in the correct position
        If frmImageSettings.Visible = False Then
            Dim tempLocation As New Point(Me.Location.X + Me.Size.Width, Me.Location.Y)
            If tempLocation.X + frmImageSettings.Width >= Screen.PrimaryScreen.WorkingArea.Width Then
                frmImageSettings.Location = New Point(Me.Location.X - frmImageSettings.Width, Me.Location.Y)
            Else
                frmImageSettings.Location = tempLocation
            End If

            frmImageSettings.Show()
            btnImageSettings.Text = appControl.GetLocalizedString(LocalizedRootStr & "btnImageSettingsHide")
        Else
            frmImageSettings.Hide()
            btnImageSettings.Text = appControl.GetLocalizedString(LocalizedRootStr & "btnImageSettings")
        End If
    End Sub

    Private Sub llblSettings_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llblSettings.LinkClicked
        frmOptions.ShowDialog()
        frmImageSettings.RefreshSettings()
    End Sub

    Private Sub llblAbout_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llblAbout.LinkClicked
        AboutBox.Show()
    End Sub

    Private Function getScanSettings() As ScanSettings
        Dim opts As ScanSettings
        opts = My.Settings.LastScanSettings
        opts.Path = ""

        Try
            opts.Resolution = Convert.ToInt32(frmImageSettings.cboResolution.Text, Globalization.CultureInfo.InvariantCulture)
        Catch ex As FormatException 'Fixes a bug
            If My.Settings.LastScanSettings.Resolution <> 0 Or (Not Nothing) Then opts.Resolution = My.Settings.LastScanSettings.Resolution
        End Try

        opts.Brightness = frmImageSettings.tbBrightness.Value
        opts.Contrast = frmImageSettings.tbContrast.Value
        opts.Intent = intent
        opts.Preview = chkPreview.Checked
        opts.Quality = frmImageSettings.tbCompression.Value
        opts.Copies = nudNCopie.Value
        opts.Scaling = frmImageSettings.tbScaling.Value
        opts.UseADF = chkADF.Checked
        opts.Duplex = chkDuplex.Checked
        opts.Multipage = chkMultipage.Checked
        opts.Center = frmImageSettings.chkCenter.Checked

        Try 'Fix for https://sourceforge.net/p/icopy/bugs/239/
            opts.PaperSize = appControl.Printer.PrinterSettings.PaperSizes.Item(cboPaperSize.SelectedIndex)
        Catch ex As IndexOutOfRangeException
            opts.PaperSize = appControl.Printer.PrinterSettings.PaperSizes.Item(0)
        End Try

        If chkSaveToFile.Checked Then
            opts.ScanOutput = ScanOutput.File
            If Not My.Settings.FileAskWhereToSave Then
                opts.Path = My.Settings.FileSavePath
            End If
        ElseIf chkPDF.Checked Then
            opts.ScanOutput = ScanOutput.PDF
            If Not My.Settings.PDFAskWhereToSave Then
                opts.Path = My.Settings.PDFSavePath
            End If
        End If

        My.Settings.LastScanSettings = opts
        Return opts
    End Function

    Private Sub cboPaperSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPaperSize.SelectedIndexChanged
        appControl.Printer.PageSettings.PaperSize = appControl.Printer.PrinterSettings.PaperSizes.Item(cboPaperSize.SelectedIndex)
        My.Settings.PrinterSize = cboPaperSize.Text 'Stores value in settings
    End Sub

    Private Sub VersionStatusLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VersionStatusLabel.Click
        Process.Start(weburl)
    End Sub

    Private Sub chkADF_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkADF.CheckedChanged
        chkDuplex.Enabled = chkADF.Checked And appControl.CanDoDuplex()

        If chkADF.Checked Then
            chkPreview.Enabled = False
            chkPreview.Checked = False
        Else
            chkPreview.Enabled = True
            chkDuplex.Checked = False
        End If
    End Sub

    Private Sub Outputchanged(sender As System.Object, e As System.EventArgs) Handles chkSaveToFile.CheckedChanged, chkPDF.CheckedChanged
        If sender Is chkPDF Then
            chkSaveToFile.Checked = chkSaveToFile.Checked And (Not chkPDF.Checked)
        Else
            chkPDF.Checked = chkPDF.Checked And (Not chkSaveToFile.Checked)
        End If

        If chkPDF.Checked Then
            chkSaveToFile.Checked = False
            PrinterStatusLabel.Image = My.Resources.pdf_icon
            'PrinterStatusLabel.Text = "Export to PDF"
            PrinterStatusLabel.Text = appControl.GetLocalizedString(LocalizedRootStr & "PrinterStatusLabel_PDF")
        ElseIf chkSaveToFile.Checked Then
            chkPDF.Checked = False
            PrinterStatusLabel.Image = My.Resources.saveToFile
            'PrinterStatusLabel.Text = "Save to file"
            PrinterStatusLabel.Text = appControl.GetLocalizedString(LocalizedRootStr & "PrinterStatusLabel_File")
        Else
            PrinterStatusLabel.Text = appControl.Printer.Name
            If PrinterStatusLabel.Text.Contains("PDF") Then
                PrinterStatusLabel.Image = My.Resources.pdf_icon
            Else
                PrinterStatusLabel.Image = My.Resources.printer
            End If
            cboPaperSize.Enabled = True
            lblPaperSize.Enabled = True
        End If

        'Enable/disable controls that are unused with the pdf and file modes
        'Print mode
        cboPrintMode.Enabled = Not (chkPDF.Checked Or chkSaveToFile.Checked)
        lblPrinter.Enabled = Not (chkPDF.Checked Or chkSaveToFile.Checked)
        'Paper Size (only for save to file)
        cboPaperSize.Enabled = Not chkSaveToFile.Checked
        lblPaperSize.Enabled = Not chkSaveToFile.Checked
        'Printer setup
        btnPrintSetup.Enabled = Not (chkPDF.Checked Or chkSaveToFile.Checked)
        'N* of copies
        nudNCopie.Enabled = Not (chkPDF.Checked Or chkSaveToFile.Checked)
        lblCopies.Enabled = Not (chkPDF.Checked Or chkSaveToFile.Checked)
        'JPEG compression
        frmImageSettings.lblCompressionLabel.Enabled = chkSaveToFile.Checked
        frmImageSettings.tbCompression.Enabled = chkSaveToFile.Checked
        frmImageSettings.lblCompression.Enabled = chkSaveToFile.Checked
        'Image centering and scaling
        frmImageSettings.chkCenter.Enabled = Not chkSaveToFile.Checked
        frmImageSettings.tbScaling.Enabled = Not chkSaveToFile.Checked
        frmImageSettings.txtScaling.Enabled = Not chkSaveToFile.Checked

    End Sub

    Private Sub nudNCopie_ValueChanged(sender As Object, e As EventArgs) Handles nudNCopie.ValueChanged

    End Sub

    Private Sub lblCopies_Click(sender As Object, e As EventArgs) Handles lblCopies.Click

    End Sub

    Private Sub cboPaperSize_DropDown(sender As Object, e As EventArgs) Handles cboPaperSize.DropDown
        Dim senderComboBox As ComboBox = sender
        Dim width As Integer = senderComboBox.DropDownWidth
        Dim font As Font = senderComboBox.Font

        Dim vertScrollBarWidth As Integer
        'Is there a vertical scroll bar?
        If senderComboBox.Items.Count > senderComboBox.MaxDropDownItems Then
            vertScrollBarWidth = senderComboBox.Items.Count
        Else
            vertScrollBarWidth = 0
        End If

        Dim newWidth As Integer

        For Each obj As Object In senderComboBox.Items
            newWidth = CType(TextRenderer.MeasureText(senderComboBox.GetItemText(obj), font).Width + vertScrollBarWidth, Integer)
            If width < newWidth Then width = newWidth
        Next

        senderComboBox.DropDownWidth = width
    End Sub
End Class
