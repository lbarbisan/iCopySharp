using System;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace iCopy
{
    // iCopy - Simple Photocopier
    // Copyright (C) 2007-2018 Matteo Rossi

    // This program is free software: you can redistribute it and/or modify
    // it under the terms of the GNU General Public License as published by
    // the Free Software Foundation, either version 3 of the License, or
    // (at your option) any later version.

    // This program is distributed in the hope that it will be useful,
    // but WITHOUT ANY WARRANTY; without even the implied warranty of
    // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    // GNU General Public License for more details.

    // You should have received a copy of the GNU General Public License
    // along with this program.  If not, see <http://www.gnu.org/licenses/>.

    internal partial class frmImageSettings
    {

        private string locRootStr;

        // Prevent the form from unloading
        private void frmImageSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                appControl.MainForm.btnImageSettings.Text = appControl.GetLocalizedString("mainFrm_btnImageSettings");
                Hide();
            }
        }

        private void frmImageSettings_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Close();
            }
        }

        private void frmImageSettings_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape | e.Control & e.KeyCode == Keys.I)
            {
                Close();
            }
        }

        public frmImageSettings()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            locRootStr = Name + "_";
            Text = appControl.GetLocalizedString(Name);
            // Applies localized strings to the controls
            foreach (Control control in Controls)
            {
                string text = appControl.GetLocalizedString(locRootStr + control.Name);
                if (!string.IsNullOrEmpty(text))
                    control.Text = text;
                ToolTip1.SetToolTip(control, appControl.GetLocalizedString(locRootStr + control.Name + "ToolTip"));
            }

            // Quality
            foreach (short res in appControl.GetAvailableResolutions())
                cboResolution.Items.Add(res);

            if (Properties.Settings.Default.LastScanSettings.Resolution != 0)
            {
                cboResolution.Text = Properties.Settings.Default.LastScanSettings.Resolution.ToString();
            }

            if (string.IsNullOrEmpty(cboResolution.Text))
            {
                cboResolution.SelectedIndex = 0;
                string res = cboResolution.Text;
                Properties.Settings.Default.LastScanSettings.Resolution = Convert.ToInt32(res);
            }

            // Loads settings
            tbBrightness.Value = Properties.Settings.Default.LastScanSettings.Brightness;
            tbContrast.Value = Properties.Settings.Default.LastScanSettings.Contrast;
            txtBrightness.Text = Properties.Settings.Default.LastScanSettings.Brightness.ToString();
            txtContrast.Text = Properties.Settings.Default.LastScanSettings.Contrast.ToString();
            tbScaling.Value = Properties.Settings.Default.LastScanSettings.Scaling;
            tbCompression.Value = Properties.Settings.Default.LastScanSettings.Quality;
            lblCompression.Text = tbCompression.Value.ToString();
            txtScaling.Text = tbScaling.Value.ToString();
        }

        public void RefreshSettings()
        {
            if (Properties.Settings.Default.LastScanSettings.Resolution != 0)
            {
                cboResolution.Text = Properties.Settings.Default.LastScanSettings.Resolution.ToString();
            }
            else
            {
                Properties.Settings.Default.LastScanSettings.Resolution = Properties.Settings.Default.DefaultScanSettings.Resolution;
                cboResolution.Text = Properties.Settings.Default.DefaultScanSettings.Resolution.ToString();
            }

            // Loads settings
            tbBrightness.Value = Properties.Settings.Default.LastScanSettings.Brightness;
            tbContrast.Value = Properties.Settings.Default.LastScanSettings.Contrast;
            txtBrightness.Text = Properties.Settings.Default.LastScanSettings.Brightness.ToString();
            txtContrast.Text = Properties.Settings.Default.LastScanSettings.Contrast.ToString();
            tbScaling.Value = Properties.Settings.Default.LastScanSettings.Scaling;
            tbCompression.Value = Properties.Settings.Default.LastScanSettings.Quality;
            lblCompression.Text = tbCompression.Value.ToString();
            txtScaling.Text = tbScaling.Value.ToString();
        }

        private void valid_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Permits only numerical input to the textbox
            // Apllied to both txtBrightness and txtContrast
            var txt = (TextBox)sender;
            if (Information.IsNumeric(e.KeyChar) | char.IsControl(e.KeyChar))
            {
                e.Handled = false;   // permits
            }
            else if (Conversions.ToBoolean(Operators.AndObject(Operators.AndObject(Conversions.ToString(e.KeyChar) == "-", Operators.OrObject(Operators.ConditionalCompareObjectEqual(txt.Text.Length, 0, false), Operators.ConditionalCompareObjectEqual(txt.SelectionStart, 0, false))), sender is TextBox))) // Permits the pressing of "-" only if it is the first char
            {
                e.Handled = false;   // permits
            }
            else
            {
                e.Handled = true;
            }    // blocks keypress
        }

        public static void CheckValue(TextBox textBox, TrackBar tb, string strvalue)
        {
            // Chcks that values inserted in brightness and contrast textboxes are correct
            short value;
            try
            {
                value = Conversions.ToShort(strvalue); // Converts text into a short
                if (value <= tb.Maximum & value >= tb.Minimum)
                {
                    tb.Value = value;
                }
                else
                {
                    Utilities.MsgBoxWrap(string.Format(appControl.GetLocalizedString("Msg_InsertNumber"), tb.Minimum, tb.Maximum), MsgBoxStyle.Information, "iCopy");
                    textBox.Text = tb.Value.ToString();
                    textBox.Focus();
                }
            }
            catch (InvalidCastException ex)
            {
                Utilities.MsgBoxWrap(string.Format(appControl.GetLocalizedString("Msg_InsertNumber"), tb.Minimum, tb.Maximum), MsgBoxStyle.Information, "iCopy");
                textBox.Text = tb.Value.ToString();
                textBox.Focus();
            }

        }

        private void txtContrast_LostFocus(object sender, EventArgs e)
        {
            CheckValue(txtContrast, tbContrast, txtContrast.Text);
            tbContrast.Value = Conversions.ToInteger(txtContrast.Text);
        }

        private void tbContrast_ValueChanged(object sender, EventArgs e)
        {
            txtContrast.Text = tbContrast.Value.ToString();
        }

        private void txtBrightness_LostFocus(object sender, EventArgs e)
        {
            CheckValue(txtBrightness, tbBrightness, txtBrightness.Text);
            tbBrightness.Value = Conversions.ToInteger(txtBrightness.Text);
        }

        private void tbBrightness_ValueChanged(object sender, EventArgs e)
        {
            txtBrightness.Text = tbBrightness.Value.ToString();
        }

        private void tbScaling_ValueChanged(object sender, EventArgs e)
        {
            txtScaling.Text = tbScaling.Value.ToString();
        }

        private void txtenlargement_LostFocus(object sender, EventArgs e)
        {
            CheckValue(txtScaling, tbScaling, txtScaling.Text);
            tbBrightness.Value = Conversions.ToInteger(txtBrightness.Text);
        }

        private void tbCompression_Scroll(object sender, EventArgs e)
        {
            lblCompression.Text = tbCompression.Value.ToString();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            tbBrightness.Value = 0;
            tbContrast.Value = 0;
            tbCompression.Value = Properties.Settings.Default.DefaultScanSettings.Quality;
            cboResolution.Text = Properties.Settings.Default.DefaultScanSettings.Resolution.ToString();
            tbScaling.Value = Properties.Settings.Default.DefaultScanSettings.Scaling;
        }
    }
}