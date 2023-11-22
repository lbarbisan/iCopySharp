using System;
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
            return ShowDialog(owner);
        }
        public new DialogResult ShowDialog(int acquiredPages)
        {
            lblAcquiredPagesN.Text = acquiredPages.ToString();
            return ShowDialog(Owner);
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