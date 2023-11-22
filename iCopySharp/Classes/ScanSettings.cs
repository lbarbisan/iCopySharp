using System;
using System.Drawing.Printing;
using Microsoft.VisualBasic;
using WIA;

namespace iCopy
{

    public enum ScanOutput
    {
        Printer,
        File,
        PDF
    }

    public class ScanSettings
    {
        public const int DEFAULT_RESOLUTION = 300;
        public const int DEFAULT_SCALING = 100;
        public const int DEFAULT_QUALITY = 100;

        private int _Brightness;
        private bool _center;
        private int _Contrast;
        private int _BitDepth;
        private short _Resolution;
        private int _Quality;
        private WiaImageIntent _Intent;
        private bool _Preview;
        private int _Scaling;
        private int _Copies;
        private string _Path;
        private bool _UseADF;
        private bool _duplex;
        private ScanOutput _scanOutput;
        private bool _multipage;
        private PaperSize _paperSize;
        private bool _rotateDuplex;

        /// Creates default properties
        public ScanSettings()
        {

            _UseADF = false;
            _duplex = false;
            _rotateDuplex = false;
            _Brightness = 0;
            _Contrast = 0;
            _Quality = DEFAULT_QUALITY;
            _Preview = false;
            _Copies = 1;
            _Intent = WiaImageIntent.ColorIntent;
            _Resolution = DEFAULT_RESOLUTION;
            _Scaling = DEFAULT_SCALING;
            _BitDepth = 0;
            _Path = "";
            _scanOutput = ScanOutput.Printer;
            _multipage = false;
            _center = true;
            _paperSize = new PaperSize("A4", 827, 1169);
        }

        public int Brightness
        {
            get
            {
                return _Brightness;
            }
            set
            {
                _Brightness = value;
            }
        }

        public bool Center
        {
            get
            {
                return _center;
            }
            set
            {
                _center = value;
            }
        }

        public int Contrast
        {
            get
            {
                return _Contrast;
            }
            set
            {
                _Contrast = value;
            }
        }

        public PaperSize PaperSize
        {
            get
            {
                return _paperSize;
            }
            set
            {
                _paperSize = value;
            }
        }

        public int Resolution
        {
            get
            {
                return _Resolution;
            }
            set
            {
                _Resolution = (short)value;
            }
        }

        [CLSCompliant(false)]
        public WiaImageIntent Intent
        {
            get
            {
                return _Intent;
            }
            set
            {
                _Intent = value;
            }
        }

        public int Quality
        {
            get
            {
                return _Quality;
            }
            set
            {
                if (value <= 100 | value > 0)
                {
                    _Quality = value;
                }
                else
                {
                    throw new ArgumentException("Quality value must be between 0 and 100");
                }
            }
        }

        public bool Preview
        {
            get
            {
                return _Preview;
            }
            set
            {
                _Preview = value;
            }
        }

        public int Scaling
        {
            get
            {
                return _Scaling;
            }
            set
            {
                _Scaling = value;
            }
        }

        public int Copies
        {
            get
            {
                return _Copies;
            }
            set
            {
                _Copies = value;
            }
        }

        public string Path
        {
            get
            {
                return _Path;
            }
            set
            {
                _Path = value;
            }
        }

        public bool Multipage
        {
            get
            {
                return _multipage;
            }
            set
            {
                _multipage = value;
            }
        }

        public ScanOutput ScanOutput
        {
            get
            {
                return _scanOutput;
            }
            set
            {
                _scanOutput = value;
            }
        }

        public bool UseADF
        {
            get
            {
                return _UseADF;
            }
            set
            {
                _UseADF = value;
            }
        }

        public bool Duplex
        {
            get
            {
                return _duplex;
            }
            set
            {
                _duplex = value;
            }
        }

        public bool RotateDuplex
        {
            get
            {
                return _rotateDuplex;
            }
            set
            {
                _rotateDuplex = value;
            }
        }

        public int BitDepth
        {
            get
            {
                return _BitDepth;
            }
            set
            {
                _BitDepth = value;
            }
        }

        public override string ToString()
        {
            return Constants.vbTab + "Bit Depth: " + Constants.vbTab + BitDepth.ToString() + Constants.vbCrLf + Constants.vbTab + "Brightness: " + Constants.vbTab + Brightness.ToString() + Constants.vbCrLf + Constants.vbTab + "Contrast: " + Constants.vbTab + Contrast.ToString() + Constants.vbCrLf + Constants.vbTab + "Resolution: " + Constants.vbTab + Resolution.ToString() + Constants.vbCrLf + Constants.vbTab + "Intent: " + Constants.vbTab + Intent.ToString() + Constants.vbCrLf + Constants.vbTab + "Quality: " + Constants.vbTab + Quality.ToString() + Constants.vbCrLf + Constants.vbTab + "Scaling: " + Constants.vbTab + Scaling.ToString() + Constants.vbCrLf + Constants.vbTab + "Copies: " + Constants.vbTab + Copies.ToString() + Constants.vbCrLf + Constants.vbTab + "Preview: " + Constants.vbTab + Preview.ToString() + Constants.vbCrLf + Constants.vbTab + "UseADF: " + Constants.vbTab + UseADF.ToString() + Constants.vbCrLf + Constants.vbTab + "Duplex: " + Constants.vbTab + Duplex.ToString() + Constants.vbCrLf + Constants.vbTab + "Rotate Duplex: " + Constants.vbTab + RotateDuplex.ToString() + Constants.vbCrLf + Constants.vbTab + "Multipage: " + Constants.vbTab + Multipage.ToString() + Constants.vbCrLf + Constants.vbTab + "Scan Output: " + Constants.vbTab + ScanOutput.ToString() + Constants.vbCrLf + Constants.vbTab + "Path: " + Constants.vbTab + Path.ToString() + Constants.vbCrLf + Constants.vbTab + "Center: " + Constants.vbTab + Center.ToString();
        }

    }
}