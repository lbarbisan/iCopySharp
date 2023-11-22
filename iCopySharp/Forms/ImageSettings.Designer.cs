using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace iCopy
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "frm")]
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    internal partial class frmImageSettings : Form
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
            tbScaling = new TrackBar();
            tbScaling.ValueChanged += new EventHandler(tbScaling_ValueChanged);
            lblScaling = new Label();
            lblResolution = new Label();
            tbContrast = new TrackBar();
            tbContrast.ValueChanged += new EventHandler(tbContrast_ValueChanged);
            lblBrightness = new Label();
            tbBrightness = new TrackBar();
            tbBrightness.ValueChanged += new EventHandler(tbBrightness_ValueChanged);
            txtContrast = new TextBox();
            txtContrast.KeyPress += new KeyPressEventHandler(valid_KeyPress);
            txtContrast.LostFocus += new EventHandler(txtContrast_LostFocus);
            lblContrast = new Label();
            txtBrightness = new TextBox();
            txtBrightness.KeyPress += new KeyPressEventHandler(valid_KeyPress);
            txtBrightness.LostFocus += new EventHandler(txtBrightness_LostFocus);
            txtScaling = new TextBox();
            txtScaling.KeyPress += new KeyPressEventHandler(valid_KeyPress);
            txtScaling.LostFocus += new EventHandler(txtenlargement_LostFocus);
            lblPerc = new Label();
            ToolTip1 = new ToolTip(components);
            cboResolution = new ComboBox();
            lblDPI = new Label();
            lblCompressionLabel = new Label();
            tbCompression = new TrackBar();
            tbCompression.ValueChanged += new EventHandler(tbCompression_Scroll);
            lblCompression = new Label();
            btnDefault = new Button();
            btnDefault.Click += new EventHandler(btnDefault_Click);
            chkCenter = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)tbScaling).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbContrast).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbCompression).BeginInit();
            SuspendLayout();
            // 
            // tbScaling
            // 
            tbScaling.LargeChange = 50;
            tbScaling.Location = new Point(162, 72);
            tbScaling.Maximum = 200;
            tbScaling.Minimum = 1;
            tbScaling.Name = "tbScaling";
            tbScaling.Size = new Size(118, 56);
            tbScaling.SmallChange = 10;
            tbScaling.TabIndex = 39;
            tbScaling.TickFrequency = 50;
            tbScaling.Value = 100;
            // 
            // lblScaling
            // 
            lblScaling.Location = new Point(0, 75);
            lblScaling.Name = "lblScaling";
            lblScaling.Size = new Size(156, 20);
            lblScaling.TabIndex = 38;
            lblScaling.Text = "lblScaling";
            lblScaling.TextAlign = ContentAlignment.TopRight;
            // 
            // lblResolution
            // 
            lblResolution.Location = new Point(1, 107);
            lblResolution.Name = "lblResolution";
            lblResolution.Size = new Size(156, 26);
            lblResolution.TabIndex = 36;
            lblResolution.Text = "lblResolution";
            lblResolution.TextAlign = ContentAlignment.TopRight;
            // 
            // tbContrast
            // 
            tbContrast.LargeChange = 10;
            tbContrast.Location = new Point(162, 43);
            tbContrast.Maximum = 100;
            tbContrast.Minimum = -100;
            tbContrast.Name = "tbContrast";
            tbContrast.Size = new Size(118, 56);
            tbContrast.TabIndex = 32;
            tbContrast.TickFrequency = 20;
            // 
            // lblBrightness
            // 
            lblBrightness.Location = new Point(-4, 17);
            lblBrightness.Name = "lblBrightness";
            lblBrightness.Size = new Size(160, 20);
            lblBrightness.TabIndex = 34;
            lblBrightness.Text = "lblBrightness";
            lblBrightness.TextAlign = ContentAlignment.TopRight;
            // 
            // tbBrightness
            // 
            tbBrightness.LargeChange = 10;
            tbBrightness.Location = new Point(162, 12);
            tbBrightness.Maximum = 100;
            tbBrightness.Minimum = -100;
            tbBrightness.Name = "tbBrightness";
            tbBrightness.Size = new Size(118, 56);
            tbBrightness.TabIndex = 30;
            tbBrightness.TickFrequency = 20;
            // 
            // txtContrast
            // 
            txtContrast.Location = new Point(288, 43);
            txtContrast.MaxLength = 4;
            txtContrast.Name = "txtContrast";
            txtContrast.Size = new Size(42, 27);
            txtContrast.TabIndex = 33;
            // 
            // lblContrast
            // 
            lblContrast.Location = new Point(0, 46);
            lblContrast.Name = "lblContrast";
            lblContrast.Size = new Size(156, 20);
            lblContrast.TabIndex = 35;
            lblContrast.Text = "lblContrast";
            lblContrast.TextAlign = ContentAlignment.TopRight;
            // 
            // txtBrightness
            // 
            txtBrightness.Location = new Point(288, 14);
            txtBrightness.MaxLength = 4;
            txtBrightness.Name = "txtBrightness";
            txtBrightness.Size = new Size(42, 27);
            txtBrightness.TabIndex = 31;
            // 
            // txtScaling
            // 
            txtScaling.Location = new Point(288, 72);
            txtScaling.MaxLength = 3;
            txtScaling.Name = "txtScaling";
            txtScaling.Size = new Size(32, 27);
            txtScaling.TabIndex = 41;
            // 
            // lblPerc
            // 
            lblPerc.AutoSize = true;
            lblPerc.Location = new Point(319, 75);
            lblPerc.Name = "lblPerc";
            lblPerc.Size = new Size(21, 20);
            lblPerc.TabIndex = 44;
            lblPerc.Text = "%";
            // 
            // cboResolution
            // 
            cboResolution.DropDownStyle = ComboBoxStyle.DropDownList;
            cboResolution.FormattingEnabled = true;
            cboResolution.Location = new Point(163, 105);
            cboResolution.Name = "cboResolution";
            cboResolution.Size = new Size(106, 28);
            cboResolution.TabIndex = 45;
            // 
            // lblDPI
            // 
            lblDPI.AutoSize = true;
            lblDPI.Location = new Point(286, 108);
            lblDPI.Name = "lblDPI";
            lblDPI.Size = new Size(49, 20);
            lblDPI.TabIndex = 46;
            lblDPI.Text = "lblDPI";
            // 
            // lblCompressionLabel
            // 
            lblCompressionLabel.Location = new Point(0, 138);
            lblCompressionLabel.Name = "lblCompressionLabel";
            lblCompressionLabel.Size = new Size(156, 38);
            lblCompressionLabel.TabIndex = 47;
            lblCompressionLabel.Text = "JPEG Compression";
            lblCompressionLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tbCompression
            // 
            tbCompression.Location = new Point(162, 138);
            tbCompression.Maximum = 100;
            tbCompression.Minimum = 1;
            tbCompression.Name = "tbCompression";
            tbCompression.Size = new Size(118, 56);
            tbCompression.TabIndex = 48;
            tbCompression.TickFrequency = 10;
            tbCompression.Value = 100;
            // 
            // lblCompression
            // 
            lblCompression.Location = new Point(286, 142);
            lblCompression.Name = "lblCompression";
            lblCompression.Size = new Size(33, 27);
            lblCompression.TabIndex = 49;
            lblCompression.Text = "lblCompression";
            // 
            // btnDefault
            // 
            btnDefault.Location = new Point(218, 184);
            btnDefault.Name = "btnDefault";
            btnDefault.Size = new Size(127, 39);
            btnDefault.TabIndex = 50;
            btnDefault.Text = "Default";
            btnDefault.UseVisualStyleBackColor = true;
            // 
            // chkCenter
            // 
            chkCenter.AutoSize = true;
            chkCenter.Checked = Properties.Settings.Default.Center;
            chkCenter.CheckState = CheckState.Checked;
            chkCenter.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "Center", true, DataSourceUpdateMode.OnPropertyChanged));
            chkCenter.Location = new Point(12, 192);
            chkCenter.Name = "chkCenter";
            chkCenter.Size = new Size(96, 24);
            chkCenter.TabIndex = 51;
            chkCenter.Text = "chkCenter";
            chkCenter.UseVisualStyleBackColor = true;
            // 
            // frmImageSettings
            // 
            AutoScaleDimensions = new SizeF(8.0f, 20.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(357, 234);
            Controls.Add(chkCenter);
            Controls.Add(btnDefault);
            Controls.Add(lblCompression);
            Controls.Add(cboResolution);
            Controls.Add(tbScaling);
            Controls.Add(tbCompression);
            Controls.Add(lblCompressionLabel);
            Controls.Add(lblDPI);
            Controls.Add(lblResolution);
            Controls.Add(lblBrightness);
            Controls.Add(txtContrast);
            Controls.Add(lblContrast);
            Controls.Add(txtBrightness);
            Controls.Add(tbContrast);
            Controls.Add(tbBrightness);
            Controls.Add(txtScaling);
            Controls.Add(lblScaling);
            Controls.Add(lblPerc);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            KeyPreview = true;
            Name = "frmImageSettings";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "Image Settings";
            ((System.ComponentModel.ISupportInitialize)tbScaling).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbContrast).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbCompression).EndInit();
            FormClosing += new FormClosingEventHandler(frmImageSettings_FormClosing);
            KeyPress += new KeyPressEventHandler(frmImageSettings_KeyPress);
            KeyUp += new KeyEventHandler(frmImageSettings_KeyUp);
            ResumeLayout(false);
            PerformLayout();

        }
        internal TrackBar tbScaling;
        internal Label lblScaling;
        internal Label lblResolution;
        internal TrackBar tbContrast;
        internal Label lblBrightness;
        internal TrackBar tbBrightness;
        internal TextBox txtContrast;
        internal Label lblContrast;
        internal TextBox txtBrightness;
        internal TextBox txtScaling;
        internal Label lblPerc;
        internal ToolTip ToolTip1;
        internal ComboBox cboResolution;
        internal Label lblDPI;
        internal Label lblCompressionLabel;
        internal TrackBar tbCompression;
        internal Label lblCompression;
        internal Button btnDefault;
        internal CheckBox chkCenter;
    }
}