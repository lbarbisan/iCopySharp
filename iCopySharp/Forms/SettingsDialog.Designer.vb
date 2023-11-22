<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingsDialog
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
        Me.components = New System.ComponentModel.Container()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblLanguage = New System.Windows.Forms.Label()
        Me.cboLanguage = New System.Windows.Forms.ComboBox()
        Me.chkRememberWindowPos = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblBitDepth = New System.Windows.Forms.Label()
        Me.cboBitDepth = New System.Windows.Forms.ComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabGeneral = New System.Windows.Forms.TabPage()
        Me.chkRotateDuplex = New System.Windows.Forms.CheckBox()
        Me.btnResetScanSettings = New System.Windows.Forms.Button()
        Me.lblNote = New System.Windows.Forms.Label()
        Me.chkUpdates = New System.Windows.Forms.CheckBox()
        Me.chkRememberScanSettings = New System.Windows.Forms.CheckBox()
        Me.tabFileSettings = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblSaveToPDF = New System.Windows.Forms.Label()
        Me.chkOpenPDF = New System.Windows.Forms.CheckBox()
        Me.btnBrowsePDF = New System.Windows.Forms.Button()
        Me.rbAskPDF = New System.Windows.Forms.RadioButton()
        Me.txtPathPDF = New System.Windows.Forms.TextBox()
        Me.rbPathPDF = New System.Windows.Forms.RadioButton()
        Me.chkOpenFile = New System.Windows.Forms.CheckBox()
        Me.btnBrowseFile = New System.Windows.Forms.Button()
        Me.txtPathFile = New System.Windows.Forms.TextBox()
        Me.rbPathFile = New System.Windows.Forms.RadioButton()
        Me.rbAskFile = New System.Windows.Forms.RadioButton()
        Me.lblSaveToFile = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.tabGeneral.SuspendLayout()
        Me.tabFileSettings.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.Location = New System.Drawing.Point(267, 360)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(87, 27)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(361, 360)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(87, 27)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'lblLanguage
        '
        Me.lblLanguage.AutoSize = True
        Me.lblLanguage.Location = New System.Drawing.Point(6, 12)
        Me.lblLanguage.Name = "lblLanguage"
        Me.lblLanguage.Size = New System.Drawing.Size(74, 20)
        Me.lblLanguage.TabIndex = 1
        Me.lblLanguage.Text = "Language"
        '
        'cboLanguage
        '
        Me.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLanguage.FormattingEnabled = True
        Me.cboLanguage.Location = New System.Drawing.Point(234, 9)
        Me.cboLanguage.Name = "cboLanguage"
        Me.cboLanguage.Size = New System.Drawing.Size(187, 28)
        Me.cboLanguage.TabIndex = 2
        '
        'chkRememberWindowPos
        '
        Me.chkRememberWindowPos.Checked = True
        Me.chkRememberWindowPos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRememberWindowPos.Location = New System.Drawing.Point(6, 79)
        Me.chkRememberWindowPos.Name = "chkRememberWindowPos"
        Me.chkRememberWindowPos.Size = New System.Drawing.Size(342, 20)
        Me.chkRememberWindowPos.TabIndex = 3
        Me.chkRememberWindowPos.Text = "Remember window position"
        Me.chkRememberWindowPos.UseVisualStyleBackColor = True
        '
        'lblBitDepth
        '
        Me.lblBitDepth.Location = New System.Drawing.Point(6, 206)
        Me.lblBitDepth.Name = "lblBitDepth"
        Me.lblBitDepth.Size = New System.Drawing.Size(229, 20)
        Me.lblBitDepth.TabIndex = 6
        Me.lblBitDepth.Text = "Force Bit Depth for Color Mode"
        '
        'cboBitDepth
        '
        Me.cboBitDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBitDepth.FormattingEnabled = True
        Me.cboBitDepth.Items.AddRange(New Object() {"Auto", "8", "16", "24", "32"})
        Me.cboBitDepth.Location = New System.Drawing.Point(312, 203)
        Me.cboBitDepth.Name = "cboBitDepth"
        Me.cboBitDepth.Size = New System.Drawing.Size(109, 28)
        Me.cboBitDepth.TabIndex = 7
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tabGeneral)
        Me.TabControl1.Controls.Add(Me.tabFileSettings)
        Me.TabControl1.Location = New System.Drawing.Point(14, 14)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(435, 339)
        Me.TabControl1.TabIndex = 8
        '
        'tabGeneral
        '
        Me.tabGeneral.Controls.Add(Me.chkRotateDuplex)
        Me.tabGeneral.Controls.Add(Me.btnResetScanSettings)
        Me.tabGeneral.Controls.Add(Me.lblNote)
        Me.tabGeneral.Controls.Add(Me.chkUpdates)
        Me.tabGeneral.Controls.Add(Me.chkRememberScanSettings)
        Me.tabGeneral.Controls.Add(Me.cboLanguage)
        Me.tabGeneral.Controls.Add(Me.lblLanguage)
        Me.tabGeneral.Controls.Add(Me.cboBitDepth)
        Me.tabGeneral.Controls.Add(Me.chkRememberWindowPos)
        Me.tabGeneral.Controls.Add(Me.lblBitDepth)
        Me.tabGeneral.Location = New System.Drawing.Point(4, 29)
        Me.tabGeneral.Name = "tabGeneral"
        Me.tabGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.tabGeneral.Size = New System.Drawing.Size(427, 281)
        Me.tabGeneral.TabIndex = 0
        Me.tabGeneral.Text = "General"
        Me.tabGeneral.UseVisualStyleBackColor = True
        '
        'chkRotateDuplex
        '
        Me.chkRotateDuplex.AutoSize = True
        Me.chkRotateDuplex.Location = New System.Drawing.Point(6, 245)
        Me.chkRotateDuplex.Name = "chkRotateDuplex"
        Me.chkRotateDuplex.Size = New System.Drawing.Size(165, 24)
        Me.chkRotateDuplex.TabIndex = 12
        Me.chkRotateDuplex.Text = "Flip duplexed pages"
        Me.chkRotateDuplex.UseVisualStyleBackColor = True
        '
        'btnResetScanSettings
        '
        Me.btnResetScanSettings.Location = New System.Drawing.Point(207, 127)
        Me.btnResetScanSettings.Name = "btnResetScanSettings"
        Me.btnResetScanSettings.Size = New System.Drawing.Size(214, 28)
        Me.btnResetScanSettings.TabIndex = 11
        Me.btnResetScanSettings.Text = "Reset to default"
        Me.btnResetScanSettings.UseVisualStyleBackColor = True
        '
        'lblNote
        '
        Me.lblNote.Location = New System.Drawing.Point(6, 172)
        Me.lblNote.MaximumSize = New System.Drawing.Size(362, 0)
        Me.lblNote.Name = "lblNote"
        Me.lblNote.Size = New System.Drawing.Size(362, 0)
        Me.lblNote.TabIndex = 10
        Me.lblNote.Text = "NOTE Don't change the following setting unless you have problems with the acquire" &
    "d images"
        '
        'chkUpdates
        '
        Me.chkUpdates.AutoSize = True
        Me.chkUpdates.Location = New System.Drawing.Point(6, 105)
        Me.chkUpdates.Name = "chkUpdates"
        Me.chkUpdates.Size = New System.Drawing.Size(152, 24)
        Me.chkUpdates.TabIndex = 9
        Me.chkUpdates.Text = "Check for Updates"
        Me.chkUpdates.UseVisualStyleBackColor = True
        '
        'chkRememberScanSettings
        '
        Me.chkRememberScanSettings.AutoSize = True
        Me.chkRememberScanSettings.Checked = True
        Me.chkRememberScanSettings.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRememberScanSettings.Location = New System.Drawing.Point(6, 49)
        Me.chkRememberScanSettings.Name = "chkRememberScanSettings"
        Me.chkRememberScanSettings.Size = New System.Drawing.Size(183, 24)
        Me.chkRememberScanSettings.TabIndex = 8
        Me.chkRememberScanSettings.Text = "Remeber Scan Settings"
        Me.chkRememberScanSettings.UseVisualStyleBackColor = True
        '
        'tabFileSettings
        '
        Me.tabFileSettings.Controls.Add(Me.chkOpenFile)
        Me.tabFileSettings.Controls.Add(Me.chkOpenPDF)
        Me.tabFileSettings.Controls.Add(Me.btnBrowseFile)
        Me.tabFileSettings.Controls.Add(Me.txtPathFile)
        Me.tabFileSettings.Controls.Add(Me.rbPathFile)
        Me.tabFileSettings.Controls.Add(Me.rbAskFile)
        Me.tabFileSettings.Controls.Add(Me.lblSaveToFile)
        Me.tabFileSettings.Controls.Add(Me.Panel1)
        Me.tabFileSettings.Location = New System.Drawing.Point(4, 29)
        Me.tabFileSettings.Name = "tabFileSettings"
        Me.tabFileSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFileSettings.Size = New System.Drawing.Size(427, 306)
        Me.tabFileSettings.TabIndex = 2
        Me.tabFileSettings.Text = "tabFileSettings"
        Me.tabFileSettings.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.lblSaveToPDF)
        Me.Panel1.Controls.Add(Me.btnBrowsePDF)
        Me.Panel1.Controls.Add(Me.rbAskPDF)
        Me.Panel1.Controls.Add(Me.txtPathPDF)
        Me.Panel1.Controls.Add(Me.rbPathPDF)
        Me.Panel1.Location = New System.Drawing.Point(0, 151)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(426, 149)
        Me.Panel1.TabIndex = 14
        '
        'lblSaveToPDF
        '
        Me.lblSaveToPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSaveToPDF.AutoSize = True
        Me.lblSaveToPDF.Location = New System.Drawing.Point(2, 28)
        Me.lblSaveToPDF.Name = "lblSaveToPDF"
        Me.lblSaveToPDF.Size = New System.Drawing.Size(99, 20)
        Me.lblSaveToPDF.TabIndex = 7
        Me.lblSaveToPDF.Text = "lblSaveToPDF"
        '
        'chkOpenPDF
        '
        Me.chkOpenPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkOpenPDF.AutoSize = True
        Me.chkOpenPDF.Location = New System.Drawing.Point(3, 276)
        Me.chkOpenPDF.Name = "chkOpenPDF"
        Me.chkOpenPDF.Size = New System.Drawing.Size(115, 24)
        Me.chkOpenPDF.TabIndex = 13
        Me.chkOpenPDF.Text = "chkOpenPDF"
        Me.chkOpenPDF.UseVisualStyleBackColor = True
        '
        'btnBrowsePDF
        '
        Me.btnBrowsePDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBrowsePDF.Location = New System.Drawing.Point(269, 95)
        Me.btnBrowsePDF.Name = "btnBrowsePDF"
        Me.btnBrowsePDF.Size = New System.Drawing.Size(96, 27)
        Me.btnBrowsePDF.TabIndex = 12
        Me.btnBrowsePDF.Text = "btnBrowsePDF"
        Me.btnBrowsePDF.UseVisualStyleBackColor = True
        '
        'rbAskPDF
        '
        Me.rbAskPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbAskPDF.AutoSize = True
        Me.rbAskPDF.Location = New System.Drawing.Point(6, 64)
        Me.rbAskPDF.Name = "rbAskPDF"
        Me.rbAskPDF.Size = New System.Drawing.Size(93, 24)
        Me.rbAskPDF.TabIndex = 9
        Me.rbAskPDF.TabStop = True
        Me.rbAskPDF.Text = "rbAskPDF"
        Me.rbAskPDF.UseVisualStyleBackColor = True
        '
        'txtPathPDF
        '
        Me.txtPathPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtPathPDF.Location = New System.Drawing.Point(3, 95)
        Me.txtPathPDF.Name = "txtPathPDF"
        Me.txtPathPDF.Size = New System.Drawing.Size(258, 27)
        Me.txtPathPDF.TabIndex = 11
        '
        'rbPathPDF
        '
        Me.rbPathPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbPathPDF.AutoSize = True
        Me.rbPathPDF.Location = New System.Drawing.Point(183, 64)
        Me.rbPathPDF.Name = "rbPathPDF"
        Me.rbPathPDF.Size = New System.Drawing.Size(95, 24)
        Me.rbPathPDF.TabIndex = 10
        Me.rbPathPDF.TabStop = True
        Me.rbPathPDF.Text = "rbPathFile"
        Me.rbPathPDF.UseVisualStyleBackColor = True
        '
        'chkOpenFile
        '
        Me.chkOpenFile.AutoSize = True
        Me.chkOpenFile.Location = New System.Drawing.Point(3, 110)
        Me.chkOpenFile.Name = "chkOpenFile"
        Me.chkOpenFile.Size = New System.Drawing.Size(112, 24)
        Me.chkOpenFile.TabIndex = 6
        Me.chkOpenFile.Text = "chkOpenFile"
        Me.chkOpenFile.UseVisualStyleBackColor = True
        '
        'btnBrowseFile
        '
        Me.btnBrowseFile.Location = New System.Drawing.Point(269, 67)
        Me.btnBrowseFile.Name = "btnBrowseFile"
        Me.btnBrowseFile.Size = New System.Drawing.Size(96, 27)
        Me.btnBrowseFile.TabIndex = 5
        Me.btnBrowseFile.Text = "btnBrowseFile"
        Me.btnBrowseFile.UseVisualStyleBackColor = True
        '
        'txtPathFile
        '
        Me.txtPathFile.Location = New System.Drawing.Point(3, 67)
        Me.txtPathFile.Name = "txtPathFile"
        Me.txtPathFile.Size = New System.Drawing.Size(258, 27)
        Me.txtPathFile.TabIndex = 4
        '
        'rbPathFile
        '
        Me.rbPathFile.AutoSize = True
        Me.rbPathFile.Location = New System.Drawing.Point(183, 40)
        Me.rbPathFile.Name = "rbPathFile"
        Me.rbPathFile.Size = New System.Drawing.Size(95, 24)
        Me.rbPathFile.TabIndex = 3
        Me.rbPathFile.TabStop = True
        Me.rbPathFile.Text = "rbPathFile"
        Me.rbPathFile.UseVisualStyleBackColor = True
        '
        'rbAskFile
        '
        Me.rbAskFile.AutoSize = True
        Me.rbAskFile.Location = New System.Drawing.Point(6, 40)
        Me.rbAskFile.Name = "rbAskFile"
        Me.rbAskFile.Size = New System.Drawing.Size(90, 24)
        Me.rbAskFile.TabIndex = 2
        Me.rbAskFile.TabStop = True
        Me.rbAskFile.Text = "rbAskFile"
        Me.rbAskFile.UseVisualStyleBackColor = True
        '
        'lblSaveToFile
        '
        Me.lblSaveToFile.AutoSize = True
        Me.lblSaveToFile.Location = New System.Drawing.Point(3, 14)
        Me.lblSaveToFile.Name = "lblSaveToFile"
        Me.lblSaveToFile.Size = New System.Drawing.Size(96, 20)
        Me.lblSaveToFile.TabIndex = 0
        Me.lblSaveToFile.Text = "lblSaveToFile"
        '
        'SettingsDialog
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(463, 401)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SettingsDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.TabControl1.ResumeLayout(False)
        Me.tabGeneral.ResumeLayout(False)
        Me.tabGeneral.PerformLayout()
        Me.tabFileSettings.ResumeLayout(False)
        Me.tabFileSettings.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblLanguage As System.Windows.Forms.Label
    Friend WithEvents cboLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents chkRememberWindowPos As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblBitDepth As System.Windows.Forms.Label
    Friend WithEvents cboBitDepth As System.Windows.Forms.ComboBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabGeneral As System.Windows.Forms.TabPage
    Friend WithEvents chkRememberScanSettings As System.Windows.Forms.CheckBox
    Friend WithEvents lblNote As System.Windows.Forms.Label
    Friend WithEvents chkUpdates As System.Windows.Forms.CheckBox
    Friend WithEvents btnResetScanSettings As System.Windows.Forms.Button
    Friend WithEvents tabFileSettings As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblSaveToPDF As System.Windows.Forms.Label
    Friend WithEvents chkOpenPDF As System.Windows.Forms.CheckBox
    Friend WithEvents btnBrowsePDF As System.Windows.Forms.Button
    Friend WithEvents rbAskPDF As System.Windows.Forms.RadioButton
    Friend WithEvents txtPathPDF As System.Windows.Forms.TextBox
    Friend WithEvents rbPathPDF As System.Windows.Forms.RadioButton
    Friend WithEvents chkOpenFile As System.Windows.Forms.CheckBox
    Friend WithEvents btnBrowseFile As System.Windows.Forms.Button
    Friend WithEvents txtPathFile As System.Windows.Forms.TextBox
    Friend WithEvents rbPathFile As System.Windows.Forms.RadioButton
    Friend WithEvents rbAskFile As System.Windows.Forms.RadioButton
    Friend WithEvents lblSaveToFile As System.Windows.Forms.Label
    Friend WithEvents chkRotateDuplex As CheckBox
End Class
