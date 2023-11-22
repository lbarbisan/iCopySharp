using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace iCopy
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class AboutBox : Form
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
            LabelProductName = new Label();
            LabelVersion = new Label();
            LabelCopyright = new Label();
            LabelCompanyName = new Label();
            LinkLabel1 = new LinkLabel();
            LinkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkLabel1_LinkClicked_1);
            lblDescription = new Label();
            OKButton = new Button();
            OKButton.Click += new EventHandler(OKButton_Click);
            Label1 = new Label();
            LinkLabel2 = new LinkLabel();
            LinkLabel3 = new LinkLabel();
            LinkLabel3.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkLabel3_LinkClicked);
            LinkLabel4 = new LinkLabel();
            LinkLabel4.Click += new EventHandler(LinkLabel4_Click);
            LinkLabel5 = new LinkLabel();
            LinkLabel5.LinkClicked += new LinkLabelLinkClickedEventHandler(Donate_LinkClicked);
            SuspendLayout();
            // 
            // LabelProductName
            // 
            LabelProductName.AutoSize = true;
            LabelProductName.BackColor = Color.Transparent;
            LabelProductName.Location = new Point(204, 28);
            LabelProductName.Name = "LabelProductName";
            LabelProductName.Size = new Size(53, 20);
            LabelProductName.TabIndex = 14;
            LabelProductName.Text = "Label1";
            // 
            // LabelVersion
            // 
            LabelVersion.AutoSize = true;
            LabelVersion.BackColor = Color.Transparent;
            LabelVersion.Location = new Point(204, 53);
            LabelVersion.Name = "LabelVersion";
            LabelVersion.Size = new Size(53, 20);
            LabelVersion.TabIndex = 15;
            LabelVersion.Text = "Label1";
            // 
            // LabelCopyright
            // 
            LabelCopyright.AutoSize = true;
            LabelCopyright.BackColor = Color.Transparent;
            LabelCopyright.Location = new Point(204, 78);
            LabelCopyright.Name = "LabelCopyright";
            LabelCopyright.Size = new Size(53, 20);
            LabelCopyright.TabIndex = 16;
            LabelCopyright.Text = "Label2";
            // 
            // LabelCompanyName
            // 
            LabelCompanyName.AutoSize = true;
            LabelCompanyName.BackColor = Color.Transparent;
            LabelCompanyName.Location = new Point(204, 105);
            LabelCompanyName.Name = "LabelCompanyName";
            LabelCompanyName.Size = new Size(53, 20);
            LabelCompanyName.TabIndex = 17;
            LabelCompanyName.Text = "Label3";
            // 
            // LinkLabel1
            // 
            LinkLabel1.AutoSize = true;
            LinkLabel1.BackColor = Color.Transparent;
            LinkLabel1.Location = new Point(204, 130);
            LinkLabel1.Name = "LinkLabel1";
            LinkLabel1.Size = new Size(84, 20);
            LinkLabel1.TabIndex = 20;
            LinkLabel1.TabStop = true;
            LinkLabel1.Text = "Homepage";
            // 
            // lblDescription
            // 
            lblDescription.BackColor = Color.Transparent;
            lblDescription.Location = new Point(204, 162);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(258, 42);
            lblDescription.TabIndex = 18;
            lblDescription.Text = "LabelDescription";
            // 
            // OKButton
            // 
            OKButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            OKButton.Location = new Point(419, 285);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(55, 27);
            OKButton.TabIndex = 19;
            OKButton.Text = "&Ok";
            OKButton.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            Label1.BackColor = Color.Transparent;
            Label1.Location = new Point(204, 201);
            Label1.Name = "Label1";
            Label1.Size = new Size(260, 45);
            Label1.TabIndex = 21;
            Label1.Text = "This program is distributed under the terms of GNU GPL License.";
            // 
            // LinkLabel2
            // 
            LinkLabel2.AutoSize = true;
            LinkLabel2.Location = new Point(-124, 220);
            LinkLabel2.Name = "LinkLabel2";
            LinkLabel2.Size = new Size(79, 20);
            LinkLabel2.TabIndex = 22;
            LinkLabel2.TabStop = true;
            LinkLabel2.Text = "LinkLabel2";
            // 
            // LinkLabel3
            // 
            LinkLabel3.AutoSize = true;
            LinkLabel3.BackColor = Color.Transparent;
            LinkLabel3.Location = new Point(204, 259);
            LinkLabel3.Name = "LinkLabel3";
            LinkLabel3.Size = new Size(203, 20);
            LinkLabel3.TabIndex = 23;
            LinkLabel3.TabStop = true;
            LinkLabel3.Text = "Click here to read license text";
            // 
            // LinkLabel4
            // 
            LinkLabel4.BackColor = Color.Transparent;
            LinkLabel4.Cursor = Cursors.Hand;
            LinkLabel4.Image = My.Resources.Resources.btn_donateCC_LG;
            LinkLabel4.Location = new Point(36, 187);
            LinkLabel4.Name = "LinkLabel4";
            LinkLabel4.Size = new Size(127, 59);
            LinkLabel4.TabIndex = 25;
            // 
            // LinkLabel5
            // 
            LinkLabel5.BackColor = Color.Transparent;
            LinkLabel5.Location = new Point(14, 246);
            LinkLabel5.Name = "LinkLabel5";
            LinkLabel5.Size = new Size(170, 33);
            LinkLabel5.TabIndex = 26;
            LinkLabel5.TabStop = true;
            LinkLabel5.Text = "Donate using PayPal";
            LinkLabel5.TextAlign = ContentAlignment.TopCenter;
            // 
            // AboutBox
            // 
            AutoScaleDimensions = new SizeF(8.0f, 20.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = My.Resources.Resources.Aboutbgr;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(483, 318);
            Controls.Add(LinkLabel5);
            Controls.Add(LinkLabel4);
            Controls.Add(LinkLabel3);
            Controls.Add(LabelProductName);
            Controls.Add(LabelVersion);
            Controls.Add(LabelCopyright);
            Controls.Add(LabelCompanyName);
            Controls.Add(LinkLabel1);
            Controls.Add(lblDescription);
            Controls.Add(OKButton);
            Controls.Add(Label1);
            Controls.Add(LinkLabel2);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutBox";
            Padding = new Padding(10);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AboutBox";
            TopMost = true;
            Load += new EventHandler(AboutBox_Load);
            VisibleChanged += new EventHandler(AboutBox_VisibleChanged);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Label LabelProductName;
        internal Label LabelVersion;
        internal Label LabelCopyright;
        internal Label LabelCompanyName;
        internal LinkLabel LinkLabel1;
        internal Label lblDescription;
        internal Button OKButton;
        internal Label Label1;
        internal LinkLabel LinkLabel2;
        internal LinkLabel LinkLabel3;
        internal LinkLabel LinkLabel4;
        internal LinkLabel LinkLabel5;

    }
}