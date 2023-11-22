using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace iCopy
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    internal partial class mainFrm : Form
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
            btnCopy = new Button();
            btnCopy.Click += new EventHandler(btnCopy_Click);
            btnSelScanner = new Button();
            btnSelScanner.Click += new EventHandler(SelScanner_Click);
            cboPrintMode = new ComboBox();
            cboPrintMode.SelectedIndexChanged += new EventHandler(cboPrintMode_SelectedIndexChanged);
            lblPrinter = new Label();
            lblScanner = new Label();
            cboScanMode = new ComboBox();
            cboScanMode.SelectedIndexChanged += new EventHandler(cboScanMode_SelectedIndexChanged);
            btnPrintSetup = new Button();
            btnPrintSetup.Click += new EventHandler(PrintSetup_Click);
            nudNCopie = new NumericUpDown();
            nudNCopie.ValueChanged += new EventHandler(nudNCopie_ValueChanged);
            lblCopies = new Label();
            lblCopies.Click += new EventHandler(lblCopies_Click);
            StatusStrip1 = new StatusStrip();
            ScannerStatusLabel = new ToolStripStatusLabel();
            ScannerStatusLabel.Click += new EventHandler(SelScanner_Click);
            PrinterStatusLabel = new ToolStripStatusLabel();
            PrinterStatusLabel.Click += new EventHandler(PrintSetup_Click);
            VersionStatusLabel = new ToolStripStatusLabel();
            VersionStatusLabel.Click += new EventHandler(VersionStatusLabel_Click);
            ToolTip1 = new ToolTip(components);
            llblAbout = new LinkLabel();
            llblAbout.LinkClicked += new LinkLabelLinkClickedEventHandler(llblAbout_LinkClicked);
            llblSettings = new LinkLabel();
            llblSettings.LinkClicked += new LinkLabelLinkClickedEventHandler(llblSettings_LinkClicked);
            btnImageSettings = new Button();
            btnImageSettings.Click += new EventHandler(btnImageSettings_Click);
            ScanMenuStrip = new ContextMenuStrip(components);
            ScanMultiplePages = new ToolStripMenuItem();
            ScanToFile = new ToolStripMenuItem();
            cboPaperSize = new ComboBox();
            cboPaperSize.SelectedIndexChanged += new EventHandler(cboPaperSize_SelectedIndexChanged);
            cboPaperSize.DropDown += new EventHandler(cboPaperSize_DropDown);
            lblPaperSize = new Label();
            chkADF = new CheckBox();
            chkADF.CheckedChanged += new EventHandler(chkADF_CheckedChanged);
            chkDuplex = new CheckBox();
            chkMultipage = new CheckBox();
            chkSaveToFile = new CheckBox();
            chkSaveToFile.CheckedChanged += new EventHandler(Outputchanged);
            chkPDF = new CheckBox();
            chkPDF.CheckedChanged += new EventHandler(Outputchanged);
            chkPreview = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)nudNCopie).BeginInit();
            StatusStrip1.SuspendLayout();
            ScanMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // btnCopy
            // 
            btnCopy.Image = My.Resources.Resources.iCopyBig;
            btnCopy.Location = new Point(12, 12);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(161, 155);
            btnCopy.TabIndex = 0;
            btnCopy.UseVisualStyleBackColor = true;
            // 
            // btnSelScanner
            // 
            btnSelScanner.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSelScanner.Location = new Point(552, 12);
            btnSelScanner.Name = "btnSelScanner";
            btnSelScanner.Size = new Size(169, 29);
            btnSelScanner.TabIndex = 7;
            btnSelScanner.Text = "btnSelScanner";
            btnSelScanner.UseVisualStyleBackColor = true;
            // 
            // cboPrintMode
            // 
            cboPrintMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cboPrintMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPrintMode.FormattingEnabled = true;
            cboPrintMode.Location = new Point(409, 47);
            cboPrintMode.Name = "cboPrintMode";
            cboPrintMode.Size = new Size(124, 28);
            cboPrintMode.TabIndex = 3;
            // 
            // lblPrinter
            // 
            lblPrinter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPrinter.Location = new Point(231, 50);
            lblPrinter.Name = "lblPrinter";
            lblPrinter.Size = new Size(172, 21);
            lblPrinter.TabIndex = 2;
            lblPrinter.Text = "lblPrinter";
            lblPrinter.TextAlign = ContentAlignment.TopRight;
            // 
            // lblScanner
            // 
            lblScanner.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblScanner.Location = new Point(228, 16);
            lblScanner.Name = "lblScanner";
            lblScanner.Size = new Size(175, 21);
            lblScanner.TabIndex = 1;
            lblScanner.Text = "lblScanner";
            lblScanner.TextAlign = ContentAlignment.TopRight;
            // 
            // cboScanMode
            // 
            cboScanMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cboScanMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboScanMode.FormattingEnabled = true;
            cboScanMode.Location = new Point(409, 13);
            cboScanMode.Name = "cboScanMode";
            cboScanMode.Size = new Size(124, 28);
            cboScanMode.TabIndex = 2;
            // 
            // btnPrintSetup
            // 
            btnPrintSetup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPrintSetup.Location = new Point(552, 46);
            btnPrintSetup.Name = "btnPrintSetup";
            btnPrintSetup.Size = new Size(169, 29);
            btnPrintSetup.TabIndex = 6;
            btnPrintSetup.Text = "btnPrintSetup";
            btnPrintSetup.UseVisualStyleBackColor = true;
            // 
            // nudNCopie
            // 
            nudNCopie.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nudNCopie.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            nudNCopie.Location = new Point(409, 120);
            nudNCopie.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudNCopie.Name = "nudNCopie";
            nudNCopie.Size = new Size(64, 24);
            nudNCopie.TabIndex = 1;
            nudNCopie.TextAlign = HorizontalAlignment.Right;
            nudNCopie.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblCopies
            // 
            lblCopies.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCopies.Cursor = Cursors.Default;
            lblCopies.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCopies.Location = new Point(228, 120);
            lblCopies.Name = "lblCopies";
            lblCopies.Size = new Size(175, 22);
            lblCopies.TabIndex = 12;
            lblCopies.Text = "lblCopies";
            lblCopies.TextAlign = ContentAlignment.MiddleRight;
            // 
            // StatusStrip1
            // 
            StatusStrip1.ImageScalingSize = new Size(20, 20);
            StatusStrip1.Items.AddRange(new ToolStripItem[] { ScannerStatusLabel, PrinterStatusLabel, VersionStatusLabel });
            StatusStrip1.Location = new Point(0, 253);
            StatusStrip1.Name = "StatusStrip1";
            StatusStrip1.Padding = new Padding(1, 0, 16, 0);
            StatusStrip1.Size = new Size(733, 26);
            StatusStrip1.SizingGrip = false;
            StatusStrip1.TabIndex = 20;
            StatusStrip1.Text = "StatusStrip1";
            // 
            // ScannerStatusLabel
            // 
            ScannerStatusLabel.Image = My.Resources.Resources.scanner;
            ScannerStatusLabel.Margin = new Padding(0, 3, 6, 2);
            ScannerStatusLabel.Name = "ScannerStatusLabel";
            ScannerStatusLabel.Size = new Size(157, 21);
            ScannerStatusLabel.Text = "ScannerStatusLabel";
            // 
            // PrinterStatusLabel
            // 
            PrinterStatusLabel.Image = My.Resources.Resources.printer;
            PrinterStatusLabel.Name = "PrinterStatusLabel";
            PrinterStatusLabel.Size = new Size(148, 20);
            PrinterStatusLabel.Text = "PrinterStatusLabel";
            // 
            // VersionStatusLabel
            // 
            VersionStatusLabel.IsLink = true;
            VersionStatusLabel.Name = "VersionStatusLabel";
            VersionStatusLabel.Size = new Size(405, 20);
            VersionStatusLabel.Spring = true;
            VersionStatusLabel.Text = "New Version Available!";
            VersionStatusLabel.TextAlign = ContentAlignment.MiddleRight;
            VersionStatusLabel.Visible = false;
            // 
            // ToolTip1
            // 
            ToolTip1.AutomaticDelay = 300;
            // 
            // llblAbout
            // 
            llblAbout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            llblAbout.Location = new Point(539, 219);
            llblAbout.Name = "llblAbout";
            llblAbout.Size = new Size(182, 24);
            llblAbout.TabIndex = 10;
            llblAbout.TabStop = true;
            llblAbout.Text = "llblAbout";
            llblAbout.TextAlign = ContentAlignment.MiddleRight;
            // 
            // llblSettings
            // 
            llblSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            llblSettings.Location = new Point(553, 190);
            llblSettings.Name = "llblSettings";
            llblSettings.Size = new Size(168, 30);
            llblSettings.TabIndex = 9;
            llblSettings.TabStop = true;
            llblSettings.Text = "llblSettings";
            llblSettings.TextAlign = ContentAlignment.TopRight;
            // 
            // btnImageSettings
            // 
            btnImageSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnImageSettings.Location = new Point(552, 103);
            btnImageSettings.Name = "btnImageSettings";
            btnImageSettings.Size = new Size(169, 64);
            btnImageSettings.TabIndex = 5;
            btnImageSettings.Text = "btnImageSettings";
            btnImageSettings.UseVisualStyleBackColor = true;
            // 
            // ScanMenuStrip
            // 
            ScanMenuStrip.ImageScalingSize = new Size(20, 20);
            ScanMenuStrip.Items.AddRange(new ToolStripItem[] { ScanMultiplePages, ScanToFile });
            ScanMenuStrip.Name = "ScanMenuStrip";
            ScanMenuStrip.Size = new Size(270, 52);
            // 
            // ScanMultiplePages
            // 
            ScanMultiplePages.Name = "ScanMultiplePages";
            ScanMultiplePages.ShortcutKeyDisplayString = "Ctrl +M";
            ScanMultiplePages.ShortcutKeys = Keys.Control | Keys.M;
            ScanMultiplePages.Size = new Size(269, 24);
            ScanMultiplePages.Text = "Scan Multiple Pages";
            // 
            // ScanToFile
            // 
            ScanToFile.Name = "ScanToFile";
            ScanToFile.ShortcutKeyDisplayString = "Ctrl+F";
            ScanToFile.ShortcutKeys = Keys.Control | Keys.F;
            ScanToFile.Size = new Size(269, 24);
            ScanToFile.Text = "Scan to &File";
            // 
            // cboPaperSize
            // 
            cboPaperSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cboPaperSize.AutoCompleteMode = AutoCompleteMode.Append;
            cboPaperSize.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboPaperSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPaperSize.DropDownWidth = 150;
            cboPaperSize.FormattingEnabled = true;
            cboPaperSize.Location = new Point(409, 81);
            cboPaperSize.Name = "cboPaperSize";
            cboPaperSize.Size = new Size(124, 28);
            cboPaperSize.TabIndex = 21;
            // 
            // lblPaperSize
            // 
            lblPaperSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPaperSize.Location = new Point(228, 84);
            lblPaperSize.Name = "lblPaperSize";
            lblPaperSize.Size = new Size(175, 27);
            lblPaperSize.TabIndex = 22;
            lblPaperSize.Text = "lblPaperSize";
            lblPaperSize.TextAlign = ContentAlignment.TopRight;
            // 
            // chkADF
            // 
            chkADF.AutoSize = true;
            chkADF.Location = new Point(204, 190);
            chkADF.Name = "chkADF";
            chkADF.Size = new Size(81, 24);
            chkADF.TabIndex = 23;
            chkADF.Text = "chkADF";
            chkADF.UseVisualStyleBackColor = true;
            // 
            // chkDuplex
            // 
            chkDuplex.AutoSize = true;
            chkDuplex.Location = new Point(204, 220);
            chkDuplex.Name = "chkDuplex";
            chkDuplex.Size = new Size(100, 24);
            chkDuplex.TabIndex = 24;
            chkDuplex.Text = "chkDuplex";
            chkDuplex.UseVisualStyleBackColor = true;
            // 
            // chkMultipage
            // 
            chkMultipage.Appearance = Appearance.Button;
            chkMultipage.Image = My.Resources.Resources.multipage;
            chkMultipage.Location = new Point(12, 192);
            chkMultipage.Name = "chkMultipage";
            chkMultipage.Size = new Size(49, 48);
            chkMultipage.TabIndex = 26;
            chkMultipage.UseVisualStyleBackColor = true;
            // 
            // chkSaveToFile
            // 
            chkSaveToFile.Appearance = Appearance.Button;
            chkSaveToFile.Image = My.Resources.Resources.saveToFile;
            chkSaveToFile.Location = new Point(67, 192);
            chkSaveToFile.Name = "chkSaveToFile";
            chkSaveToFile.Size = new Size(49, 48);
            chkSaveToFile.TabIndex = 26;
            chkSaveToFile.UseVisualStyleBackColor = true;
            // 
            // chkPDF
            // 
            chkPDF.Appearance = Appearance.Button;
            chkPDF.Image = My.Resources.Resources.pdficon_large;
            chkPDF.Location = new Point(124, 192);
            chkPDF.Name = "chkPDF";
            chkPDF.Size = new Size(49, 48);
            chkPDF.TabIndex = 26;
            chkPDF.UseVisualStyleBackColor = true;
            // 
            // chkPreview
            // 
            chkPreview.AutoSize = true;
            chkPreview.Location = new Point(204, 160);
            chkPreview.Name = "chkPreview";
            chkPreview.Size = new Size(104, 24);
            chkPreview.TabIndex = 4;
            chkPreview.Text = "chkPreview";
            chkPreview.UseVisualStyleBackColor = true;
            // 
            // mainFrm
            // 
            AutoScaleDimensions = new SizeF(8.0f, 20.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(733, 279);
            Controls.Add(btnCopy);
            Controls.Add(chkDuplex);
            Controls.Add(chkADF);
            Controls.Add(chkPDF);
            Controls.Add(chkSaveToFile);
            Controls.Add(chkMultipage);
            Controls.Add(lblPrinter);
            Controls.Add(lblPaperSize);
            Controls.Add(cboPrintMode);
            Controls.Add(cboPaperSize);
            Controls.Add(cboScanMode);
            Controls.Add(lblScanner);
            Controls.Add(StatusStrip1);
            Controls.Add(btnImageSettings);
            Controls.Add(btnSelScanner);
            Controls.Add(llblSettings);
            Controls.Add(lblCopies);
            Controls.Add(btnPrintSetup);
            Controls.Add(llblAbout);
            Controls.Add(nudNCopie);
            Controls.Add(chkPreview);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "mainFrm";
            StartPosition = FormStartPosition.Manual;
            Text = "iCopy";
            ((System.ComponentModel.ISupportInitialize)nudNCopie).EndInit();
            StatusStrip1.ResumeLayout(false);
            StatusStrip1.PerformLayout();
            ScanMenuStrip.ResumeLayout(false);
            KeyUp += new KeyEventHandler(Hotkeys);
            Load += new EventHandler(mainFrm_Load);
            FormClosed += new FormClosedEventHandler(mainFrm_FormClosed);
            Move += new EventHandler(mainFrm_Move);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Button btnCopy;
        internal Button btnSelScanner;
        internal Button btnPrintSetup;
        internal NumericUpDown nudNCopie;
        internal Label lblCopies;
        internal ComboBox cboScanMode;
        internal ComboBox cboPrintMode;
        internal Label lblPrinter;
        internal Label lblScanner;
        internal StatusStrip StatusStrip1;
        internal ToolTip ToolTip1;
        internal LinkLabel llblAbout;
        internal LinkLabel llblSettings;
        internal Button btnImageSettings;
        internal ContextMenuStrip ScanMenuStrip;
        internal ToolStripMenuItem ScanToFile;
        internal ToolStripMenuItem ScanMultiplePages;
        internal ComboBox cboPaperSize;
        internal Label lblPaperSize;
        internal ToolStripStatusLabel ScannerStatusLabel;
        internal ToolStripStatusLabel PrinterStatusLabel;
        internal ToolStripStatusLabel VersionStatusLabel;
        internal CheckBox chkADF;
        internal CheckBox chkDuplex;
        internal CheckBox chkMultipage;
        internal CheckBox chkSaveToFile;
        internal CheckBox chkPDF;
        internal CheckBox chkPreview;
    }
}