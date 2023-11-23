using System;
using System.IO;
using System.Threading;
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

namespace iCopy
{

    public partial class dlgScanMorePages
    {
        private string localizedRootStr;

        public dlgScanMorePages()
        {
            InitializeComponent();
        }

        /// <summary>
    /// Shows the dlgScanMorePages as a dialog
    /// </summary>
    /// <param name="acquiredPages">The number of pages already acquired. It is shown in the dialog as a reference</param>
    /// <returns></returns>
    /// <remarks></remarks>
        public new DialogResult ShowDialog(int acquiredPages, IWin32Window owner)
        {
            
            lblAcquiredPagesN.Text = acquiredPages.ToString();

            var file = Path.GetDirectoryName(this.GetType().Assembly.Location);
            FileSystemWatcher morePageswatcher = new FileSystemWatcher(file);
            morePageswatcher.EnableRaisingEvents = true;
            morePageswatcher.Changed += MorePagesWatcher_Changed;
            
            var result = ShowDialog(owner);
            return result;
        }

        public new DialogResult ShowDialog(int acquiredPages)
        {
            
            lblAcquiredPagesN.Text = acquiredPages.ToString();

            var file = Path.GetDirectoryName(this.GetType().Assembly.Location);
            FileSystemWatcher morePageswatcher = new FileSystemWatcher(file);
            morePageswatcher.EnableRaisingEvents = true;
            morePageswatcher.Created += MorePagesWatcher_Changed;

            var result = ShowDialog(Owner);
            return result;
        }

        private void MorePagesWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            var file = Path.GetDirectoryName(this.GetType().Assembly.Location);
            if (File.Exists(file + "\\morepages.lock"))
            {
                this.BeginInvoke((MethodInvoker)delegate {
                    // Running on the UI thread
                    Thread.Sleep(1000);
                    File.Delete(e.FullPath);
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                });
            }

            if (File.Exists(file + "\\stopscan.lock"))
            {
                this.BeginInvoke((MethodInvoker)delegate {
                    // Running on the UI thread
                    Thread.Sleep(1000);
                    File.Delete(e.FullPath);
                    this.DialogResult = DialogResult.No;
                    this.Close();
                });
            }

        }

        private void DlgScanMorePages_Load(object sender, EventArgs e)
        {
            localizedRootStr = Name + "_";
            Text = appControl.GetLocalizedString(Name);
            foreach (Control control in Controls)
            {
                string str = appControl.GetLocalizedString(localizedRootStr + control.Name);
                if (str != null)
                {
                    control.Text = str;
                }

                ToolTip1.SetToolTip(control, appControl.GetLocalizedString(localizedRootStr + control.Name + "ToolTip"));
            }
        }
    }
}