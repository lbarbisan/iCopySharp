using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace iCopy
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class SettingsDialog : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnOk = new Button();
            btnOk.Click += new EventHandler(OK_Button_Click);
            btnCancel = new Button();
            btnCancel.Click += new EventHandler(Cancel_Button_Click);
            lblLanguage = new Label();
            cboLanguage = new ComboBox();
            chkRememberWindowPos = new CheckBox();
            ToolTip1 = new ToolTip(components);
            lblBitDepth = new Label();
            cboBitDepth = new ComboBox();
            TabControl1 = new TabControl();
            tabGeneral = new TabPage();
            chkRotateDuplex = new CheckBox();
            chkRotateDuplex.CheckedChanged += new EventHandler(chkRotateDuplex_CheckedChanged);
            btnResetScanSettings = new Button();
            btnResetScanSettings.Click += new EventHandler(btnResetScanSettings_Click);
            lblNote = new Label();
            chkUpdates = new CheckBox();
            chkRememberScanSettings = new CheckBox();
            tabFileSettings = new TabPage();
            Panel1 = new Panel();
            lblSaveToPDF = new Label();
            chkOpenPDF = new CheckBox();
            btnBrowsePDF = new Button();
            btnBrowsePDF.Click += new EventHandler(btnBrowsePDF_Click);
            rbAskPDF = new RadioButton();
            rbAskPDF.CheckedChanged += new EventHandler(rbAskPDF_CheckedChanged);
            txtPathPDF = new TextBox();
            rbPathPDF = new RadioButton();
            chkOpenFile = new CheckBox();
            btnBrowseFile = new Button();
            btnBrowseFile.Click += new EventHandler(btnBrowseFile_Click);
            txtPathFile = new TextBox();
            rbPathFile = new RadioButton();
            rbAskFile = new RadioButton();
            rbAskFile.CheckedChanged += new EventHandler(rbAskFile_CheckedChanged);
            lblSaveToFile = new Label();
            TabControl1.SuspendLayout();
            tabGeneral.SuspendLayout();
            tabFileSettings.SuspendLayout();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.Location = new Point(267, 360);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(87, 27);
            btnOk.TabIndex = 0;
            btnOk.Text = "OK";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(361, 360);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(87, 27);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            // 
            // lblLanguage
            // 
            lblLanguage.AutoSize = true;
            lblLanguage.Location = new Point(6, 12);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(74, 20);
            lblLanguage.TabIndex = 1;
            lblLanguage.Text = "Language";
            // 
            // cboLanguage
            // 
            cboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLanguage.FormattingEnabled = true;
            cboLanguage.Location = new Point(234, 9);
            cboLanguage.Name = "cboLanguage";
            cboLanguage.Size = new Size(187, 28);
            cboLanguage.TabIndex = 2;
            // 
            // chkRememberWindowPos
            // 
            chkRememberWindowPos.Checked = true;
            chkRememberWindowPos.CheckState = CheckState.Checked;
            chkRememberWindowPos.Location = new Point(6, 79);
            chkRememberWindowPos.Name = "chkRememberWindowPos";
            chkRememberWindowPos.Size = new Size(342, 20);
            chkRememberWindowPos.TabIndex = 3;
            chkRememberWindowPos.Text = "Remember window position";
            chkRememberWindowPos.UseVisualStyleBackColor = true;
            // 
            // lblBitDepth
            // 
            lblBitDepth.Location = new Point(6, 206);
            lblBitDepth.Name = "lblBitDepth";
            lblBitDepth.Size = new Size(229, 20);
            lblBitDepth.TabIndex = 6;
            lblBitDepth.Text = "Force Bit Depth for Color Mode";
            // 
            // cboBitDepth
            // 
            cboBitDepth.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBitDepth.FormattingEnabled = true;
            cboBitDepth.Items.AddRange(new object[] { "Auto", "8", "16", "24", "32" });
            cboBitDepth.Location = new Point(312, 203);
            cboBitDepth.Name = "cboBitDepth";
            cboBitDepth.Size = new Size(109, 28);
            cboBitDepth.TabIndex = 7;
            // 
            // TabControl1
            // 
            TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TabControl1.Controls.Add(tabGeneral);
            TabControl1.Controls.Add(tabFileSettings);
            TabControl1.Location = new Point(14, 14);
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(435, 339);
            TabControl1.TabIndex = 8;
            // 
            // tabGeneral
            // 
            tabGeneral.Controls.Add(chkRotateDuplex);
            tabGeneral.Controls.Add(btnResetScanSettings);
            tabGeneral.Controls.Add(lblNote);
            tabGeneral.Controls.Add(chkUpdates);
            tabGeneral.Controls.Add(chkRememberScanSettings);
            tabGeneral.Controls.Add(cboLanguage);
            tabGeneral.Controls.Add(lblLanguage);
            tabGeneral.Controls.Add(cboBitDepth);
            tabGeneral.Controls.Add(chkRememberWindowPos);
            tabGeneral.Controls.Add(lblBitDepth);
            tabGeneral.Location = new Point(4, 29);
            tabGeneral.Name = "tabGeneral";
            tabGeneral.Padding = new Padding(3);
            tabGeneral.Size = new Size(427, 281);
            tabGeneral.TabIndex = 0;
            tabGeneral.Text = "General";
            tabGeneral.UseVisualStyleBackColor = true;
            // 
            // chkRotateDuplex
            // 
            chkRotateDuplex.AutoSize = true;
            chkRotateDuplex.Location = new Point(6, 245);
            chkRotateDuplex.Name = "chkRotateDuplex";
            chkRotateDuplex.Size = new Size(165, 24);
            chkRotateDuplex.TabIndex = 12;
            chkRotateDuplex.Text = "Flip duplexed pages";
            chkRotateDuplex.UseVisualStyleBackColor = true;
            // 
            // btnResetScanSettings
            // 
            btnResetScanSettings.Location = new Point(207, 127);
            btnResetScanSettings.Name = "btnResetScanSettings";
            btnResetScanSettings.Size = new Size(214, 28);
            btnResetScanSettings.TabIndex = 11;
            btnResetScanSettings.Text = "Reset to default";
            btnResetScanSettings.UseVisualStyleBackColor = true;
            // 
            // lblNote
            // 
            lblNote.Location = new Point(6, 172);
            lblNote.MaximumSize = new Size(362, 0);
            lblNote.Name = "lblNote";
            lblNote.Size = new Size(362, 0);
            lblNote.TabIndex = 10;
            lblNote.Text = "NOTE Don't change the following setting unless you have problems with the acquire" + "d images";
            // 
            // chkUpdates
            // 
            chkUpdates.AutoSize = true;
            chkUpdates.Location = new Point(6, 105);
            chkUpdates.Name = "chkUpdates";
            chkUpdates.Size = new Size(152, 24);
            chkUpdates.TabIndex = 9;
            chkUpdates.Text = "Check for Updates";
            chkUpdates.UseVisualStyleBackColor = true;
            // 
            // chkRememberScanSettings
            // 
            chkRememberScanSettings.AutoSize = true;
            chkRememberScanSettings.Checked = true;
            chkRememberScanSettings.CheckState = CheckState.Checked;
            chkRememberScanSettings.Location = new Point(6, 49);
            chkRememberScanSettings.Name = "chkRememberScanSettings";
            chkRememberScanSettings.Size = new Size(183, 24);
            chkRememberScanSettings.TabIndex = 8;
            chkRememberScanSettings.Text = "Remeber Scan Settings";
            chkRememberScanSettings.UseVisualStyleBackColor = true;
            // 
            // tabFileSettings
            // 
            tabFileSettings.Controls.Add(chkOpenFile);
            tabFileSettings.Controls.Add(chkOpenPDF);
            tabFileSettings.Controls.Add(btnBrowseFile);
            tabFileSettings.Controls.Add(txtPathFile);
            tabFileSettings.Controls.Add(rbPathFile);
            tabFileSettings.Controls.Add(rbAskFile);
            tabFileSettings.Controls.Add(lblSaveToFile);
            tabFileSettings.Controls.Add(Panel1);
            tabFileSettings.Location = new Point(4, 29);
            tabFileSettings.Name = "tabFileSettings";
            tabFileSettings.Padding = new Padding(3);
            tabFileSettings.Size = new Size(427, 306);
            tabFileSettings.TabIndex = 2;
            tabFileSettings.Text = "tabFileSettings";
            tabFileSettings.UseVisualStyleBackColor = true;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.Controls.Add(lblSaveToPDF);
            Panel1.Controls.Add(btnBrowsePDF);
            Panel1.Controls.Add(rbAskPDF);
            Panel1.Controls.Add(txtPathPDF);
            Panel1.Controls.Add(rbPathPDF);
            Panel1.Location = new Point(0, 151);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(426, 149);
            Panel1.TabIndex = 14;
            // 
            // lblSaveToPDF
            // 
            lblSaveToPDF.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblSaveToPDF.AutoSize = true;
            lblSaveToPDF.Location = new Point(2, 28);
            lblSaveToPDF.Name = "lblSaveToPDF";
            lblSaveToPDF.Size = new Size(99, 20);
            lblSaveToPDF.TabIndex = 7;
            lblSaveToPDF.Text = "lblSaveToPDF";
            // 
            // chkOpenPDF
            // 
            chkOpenPDF.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkOpenPDF.AutoSize = true;
            chkOpenPDF.Location = new Point(3, 276);
            chkOpenPDF.Name = "chkOpenPDF";
            chkOpenPDF.Size = new Size(115, 24);
            chkOpenPDF.TabIndex = 13;
            chkOpenPDF.Text = "chkOpenPDF";
            chkOpenPDF.UseVisualStyleBackColor = true;
            // 
            // btnBrowsePDF
            // 
            btnBrowsePDF.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBrowsePDF.Location = new Point(269, 95);
            btnBrowsePDF.Name = "btnBrowsePDF";
            btnBrowsePDF.Size = new Size(96, 27);
            btnBrowsePDF.TabIndex = 12;
            btnBrowsePDF.Text = "btnBrowsePDF";
            btnBrowsePDF.UseVisualStyleBackColor = true;
            // 
            // rbAskPDF
            // 
            rbAskPDF.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            rbAskPDF.AutoSize = true;
            rbAskPDF.Location = new Point(6, 64);
            rbAskPDF.Name = "rbAskPDF";
            rbAskPDF.Size = new Size(93, 24);
            rbAskPDF.TabIndex = 9;
            rbAskPDF.TabStop = true;
            rbAskPDF.Text = "rbAskPDF";
            rbAskPDF.UseVisualStyleBackColor = true;
            // 
            // txtPathPDF
            // 
            txtPathPDF.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtPathPDF.Location = new Point(3, 95);
            txtPathPDF.Name = "txtPathPDF";
            txtPathPDF.Size = new Size(258, 27);
            txtPathPDF.TabIndex = 11;
            // 
            // rbPathPDF
            // 
            rbPathPDF.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            rbPathPDF.AutoSize = true;
            rbPathPDF.Location = new Point(183, 64);
            rbPathPDF.Name = "rbPathPDF";
            rbPathPDF.Size = new Size(95, 24);
            rbPathPDF.TabIndex = 10;
            rbPathPDF.TabStop = true;
            rbPathPDF.Text = "rbPathFile";
            rbPathPDF.UseVisualStyleBackColor = true;
            // 
            // chkOpenFile
            // 
            chkOpenFile.AutoSize = true;
            chkOpenFile.Location = new Point(3, 110);
            chkOpenFile.Name = "chkOpenFile";
            chkOpenFile.Size = new Size(112, 24);
            chkOpenFile.TabIndex = 6;
            chkOpenFile.Text = "chkOpenFile";
            chkOpenFile.UseVisualStyleBackColor = true;
            // 
            // btnBrowseFile
            // 
            btnBrowseFile.Location = new Point(269, 67);
            btnBrowseFile.Name = "btnBrowseFile";
            btnBrowseFile.Size = new Size(96, 27);
            btnBrowseFile.TabIndex = 5;
            btnBrowseFile.Text = "btnBrowseFile";
            btnBrowseFile.UseVisualStyleBackColor = true;
            // 
            // txtPathFile
            // 
            txtPathFile.Location = new Point(3, 67);
            txtPathFile.Name = "txtPathFile";
            txtPathFile.Size = new Size(258, 27);
            txtPathFile.TabIndex = 4;
            // 
            // rbPathFile
            // 
            rbPathFile.AutoSize = true;
            rbPathFile.Location = new Point(183, 40);
            rbPathFile.Name = "rbPathFile";
            rbPathFile.Size = new Size(95, 24);
            rbPathFile.TabIndex = 3;
            rbPathFile.TabStop = true;
            rbPathFile.Text = "rbPathFile";
            rbPathFile.UseVisualStyleBackColor = true;
            // 
            // rbAskFile
            // 
            rbAskFile.AutoSize = true;
            rbAskFile.Location = new Point(6, 40);
            rbAskFile.Name = "rbAskFile";
            rbAskFile.Size = new Size(90, 24);
            rbAskFile.TabIndex = 2;
            rbAskFile.TabStop = true;
            rbAskFile.Text = "rbAskFile";
            rbAskFile.UseVisualStyleBackColor = true;
            // 
            // lblSaveToFile
            // 
            lblSaveToFile.AutoSize = true;
            lblSaveToFile.Location = new Point(3, 14);
            lblSaveToFile.Name = "lblSaveToFile";
            lblSaveToFile.Size = new Size(96, 20);
            lblSaveToFile.TabIndex = 0;
            lblSaveToFile.Text = "lblSaveToFile";
            // 
            // SettingsDialog
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(8.0f, 20.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(463, 401);
            Controls.Add(TabControl1);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            TabControl1.ResumeLayout(false);
            tabGeneral.ResumeLayout(false);
            tabGeneral.PerformLayout();
            tabFileSettings.ResumeLayout(false);
            tabFileSettings.PerformLayout();
            Panel1.ResumeLayout(false);
            Panel1.PerformLayout();
            Load += new EventHandler(SettingsDialog_Load);
            ResumeLayout(false);

        }
        internal Button btnOk;
        internal Button btnCancel;
        internal Label lblLanguage;
        internal ComboBox cboLanguage;
        internal CheckBox chkRememberWindowPos;
        internal ToolTip ToolTip1;
        internal Label lblBitDepth;
        internal ComboBox cboBitDepth;
        internal TabControl TabControl1;
        internal TabPage tabGeneral;
        internal CheckBox chkRememberScanSettings;
        internal Label lblNote;
        internal CheckBox chkUpdates;
        internal Button btnResetScanSettings;
        internal TabPage tabFileSettings;
        internal Panel Panel1;
        internal Label lblSaveToPDF;
        internal CheckBox chkOpenPDF;
        internal Button btnBrowsePDF;
        internal RadioButton rbAskPDF;
        internal TextBox txtPathPDF;
        internal RadioButton rbPathPDF;
        internal CheckBox chkOpenFile;
        internal Button btnBrowseFile;
        internal TextBox txtPathFile;
        internal RadioButton rbPathFile;
        internal RadioButton rbAskFile;
        internal Label lblSaveToFile;
        internal CheckBox chkRotateDuplex;
    }
}