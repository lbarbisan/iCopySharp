using System;

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
    public sealed partial class SplashScreen
    {
        private System.Timers.Timer timer;

        public SplashScreen()
        {
            timer = new System.Timers.Timer();
            InitializeComponent();
        }


        public void KillMe(object sender, EventArgs e)
        {
            Close();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            // Set up the dialog text at runtime according to the application's assembly information.  
            timer.Interval = 2000d;
            timer.Enabled = true;
            if (Properties.Settings.Default.CustomCulture)
                System.Threading.Thread.CurrentThread.CurrentUICulture = Properties.Settings.Default.Culture;
            // Application title
            ApplicationTitle.Text = string.Format(appControl.GetLocalizedString("splashScreen_Loading"), My.MyProject.Application.Info.Title);

            Version.Text = My.MyProject.Application.Info.Version.ToString();

            // Copyright info
            Copyright.Text = My.MyProject.Application.Info.Copyright;
        }

    }
}