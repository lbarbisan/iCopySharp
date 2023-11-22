using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic;

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

    public sealed partial class AboutBox
    {
        public AboutBox()
        {
            InitializeComponent();
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            // Disables main form
            appControl.MainForm.Enabled = false;
            // Set the title of the form.
            string ApplicationTitle;
            if (!string.IsNullOrEmpty(My.MyProject.Application.Info.Title))
            {
                ApplicationTitle = My.MyProject.Application.Info.Title;
            }
            else
            {
                ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.MyProject.Application.Info.AssemblyName);
            }
            Text = string.Format("About {0}", ApplicationTitle);
            // Initialize all of the text displayed on the About Box.

            // properties dialog (under the "Project" menu).
            LabelProductName.Text = My.MyProject.Application.Info.ProductName;
            LabelVersion.Text = string.Format("Version {0}", My.MyProject.Application.Info.Version.ToString());
            LabelCopyright.Text = My.MyProject.Application.Info.Copyright;
            LabelCompanyName.Text = My.MyProject.Application.Info.CompanyName;
            lblDescription.Text = appControl.GetLocalizedString("AboutBox_lblDescription");
            Label1.Text = appControl.GetLocalizedString("AboutBox_Label1");
            LinkLabel3.Text = appControl.GetLocalizedString("AboutBox_LinkLabel3");
            LinkLabel5.Text = appControl.GetLocalizedString("AboutBox_Donate");
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string licenseFilePath = Application.StartupPath + @"\License.txt";
            if (System.IO.File.Exists(licenseFilePath))
            {
                Process.Start("Notepad.exe", licenseFilePath);
                Hide();
            }
            else
            {
                Utilities.MsgBoxWrap(appControl.GetLocalizedString("Msg_LicenseFileNotFound"), MsgBoxStyle.Information, "iCopy");
            }
        }

        private void LinkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://icopy.sourceforge.net");
            Hide();
        }

        private void Donate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://icopy.sourceforge.net/donate.html");
            Hide();
        }

        private void LinkLabel4_Click(object sender, EventArgs e)
        {
            Process.Start("http://icopy.sourceforge.net/donate.html");
            Hide();
        }

        private void AboutBox_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                appControl.MainForm.Enabled = true;
                appControl.MainForm.Show();
            }
        }

    }
}