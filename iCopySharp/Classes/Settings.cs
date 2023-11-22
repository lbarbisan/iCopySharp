
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

using System.Configuration;
using Microsoft.VisualBasic.CompilerServices;

namespace iCopy.My
{

    // This class allows you to handle specific events on the settings class:
    // The SettingChanging event is raised before a setting's value is changed.
    // The PropertyChanged event is raised after a setting's value is changed.
    // The SettingsLoaded event is raised after the setting values are loaded.
    // The SettingsSaving event is raised before the setting values are saved.

    internal sealed partial class MySettings
    {
        public MySettings()
        {
            SettingChanging += MySettings_SettingChanging;
        }

        private void MySettings_SettingChanging(object sender, SettingChangingEventArgs e)
        {
            if (e.SettingName == "DefaultIntent")
            {
                if (Conversions.ToBoolean(!Operators.OrObject(Operators.OrObject(Operators.ConditionalCompareObjectEqual(e.NewValue, 1, false), Operators.ConditionalCompareObjectEqual(e.NewValue, 2, false)), Operators.ConditionalCompareObjectEqual(e.NewValue, 4, false))))
                {
                    e.Cancel = true;
                }
            }

        }

    }
}