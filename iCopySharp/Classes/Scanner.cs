using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.VisualBasic;
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
    public class Scanner
    {

        private DeviceManager manager;
        private Device _device = null;
        private Item _scanner = null;
        private List<int> _AvailableResolutions;
        private string _description = "";
        private string _deviceID = "";
        private bool _canUseADF = false;
        private bool _canDoDuplex = false;

        public Scanner(string deviceID)
        {
            manager = new DeviceManager();
            if (deviceID is null)
                throw new ArgumentNullException("deviceID", "No deviceID specified");
            if (manager.DeviceInfos.Count == 0)
                throw new ArgumentException("No WIA device connected");

            try
            {
                Trace.WriteLine(string.Format("Trying to establish connection with the Device {0}", deviceID));
                object argIndex = deviceID;
                _device = manager.DeviceInfos[ref argIndex].Connect();
                _deviceID = deviceID;
                _scanner = _device.Items[1];
                object argIndex1 = "Description";
                _description = Conversions.ToString(_device.Properties[ref argIndex1].get_Value());
                Trace.WriteLine(string.Format("Connection established with {0}. DeviceID: {1}", _description, _deviceID));
                _AvailableResolutions = GetAvailableResolutions();

                try
                {
                    object argIndex2 = "Document Handling Capabilities";
                    WIA_DPS_DOCUMENT_HANDLING_CAPABILITIES caps = (WIA_DPS_DOCUMENT_HANDLING_CAPABILITIES)Conversions.ToInteger(_device.Properties[ref argIndex2].get_Value());
                    if (Conversions.ToBoolean(caps & WIA_DPS_DOCUMENT_HANDLING_CAPABILITIES.FEED))
                    {
                        Trace.WriteLine(string.Format("This scanner supports ADF"));
                        _canUseADF = true;
                    }
                    if (Conversions.ToBoolean(caps & WIA_DPS_DOCUMENT_HANDLING_CAPABILITIES.DUP))
                    {
                        _canDoDuplex = true;
                    }
                }

                catch (Exception ex)
                {
                    // The scanner does not support ADF
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Couldn't connect to the scanner. DeviceID: {0}", deviceID));
                Trace.WriteLine(string.Format("Exception caught: {0}", ex.Message));
                throw;
            }
            finally
            {
                _scanner = null;
            }
            _device = null;
        }

        // This function calls the appropriate acquisition function depending on the scanner name / manufacturer.
        public List<string> Scan(ScanSettings options)
        {
            _device = null;
            if (_description == "Brother MFC-6800" | _description.Contains("Brother MFC-5440CN"))
            {
                return ScanBrother6800(options);
            }
            else if (_description.ToLower().Contains("brother") | _description.Contains("Canon MF4500") | _description.ToLower().Contains("epson xp-850 mfp"))
            {
                return ScanBrother(options);
            }
            else
            {
                return ScanNormal(options);
            }
            _device = null;
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        public string DeviceId
        {
            get
            {
                return _deviceID;
            }
        }

        public List<int> AvailableResolutions
        {
            get
            {
                return _AvailableResolutions;
            }
        }

        public bool CanUseADF
        {
            get
            {
                return _canUseADF;
            }
        }

        public bool CanDoDuplex
        {
            get
            {
                return _canDoDuplex;
            }
        }

        [CLSCompliant(false)]
        public DeviceEvents Events
        {
            get
            {
                object argIndex = DeviceId;
                var device = manager.DeviceInfos[ref argIndex].Connect();
                return device.Events;
            }
        }

        public void XMLReport()
        {
            Device _device;
            Item _scanner;
            object argIndex = DeviceId;
            _device = manager.DeviceInfos[ref argIndex].Connect();
            _deviceID = DeviceId;
            _scanner = _device.Items[1];
            var doc = new XmlTextWriter("properties.xml", null);
            doc.WriteStartDocument();
            doc.WriteStartElement("Device");
            doc.WriteStartElement("DeviceProperties");
            PropertiesToXMLType.PropertiesToXML(_device.Properties, doc);
            doc.WriteEndElement();

            doc.WriteStartElement("ScannerProperties");
            PropertiesToXMLType.PropertiesToXML(_scanner.Properties, doc);
            doc.WriteEndElement();
            doc.WriteEndElement();
            doc.WriteEndDocument();
            doc.Close();
            _scanner = null;
        }

        public void WritePropertiesLog()
        {
            Trace.WriteLine("SCANNER PROPERTIES");
            Trace.Indent();
            Device _device;
            Item _scanner;
            object argIndex = DeviceId;
            _device = manager.DeviceInfos[ref argIndex].Connect();
            _deviceID = DeviceId;
            _scanner = _device.Items[1];
            foreach (Property p in _device.Properties)
            {
                try
                {
                    TraceProp(p);
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Couldn't read property {0}", p.ToString());
                }
            }

            foreach (Property p in _scanner.Properties)
            {
                try
                {
                    TraceProp(p);
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Couldn't read property {0}", p.PropertyID);
                }
            }

            Trace.Unindent();
            _scanner = null;
        }

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        private void SetWIAProperty(IProperties properties, object propName, object propValue)
        {
            Property prop = properties.get_Item(ref propName);

            /*try
            {*/
                prop.set_Value(ref propValue);
            /*}
            catch
            {
                // DPI can only be set to values listed in SubTypeValues
                // This sets the DPI to the lowest one supported by the scanner
                if (propName.ToString() == WIA_HORIZONTAL_SCAN_RESOLUTION_DPI || propName.ToString() == WIA_VERTICAL_SCAN_RESOLUTION_DPI)
                {
                    foreach (object test in prop.SubTypeValues)
                    {
                        prop.set_Value(test);
                        break;
                    }
                }
            }*/
        }

        private void SetBrightess(int value)
        {
            string prop_name = "Brightness";
            object argIndex = prop_name;
            var temp = _scanner.Properties[ref argIndex];
            prop_name = Conversions.ToString(argIndex);

            if (temp.SubType == WiaSubType.RangeSubType)
            {
                int min = temp.SubTypeMin;
                int max = temp.SubTypeMax;
                // Dim stp As Integer = temp.SubTypeStep
                int center = (int)Math.Round((max + min) / 2d);
                int delta = max - center;

                if (value <= 100 & value >= -100)
                {
                    int tmpVal = (int)Math.Round(Math.Round(value / 100d * delta + center, 0));
                    try
                    {
                        //temp = (object)tmpVal;
                        SetWIAProperty(_scanner.Properties, argIndex, tmpVal);
                        object argIndex1 = prop_name;
                        Trace.WriteLine(string.Format("Brightness set to {0} -> {1}", value.ToString(), _scanner.Properties[ref argIndex1].get_Value().ToString()));
                        prop_name = Conversions.ToString(argIndex1);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(string.Format("Brighness value not accepted by the scanner: {0}", tmpVal.ToString()));
                        throw new ArgumentException(prop_name + " value not accepted by the scanner: " + value.ToString() + " -> " + tmpVal.ToString());
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("value", prop_name + " setting must be between -100 and 100," + value.ToString() + " entered.");
                }
            }
            else
            {
                try
                {
                    //temp = (object)value;
                    SetWIAProperty(_scanner.Properties, argIndex, value);
                }
                catch (Exception ex)
                {
                    Utilities.MsgBoxWrap("There was an exception while setting the property " + prop_name + " to " + value.ToString() + ". Please report this information to iCopy bug tracker on Sourceforge:" + Constants.vbCrLf + "property type: " + temp.Type.ToString() + Constants.vbCrLf + "property subtype: " + temp.SubType.ToString(), MsgBoxStyle.Critical, "iCopy");
                }
            }
        }

        private void SetContrast(int value)
        {
            string prop_name = "Contrast";
            object argIndex = prop_name;
            var temp = _scanner.Properties[ref argIndex];
            prop_name = Conversions.ToString(argIndex);

            if (temp.SubType == WiaSubType.RangeSubType)
            {
                int min = temp.SubTypeMin;
                int max = temp.SubTypeMax;
                // Dim stp As Integer = temp.SubTypeStep
                int center = (int)Math.Round((max + min) / 2d);
                int delta = max - center;

                if (value <= 100 & value >= -100)
                {
                    int tmpVal = (int)Math.Round(Math.Round(value / 100d * delta + center, 0));
                    try
                    {
                        SetWIAProperty(_scanner.Properties, argIndex, tmpVal);
                        object argIndex1 = prop_name;
                        Trace.WriteLine(string.Format("Contrast set to {0} -> {1}", value.ToString(), _scanner.Properties[ref argIndex1].get_Value().ToString()));
                        prop_name = Conversions.ToString(argIndex1);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(string.Format("Contrast value not accepted by the scanner: {0}", tmpVal.ToString()));
                        throw new ArgumentException(prop_name + " value not accepted by the scanner: " + value.ToString() + " -> " + tmpVal.ToString());
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("value", prop_name + " setting must be between -100 and 100," + value.ToString() + " entered.");
                }
            }
            else
            {
                try
                {
                    SetWIAProperty(_scanner.Properties, argIndex, value);
                }
                catch (Exception ex)
                {
                    Utilities.MsgBoxWrap("There was an exception while setting the property " + prop_name + " to " + value.ToString() + ". Please report this information to iCopy bug tracker on Sourceforge:" + Constants.vbCrLf + "property type: " + temp.Type.ToString() + Constants.vbCrLf + "property subtype: " + temp.SubType.ToString(), MsgBoxStyle.Critical, "iCopy");
                }
            }
        }

        private void SetBitDepth(short value) // TODO: Probably useless
        {
            if (value <= 32 & value % 8 == 0) // La profondità è multipla di 8 e minore o uguale a 32 bit
            {
                try
                {
                    //object argIndex = "Bits Per Pixel";
                    //_scanner.Properties[ref argIndex].get_Value() = (object)value;
                    SetWIAProperty(_scanner.Properties, "Bits Per Pixel", value);
                    Trace.WriteLine(string.Format("Bits per Pixel set to {0}", value));
                }
                catch (ArgumentException ex)
                {
                    Trace.WriteLine(string.Format("Couldn't set Bits per Pixel set to {0}. ERROR {1}", value, ex.Message));
                }
                // Do nothing, there isn't any problem 
                catch (UnauthorizedAccessException ex)
                {
                }
                // Do nothing, the scanner doesn't allow to change the property
                catch (COMException ex)
                {
                    Trace.WriteLine(string.Format("Couldn't set Bits per Pixel set to {0}. ERROR {1}", value, ex.Message));
                }
            }
            else
            {
                throw new ArgumentException("Bit depth must be multiple of 8 and less or equal to 32");
            }

        }

        [CLSCompliant(false)]
        private void SetIntent(WiaImageIntent value)
        {
            if (value == WiaImageIntent.ColorIntent)
            {
                try
                {
                    object argIndex = "Current Intent";
                    //_scanner.Properties[ref argIndex].get_Value() = value;
                    SetWIAProperty(_scanner.Properties, argIndex, value);
                    Trace.WriteLine(string.Format("Intent set to {0}", value));
                }
                catch (COMException e)
                {
                    if (e.ErrorCode == (int)WIA_ERRORS.WIA_ERROR_PROPERTY_DONT_EXIST)
                    {
                        try
                        {
                            object argIndex1 = "Channels per pixel";
                            //_scanner.Properties[ref argIndex1].get_Value() = (object)3; // 3 channels (RGB)
                            SetWIAProperty(_scanner.Properties, argIndex1, 3);
                            Trace.WriteLine(string.Format("E: Couldn't set intent. Set channels per pixel instead"));
                        }
                        catch (COMException ex)
                        {
                            Trace.WriteLine(string.Format("E: Couldn't set intent. Report error"));
                            throw;
                        }
                    }
                    else
                    {
                        throw;
                    }
                }

                if (Properties.Settings.Default.LastScanSettings.BitDepth != 0)
                {
                    try
                    {
                        SetBitDepth((short)Properties.Settings.Default.LastScanSettings.BitDepth);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else if (value == WiaImageIntent.GrayscaleIntent | value == WiaImageIntent.TextIntent)
            {
                try
                {
                    object argIndex3 = "Current Intent";
                    //_scanner.Properties[ref argIndex3].get_Value() = value;
                    SetWIAProperty(_scanner.Properties, argIndex3, value);
                }
                catch (ArgumentException e)
                {
                    // TODO:Localization or better handling
                    Interaction.MsgBox("Yor scanner doesn't seem to support this scan intent. iCopy will use the default intent.", Constants.vbExclamation, "iCopy");
                }
                catch (COMException e)
                {
                    if (e.ErrorCode == (int)WIA_ERRORS.WIA_ERROR_PROPERTY_DONT_EXIST)
                    {
                        try
                        {
                            //object argIndex4 = "Channels per pixel";
                            //_scanner.Properties[ref argIndex4].get_Value() = (object)1; // 1 channel (Grayscale)
                            SetWIAProperty(_scanner.Properties, "Channels per pixel", 1);
                            Trace.WriteLine(string.Format("E: Couldn't set intent. Set channels per pixel instead"));
                        }
                        catch (COMException ex)
                        {
                            Trace.WriteLine(string.Format("E: Couldn't set intent. Report error"));
                            Interaction.MsgBox("Yor scanner doesn't seem to support this scan intent. iCopy will use the default intent.", Constants.vbExclamation, "iCopy");
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                object argIndex2 = "Current Intent";
                //_scanner.Properties[ref argIndex2].get_Value() = WiaImageIntent.UnspecifiedIntent;
                SetWIAProperty(_scanner.Properties, argIndex2, value);
            }
        }

        private void SetResolution(int value)
        {
            if (value == 0)
            {
                object argIndex = "Horizontal Resolution";
                value = Conversions.ToInteger(_scanner.Properties[ref argIndex].SubTypeDefault);
            }

            object argIndex1 = "Horizontal Resolution";
            //_scanner.Properties[ref argIndex1].get_Value() = (object)value;
            SetWIAProperty(_scanner.Properties, argIndex1, value);
            object argIndex2 = "Vertical Resolution";
            //_scanner.Properties[ref argIndex2].get_Value() = (object)value;
            SetWIAProperty(_scanner.Properties, argIndex2, value);
            Trace.WriteLine(string.Format("Resolution set to {0}", value));

        }

        private void SetExtent(short resolution, PaperSize pagesize)
        {
            // Calculate the extent in pixels based on paper size and resolution
            // Papersize width is in hundredths of an inch. 
            int width = (int)Math.Round(1.0d * (pagesize.Width * resolution / 100d));
            int height = (int)Math.Round(1.0d * (pagesize.Height * resolution / 100d));

            object argIndex = "Horizontal Extent";
            {
                var withBlock = _scanner.Properties[ref argIndex];
                try
                {
                    int max = withBlock.SubTypeMax;
                    if (width < max)
                    {
                        SetWIAProperty(_scanner.Properties, argIndex, width);
                        Trace.WriteLine(string.Format("Set Horizontal Extent to: {0}", withBlock));
                    }
                    else
                    {
                        //withBlock = (object)max;
                        SetWIAProperty(_scanner.Properties, argIndex, max);
                        Trace.WriteLine(string.Format("Set Horizontal Extent to its maximum value: {0}", withBlock));
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("Couldn't set Horizontal Extent. Exception: {0}", ex.ToString()));

                }
            }

            object argIndex1 = "Vertical Extent";
            {
                //var withBlock1 = _scanner.Properties[ref argIndex1].get_Value();
                try
                {
                    int max = (int)_scanner.Properties[ref argIndex1].get_Value();
                    if (height < max)
                    {
                        //withBlock1 = (object)height;
                        SetWIAProperty(_scanner.Properties, argIndex1, height);
                        Trace.WriteLine(string.Format("Set Vertical Extent to: {0}", height));
                    }
                    else
                    {
                        //withBlock1 = (object)max;
                        SetWIAProperty(_scanner.Properties, argIndex1, max);
                        Trace.WriteLine(string.Format("Set Vertical Extent to its maximum value: {0}", max));
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("Couldn't set Vertical Extent. Exception: {0}", ex.ToString()));

                }
            }
        }

        private void SetADF(ScanSettings settings)
        {
            // Set FEEDER options
            var handling = WIA_DPS_DOCUMENT_HANDLING_SELECT.FLATBED;

            if (settings.UseADF & CanUseADF)
            {
                handling = WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER;
                if (settings.Duplex & CanDoDuplex)
                {
                    handling = handling | WIA_DPS_DOCUMENT_HANDLING_SELECT.DUPLEX;
                }
            }

            // When duplexing, some scanners need WIA_DPS_PAGES to be set to 2, otherwise only the front page Is acquired
            if (Conversions.ToBoolean(handling & WIA_DPS_DOCUMENT_HANDLING_SELECT.DUPLEX))
            {
                try
                {
                    object argIndex = "Pages";
                    Trace.WriteLine(string.Format("WIA_DPS_PAGES Value: {0}", _device.Properties[ref argIndex].get_Value()));
                    object argIndex1 = "Pages";
                    //_device.Properties[ref argIndex1].get_Value() = (object)2;
                    SetWIAProperty(_device.Properties, argIndex1,2);
                }
                catch (COMException ex)
                {
                    Trace.WriteLine(string.Format("Couldn't read/write WIA_DPS_PAGES. Error {0}", ex.ErrorCode));
                }

            }

            try
            {
                Trace.WriteLine(string.Format("Trying to set WIA_DPS_DOCUMENT_HANDLING_SELECT to {0}", handling));
                object argIndex2 = "Document Handling Select";
                //_device.Properties[ref argIndex2].get_Value() = handling;
                SetWIAProperty(_device.Properties, argIndex2, handling);
                object argIndex3 = "Document Handling Select";
                Trace.WriteLine(string.Format("WIA_DPS_DOCUMENT_HANDLING_SELECT value: {0}", (WIA_DPS_DOCUMENT_HANDLING_SELECT)Conversions.ToInteger(_device.Properties[ref argIndex3].get_Value())));
            }
            catch (COMException ex)
            {
                switch (ex.ErrorCode)
                {
                    case (int)WIA_ERRORS.WIA_ERROR_PROPERTY_DONT_EXIST:
                        {
                            Trace.WriteLine(string.Format("WIA_DPS_DOCUMENT_HANDLING_SELECT not supported"));
                            break;
                        }

                    default:
                        {
                            Trace.WriteLine(string.Format("Couldn't set WIA_DPS_DOCUMENT_HANDLING_SELECT. Error code {0}", (WIA_ERRORS)ex.ErrorCode));
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception thrown while setting WIA_DPS_DOCUMENT_HANDLING_SELECT"));
                Trace.WriteLine(ex.ToString());
            }
        }

        private List<int> GetAvailableResolutions()
        {
            var _AvailableResolutions = new List<int>();

            object argIndex = "Horizontal Resolution";
            var res = _scanner.Properties[ref argIndex];
            if (res.SubType == WiaSubType.RangeSubType)
            {
                int stp = 100;
                int min = 100;
                int max = 2000;
                if (res.SubTypeMin <= 75)
                    _AvailableResolutions.Add(75); // Add ultra low resolution
                if (res.SubTypeMin > min)
                    min = res.SubTypeMin;
                if (res.SubTypeStep > stp)
                    stp = res.SubTypeStep;
                if (res.SubTypeMax < max)
                    max = res.SubTypeMax;
                for (int i = min, loopTo = max; stp >= 0 ? i <= loopTo : i >= loopTo; i += stp)
                    _AvailableResolutions.Add(i);
            }

            else if (res.SubType == WiaSubType.ListSubType)
            {
                object argIndex3 = "Horizontal Resolution";
                for (int i = 1, loopTo1 = _scanner.Properties[ref argIndex3].SubTypeValues.Count; i <= loopTo1; i++)
                {
                    object argIndex1 = "Horizontal Resolution";
                    object argIndex2 = "Horizontal Resolution";
                    _AvailableResolutions.Add(Conversions.ToInteger(_scanner.Properties[ref argIndex2].SubTypeValues.get_Item(i)));
                }
            }
            return _AvailableResolutions;
        }

        private ImageFile Compress(int quality, ImageFile tmpImg)
        {
            var ip = new ImageProcess();
            object argIndex = "Convert";
            ip.Filters.Add(ip.FilterInfos[ref argIndex].FilterID);
            object argIndex1 = "FormatID";
            //ip.Filters[1].Properties[ref argIndex1] = FormatID.wiaFormatJPEG;
            SetWIAProperty(ip.Filters[1].Properties, argIndex1, FormatID.wiaFormatJPEG);
            object argIndex2 = "Quality";
            //ip.Filters[1].Properties[ref argIndex2] = (object)quality;
            SetWIAProperty(ip.Filters[1].Properties, argIndex2, quality);

            var newimg = ip.Apply(tmpImg);
            tmpImg = null;
            return newimg;
        }

        private bool CheckIfThereAreMorePages()
        {
            bool hasMorePages = false;
            try
            {
                object argIndex = "Document Handling Status";
                WIA_DPS_DOCUMENT_HANDLING_STATUS status = (WIA_DPS_DOCUMENT_HANDLING_STATUS)Conversions.ToInteger(_device.Properties[ref argIndex].get_Value());
                Trace.WriteLine(string.Format("WIA_DPS_DOCUMENT_HANDLING_STATUS: {0}", status.ToString()));
                hasMorePages = (status & WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) != 0;
            }
            catch (COMException ex)
            {
                switch (ex.ErrorCode)
                {
                    case (int)WIA_ERRORS.WIA_ERROR_PROPERTY_DONT_EXIST:
                        {
                            Trace.WriteLine(string.Format("WIA_DPS_DOCUMENT_HANDLING_STATUS not supported"));
                            break;
                        }

                    default:
                        {
                            Trace.WriteLine(string.Format("Couldn't get WIA_DPS_DOCUMENT_HANDLING_STATUS. Error code {0}", ex.ErrorCode));
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception thrown on WIA_DPS_DOCUMENT_HANDLING_STATUS"));
                Console.Write(ex.ToString());
            }

            try
            {
                object argIndex1 = "Pages";
                Trace.WriteLine(string.Format("WIA_DPS_PAGES Value: {0}", _device.Properties[ref argIndex1].get_Value()));
                object argIndex2 = "Pages";
                if (Convert.ToInt32(_device.Properties[ref argIndex2].get_Value()) > 0)
                {
                    // More pages are available
                    hasMorePages = true;
                }
            }
            catch (COMException ex)
            {
                Trace.WriteLine(string.Format("Couldn't read WIA_DPS_PAGES. Error {0}", ex.ErrorCode));
            }

            return hasMorePages;
        }

        private List<string> ScanBrother6800(ScanSettings settings)
        {
            Trace.WriteLine(string.Format("Starting acquisition (Brother MFC-6800/5440CN)"));
            Trace.Indent();
            var imageList = new List<string>();
            var dialog = new WIA.CommonDialog();

            bool hasMorePages = true;

            ImageFile img = null;

            int AcquiredPages = 0;

            try // Make connection to the device
            {
                object argIndex = DeviceId;
                _device = manager.DeviceInfos[ref argIndex].Connect();
                _deviceID = DeviceId;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Couldn't connect to the device. ERROR {0}", ex.Message));
                throw;
            }

            // Set the feeder options
            SetADF(settings);

            // Connects the scanner
            try
            {
                _scanner = _device.Items[1];
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Couldn't connect to the scanner. ERROR {0}", ex.Message));
                throw;
            }

            // Set all properties
            Trace.WriteLine(string.Format("Setting scan properties"));
            Trace.WriteLine(settings);

            SetBrightess(settings.Brightness);
            SetContrast(settings.Contrast);
            SetIntent(settings.Intent);

            try
            {
                SetResolution(settings.Resolution);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Couldn't set resolution to {0}.", settings.Resolution));
                Trace.WriteLine(string.Format(@"\tError: {0}", ex.ToString()));
            }

            SetExtent((short)settings.Resolution, settings.PaperSize); // After setting resolution, maximize the extent

            try
            {
                SetBitDepth((short)settings.BitDepth);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Couldn't set BitDepth to {0}.", settings.BitDepth));
            }

            // Acquisition loop
            while (hasMorePages)
            {
                Trace.WriteLine(string.Format("Image count {0}. Acquiring next image", AcquiredPages));

                try // Some scanner need WIA_DPS_PAGES to be set to 1, otherwise all pages are acquired but only one is returned as ImageFile
                {
                    // Trace.WriteLine(String.Format("WIA_DPS_PAGES Value: {0}", _device.Properties("Pages").Value))
                    object argIndex1 = "Pages";
                    //_device.Properties[ref argIndex1].get_Value() = (object)1;
                    SetWIAProperty(_device.Properties, argIndex1, 1);
                }
                catch (COMException ex)
                {
                    Trace.WriteLine(string.Format("Couldn't read/write WIA_DPS_PAGES. Error {0}", ex.ErrorCode));
                }

                try // Check DOCUMENT_HANDLING_STATUS for debug purpose
                {
                    object argIndex2 = "Document Handling Status";
                    object argIndex3 = "Document Handling Status";
                    Trace.WriteLine(string.Format("WIA_DPS_DOCUMENT_HANDLING_STATUS: {0}", (_device.Properties[ref argIndex3].get_Value()).ToString()));
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Couldn't evaluate WIA_DPS_DOCUMENT_HANDLING_STATUS");
                }

                try // This is the acquisition part.
                {
                    if (settings.Preview)
                    {
                        img = dialog.ShowAcquireImage(WiaDeviceType.ScannerDeviceType, settings.Intent, AlwaysSelectDevice: false, UseCommonUI: false, CancelError: false);
                    }
                    else
                    {
                        img = (ImageFile)dialog.ShowTransfer(_scanner, CancelError: false);
                    } // This could throw ArgumentException.

                    Trace.WriteLine("Image acquired");
                    // Process the image to temporary file
                    if (img != null)
                    {
                        string tpath = Path.GetTempFileName();
                        try
                        {
                            File.Delete(tpath);
                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            img.SaveFile(tpath);
                            imageList.Add(tpath);
                            AcquiredPages += 1;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }

                        img = null;
                    }
                    else // Acquisition canceled
                    {
                        Trace.WriteLine("Acquisition canceled by the user");
                        break;
                    }
                }
                catch (COMException ex)
                {
                    bool exitWhile = false;
                    switch (ex.ErrorCode)
                    {
                        case (int)WIA_ERRORS.WIA_ERROR_WARMING_UP:
                            {
                                if (Utilities.MsgBoxWrap("The device is warming up. Press OK to Retry.", (MsgBoxStyle)((int)MsgBoxStyle.Information + (int)Constants.vbOKCancel)) == MsgBoxResult.Ok)
                                {
                                    Trace.WriteLine("Device warming up, waiting 2 seconds...");
                                    System.Threading.Thread.Sleep(2000);
                                    continue;
                                }
                                else
                                {
                                    exitWhile = true;
                                    break;
                                }
                            }
                        case (int)WIA_ERRORS.WIA_ERROR_PAPER_EMPTY:   // This error is reported when ADF is empty
                            {
                                Trace.WriteLine(string.Format("The ADF is empty"));
                                exitWhile = true;
                                break;                          // The acquisition is complete
                            }
                        case (int)WIA_ERRORS.WIA_ERROR_PAPER_JAM:
                            {
                                var result = Utilities.MsgBoxWrap("The paper in the document feeder is jammed." + "Please check the feeder and click Ok to resume the acquisition, Cancel to abort", (MsgBoxStyle)((int)Constants.vbOKCancel + (int)Constants.vbExclamation), "iCopy");
                                if (result == MsgBoxResult.Ok)
                                    continue;
                                if (result == MsgBoxResult.Cancel)
                                {
                                    exitWhile = true;
                                    break;
                                }

                                break;
                            }
                        case (int)WIA_ERRORS.WIA_ERROR_BUSY:
                            {
                                Trace.WriteLine("Device busy, waiting 2 seconds...");
                                System.Threading.Thread.Sleep(2000);
                                continue;
                            }

                        default:
                            {
                                Trace.WriteLine(string.Format("Acquisition threw the exception {0}", ex.ErrorCode));
                                throw;
                            }
                    }

                    if (exitWhile)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("Acquisition threw the exception {0}", ex.ToString()));
                    throw; // TODO: Error handling
                }

                if (!settings.UseADF)
                    break;
                // Determine if there are any more pages waiting
                Trace.WriteLine(string.Format("Checking if there are more pages..."));

                hasMorePages = CheckIfThereAreMorePages();
            }

            if (_scanner != null)
                _scanner = null;
            Console.Write("Acquisition complete, returning {0} images", AcquiredPages);
            Trace.Unindent();
            return imageList;
        }

        private List<string> ScanBrother(ScanSettings settings)
        {
            Trace.WriteLine(string.Format("Starting acquisition (Brother)"));
            Trace.Indent();
            var imageList = new List<string>();
            var dialog = new WIA.CommonDialog();
            bool hasMorePages = true;

            ImageFile img = null;

            int AcquiredPages = 0;

            try // Make connection to the device
            {
                object argIndex = DeviceId;
                _device = manager.DeviceInfos[ref argIndex].Connect();
                _deviceID = DeviceId;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Couldn't connect to the device. ERROR {0}", ex.Message));
                throw;
            }

            // Set FEEDER options
            SetADF(settings);

            // Connects the scanner
            try
            {
                _scanner = _device.Items[1];
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Couldn't connect to the scanner. ERROR {0}", ex.Message));
                throw;
            }

            // Set all properties
            Trace.WriteLine(string.Format("Setting scan properties"));
            Trace.WriteLine(settings);

            SetBrightess(settings.Brightness);
            SetContrast(settings.Contrast);
            SetIntent(settings.Intent);

            try
            {
                SetResolution(settings.Resolution);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Couldn't set resolution to {0}.", settings.Resolution));
                Trace.WriteLine(string.Format(@"\tError: {0}", ex.ToString()));
            }

            SetExtent((short)settings.Resolution, settings.PaperSize); // After setting resolution, maximize the extent

            try
            {
                if (settings.BitDepth != 0)
                    SetBitDepth((short)settings.BitDepth);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Couldn't set BitDepth to {0}.", settings.BitDepth));
            }

            // Acquisition loop
            while (hasMorePages)
            {
                Trace.WriteLine(string.Format("Image count {0}. Acquiring next image", AcquiredPages));

                try // Some scanner need WIA_DPS_PAGES to be set to 1, otherwise all pages are acquired but only one is returned as ImageFile
                {
                    object argIndex1 = "Pages";
                    Trace.WriteLine(string.Format("WIA_DPS_PAGES Value: {0}", _device.Properties[ref argIndex1].get_Value()));
                    object argIndex2 = "Pages";
                    //_device.Properties[ref argIndex2].get_Value() = (object)1;
                    SetWIAProperty(_device.Properties, argIndex2, 1);
                }
                catch (COMException ex)
                {
                    Trace.WriteLine(string.Format("Couldn't read/write WIA_DPS_PAGES. Error {0}", ex.ErrorCode));
                }

                try // Check DOCUMENT_HANDLING_STATUS for debug purpose
                {
                    object argIndex3 = "Document Handling Status";
                    object argIndex4 = "Document Handling Status";
                    Trace.WriteLine(string.Format("WIA_DPS_DOCUMENT_HANDLING_STATUS: {0}", (_device.Properties[ref argIndex4].get_Value()).ToString()));
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Couldn't evaluate DOCUMENT_HANDLING_STATUS");
                }

                try // This is the acquisition part
                {
                    if (settings.Preview)
                    {
                        img = dialog.ShowAcquireImage(WiaDeviceType.ScannerDeviceType, settings.Intent, AlwaysSelectDevice: false, UseCommonUI: false, CancelError: false);
                    }
                    else
                    {
                        img = (ImageFile)dialog.ShowTransfer(_scanner, CancelError: false);
                    } // This could throw ArgumentException.

                    Trace.WriteLine("Image acquired");
                    // Process the image to temporary file
                    if (img != null)
                    {
                        string tpath = Path.GetTempFileName();
                        try
                        {
                            File.Delete(tpath);
                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            img.SaveFile(tpath);
                            imageList.Add(tpath);
                            AcquiredPages += 1;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }

                        img = null;
                    }
                    else // Acquisition canceled
                    {
                        Trace.WriteLine("Acquisition canceled by the user");
                        break;
                    }
                }

                catch (COMException ex)
                {
                    bool exitWhile = false;
                    switch (ex.ErrorCode)
                    {
                        case (int)WIA_ERRORS.WIA_ERROR_PAPER_EMPTY:   // This error is reported when ADF is empty
                            {
                                Trace.WriteLine(string.Format("The ADF is empty"));
                                exitWhile = true;
                                break;                          // The acquisition is complete
                            }
                        case (int)WIA_ERRORS.WIA_ERROR_PAPER_JAM:
                            {
                                var result = Utilities.MsgBoxWrap("The paper in the document feeder is jammed." + "Please check the feeder and click Ok to resume the acquisition, Cancel to abort", (MsgBoxStyle)((int)Constants.vbOKCancel + (int)Constants.vbExclamation), "iCopy");
                                if (result == MsgBoxResult.Ok)
                                    continue;
                                if (result == MsgBoxResult.Cancel)
                                {
                                    exitWhile = true;
                                    break;
                                }

                                break;
                            }
                        case (int)WIA_ERRORS.WIA_ERROR_BUSY:
                            {
                                Trace.WriteLine("Device busy, waiting 2 seconds...");
                                System.Threading.Thread.Sleep(2000);
                                continue;
                            }

                        default:
                            {
                                Trace.WriteLine(string.Format("Acquisition threw the exception {0}", ex.ErrorCode));
                                throw;
                            }
                    }

                    if (exitWhile)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("Acquisition threw the exception {0}", ex.ToString()));
                    throw; // TODO: Error handling
                }

                if (!settings.UseADF)
                    break;
                // Determine if there are any more pages waiting
                Trace.WriteLine(string.Format("Checking if there are more pages..."));

                hasMorePages = CheckIfThereAreMorePages();
            }
            if (_scanner != null)
                _scanner = null;
            Trace.WriteLine(string.Format("Acquisition complete, returning {0} images", AcquiredPages));
            Trace.Unindent();
            return imageList;
        }

        private List<string> ScanNormal(ScanSettings settings)
        {
            Trace.WriteLine(string.Format("Starting acquisition"));
            Trace.Indent();
            Trace.WriteLine(settings);
            var imageList = new List<string>();
            var dialog = new WIA.CommonDialog();

            bool hasMorePages = true;

            ImageFile img = null;

            int AcquiredPages = 0;

            while (hasMorePages)
            {
                try // Make connection to the device
                {
                    object argIndex = DeviceId;
                    _device = manager.DeviceInfos[ref argIndex].Connect();
                    _deviceID = DeviceId;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("Couldn't connect to the device. ERROR {0}", ex.Message));
                    throw;
                }

                // Set FEEDER options
                SetADF(settings);

                // Connects to the scanner
                try
                {
                    _scanner = _device.Items[1];
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("Couldn't connect to the scanner. ERROR {0}", ex.Message));
                    throw;
                }

                // Set all properties
                Trace.WriteLine(string.Format("Setting scan properties"));

                SetBrightess(settings.Brightness);
                SetContrast(settings.Contrast);
                SetIntent(settings.Intent);

                try
                {
                    SetResolution(settings.Resolution);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("Couldn't set resolution to {0}.", settings.Resolution));
                    Trace.WriteLine(string.Format(@"\tError: {0}", ex.ToString()));
                }

                SetExtent((short)settings.Resolution, settings.PaperSize); // After setting resolution, maximize the extent

                // Used to set the page size to A4 to fix longer pages with ADF. Most scanners don't support this property so they won't be affected
                try
                {
                    if (settings.UseADF & settings.PaperSize.PaperName == "A4")
                    {
                        Trace.WriteLine("Setting scan extent to A4");
                        object argIndex1 = "Page Size";
                        //_device.Properties[ref argIndex1].get_Value() = WIA_IPS_PAGE_SIZE.WIA_PAGE_A4;
                        SetWIAProperty(_device.Properties, argIndex1, WIA_IPS_PAGE_SIZE.WIA_PAGE_A4) ;
                    }
                    else if (settings.UseADF & settings.PaperSize.PaperName == "Letter")
                    {
                        Trace.WriteLine("Setting scan extent to Letter");
                        object argIndex2 = "Page Size";
                        //_device.Properties[ref argIndex2].get_Value() = WIA_IPS_PAGE_SIZE.WIA_PAGE_LETTER;
                        SetWIAProperty(_device.Properties, argIndex2, WIA_IPS_PAGE_SIZE.WIA_PAGE_LETTER);
                    }
                    object argIndex3 = "Horizontal Extent";
                    object argIndex4 = "Vertical Extent";
                    Trace.WriteLine(string.Format("Horizontal extent {0}, Vertical extent {1}", _scanner.Properties[ref argIndex3].get_Value(), _scanner.Properties[ref argIndex4].get_Value()));
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Scanner does not support WIA_IPS_PAGE_SIZE");
                }

                try
                {
                    SetBitDepth((short)settings.BitDepth);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("Couldn't set BitDepth to {0}.", settings.BitDepth));
                }

                try
                {
                    Trace.WriteLine(string.Format("Image count {0}. Acquiring next image", AcquiredPages));
                    try
                    {
                        object argIndex5 = "Document Handling Status";
                        object argIndex6 = "Document Handling Status";
                        Trace.WriteLine(string.Format("WIA_DPS_DOCUMENT_HANDLING_STATUS: {0}", (_device.Properties[ref argIndex6].get_Value()).ToString()));
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine("Couldn't evaluate DOCUMENT_HANDLING_STATUS");
                    }
                    Trace.WriteLine(string.Format("Subitems count: {0}", _scanner.Items.Count));

                    if (settings.Preview)
                    {
                        img = dialog.ShowAcquireImage(WiaDeviceType.ScannerDeviceType, settings.Intent, AlwaysSelectDevice: false, UseCommonUI: false, CancelError: false);
                    }
                    else
                    {
                        img = (ImageFile)dialog.ShowTransfer(_scanner, CancelError: false);
                    }

                    if (img != null)
                    {
                        if (settings.Path.EndsWith("jpg")) // If this is a ScanToFile to jpg, apply compression
                        {
                            img = Compress(settings.Quality, img);
                        }

                        string tpath = Path.GetTempFileName();
                        try
                        {
                            File.Delete(tpath);
                        }
                        catch (Exception ex)
                        {
                        }

                        img.SaveFile(tpath);
                        // Sets the file as temporary, this should increase performance as the file is cached
                        File.SetAttributes(tpath, File.GetAttributes(tpath) | FileAttributes.Temporary);
                        imageList.Add(tpath);
                        img = null;

                        AcquiredPages += 1;
                    }
                    else // Acquisition canceled
                    {
                        Trace.WriteLine("Acquisition canceled by the user");
                        break;
                    }
                }
                catch (COMException ex)
                {
                    bool exitWhile = false;
                    switch (ex.ErrorCode)
                    {
                        case (int)WIA_ERRORS.WIA_ERROR_PAPER_EMPTY:   // This error is reported when ADF is empty
                            {
                                Trace.WriteLine(string.Format("The ADF is empty"));
                                exitWhile = true;
                                break;                          // The acquisition is complete
                            }
                        case (int)WIA_ERRORS.WIA_ERROR_PAPER_JAM:
                            {
                                Trace.WriteLine("Paper jammed.");
                                var result = Utilities.MsgBoxWrap("The paper in the document feeder is jammed." + "Please check the feeder and click Ok to resume the acquisition, Cancel to abort", (MsgBoxStyle)((int)Constants.vbOKCancel + (int)Constants.vbExclamation), "iCopy");
                                if (result == MsgBoxResult.Ok)
                                    continue;
                                if (result == MsgBoxResult.Cancel)
                                {
                                    exitWhile = true;
                                    break;
                                }

                                break;
                            }
                        case (int)WIA_ERRORS.WIA_ERROR_BUSY:
                            {
                                Trace.WriteLine("Device busy, waiting 2 seconds...");
                                System.Threading.Thread.Sleep(2000);
                                _scanner = null;
                                continue;
                            }

                        default:
                            {
                                Trace.WriteLine(string.Format("Acquisition threw the exception {0}", ex.ErrorCode));
                                break;
                            }
                    }

                    if (exitWhile)
                    {
                        break;
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("Acquisition threw the exception {0}", ex.ToString()));
                    throw; // TODO: Error handling
                }
                if (!settings.UseADF)
                    break;
                // determine if there are any more pages waiting
                Trace.WriteLine(string.Format("Checking if there are more pages..."));

                hasMorePages = CheckIfThereAreMorePages();

                _scanner = null;
                Trace.WriteLine(string.Format("Closed connection to the scanner"));
            }
            if (_scanner != null)
                _scanner = null;
            Console.Write("Acquisition complete, returning {0} images", AcquiredPages);
            Trace.Unindent();
            return imageList;
        }

        private void TraceProp(IProperty prop)
        {
            Trace.WriteLine(string.Format("Property {0}: {1}  TYPE {2}", prop.PropertyID, prop.Name, prop.Type));

            if (prop.IsVector)
            {
                Trace.WriteLine(Constants.vbTab + "IsVector");
            }
            else if (prop.SubType != WiaSubType.UnspecifiedSubType)
            {
                Trace.WriteLine(string.Format(Constants.vbTab + "Default value: {0}", prop.SubTypeDefault));
            }

            Trace.WriteLine(string.Format(Constants.vbTab + "ReadOnly: {0}, Value: {1}, SubType: {2}", prop.IsReadOnly, prop, prop.SubType));

            switch (prop.SubType)
            {
                case WiaSubType.FlagSubType:
                    {
                        for (int i = 1, loopTo = prop.SubTypeValues.Count; i <= loopTo; i++)
                            Trace.WriteLine(string.Format(Constants.vbTab + Constants.vbTab + "{0}", prop.SubTypeValues.get_Item(i)));
                        break;
                    }
                case WiaSubType.ListSubType:
                    {
                        for (int i = 1, loopTo1 = prop.SubTypeValues.Count; i <= loopTo1; i++)
                            Trace.WriteLine(string.Format(Constants.vbTab + Constants.vbTab + "{0}", prop.SubTypeValues.get_Item(i)));
                        break;
                    }
                case WiaSubType.RangeSubType:
                    {
                        Trace.WriteLine(string.Format(Constants.vbTab + Constants.vbTab + "Min {0}, Max {1}, Step {2}", prop.SubTypeMin, prop.SubTypeMax, prop.SubTypeStep)); // UnspecifiedSubType
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        #endregion
    }
}