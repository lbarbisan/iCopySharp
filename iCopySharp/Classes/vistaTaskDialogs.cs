using System;
using Microsoft.VisualBasic;
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

using Microsoft.WindowsAPICodePack.Dialogs;

namespace iCopy
{

    class WIARegisterDialog : IDisposable
    {

        private TaskDialog td;
        private bool OKClicked;

        public TaskDialogResult Show(string text, string instructions, string title, string cancel)
        {

            td = new TaskDialog();
            td.Caption = title;
            td.InstructionText = instructions;
            td.Icon = TaskDialogStandardIcon.Shield;
            td.Cancelable = true;
            td.Text = text;

            var CancelButton = new TaskDialogButton("Cancel", cancel);
            var OKButton = new TaskDialogButton("OK", "OK");
            OKButton.ShowElevationIcon = true;
            OKButton.Default = true;

            td.Controls.Add(OKButton);
            td.Controls.Add(CancelButton);

            CancelButton.Click += Cancel_Click;
            OKButton.Click += (_, __) => OK_Click();
            try
            {
                td.Show();
            }
            catch (NotSupportedException ex)
            {
                var msg = Utilities.MsgBoxWrap(text, (MsgBoxStyle)((int)MsgBoxStyle.Exclamation + (int)MsgBoxStyle.OkCancel), title);
                if (msg == MsgBoxResult.Ok)
                    return TaskDialogResult.Ok;
                else
                    return TaskDialogResult.Cancel;
            }

            if (OKClicked)
                return TaskDialogResult.Ok;
            else
                return TaskDialogResult.Cancel;
        }

        public void OK_Click()
        {
            td.Close(TaskDialogResult.Ok);
            OKClicked = true;
        }

        public void Cancel_Click(object sender, EventArgs e)
        {
            td.Close(TaskDialogResult.Cancel);
            OKClicked = false;
        }

        #region IDisposable Support
        private bool disposedValue; // To detect redundant calls

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    td.Close();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        // Protected Overrides Sub Finalize()
        // ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        // Dispose(False)
        // MyBase.Finalize()
        // End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}