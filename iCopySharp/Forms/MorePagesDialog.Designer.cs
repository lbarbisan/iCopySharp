using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace iCopy
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class dlgScanMorePages : Form
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
            btnAddPage = new Button();
            btnPrint = new Button();
            Label1 = new Label();
            ToolTip1 = new ToolTip(components);
            lblAcquiredPagesNText = new Label();
            lblAcquiredPagesN = new Label();
            SuspendLayout();
            // 
            // btnAddPage
            // 
            btnAddPage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddPage.BackgroundImageLayout = ImageLayout.Stretch;
            btnAddPage.Cursor = Cursors.Default;
            btnAddPage.DialogResult = DialogResult.Yes;
            btnAddPage.Image = My.Resources.Resources.scanner_big;
            btnAddPage.ImageAlign = ContentAlignment.TopCenter;
            btnAddPage.Location = new Point(12, 124);
            btnAddPage.Name = "btnAddPage";
            btnAddPage.Size = new Size(146, 141);
            btnAddPage.TabIndex = 0;
            btnAddPage.Text = "Scan Another Page";
            btnAddPage.TextAlign = ContentAlignment.BottomCenter;
            btnAddPage.TextImageRelation = TextImageRelation.ImageAboveText;
            btnAddPage.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            btnPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPrint.DialogResult = DialogResult.No;
            btnPrint.Image = My.Resources.Resources.Check_Mark;
            btnPrint.ImageAlign = ContentAlignment.TopCenter;
            btnPrint.Location = new Point(198, 124);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(146, 141);
            btnPrint.TabIndex = 1;
            btnPrint.Text = "Print Pages";
            btnPrint.TextAlign = ContentAlignment.BottomCenter;
            btnPrint.TextImageRelation = TextImageRelation.ImageAboveText;
            btnPrint.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            Label1.Location = new Point(12, 9);
            Label1.Name = "Label1";
            Label1.Size = new Size(332, 56);
            Label1.TabIndex = 2;
            Label1.Text = "Choose if you want to add more pages or if you want to print the acquired pages.";
            Label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblAcquiredPagesNText
            // 
            lblAcquiredPagesNText.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAcquiredPagesNText.Location = new Point(12, 79);
            lblAcquiredPagesNText.Name = "lblAcquiredPagesNText";
            lblAcquiredPagesNText.Size = new Size(194, 27);
            lblAcquiredPagesNText.TabIndex = 3;
            lblAcquiredPagesNText.Text = "lblAcquiredPagesNText";
            lblAcquiredPagesNText.TextAlign = ContentAlignment.TopRight;
            // 
            // lblAcquiredPagesN
            // 
            lblAcquiredPagesN.AutoSize = true;
            lblAcquiredPagesN.Location = new Point(222, 79);
            lblAcquiredPagesN.Name = "lblAcquiredPagesN";
            lblAcquiredPagesN.Size = new Size(53, 20);
            lblAcquiredPagesN.TabIndex = 4;
            lblAcquiredPagesN.Text = "Label2";
            // 
            // dlgScanMorePages
            // 
            AutoScaleDimensions = new SizeF(8.0f, 20.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(356, 277);
            Controls.Add(lblAcquiredPagesN);
            Controls.Add(lblAcquiredPagesNText);
            Controls.Add(Label1);
            Controls.Add(btnPrint);
            Controls.Add(btnAddPage);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "dlgScanMorePages";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "Scan More Pages";
            Load += new EventHandler(DlgScanMorePages_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Button btnAddPage;
        internal Button btnPrint;
        internal Label Label1;
        internal ToolTip ToolTip1;
        internal Label lblAcquiredPagesNText;
        internal Label lblAcquiredPagesN;

    }
}