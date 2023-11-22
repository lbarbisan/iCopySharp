Imports WIA
Imports System.Drawing.Printing

Public Enum ScanOutput
    Printer
    File
    PDF
End Enum

Public Class ScanSettings
    Public Const DEFAULT_RESOLUTION = 300
    Public Const DEFAULT_SCALING = 100
    Public Const DEFAULT_QUALITY = 100

    Private _Brightness As Integer
    Private _center As Boolean
    Private _Contrast As Integer
    Private _BitDepth As Integer
    Private _Resolution As Short
    Private _Quality As Integer
    Private _Intent As WiaImageIntent
    Private _Preview As Boolean
    Private _Scaling As Integer
    Private _Copies As Integer
    Private _Path As String
    Private _UseADF As Boolean
    Private _duplex As Boolean
    Private _scanOutput As ScanOutput
    Private _multipage As Boolean
    Private _paperSize As PaperSize
    Private _rotateDuplex As Boolean

    ''' Creates default properties
    Public Sub New()

        _UseADF = False
        _duplex = False
        _rotateDuplex = False
        _Brightness = 0
        _Contrast = 0
        _Quality = DEFAULT_QUALITY
        _Preview = False
        _Copies = 1
        _Intent = WIA.WiaImageIntent.ColorIntent
        _Resolution = DEFAULT_RESOLUTION
        _Scaling = DEFAULT_SCALING
        _BitDepth = 0
        _Path = ""
        _scanOutput = iCopy.ScanOutput.Printer
        _multipage = False
        _center = True
        _paperSize = New PaperSize("A4", 827, 1169)
    End Sub

    Public Property Brightness() As Integer
        Get
            Return _Brightness
        End Get
        Set(ByVal value As Integer)
            _Brightness = value
        End Set
    End Property

    Public Property Center() As Boolean
        Get
            Return _center
        End Get
        Set(value As Boolean)
            _center = value
        End Set
    End Property

    Public Property Contrast() As Integer
        Get
            Return _Contrast
        End Get
        Set(ByVal value As Integer)
            _Contrast = value
        End Set
    End Property

    Public Property PaperSize() As PaperSize
        Get
            Return _paperSize
        End Get
        Set(ByVal value As PaperSize)
            _paperSize = value
        End Set
    End Property

    Public Property Resolution() As Integer
        Get
            Return _Resolution
        End Get
        Set(ByVal value As Integer)
            _Resolution = value
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Property Intent() As WiaImageIntent
        Get
            Return _Intent
        End Get
        Set(ByVal value As WiaImageIntent)
            _Intent = value
        End Set
    End Property

    Public Property Quality() As Integer
        Get
            Return _Quality
        End Get
        Set(ByVal value As Integer)
            If value <= 100 Or value > 0 Then
                _Quality = value
            Else
                Throw New ArgumentException("Quality value must be between 0 and 100")
            End If
        End Set
    End Property

    Public Property Preview() As Boolean
        Get
            Return _Preview
        End Get
        Set(ByVal value As Boolean)
            _Preview = value
        End Set
    End Property

    Public Property Scaling() As Integer
        Get
            Return _Scaling
        End Get
        Set(ByVal value As Integer)
            _Scaling = value
        End Set
    End Property

    Public Property Copies() As Integer
        Get
            Return _Copies
        End Get
        Set(ByVal value As Integer)
            _Copies = value
        End Set
    End Property

    Public Property Path() As String
        Get
            Return _Path
        End Get
        Set(ByVal value As String)
            _Path = value
        End Set
    End Property

    Public Property Multipage() As Boolean
        Get
            Return _multipage
        End Get
        Set(ByVal value As Boolean)
            _multipage = value
        End Set
    End Property

    Public Property ScanOutput() As ScanOutput
        Get
            Return _scanOutput
        End Get
        Set(ByVal value As ScanOutput)
            _scanOutput = value
        End Set
    End Property

    Public Property UseADF() As Boolean
        Get
            Return _UseADF
        End Get
        Set(ByVal value As Boolean)
            _UseADF = value
        End Set
    End Property

    Public Property Duplex() As Boolean
        Get
            Return _duplex
        End Get
        Set(ByVal value As Boolean)
            _duplex = value
        End Set
    End Property

    Public Property RotateDuplex() As Boolean
        Get
            Return _rotateDuplex
        End Get
        Set(ByVal value As Boolean)
            _rotateDuplex = value
        End Set
    End Property

    Public Property BitDepth() As Integer
        Get
            Return _BitDepth
        End Get
        Set(ByVal value As Integer)
            _BitDepth = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return vbTab + "Bit Depth: " + vbTab + BitDepth.ToString() + vbCrLf +
            vbTab + "Brightness: " + vbTab + Brightness.ToString() + vbCrLf +
            vbTab + "Contrast: " + vbTab + Contrast.ToString() + vbCrLf +
            vbTab + "Resolution: " + vbTab + Resolution.ToString() + vbCrLf +
            vbTab + "Intent: " + vbTab + Intent.ToString() + vbCrLf +
            vbTab + "Quality: " + vbTab + Quality.ToString() + vbCrLf +
            vbTab + "Scaling: " + vbTab + Scaling.ToString() + vbCrLf +
            vbTab + "Copies: " + vbTab + Copies.ToString() + vbCrLf +
            vbTab + "Preview: " + vbTab + Preview.ToString() + vbCrLf +
            vbTab + "UseADF: " + vbTab + UseADF.ToString() + vbCrLf +
            vbTab + "Duplex: " + vbTab + Duplex.ToString() + vbCrLf +
            vbTab + "Rotate Duplex: " + vbTab + RotateDuplex.ToString() + vbCrLf +
            vbTab + "Multipage: " + vbTab + Multipage.ToString() + vbCrLf +
            vbTab + "Scan Output: " + vbTab + ScanOutput.ToString() + vbCrLf +
            vbTab + "Path: " + vbTab + Path.ToString() + vbCrLf +
            vbTab + "Center: " + vbTab + Center.ToString()
    End Function

End Class


