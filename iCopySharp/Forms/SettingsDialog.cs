using System;
using System.Globalization;
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

using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using WIA;

namespace iCopy
{

    public partial class SettingsDialog
    {
        private string localeRootStr;

        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public void SaveSettings()
        {
            if (cboLanguage.SelectedItem!=null && cboLanguage.SelectedItem is CultureInfo 
                    && !((CultureInfo)cboLanguage.SelectedItem).Equals(Properties.Settings.Default.Culture.LCID))
            {
                Properties.Settings.Default.Culture = (CultureInfo)cboLanguage.SelectedItem;
                Properties.Settings.Default.CustomCulture = true;
                Utilities.MsgBoxWrap(appControl.GetLocalizedString("Msg_Language"), MsgBoxStyle.Information, "iCopy");
            }

            Properties.Settings.Default.RememberSettings = chkRememberScanSettings.Checked;
            Properties.Settings.Default.StoreLocation = chkRememberWindowPos.Checked;
            Properties.Settings.Default.CheckForUpdates = chkUpdates.Checked;

            Properties.Settings.Default.PDFAskWhereToSave = rbAskPDF.Checked;
            Properties.Settings.Default.PDFOpenAfterAcquisition = chkOpenPDF.Checked;

            Properties.Settings.Default.FileAskWhereToSave = rbAskFile.Checked;
            Properties.Settings.Default.FileOpenAfterAcquisition = chkOpenFile.Checked;

            if (txtPathFile.Text.EndsWith(@"\"))
            {
                Properties.Settings.Default.FileSavePath = txtPathFile.Text;
            }
            else
            {
                Properties.Settings.Default.FileSavePath = txtPathFile.Text + @"\";
            }

            if (txtPathPDF.Text.EndsWith(@"\"))
            {
                Properties.Settings.Default.PDFSavePath = txtPathPDF.Text;
            }
            else
            {
                Properties.Settings.Default.PDFSavePath = txtPathPDF.Text + @"\";
            }

            if (cboBitDepth.Text == "Auto" | string.IsNullOrEmpty(cboBitDepth.Text))
            {
                Properties.Settings.Default.LastScanSettings.BitDepth = 0;
            }
            else
            {
                Properties.Settings.Default.LastScanSettings.BitDepth = Conversions.ToInteger(cboBitDepth.Text);
            }

            Properties.Settings.Default.LastScanSettings.RotateDuplex = chkRotateDuplex.Checked;
        }

        public void LoadSettings()
        {
            localeRootStr = Name + "_";
            Text = appControl.GetLocalizedString(Name);
            // Applies localized strings to the controls
            foreach (Control control in Controls)
            {

                string text = appControl.GetLocalizedString(localeRootStr + control.Name);
                if (!string.IsNullOrEmpty(text))
                    control.Text = text;
                ToolTip1.SetToolTip(control, appControl.GetLocalizedString(localeRootStr + control.Name + "ToolTip"));
                foreach (Control subcontrol in control.Controls)
                {
                    text = appControl.GetLocalizedString(localeRootStr + subcontrol.Name);
                    if (!string.IsNullOrEmpty(text))
                        subcontrol.Text = text;
                    ToolTip1.SetToolTip(subcontrol, appControl.GetLocalizedString(localeRootStr + subcontrol.Name + "ToolTip"));
                    foreach (Control subsubcontrol in subcontrol.Controls)
                    {
                        text = appControl.GetLocalizedString(localeRootStr + subsubcontrol.Name);
                        if (!string.IsNullOrEmpty(text))
                            subsubcontrol.Text = text;
                        ToolTip1.SetToolTip(subsubcontrol, appControl.GetLocalizedString(localeRootStr + subsubcontrol.Name + "ToolTip"));
                        foreach (Control subsubsubcontrol in subsubcontrol.Controls)
                        {
                            text = appControl.GetLocalizedString(localeRootStr + subsubsubcontrol.Name);
                            if (!string.IsNullOrEmpty(text))
                                subsubsubcontrol.Text = text;
                            ToolTip1.SetToolTip(subsubsubcontrol, appControl.GetLocalizedString(localeRootStr + subsubsubcontrol.Name + "ToolTip"));
                        }
                    }
                }
            }

            // Populates language combobox

            // Get the available languages
            if (appControl.AvailableLanguages.Count == 0)
                appControl.GetAvailableLanguages();
            var availableCultures = appControl.AvailableLanguages;
            cboLanguage.DataSource = appControl.AvailableLanguages;
            cboLanguage.DisplayMember = "EnglishName";


            // Select the current culture from the combo box
            cboLanguage.SelectedItem = System.Threading.Thread.CurrentThread.CurrentUICulture;
            // This happens if iCopy is not translated into the system default culture
            if (cboLanguage.SelectedItem is null)
            {
                cboLanguage.SelectedItem = CultureInfo.GetCultureInfo("en-US");
            }

            // Load bit depth settings
            if (Properties.Settings.Default.LastScanSettings.BitDepth == 0)
            {
                cboBitDepth.SelectedIndex = 0;
            }
            else
            {
                cboBitDepth.Text = Properties.Settings.Default.LastScanSettings.BitDepth.ToString();
            }


            // Load rotate duplex settings
            chkRotateDuplex.Checked = Properties.Settings.Default.LastScanSettings.RotateDuplex;

            // Load file saving preferences
            rbAskFile.Checked = Properties.Settings.Default.FileAskWhereToSave;
            rbPathFile.Checked = !Properties.Settings.Default.FileAskWhereToSave;
            rbAskPDF.Checked = Properties.Settings.Default.PDFAskWhereToSave;
            rbPathPDF.Checked = !Properties.Settings.Default.PDFAskWhereToSave;

            txtPathFile.Text = Properties.Settings.Default.FileSavePath;
            txtPathPDF.Text = Properties.Settings.Default.PDFSavePath;

            chkOpenFile.Checked = Properties.Settings.Default.FileOpenAfterAcquisition;
            chkOpenPDF.Checked = Properties.Settings.Default.PDFOpenAfterAcquisition;


            chkRememberScanSettings.Checked = Properties.Settings.Default.RememberSettings;
            chkRememberWindowPos.Checked = Properties.Settings.Default.StoreLocation;
            chkUpdates.Checked = Properties.Settings.Default.CheckForUpdates;
        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }


        private void btnResetScanSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LastScanSettings = Properties.Settings.Default.DefaultScanSettings;
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            var fldr = new FolderBrowserDialog();
            fldr.SelectedPath = txtPathFile.Text;
            var res = fldr.ShowDialog();
            if (res == DialogResult.OK)
            {
                txtPathFile.Text = fldr.SelectedPath;
            }
        }

        private void btnBrowsePDF_Click(object sender, EventArgs e)
        {
            var fldr = new FolderBrowserDialog();
            fldr.SelectedPath = txtPathPDF.Text;
            var res = fldr.ShowDialog();
            if (res == DialogResult.OK)
            {
                txtPathPDF.Text = fldr.SelectedPath;
            }
        }

        private void rbAskFile_CheckedChanged(object sender, EventArgs e)
        {
            txtPathFile.Enabled = !rbAskFile.Checked;
            btnBrowseFile.Enabled = !rbAskFile.Checked;
        }

        private void rbAskPDF_CheckedChanged(object sender, EventArgs e)
        {
            txtPathPDF.Enabled = !rbAskPDF.Checked;
            btnBrowsePDF.Enabled = !rbAskPDF.Checked;
        }

        private void chkRotateDuplex_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

    class WIAEventWrapper : DeviceEvent
    {

        private DeviceEvent _ev;
        private Action _action;

        public WIAEventWrapper(DeviceEvent ev)
        {
            _ev = ev;
        }

        public Action currentAction
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
            }
        }

        public override string ToString()
        {
            return _ev.Name;
        }

        public string Description
        {
            get
            {
                return _ev.Description;
            }
        }

        public string EventID
        {
            get
            {
                return _ev.EventID;
            }
        }

        public string Name
        {
            get
            {
                return _ev.Name;
            }
        }

        public WiaEventFlag Type
        {
            get
            {
                return _ev.Type;
            }
        }
    }

    class Action
    {
        public Action(string Description, string Arguments)
        {
            _description = Description;
            _arguments = Arguments;
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        private string _arguments;
        public string Arguments
        {
            get
            {
                return _arguments;
            }
            set
            {
                _arguments = value;
            }
        }

    }
}