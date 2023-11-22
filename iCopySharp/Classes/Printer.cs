using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace iCopy
{

    internal class Printer
    {

        private PrintDocument _pd;

        private PrintDocument pd
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _pd;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_pd != null)
                {
                    _pd.PrintPage -= pd_Print;
                    _pd.BeginPrint -= pd_BeginPrint;
                    _pd.EndPrint -= pd_EndPrint;
                }

                _pd = value;
                if (_pd != null)
                {
                    _pd.PrintPage += pd_Print;
                    _pd.BeginPrint += pd_BeginPrint;
                    _pd.EndPrint += pd_EndPrint;
                }
            }
        }

        private short _scaleperc;
        private bool _center;

        private Queue<string> _images = new Queue<string>(); // The buffer of images to be printed

        public event PrintEventHandler BeginPrint;
        public event PrintEventHandler EndPrint;

        public Printer()
        {
            // Initializes PrintDocument
            pd = new PrintDocument();
        }

        public string Name
        {
            get
            {
                string NameRet = default;
                NameRet = pd.PrinterSettings.PrinterName;
                return NameRet;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    var tmpPd = new PrintDocument();
                    value = tmpPd.PrinterSettings.PrinterName;
                }
                pd.PrinterSettings.PrinterName = value;
                if (!pd.PrinterSettings.IsValid)
                    throw new ArgumentException("Printer name is not valid");
                Trace.WriteLine("Printer set to " + value);
            }
        }

        public PrinterSettings PrinterSettings
        {
            get
            {
                PrinterSettings PrinterSettingsRet = default;
                PrinterSettingsRet = pd.PrinterSettings;
                return PrinterSettingsRet;
            }
        }

        public PageSettings PageSettings
        {
            get
            {
                PageSettings PageSettingsRet = default;
                PageSettingsRet = pd.DefaultPageSettings;
                return PageSettingsRet;
            }
        }

        public void showPreferences()
        {
            var dlg = new PrintDialog();
            Trace.WriteLine("Changing printer settings");
            dlg.Document = pd;
            Trace.Indent();
            Trace.WriteLine("Current page settings: " + pd.DefaultPageSettings.ToString());
            Trace.WriteLine("Current printer settings: " + pd.PrinterSettings.ToString());
            dlg.UseEXDialog = true;
            dlg.AllowSelection = false;
            dlg.AllowSomePages = false;
            dlg.ShowDialog();
            pd.DefaultPageSettings = dlg.Document.DefaultPageSettings;
            Trace.WriteLine("New page settings: " + pd.DefaultPageSettings.ToString());
            Trace.WriteLine("New printer settings: " + pd.PrinterSettings.ToString());
            Trace.Unindent();
        }

        public void AddImages(List<string> images, short scaleperc = 100, bool center = false)
        {
            if (images != null)
            {
                foreach (string img in images)
                    _images.Enqueue(img);
                _scaleperc = scaleperc;
                _center = center;
            }
        }

        public void Print(short copies = 1)
        {
            // Check if Image Buffer is empty
            if (_images.Count == 0)
                return;

            pd.PrinterSettings.Copies = copies;
            pd.DocumentName = "iCopy " + DateTime.Now.ToString("yyyy-MM-dd hh-mm");
            // Starts printing process
            pd.Print();

        }

        private void pd_Print(object sender, PrintPageEventArgs e)
        {
            // Print the current image in the image buffer

            // Loads the image from the temporary file
            string imgPath = _images.Dequeue();
            var img = Image.FromFile(imgPath);

            // Resize the image, then draw it 
            if (_center)
            {
                var argpageUnit = e.Graphics.PageUnit;
                var picture_bounds = img.GetBounds(ref argpageUnit);
                e.Graphics.PageUnit = argpageUnit;

                var margin_bounds = e.Graphics.VisibleClipBounds;

                // Apply the transformation.
                float dx = (float)((double)margin_bounds.Left + ((double)margin_bounds.Width - _scaleperc / 100d * (double)e.Graphics.DpiX / (double)img.HorizontalResolution * (double)picture_bounds.Width) / 2d);
                float dy = (float)((double)margin_bounds.Top + ((double)margin_bounds.Height - _scaleperc / 100d * (double)e.Graphics.DpiY / (double)img.VerticalResolution * (double)picture_bounds.Height) / 2d);
                e.Graphics.TranslateTransform(dx, dy);
            }
            e.Graphics.ScaleTransform((float)(_scaleperc / 100d), (float)(_scaleperc / 100d));

            e.Graphics.DrawImage(img, 0, 0);

            img.Dispose();

            try
            {
                System.IO.File.Delete(imgPath);
            }
            catch (Exception ex)
            {
                throw;
            }
            // Check if other pages have to be printed
            if (_images.Count > 0)
            {
                e.HasMorePages = true;
            }
        }

        public void pd_BeginPrint(object sender, PrintEventArgs e)
        {
            BeginPrint?.Invoke(sender, e);
        }

        public void pd_EndPrint(object sender, PrintEventArgs e)
        {
            EndPrint?.Invoke(sender, e);

            // Empty image buffer
            _images.Clear();
        }

        public void ClearBuffer()
        {
            _images.Clear();
        }

    }
}