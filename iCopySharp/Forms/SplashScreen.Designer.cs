using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace iCopy
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class SplashScreen : Form
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
        internal Label ApplicationTitle;
        internal Label Version;
        internal Label Copyright;
        internal TableLayoutPanel MainLayoutPanel;
        internal TableLayoutPanel DetailsLayoutPanel;

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            MainLayoutPanel = new TableLayoutPanel();
            DetailsLayoutPanel = new TableLayoutPanel();
            Version = new Label();
            Copyright = new Label();
            ApplicationTitle = new Label();
            ProgressBar1 = new ProgressBar();
            MainLayoutPanel.SuspendLayout();
            DetailsLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainLayoutPanel
            // 
            MainLayoutPanel.BackgroundImage = My.Resources.Resources.Aboutbgr;
            MainLayoutPanel.BackgroundImageLayout = ImageLayout.Stretch;
            MainLayoutPanel.ColumnCount = 2;
            MainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 296.0f));
            MainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 282.0f));
            MainLayoutPanel.Controls.Add(DetailsLayoutPanel, 1, 1);
            MainLayoutPanel.Controls.Add(ApplicationTitle, 1, 0);
            MainLayoutPanel.Controls.Add(ProgressBar1, 1, 2);
            MainLayoutPanel.Dock = DockStyle.Fill;
            MainLayoutPanel.Location = new Point(0, 0);
            MainLayoutPanel.Name = "MainLayoutPanel";
            MainLayoutPanel.RowCount = 3;
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 200.0f));
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 74.0f));
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 59.0f));
            MainLayoutPanel.Size = new Size(579, 350);
            MainLayoutPanel.TabIndex = 0;
            // 
            // DetailsLayoutPanel
            // 
            DetailsLayoutPanel.Anchor = AnchorStyles.None;
            DetailsLayoutPanel.BackColor = Color.Transparent;
            DetailsLayoutPanel.ColumnCount = 1;
            DetailsLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 288.0f));
            DetailsLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 166.0f));
            DetailsLayoutPanel.Controls.Add(Version, 0, 0);
            DetailsLayoutPanel.Controls.Add(Copyright, 0, 1);
            DetailsLayoutPanel.Location = new Point(300, 203);
            DetailsLayoutPanel.Name = "DetailsLayoutPanel";
            DetailsLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.0f));
            DetailsLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.0f));
            DetailsLayoutPanel.Size = new Size(275, 67);
            DetailsLayoutPanel.TabIndex = 1;
            // 
            // Version
            // 
            Version.Anchor = AnchorStyles.None;
            Version.BackColor = Color.Transparent;
            Version.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Version.Location = new Point(3, 5);
            Version.Name = "Version";
            Version.Size = new Size(281, 23);
            Version.TabIndex = 1;
            Version.Text = "Version {0}.{1:00}";
            // 
            // Copyright
            // 
            Copyright.Anchor = AnchorStyles.None;
            Copyright.BackColor = Color.Transparent;
            Copyright.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Copyright.Location = new Point(3, 33);
            Copyright.Name = "Copyright";
            Copyright.Size = new Size(281, 33);
            Copyright.TabIndex = 2;
            Copyright.Text = "Copyright";
            // 
            // ApplicationTitle
            // 
            ApplicationTitle.Anchor = AnchorStyles.None;
            ApplicationTitle.BackColor = Color.Transparent;
            ApplicationTitle.Font = new Font("Microsoft Sans Serif", 18.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ApplicationTitle.Location = new Point(300, 6);
            ApplicationTitle.Name = "ApplicationTitle";
            ApplicationTitle.Size = new Size(275, 187);
            ApplicationTitle.TabIndex = 0;
            ApplicationTitle.Text = "Application Title";
            ApplicationTitle.TextAlign = ContentAlignment.BottomLeft;
            // 
            // ProgressBar1
            // 
            ProgressBar1.Anchor = AnchorStyles.Left;
            ProgressBar1.Location = new Point(308, 298);
            ProgressBar1.Margin = new Padding(12, 3, 3, 3);
            ProgressBar1.Name = "ProgressBar1";
            ProgressBar1.Size = new Size(202, 28);
            ProgressBar1.Style = ProgressBarStyle.Marquee;
            ProgressBar1.TabIndex = 2;
            // 
            // SplashScreen
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(579, 350);
            ControlBox = false;
            Controls.Add(MainLayoutPanel);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SplashScreen";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            MainLayoutPanel.ResumeLayout(false);
            DetailsLayoutPanel.ResumeLayout(false);
            Load += new EventHandler(SplashScreen_Load);
            ResumeLayout(false);

        }
        internal ProgressBar ProgressBar1;

    }
}