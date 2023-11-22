using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
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

using WIA;

namespace iCopy
{

    internal partial class mainFrm
    {
        public static frmImageSettings frmImageSettings;
        public static AboutBox AboutBox;
        public static SettingsDialog frmOptions;
        private SplashScreen splash;

        private WiaImageIntent intent = Properties.Settings.Default.LastScanSettings.Intent;

        private System.Threading.Thread VersionCheckThread;
        private string weburl;
        private string LocalizedRootStr;

        public mainFrm()
        {
            VersionCheckThread = new System.Threading.Thread(VersionCheck);
            InitializeComponent();
        }

        public void VersionCheck()
        {
            System.Xml.XmlTextReader reader;
            var curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var newVersion = curVersion;
            try
            {
                reader = new System.Xml.XmlTextReader(My.Resources.Resources.VersionCheckURL);
                reader.MoveToContent();
                if (reader.NodeType == System.Xml.XmlNodeType.Element & (reader.Name ?? "") == (Application.ProductName ?? ""))
                {
                    string elementName = "";
                    while (reader.Read())
                    {
                        if (reader.NodeType == System.Xml.XmlNodeType.Element)
                        {
                            elementName = reader.Name;
                        }
                        else if (reader.NodeType == System.Xml.XmlNodeType.Text & reader.HasValue)
                        {
                            switch (elementName ?? "")
                            {
                                case "version":
                                    {
                                        newVersion = new Version(reader.Value);
                                        break;
                                    }
                                case "url":
                                    {
                                        weburl = reader.Value;
                                        break;
                                    }
                            }
                        }
                    }
                }
                if (curVersion.CompareTo(newVersion) < 0)
                {
                    VersionStatusLabel.Visible = true;
                }
                Properties.Settings.Default.LastVersionCheck = DateTime.Today;
            }
            catch (Exception ex) // File is not available, or internet access missing. Just die without any output
            {
                Trace.WriteLine("Couldn't check lastversion: " + ex.Message);
                return;
            }
        }

        public void LoadSettings()
        {

            AboutBox = new AboutBox();
            frmOptions = new SettingsDialog();
            frmImageSettings = new frmImageSettings();

            if (Properties.Settings.Default.CheckForUpdates)
            {
                VersionCheckThread.Start(); // Version check
            }
            // Loads form location if storelocation is true
            if (Properties.Settings.Default.StoreLocation)
            {
                Location = Properties.Settings.Default.Location;
            }
            else
            {
                Location = new Point((int)Math.Round((Screen.GetBounds(this).Width - Width) / 2d), (int)Math.Round((Screen.GetBounds(this).Height - Height) / 2d));
            }

            btnCopy.Image = My.Resources.Resources.iCopyBig;
            Icon = My.Resources.Resources.iCopyIco;

            // Set frmImageSettings as child
            AddOwnedForm(frmImageSettings);

            // Applies localized strings to the controls
            foreach (Control control in Controls)
            {
                string text = appControl.GetLocalizedString(LocalizedRootStr + control.Name);
                if (!string.IsNullOrEmpty(text))
                    control.Text = text;
                ToolTip1.SetToolTip(control, appControl.GetLocalizedString(LocalizedRootStr + control.Name + "ToolTip"));
            }

            // Applies localized strings to the menustrip
            foreach (ToolStripItem strip in ScanMenuStrip.Items)
                strip.Text = appControl.GetLocalizedString(LocalizedRootStr + strip.Name);

            // Populates comboboxes
            for (int i = 0; i <= 2; i++)
                cboScanMode.Items.Add(appControl.GetLocalizedString(LocalizedRootStr + "cboScanModeItem" + i));

            for (int i = 0; i <= 1; i++)
                cboPrintMode.Items.Add(appControl.GetLocalizedString(LocalizedRootStr + "cboPrintModeItem" + i));

            // Sets default copies number
            nudNCopie.Controls[1].Text = "1";

            // Loads default printer
            try
            {
                appControl.Printer.Name = Properties.Settings.Default.DefaultPrinter;
            }
            catch (ArgumentException ex)
            {
                if (ex.Message == "Printer name is not valid")
                {
                    var sets = new PrinterSettings();
                    appControl.Printer.Name = sets.PrinterName;
                }
            }
            Properties.Settings.Default.DefaultPrinter = appControl.Printer.Name;
            PrinterStatusLabel.Text = appControl.Printer.Name;

            // Statusbar labels
            ScannerStatusLabel.Image = My.Resources.Resources.scanner;
            ScannerStatusLabel.Text = appControl.ScannerDescription;
            PrinterStatusLabel.Image = My.Resources.Resources.printer;
            if (PrinterStatusLabel.Text.Contains("PDF"))
            {
                PrinterStatusLabel.Image = My.Resources.Resources.pdf_icon;
            }
            else
            {
                PrinterStatusLabel.Image = My.Resources.Resources.printer;
            }

            // Loads saved intent setting
            if ((int)Properties.Settings.Default.LastScanSettings.Intent == 4 | Properties.Settings.Default.LastScanSettings.Intent == 0)
            {
                cboScanMode.SelectedIndex = 2;
            }
            else
            {
                cboScanMode.SelectedIndex = (int)Properties.Settings.Default.LastScanSettings.Intent - 1;
            }

            // Populates paper sizes combo box
            cboPaperSize.DisplayMember = "PaperName"; // Links 
            foreach (PaperSize pSize in appControl.Printer.PrinterSettings.PaperSizes)
                cboPaperSize.Items.Add(pSize);

            cboPaperSize.Text = Properties.Settings.Default.PrinterSize; // Sets default paper size as stored in settings
            chkADF.Enabled = appControl.CanUseADF();
            chkDuplex.Enabled = chkADF.Checked & appControl.CanDoDuplex();

            chkADF.Checked = Properties.Settings.Default.LastScanSettings.UseADF;
            chkDuplex.Checked = Properties.Settings.Default.LastScanSettings.Duplex;
            chkPDF.Checked = Properties.Settings.Default.LastScanSettings.ScanOutput == ScanOutput.PDF;
            chkMultipage.Checked = Properties.Settings.Default.LastScanSettings.Multipage;
            chkSaveToFile.Checked = Properties.Settings.Default.LastScanSettings.ScanOutput == ScanOutput.File;

        }

        private void Hotkeys(object sender, KeyEventArgs e)
        {
            // Shortcuts
            if (e.Control) // If CTRL is pressed
            {
                var ea = new EventArgs();
                switch (e.KeyCode)
                {
                    case Keys.S: // Copy
                        {
                            btnCopy_Click(btnCopy, ea);
                            break;
                        }
                    case Keys.M: // Copy Multiple Pages
                        {
                            chkMultipage.Checked = !chkMultipage.Checked;
                            break;
                        }
                    case Keys.F: // Scan to File
                        {
                            chkSaveToFile.Checked = !chkSaveToFile.Checked;
                            break;
                        }
                    case Keys.I: // Image settings
                        {
                            btnImageSettings_Click(btnImageSettings, ea);
                            break;
                        }
                    case Keys.P: // Scan to PDF
                        {
                            chkPDF.Checked = !chkPDF.Checked;
                            break;
                        }
                }
            }
        }

        private void StartSplash()
        {
            splash = new SplashScreen();
            Application.Run(splash);
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {

            if (Properties.Settings.Default.CustomCulture & Properties.Settings.Default.Culture != null)
                System.Threading.Thread.CurrentThread.CurrentUICulture = Properties.Settings.Default.Culture;

            BringToFront();
            Focus();

            LocalizedRootStr = Name + "_";
            var SplashThread = new System.Threading.Thread(StartSplash);
            SplashThread.Start();

            LoadSettings(); // Loads stored settings


            splash.Invoke(new EventHandler(splash.KillMe));
            splash.Dispose();
            splash = null;

            BringToFront();
            Focus();
        }

        private void mainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Stores form location
            if (Properties.Settings.Default.StoreLocation)
                Properties.Settings.Default.Location = Location;
            // Gets the last used settings and saves them
            Properties.Settings.Default.LastScanSettings = getScanSettings();
        }

        private void mainFrm_Move(object sender, EventArgs e)
        {
            if (frmImageSettings != null)
            {
                if (frmImageSettings.Visible) // Moves the image settings form with main form
                {
                    var tempLocation = new Point(Location.X + Size.Width, Location.Y);
                    if (tempLocation.X + frmImageSettings.Width >= Screen.PrimaryScreen.WorkingArea.Width)
                    {
                        frmImageSettings.Location = new Point(Location.X - frmImageSettings.Width, Location.Y);
                    }
                    else
                    {
                        frmImageSettings.Location = tempLocation;
                    }
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Enabled = false;

            // Starts copy process
            var settings = getScanSettings();

            appControl.Copy(settings);

            Enabled = true;
        }

        private void SelScanner_Click(object sender, EventArgs e)
        {
            try // Tries changing the scanner
            {
                string newscannerID = appControl.changescanner();
                if (newscannerID is null)
                    return;

                if ((Properties.Settings.Default.DeviceID ?? "") != (newscannerID ?? ""))
                {
                    Properties.Settings.Default.DeviceID = newscannerID; // if a deviceId is returned, store it
                }

                ScannerStatusLabel.Text = appControl.ScannerDescription;
                chkADF.Enabled = appControl.CanUseADF();
                if (frmImageSettings.Visible)
                    btnImageSettings.PerformClick();
                frmImageSettings.Dispose();
                frmImageSettings = new frmImageSettings();
            }
            catch (ExitException ex)
            {
            }
            // Don't change the scanner
            catch (NullReferenceException ex)
            {
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        // Show printer settings
        private void PrintSetup_Click(object sender, EventArgs e)
        {
            // Disable printer settings when chkPDF is selected (applies to the status bar click, the button gets disabled)
            if (!chkPDF.Checked)
            {

                // Address bug #277: some printers, namely Foxit PDF Printer, don't load the PrintDocument.PageDefaultSettings,
                // thus replacing previously set options such as PaperSize to their default. This is unexpected user behavior.
                // We replicate the same behavior as Word, i.e. force the settings chosen from iCopy interface:
                // * Color
                // * PaperSize
                PaperSize tmpPaperSize = (PaperSize)cboPaperSize.SelectedItem;
                bool tmpColor = appControl.Printer.PageSettings.Color;

                appControl.Printer.showPreferences();
                cboPaperSize.Items.Clear();

                appControl.Printer.PageSettings.PaperSize = tmpPaperSize;
                appControl.Printer.PageSettings.Color = tmpColor;

                // Update paper sizes combo box
                cboPaperSize.DisplayMember = "PaperName"; // Links 
                int i;
                foreach (PaperSize pSize in appControl.Printer.PrinterSettings.PaperSizes)
                {

                    i = cboPaperSize.Items.Add(pSize);
                    if (pSize.Kind == appControl.Printer.PageSettings.PaperSize.Kind)
                    {
                        cboPaperSize.SelectedIndex = i;
                    }

                }


                Properties.Settings.Default.DefaultPrinter = appControl.Printer.Name;
                PrinterStatusLabel.Text = appControl.Printer.Name;
                if (PrinterStatusLabel.Text.Contains("PDF"))
                {
                    PrinterStatusLabel.Image = My.Resources.Resources.pdf_icon;
                }
                else
                {
                    PrinterStatusLabel.Image = My.Resources.Resources.printer;
                }
            }
        }

        private void cboScanMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Changes scanning intent and print mode
            switch (cboScanMode.SelectedIndex)
            {
                case 0:
                    {
                        intent = WiaImageIntent.ColorIntent;
                        cboPrintMode.SelectedIndex = 0;
                        break;
                    }
                case 1:
                    {
                        intent = WiaImageIntent.GrayscaleIntent;
                        cboPrintMode.SelectedIndex = 1;
                        break;
                    }
                case 2:
                    {
                        intent = WiaImageIntent.TextIntent;
                        cboPrintMode.SelectedIndex = 1;
                        break;
                    }
            }
        }

        private void cboPrintMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Changes print mode
            appControl.Printer.PageSettings.Color = !Conversions.ToBoolean(cboPrintMode.SelectedIndex); // If index is 0, returns true
            Properties.Settings.Default.PrintColor = appControl.Printer.PageSettings.Color;
        }


        private void btnImageSettings_Click(object sender, EventArgs e)
        {

            // Shows / hides image settings form in the correct position
            if (frmImageSettings.Visible == false)
            {
                var tempLocation = new Point(Location.X + Size.Width, Location.Y);
                if (tempLocation.X + frmImageSettings.Width >= Screen.PrimaryScreen.WorkingArea.Width)
                {
                    frmImageSettings.Location = new Point(Location.X - frmImageSettings.Width, Location.Y);
                }
                else
                {
                    frmImageSettings.Location = tempLocation;
                }

                frmImageSettings.Show();
                btnImageSettings.Text = appControl.GetLocalizedString(LocalizedRootStr + "btnImageSettingsHide");
            }
            else
            {
                frmImageSettings.Hide();
                btnImageSettings.Text = appControl.GetLocalizedString(LocalizedRootStr + "btnImageSettings");
            }
        }

        private void llblSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmOptions.ShowDialog();
            frmImageSettings.RefreshSettings();
        }

        private void llblAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AboutBox.Show();
        }

        private ScanSettings getScanSettings()
        {
            ScanSettings opts;
            opts = Properties.Settings.Default.LastScanSettings;
            opts.Path = "";

            try
            {
                opts.Resolution = Convert.ToInt32(frmImageSettings.cboResolution.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException ex) // Fixes a bug
            {
                if (Conversions.ToBoolean(Conversions.ToInteger(Properties.Settings.Default.LastScanSettings.Resolution != 0) | ~default(int)))
                    opts.Resolution = Properties.Settings.Default.LastScanSettings.Resolution;
            }

            opts.Brightness = frmImageSettings.tbBrightness.Value;
            opts.Contrast = frmImageSettings.tbContrast.Value;
            opts.Intent = intent;
            opts.Preview = chkPreview.Checked;
            opts.Quality = frmImageSettings.tbCompression.Value;
            opts.Copies = (int)Math.Round(nudNCopie.Value);
            opts.Scaling = frmImageSettings.tbScaling.Value;
            opts.UseADF = chkADF.Checked;
            opts.Duplex = chkDuplex.Checked;
            opts.Multipage = chkMultipage.Checked;
            opts.Center = frmImageSettings.chkCenter.Checked;

            try // Fix for https://sourceforge.net/p/icopy/bugs/239/
            {
                opts.PaperSize = appControl.Printer.PrinterSettings.PaperSizes[cboPaperSize.SelectedIndex];
            }
            catch (IndexOutOfRangeException ex)
            {
                opts.PaperSize = appControl.Printer.PrinterSettings.PaperSizes[0];
            }

            if (chkSaveToFile.Checked)
            {
                opts.ScanOutput = ScanOutput.File;
                if (!Properties.Settings.Default.FileAskWhereToSave)
                {
                    opts.Path = Properties.Settings.Default.FileSavePath;
                }
            }
            else if (chkPDF.Checked)
            {
                opts.ScanOutput = ScanOutput.PDF;
                if (!Properties.Settings.Default.PDFAskWhereToSave)
                {
                    opts.Path = Properties.Settings.Default.PDFSavePath;
                }
            }

            Properties.Settings.Default.LastScanSettings = opts;
            return opts;
        }

        private void cboPaperSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            appControl.Printer.PageSettings.PaperSize = appControl.Printer.PrinterSettings.PaperSizes[cboPaperSize.SelectedIndex];
            Properties.Settings.Default.PrinterSize = cboPaperSize.Text; // Stores value in settings
        }

        private void VersionStatusLabel_Click(object sender, EventArgs e)
        {
            Process.Start(weburl);
        }

        private void chkADF_CheckedChanged(object sender, EventArgs e)
        {
            chkDuplex.Enabled = chkADF.Checked & appControl.CanDoDuplex();

            if (chkADF.Checked)
            {
                chkPreview.Enabled = false;
                chkPreview.Checked = false;
            }
            else
            {
                chkPreview.Enabled = true;
                chkDuplex.Checked = false;
            }
        }

        private void Outputchanged(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, chkPDF))
            {
                chkSaveToFile.Checked = chkSaveToFile.Checked & !chkPDF.Checked;
            }
            else
            {
                chkPDF.Checked = chkPDF.Checked & !chkSaveToFile.Checked;
            }

            if (chkPDF.Checked)
            {
                chkSaveToFile.Checked = false;
                PrinterStatusLabel.Image = My.Resources.Resources.pdf_icon;
                // PrinterStatusLabel.Text = "Export to PDF"
                PrinterStatusLabel.Text = appControl.GetLocalizedString(LocalizedRootStr + "PrinterStatusLabel_PDF");
            }
            else if (chkSaveToFile.Checked)
            {
                chkPDF.Checked = false;
                PrinterStatusLabel.Image = My.Resources.Resources.saveToFile;
                // PrinterStatusLabel.Text = "Save to file"
                PrinterStatusLabel.Text = appControl.GetLocalizedString(LocalizedRootStr + "PrinterStatusLabel_File");
            }
            else
            {
                PrinterStatusLabel.Text = appControl.Printer.Name;
                if (PrinterStatusLabel.Text.Contains("PDF"))
                {
                    PrinterStatusLabel.Image = My.Resources.Resources.pdf_icon;
                }
                else
                {
                    PrinterStatusLabel.Image = My.Resources.Resources.printer;
                }
                cboPaperSize.Enabled = true;
                lblPaperSize.Enabled = true;
            }

            // Enable/disable controls that are unused with the pdf and file modes
            // Print mode
            cboPrintMode.Enabled = !(chkPDF.Checked | chkSaveToFile.Checked);
            lblPrinter.Enabled = !(chkPDF.Checked | chkSaveToFile.Checked);
            // Paper Size (only for save to file)
            cboPaperSize.Enabled = !chkSaveToFile.Checked;
            lblPaperSize.Enabled = !chkSaveToFile.Checked;
            // Printer setup
            btnPrintSetup.Enabled = !(chkPDF.Checked | chkSaveToFile.Checked);
            // N* of copies
            nudNCopie.Enabled = !(chkPDF.Checked | chkSaveToFile.Checked);
            lblCopies.Enabled = !(chkPDF.Checked | chkSaveToFile.Checked);
            // JPEG compression
            frmImageSettings.lblCompressionLabel.Enabled = chkSaveToFile.Checked;
            frmImageSettings.tbCompression.Enabled = chkSaveToFile.Checked;
            frmImageSettings.lblCompression.Enabled = chkSaveToFile.Checked;
            // Image centering and scaling
            frmImageSettings.chkCenter.Enabled = !chkSaveToFile.Checked;
            frmImageSettings.tbScaling.Enabled = !chkSaveToFile.Checked;
            frmImageSettings.txtScaling.Enabled = !chkSaveToFile.Checked;

        }

        private void nudNCopie_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblCopies_Click(object sender, EventArgs e)
        {

        }

        private void cboPaperSize_DropDown(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            var font = senderComboBox.Font;

            int vertScrollBarWidth;
            // Is there a vertical scroll bar?
            if (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
            {
                vertScrollBarWidth = senderComboBox.Items.Count;
            }
            else
            {
                vertScrollBarWidth = 0;
            }

            int newWidth;

            foreach (object obj in senderComboBox.Items)
            {
                newWidth = TextRenderer.MeasureText(senderComboBox.GetItemText(obj), font).Width + vertScrollBarWidth;
                if (width < newWidth)
                    width = newWidth;
            }

            senderComboBox.DropDownWidth = width;
        }
    }
}