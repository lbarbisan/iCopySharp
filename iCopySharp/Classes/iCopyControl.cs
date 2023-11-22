using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
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

[assembly: CLSCompliant(true)]

namespace iCopy
{
    class appControl
    {

        private static string _deviceID;
        private static System.Resources.ResourceManager LocRM;
        private static System.Threading.Thread GetCulturesThread;

        private static List<CultureInfo> _availableCultures;
        private static Scanner __scanner;

        private static Scanner _scanner
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return __scanner;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                __scanner = value;
            }
        }
        private static Printer _printer;
        private static bool CommandLine;

        public static mainFrm MainForm = null;

        static appControl()
        {
            _printer = new Printer();
        }

        [STAThread()]
        public static void Main(string[] sArgs)
        {
            Application.EnableVisualStyles();
            Device _device;
            Item _wscanner;
            DeviceManager manager;

            Properties.Settings.Default.Silent = false;
            if (Properties.Settings.Default.LastScanSettings is null)
            {
                Properties.Settings.Default.LastScanSettings = new ScanSettings();
                Properties.Settings.Default.Save();
            }

            // Initialize default settings (should appen only at first start or setting deletion)
            if (Properties.Settings.Default.DefaultScanSettings is null)
            {
                Properties.Settings.Default.DefaultScanSettings = new ScanSettings();
                Properties.Settings.Default.Save();
            }

            // --------------- LOGGING ----------------------------------
            // Deletes log file if it is more than 50 KB long
            string logPath = Path.Combine(Utilities.GetWritablePath(), "iCopy.log");

            try
            {
                var MyFile = new FileInfo(logPath);
                long FileSize = MyFile.Length;
                if (FileSize > 50 * 1024) // Clear the log if it is more than 50 KB
                {

                    File.Delete(logPath);

                }
            }
            catch (Exception ex)
            {

            }

            // Sets trace output to console and log file
            Trace.AutoFlush = true;
            var logListener = new TextWriterTraceListener(logPath, "log");

            Trace.Listeners.Add(logListener);

            Trace.WriteLine("");
            Trace.WriteLine(DateTime.Now);
            Trace.WriteLine(string.Format("iCopy Version: {0}", Application.ProductVersion));
            Trace.WriteLine(string.Format("Windows Version: {0}", Environment.OSVersion.VersionString));
            Trace.WriteLine(string.Format(".NET Version: {0}", Environment.Version.ToString()));
            Trace.WriteLine(string.Format("-----------------"));
            // ----------------- LOGGING END -----------------------------------------


            GetCulturesThread = new System.Threading.Thread(GetAvailableLanguages);

            LocRM = new System.Resources.ResourceManager("iCopy.WinFormStrings", typeof(mainFrm).Assembly);

            Trace.Indent();
            try
            {
                manager = new DeviceManager(); // This is the first call to WIA library. If it isn't registered, an error is thrown
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception caught when initializing WIA.DeviceManager");
                Trace.WriteLine(ex.ToString());
                RegisterWiaautdll(true);
                return;
            }

            Trace.WriteLine(string.Format("WIA Device count: {0}", manager.DeviceInfos.Count));

            try
            {
                if (sArgs.Length == 0) // If there are no arguments, run app normally
                {

                    CommandLine = false;
                    // Avoids that two processes run simultaneously
                    if (Process.GetProcessesByName("icopy").Length > 1)
                    {
                        Utilities.MsgBoxWrap(LocRM.GetString("Msg_AlreadyRunning"), MsgBoxStyle.Information, "iCopy");
                        throw new ExitException();
                    }

                    // Searches for languages installed
                    try            // Should avoid ThreadStateException
                    {
                        if (GetCulturesThread.ThreadState == System.Threading.ThreadState.Unstarted)
                        {
                            GetCulturesThread.Start();
                        }
                    }
                    catch (System.Threading.ThreadStateException ex)
                    {
                        Utilities.MsgBoxWrap(ex.ToString());
                    }

                    // Initializes new scanning interface
                    CreateScanner(Properties.Settings.Default.DeviceID);

                    try
                    {
                        Properties.Settings.Default.DeviceID = _scanner.DeviceId;
                    }
                    catch (NullReferenceException ex)
                    {
                        Application.Exit();
                    }

                    MainForm = new mainFrm();
                    Application.Run(MainForm);
                    if (!Properties.Settings.Default.RememberSettings)
                    {
                        Properties.Settings.Default.LastScanSettings = new ScanSettings(); // Scan settings are restored to the default
                    }

                    Properties.Settings.Default.Save();
                    Application.Exit();
                }

                else    // Handle Command line arguments
                {
                    CommandLine = true; // To inform the program that it is running in command line mode
                    bool openAfterAcquisiton = false;
                    // Prints the argument list for debugging purpose
                    string argstring = "";
                    foreach (string arg in sArgs)
                        argstring += arg + " ";
                    Trace.WriteLine(string.Format("Command Line parameters: {0}", argstring));

                    for (int i = 0, loopTo = sArgs.GetUpperBound(0); i <= loopTo; i++)
                    {
                        if (sArgs[i] == "/silent")
                            Properties.Settings.Default.Silent = Conversions.ToBoolean("True");
                    }

                    // Utility commands (no WIA library references)
                    for (int i = 0, loopTo1 = sArgs.GetUpperBound(0); i <= loopTo1; i++)
                    {
                        switch (sArgs[i].ToLower() ?? "")
                        {
                            case "/?":
                            case "/help":
                                {
                                    Utilities.MsgBoxWrap(LocRM.GetString("Console_Help"), MsgBoxStyle.Information);
                                    return;
                                }
                            case "/wiareg":
                            case "/wr":
                                {
                                    try
                                    {
                                        manager = new DeviceManager();
                                    }
                                    catch (Exception ex)
                                    {
                                        RegisterWiaautdll(true);
                                    }
                                    return;
                                }

                        }
                    }

                    var settings = new ScanSettings();
                    // Recover last used settings
                    if (Properties.Settings.Default.LastScanSettings != null)
                    {
                        settings = Properties.Settings.Default.LastScanSettings;
                        settings.Path = "";
                    }

                    for (int i = 0, loopTo2 = sArgs.GetUpperBound(0); i <= loopTo2; i++)
                    {
                        switch (sArgs[i].ToLower() ?? "")
                        {
                            case "/register":
                            case "/reg":
                                {
                                    try
                                    {
                                        manager.RegisterPersistentEvent(Application.ExecutablePath + " /StiDevice:%1 /StiEvent:%2 /copy", "iCopy", "Directly print using iCopy", Application.ExecutablePath + ",0", EventID.wiaEventScanImage);
                                        manager.RegisterPersistentEvent(Application.ExecutablePath + " /StiDevice:%1 /StiEvent:%2 /copy", "iCopy", "Directly print using iCopy", Application.ExecutablePath + ",0", EventID.wiaEventScanImage2);
                                        manager.RegisterPersistentEvent(Application.ExecutablePath + " /StiDevice:%1 /StiEvent:%2 /file", "iCopy", "Save to file using iCopy", Application.ExecutablePath + ",0", EventID.wiaEventScanImage4);
                                        Utilities.MsgBoxWrap("iCopy successfully registered to the scanner button. You may need to restart the computer in order for the change to take effect.", Constants.vbInformation, "iCopy");
                                    }
                                    catch (UnauthorizedAccessException ex)
                                    {
                                        Utilities.MsgBoxWrap("iCopy must be executed with administrative privileges in order to complete the operation.", Constants.vbInformation, "iCopy");
                                    }
                                    return;
                                }
                            case "/unregister":
                            case "/unreg":
                                {
                                    try
                                    {
                                        manager.UnregisterPersistentEvent(Application.ExecutablePath + " /StiDevice:%1 /StiEvent:%2 /copy", "iCopy", "Directly print using iCopy", Application.ExecutablePath + ",0", EventID.wiaEventScanImage);
                                        manager.UnregisterPersistentEvent(Application.ExecutablePath + " /StiDevice:%1 /StiEvent:%2 /copy", "iCopy", "Directly print using iCopy", Application.ExecutablePath + ",0", EventID.wiaEventScanImage2);
                                        manager.UnregisterPersistentEvent(Application.ExecutablePath + " /StiDevice:%1 /StiEvent:%2 /file", "iCopy", "Save to file using iCopy", Application.ExecutablePath + ",0", EventID.wiaEventScanImage4);
                                        Utilities.MsgBoxWrap("iCopy successfully unregistered from the scanner button. You may need to restart the computer in order for the change to take effect.", Constants.vbInformation, "iCopy");
                                    }
                                    catch (UnauthorizedAccessException ex)
                                    {
                                        Utilities.MsgBoxWrap("iCopy must be executed with administrative privileges in order to complete the operation.", Constants.vbInformation, "iCopy");
                                    }
                                    catch (ArgumentException ex) // Thrown if the event is not found. Either wrong sintax or has already been removed
                                    {
                                        Utilities.MsgBoxWrap("Couldn't find the correct entry in the registry. Maybe it has already been removed?");
                                    }
                                    return;
                                }
                        }
                    }

                    // Command line arguments parsing
                    // STEP 1 Parameters with an argument
                    for (int i = 0, loopTo3 = sArgs.GetUpperBound(0); i <= loopTo3; i++)
                    {
                        try
                        {
                            switch (sArgs[i] ?? "")
                            {
                                case "/brightness":
                                case "/b":
                                    {
                                        settings.Brightness = Conversions.ToInteger(sArgs[i + 1]);
                                        continue;
                                    }
                                case "/contrast":
                                case "/cnt":
                                    {
                                        settings.Contrast = Conversions.ToInteger(sArgs[i + 1]);
                                        continue;
                                    }
                                case "/resolution":
                                case "/r":
                                    {
                                        settings.Resolution = Conversions.ToInteger(sArgs[i + 1]);
                                        continue;
                                    }
                                case "/copies":
                                case "/nc":
                                    {
                                        settings.Copies = Conversions.ToInteger(sArgs[i + 1]);
                                        continue;
                                    }
                                case "/quality":
                                    {
                                        try
                                        {
                                            settings.Quality = Conversions.ToInteger(sArgs[i + 1]);
                                        }
                                        catch (ArgumentException a)
                                        {
                                            Utilities.MsgBoxWrap("Command line parsing failed. See README for correct syntax");
                                            return;
                                        }
                                        continue;
                                    }
                                case "/scaling":
                                case "/s":
                                    {
                                        settings.Scaling = Conversions.ToInteger(sArgs[i + 1]);
                                        continue;
                                    }
                                case "/printer":
                                    {
                                        try
                                        {
                                            _printer.Name = sArgs[i + 1];
                                        }
                                        catch (ArgumentException ex)
                                        {
                                            Utilities.MsgBoxWrap("The provided printer name is not valid. Using default printer.");
                                        }
                                        continue;
                                    }
                                case "/path":
                                    {
                                        settings.Path = sArgs[i + 1];
                                        continue;
                                    }
                            }
                        }
                        catch (InvalidCastException ex)
                        {
                            Utilities.MsgBoxWrap("Command line parsing failed. See README for correct syntax");
                            return;
                        }

                        // STEP 2 Parameters without an argument
                        switch (sArgs[i] ?? "")
                        {
                            case "/log":
                                {
                                    Trace.Listeners.Add(new ConsoleTraceListener());
                                    continue;
                                }
                            case "/color":
                            case "/colour":
                            case "/col":
                                {
                                    settings.Intent = WiaImageIntent.ColorIntent;
                                    continue;
                                }
                            case "/grayscale":
                            case "/gray":
                                {
                                    settings.Intent = WiaImageIntent.GrayscaleIntent;
                                    continue;
                                }
                            case "/text":
                            case "/bw":
                                {
                                    settings.Intent = WiaImageIntent.TextIntent;
                                    continue;
                                }
                            case "/preview":
                            case "/p":
                                {
                                    settings.Preview = true;
                                    continue;
                                }
                            case "/adf":
                                {
                                    settings.UseADF = true;
                                    continue;
                                }
                            case "/duplex":
                            case "/d":
                                {
                                    settings.Duplex = true;
                                    continue;
                                }
                            case "/multiplepages":
                                {
                                    settings.Multipage = true;
                                    break;
                                }
                            case "/open":
                                {
                                    openAfterAcquisiton = true;
                                    continue;
                                }
                        }

                        if (sArgs[i].StartsWith("/StiDevice:"))
                        {
                            _deviceID = sArgs[i].Substring(sArgs[i].IndexOf(":") + 1);
                        }
                        if (sArgs[i].StartsWith("/StiEvent:"))
                        {
                            // TODO: Implement
                        }
                    }

                    if (string.IsNullOrEmpty(_deviceID))
                    {
                        _deviceID = changescanner(Properties.Settings.Default.DeviceID);
                    }
                    else
                    {
                        _deviceID = changescanner(_deviceID);
                    }

                    object argIndex = _deviceID;
                    _device = manager.DeviceInfos[ref argIndex].Connect();
                    _deviceID = Conversions.ToString(argIndex);
                    Trace.WriteLine(string.Format("DeviceID = {0}", _deviceID));
                    _wscanner = _device.Items[1];

                    // STEP 3 Scanner action parameters
                    for (int i = 0, loopTo4 = sArgs.GetUpperBound(0); i <= loopTo4; i++)
                    {
                        switch (sArgs[i].ToLower() ?? "")
                        {
                            case "/copy":
                            case "/c":
                                {
                                    Copy(settings);
                                    // Application.Exit()
                                    return;
                                }
                            case "/pdf":
                                {
                                    settings.ScanOutput = ScanOutput.PDF;
                                    if (!Properties.Settings.Default.PDFAskWhereToSave)
                                    {
                                        settings.Path = Properties.Settings.Default.PDFSavePath;

                                    }
                                    try
                                    {
                                        Copy(settings, openAfterAcquisiton);
                                        return;
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        Utilities.MsgBoxWrap(ex.Message, Constants.vbExclamation, "iCopy");
                                    }

                                    break;
                                }
                            case "/file":
                            case "/tofile":
                            case "/Scantofile":
                            case "/f":
                                {
                                    settings.ScanOutput = ScanOutput.File;
                                    if (!Properties.Settings.Default.FileAskWhereToSave)
                                    {
                                        settings.Path = Properties.Settings.Default.FileSavePath;
                                    }
                                    try
                                    {
                                        Copy(settings, openAfterAcquisiton);
                                        return;
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        Utilities.MsgBoxWrap(ex.Message, Constants.vbExclamation, "iCopy");
                                    }

                                    break;
                                }
                        }
                    }

                    // If no action parameter is set, exit
                    // Application.Exit()
                    return;
                }
            }
            catch (ExitException ex)
            {
            }
            // Exit
            catch (FileNotFoundException ex)
            {
                Trace.WriteLine("Need wiaaut registration");
                RegisterWiaautdll(false);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                if (ex.ErrorCode == (int)WIA_ERRORS.WIA_ERROR_NOT_REGISTERED) // WIA COM error
                {
                    Trace.WriteLine("Need wiaaut registration");
                    RegisterWiaautdll(false);
                }
                else if (ex.ErrorCode == (int)WIA_ERRORS.WIA_ERROR_UNKNOWN_ERROR | ex.ErrorCode == (int)WIA_ERRORS.WIA_ERROR_NO_SCANNER_CONNECTED)
                {
                    Utilities.MsgBoxWrap("There is a problem with your scanner connection. Please try to reconnect your scanner and restart iCopy. If this doesn't solve the problem, please report it on iCopy website", MsgBoxStyle.Critical, "iCopy");
                }
            }
            catch (Exception ex)
            {
                /* TODO ERROR: Skipped IfDirectiveTrivia
                #If DEBUG = False Then
                *//* TODO ERROR: Skipped DisabledTextTrivia
                            HandleException(ex) 'Overrides .NET message box to include error reporting
                *//* TODO ERROR: Skipped ElseDirectiveTrivia
                #Else
                */
                throw;
                /* TODO ERROR: Skipped EndIfDirectiveTrivia
                #End If
                */
            }
            Trace.WriteLine(Constants.vbCrLf);
        }

        private static void HandleException(Exception ex)
        {

            Trace.TraceError("Exception caught.");
            Trace.TraceError(ex.ToString());

            // If the exception is unhandled, prepare error report and send info
            var sendReport = Utilities.MsgBoxWrap(string.Format(GetLocalizedString("Msg_SendErrorReport2"), ex.GetType().ToString() + " in " + ex.TargetSite.ToString()), (MsgBoxStyle)((int)MsgBoxStyle.Critical + (int)MsgBoxStyle.OkCancel), "iCopy");
            if (sendReport == MsgBoxResult.Cancel)
            {
                return;
            }

            Trace.Close();

            var rd = new StreamReader(Path.Combine(Utilities.GetWritablePath(), "iCopy.log"));
            Clipboard.SetText(rd.ReadToEnd());

            Process.Start("https://sourceforge.net/p/icopy/bugs/new/");
            rd.Dispose();
        }

        public static string GetLocalizedString(string Label)
        {
            string LocalizedString;
            LocalizedString = LocRM.GetString(Label);
            return LocalizedString;
        }

        private static void RegisterWiaautdll(bool suppressMessage)
        {
            Trace.WriteLine("wiaaut.dll registration");
            Trace.Indent();
            // Check if iCopy is run as administrator
            bool isElevated;
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            isElevated = principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);

            if (isElevated)
            {
                // Copy wiaaut.dll to system32
                bool proceed = true;
                if (!suppressMessage)
                {
                    if (Utilities.MsgBoxWrap(LocRM.GetString("Msg_WIAUnregistered"), (MsgBoxStyle)((int)MsgBoxStyle.OkCancel + (int)MsgBoxStyle.Information), "iCopy") == MsgBoxResult.Cancel)
                        proceed = false;
                }
                if (proceed)
                {
                    try
                    {
                        File.Copy("wiaaut.dll", @"C:\WINDOWS\system32\wiaaut.dll", false);
                        Trace.WriteLine("wiaaut.dll copied to system32");
                    }
                    catch (UnauthorizedAccessException authEx) // iCopy has not administrator privileges
                    {
                        Utilities.MsgBoxWrap(LocRM.GetString("Msg_UnauthorizedAccess"), MsgBoxStyle.Exclamation, "iCopy");
                        return;
                    }
                    catch (FileNotFoundException fileEx) // File is missing from iCopy directory
                    {
                        Utilities.MsgBoxWrap(LocRM.GetString("Msg_WiaautMissing"), MsgBoxStyle.Exclamation, "iCopy");
                        return;
                    }
                    catch (IOException ex)
                    {
                        // The file is already in system32
                        Trace.WriteLine("wiaaut.dll already in system32");
                    }

                    // Start regsvr32 to register the dll
                    var psi = new ProcessStartInfo();
                    psi.FileName = "regsvr32";
                    psi.Arguments = @"C:\WINDOWS\system32\wiaaut.dll /s";
                    Process.Start(psi);
                    Trace.WriteLine("wiaaut.dll registered");

                    Utilities.MsgBoxWrap(LocRM.GetString("Msg_WIARegistrationSuccessful"), (MsgBoxStyle)((int)MsgBoxStyle.Information + (int)MsgBoxStyle.OkOnly), "iCopy");
                }
            }
            else if (Environment.OSVersion.Version.Major >= 6)
            {
                var WIADialog = new WIARegisterDialog();
                var msg = WIADialog.Show(LocRM.GetString("Msg_WIAUnregistered"), LocRM.GetString("Msg_WIAUnregisteredInstruction"), "iCopy", LocRM.GetString("Msg_WIAUnregisteredCancel"));
                if (msg == Microsoft.WindowsAPICodePack.Dialogs.TaskDialogResult.Ok)
                {
                    var psi = new ProcessStartInfo();
                    psi.FileName = Application.ExecutablePath;
                    psi.Arguments = "/wiareg";
                    psi.Verb = "runas";
                    Process.Start(psi);
                    return;
                }
                else if (msg == Microsoft.WindowsAPICodePack.Dialogs.TaskDialogResult.Cancel)
                {
                    return;
                }
            }
            else
            {
                var msg = Utilities.MsgBoxWrap(LocRM.GetString("Msg_WIAUnregistered"), (MsgBoxStyle)((int)MsgBoxStyle.Exclamation + (int)MsgBoxStyle.YesNo), "iCopy");
                if (msg == MsgBoxResult.Yes)
                {
                    var psi = new ProcessStartInfo();
                    psi.FileName = Application.ExecutablePath;
                    psi.Arguments = "/wiareg";
                    psi.Verb = "runas";
                    Process.Start(psi);
                    return;
                }
                else if (msg == MsgBoxResult.No)
                {
                    return;
                }
            }
        }

        public static void GetAvailableLanguages()
        {
            _availableCultures = new List<CultureInfo>();

            // Need to find a faster way
            System.Resources.ResourceSet resSet;
            foreach (CultureInfo cult in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                if (!cult.IsNeutralCulture) // Excludes neutral languages
                {
                    resSet = LocRM.GetResourceSet(cult, true, false); // Verify if resources for that culture are present
                    if (resSet != null)
                    {
                        if (!(cult.LCID == 127)) // Excludes standard language
                        {
                            _availableCultures.Add(cult);
                        }
                    }
                }
            }
        }

        /// <summary>
    /// Changes the scanner. If no DeviceID is passed, a dialog lets the user choose the scanner
    /// </summary>
    /// <param name="deviceID">If provided, it connects to the corresponding scanner</param>
    /// <returns>The DeviceID of the new scanner</returns>
    /// <remarks></remarks>
        public static string changescanner(string deviceID = "")
        {
            try
            {
                if (string.IsNullOrEmpty(deviceID))
                {
                    // Shows WIA scanner selection dialog
                    var dialog = new WIA.CommonDialog();
                    _scanner = new Scanner(dialog.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, true, true).DeviceID);
                    return _scanner.DeviceId;
                }
                else
                {
                    _scanner = new Scanner(deviceID);
                    return _scanner.DeviceId;
                }
            }

            // **************Exception handling*************
            catch (ArgumentException ex)
            {
                var msg = Utilities.MsgBoxWrap(LocRM.GetString("Msg_NoScannerConnected"), (MsgBoxStyle)((int)MsgBoxStyle.RetryCancel + (int)MsgBoxStyle.Information), "iCopy");
                if (msg == MsgBoxResult.Retry)
                {
                    return changescanner(deviceID);
                }
                else
                {
                    throw new ExitException();
                }
            }

            catch (System.Runtime.InteropServices.COMException ex)
            {

                switch (ex.ErrorCode)
                {
                    case (int)WIA_ERRORS.WIA_ERROR_NO_SCANNER_SELECTED: // No scanner is selected
                        {
                            return null;
                        }

                    case (int)WIA_ERRORS.WIA_ERROR_NO_SCANNER_CONNECTED: // No scanner is connected
                        {
                            var msg = Utilities.MsgBoxWrap(LocRM.GetString("Msg_NoScannerConnected"), (MsgBoxStyle)((int)MsgBoxStyle.RetryCancel + (int)MsgBoxStyle.Information), "iCopy");
                            if (msg == MsgBoxResult.Retry)
                            {
                                return changescanner(deviceID);
                            }
                            else
                            {
                                throw new ExitException();
                            }
                        }

                    case (int)WIA_ERRORS.WIA_ERROR_CONNECTION_ERROR:  // Can't establish connection with the scanner
                        {
                            var msg = Utilities.MsgBoxWrap(LocRM.GetString("Msg_ConnectionError"), (MsgBoxStyle)((int)MsgBoxStyle.RetryCancel + (int)MsgBoxStyle.Exclamation), "iCopy");
                            if (msg == MsgBoxResult.Retry)
                            {
                                return changescanner(deviceID);
                            }
                            else
                            {
                                throw new ExitException();
                            }
                        }

                    case (int)WIA_ERRORS.WIA_ERROR_OFFLINE:
                        {
                            var msg = Utilities.MsgBoxWrap(LocRM.GetString("Msg_ScannerOffline"), (MsgBoxStyle)((int)MsgBoxStyle.RetryCancel + (int)MsgBoxStyle.Information), "iCopy");
                            if (msg == MsgBoxResult.Retry)
                            {
                                return changescanner(deviceID);
                            }
                            else
                            {
                                throw new ExitException();
                            }
                        }

                    case (int)WIA_ERRORS.WIA_ERROR_EXCEPTION_IN_DRIVER:
                        {
                            Utilities.MsgBoxWrap(My.Resources.WinFormStrings.Msg_DriverException, MsgBoxStyle.Critical, "iCopy");
                            throw new ExitException();
                        }

                    case (int)WIA_ERRORS.WIA_ERROR_BUSY: // Scanner in use
                        {
                            var msg = Utilities.MsgBoxWrap(LocRM.GetString("Msg_ScannerInUse"), (MsgBoxStyle)((int)MsgBoxStyle.Exclamation + (int)MsgBoxStyle.RetryCancel));
                            if (msg == MsgBoxResult.Retry)
                            {
                                return changescanner(deviceID);
                            }
                            else
                            {
                                throw new ExitException();
                            }
                        }
                    case (int)WIA_ERRORS.WIA_ERROR_UNKNOWN_ERROR: // Could happen if the deviceID is invalid (e.g. changed scanner)
                        {
                            return changescanner();
                        }

                    default:
                        {
                            throw;
                        }
                }
            }

            return null;
        }

        public static void CreateScanner(string deviceID = null)
        {
        retry:
            ;

            if (string.IsNullOrEmpty(deviceID) | string.IsNullOrEmpty(deviceID))
            {
                string newscannerID = changescanner();
                if (newscannerID is null)
                {
                    var msg = Utilities.MsgBoxWrap(LocRM.GetString("Msg_ChooseAScanner"), (MsgBoxStyle)((int)MsgBoxStyle.YesNo + (int)MsgBoxStyle.Information), "iCopy");
                    if (msg == MsgBoxResult.Yes)
                    {
                        goto retry;
                    }
                    else
                    {
                        throw new ExitException();
                    }
                }
            }

            else if (changescanner(deviceID) is null)
            {
                throw new ExitException();
            }

            Scanner.WritePropertiesLog();
        }

        public static List<int> GetAvailableResolutions()
        {
            return _scanner.AvailableResolutions;
        }

        public static bool CanUseADF()
        {
            return _scanner.CanUseADF;
        }

        public static bool CanDoDuplex()
        {
            return _scanner.CanDoDuplex;
        }

        private static void OpenFile(string path)
        {
            try
            {
                Process.Start(path);
            }
            catch (Exception ex)
            {
                Utilities.MsgBoxWrap(string.Format(LocRM.GetString("Msg_CantOpenfile"), ex.Message), MsgBoxStyle.Exclamation);
            }
        }

        public static void Copy(ScanSettings options, bool openAfterAcquisition = false)
        {
            string pathWoExt = "";
            string ext = "";
            var format = ImageFormat.Jpeg;

            if (options.ScanOutput == ScanOutput.File | options.ScanOutput == ScanOutput.PDF)
            {
                if (string.IsNullOrEmpty(options.Path))
                {
                    var dialog = new SaveFileDialog();

                    if (options.ScanOutput == ScanOutput.File)
                    {
                        dialog.AddExtension = true;
                        dialog.DefaultExt = "jpg";
                        dialog.Filter = "JPEG image|*.jpg|Windows Bitmap|*.bmp|GIF|*.gif|Portable Network Graphics (PNG)|*.png";
                    }
                    else
                    {
                        dialog.AddExtension = true;
                        dialog.DefaultExt = "pdf";
                        dialog.Filter = "Adobe PDF file|*.pdf";
                    }

                    if (!(dialog.ShowDialog() == DialogResult.Cancel))
                    {
                        options.Path = dialog.FileName;
                    }
                    else
                    {
                        return;
                    }
                }

                // Check if the provided path is a directory
                else if (string.IsNullOrEmpty(Path.GetFileName(options.Path)))
                {
                    // In this case, create the file name according to the current date and time
                    string filename = "iCopy " + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                    if (options.ScanOutput == ScanOutput.PDF)
                    {
                        filename += ".pdf";
                    }
                    else
                    {
                        filename += ".jpg";
                    }

                    options.Path = Path.Combine(options.Path, filename);
                }

                // Check if the path points to an existing file or an unwritable location. In that case throws an error message
                if (File.Exists(options.Path))
                {
                    if (Properties.Settings.Default.Silent)
                    {
                        try
                        {
                            File.Delete(options.Path);
                        }
                        catch (Exception ex)
                        {
                            Utilities.MsgBoxWrap(string.Format(LocRM.GetString("Msg_FileError"), options.Path), (MsgBoxStyle)((int)MsgBoxStyle.OkOnly + (int)MsgBoxStyle.Information), "iCopy");
                            return;
                        }
                    }
                    else
                    {
                        var msgboxres = Interaction.MsgBox(string.Format(LocRM.GetString("Msg_FileAlreadyExists"), options.Path), (MsgBoxStyle)((int)MsgBoxStyle.OkCancel + (int)MsgBoxStyle.Question), "iCopy");
                        if (msgboxres == MsgBoxResult.Ok)
                        {
                            try
                            {
                                File.Delete(options.Path);
                            }
                            catch (Exception ex)
                            {
                                Utilities.MsgBoxWrap(string.Format(LocRM.GetString("Msg_FileError"), options.Path), (MsgBoxStyle)((int)MsgBoxStyle.OkOnly + (int)MsgBoxStyle.Information), "iCopy");
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }


                // Check if the provided path is valid (AUTHORIZATION, SYNTAX, ecc)
                try
                {
                    var a = File.Create(options.Path);
                    a.Close();
                }
                catch (Exception ex)
                {
                    Utilities.MsgBoxWrap(string.Format(LocRM.GetString("Msg_FileError"), options.Path), (MsgBoxStyle)((int)MsgBoxStyle.OkOnly + (int)MsgBoxStyle.Information), "iCopy");
                    try
                    {
                        File.Delete(options.Path);
                    }
                    catch (Exception e)
                    {

                    }
                    return;
                }

                try // Delete the temporary file created.
                {
                    File.Delete(options.Path);
                }
                catch (Exception e)
                {

                }

                if (options.ScanOutput == ScanOutput.File)
                {
                    // Determines the extension of the file in order to use the correct format
                    ext = Strings.Right(options.Path, 3);
                    pathWoExt = Strings.Left(options.Path, options.Path.Length - 4);
                    switch (ext ?? "")
                    {
                        case "jpg":
                            {
                                format = ImageFormat.Jpeg;
                                break;
                            }
                        case "bmp":
                            {
                                format = ImageFormat.Bmp;
                                break;
                            }
                        case "gif":
                            {
                                format = ImageFormat.Gif;
                                break;
                            }
                        case "png":
                            {
                                format = ImageFormat.Png;
                                break;
                            }

                        default:
                            {
                                throw new ArgumentException("File extension isn't a valid extension");
                            }
                    }

                }
            }

            var morePages = DialogResult.No;
            // List of acquired images
            var images = new List<string>();

            var dlg = new dlgScanMorePages();
            if (CommandLine)
            {
                dlg.Location = new Point((int)Math.Round(Screen.PrimaryScreen.WorkingArea.Width / 2d - dlg.Width), (int)Math.Round(Screen.PrimaryScreen.WorkingArea.Height / 2d - dlg.Height));
            }
            else
            {
                dlg.Location = new Point((int)Math.Round(MainForm.Left + (MainForm.Width - dlg.Width) / 2d), (int)Math.Round(MainForm.Top + (MainForm.Height - dlg.Height) / 2d));
            }

            changescanner(_scanner.DeviceId);

            // Calls scan routine
            do
            {
                try
                {
                    // Add the image to the printer buffer
                    images.AddRange(_scanner.Scan(options));


                    if (options.Multipage)
                    {
                        if (MainForm is null) // If in command line mode, launch morepages window
                        {
                            morePages = dlg.ShowDialog(images.Count);
                        }
                        else                        // If in GUI mode, set main form as owner to avoid focusing problems
                        {
                            morePages = dlg.ShowDialog(images.Count, MainForm);
                        }
                    }
                }

                catch (System.Runtime.InteropServices.COMException ex)
                {
                    if (ex.ErrorCode == -2145320860)       // If acquisition is cancelled
                    {
                        return;
                    }
                    else if (ex.ErrorCode == (int)WIA_ERRORS.WIA_ERROR_USER_INTERVENTION)
                    {
                        Utilities.MsgBoxWrap(LocRM.GetString("Msg_UserIntervention"), MsgBoxStyle.Exclamation, "iCopy");
                        return;
                    }
                    else if (ex.ErrorCode == (int)WIA_ERRORS.WIA_ERROR_WARMING_UP)
                    {
                        Utilities.MsgBoxWrap(LocRM.GetString("Msg_ScannerWarmingUp"), MsgBoxStyle.Exclamation, "iCopy");
                        return;
                    }
                    else if (ex.ErrorCode == (int)WIA_ERRORS.WIA_ERROR_EXCEPTION_IN_DRIVER)
                    {
                        Utilities.MsgBoxWrap(LocRM.GetString("Msg_ExceptionInDriver"), MsgBoxStyle.Exclamation, "iCopy");
                        return;
                    }
                    else if (ex.ErrorCode == Convert.ToInt32("0x80004005", 16))
                    {
                        Utilities.MsgBoxWrap("An error occured while processing the acquired image. Please try again with a lower resolution." + Constants.vbCrLf + "If the problem persists please report it (http://icopy.sourceforge.net/reportabug.html).", MsgBoxStyle.Critical, "iCopy");
                        return;
                    }
                    else
                    {
                        throw;
                    }
                }

                if (morePages == DialogResult.Cancel)
                {
                    // If the process is canceled by closing the dialog
                    var res = Utilities.MsgBoxWrap(LocRM.GetString("Msg_CancelAcquisition"), (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo), "iCopy");
                    if (res == MsgBoxResult.Yes)
                    {
                        images.Clear();
                        break;
                    }

                    else
                    {
                        continue;
                    }

                }
            }
            while (morePages == DialogResult.Yes);

            if (images.Count == 0)
                return; // The user canceled the acquisition

            // Some scanners do not rotate the even pages when duplex ADF
            // If the user checks the option, rotate the even pages
            if (images.Count > 1)
            {
                if (options.UseADF & options.Duplex & options.RotateDuplex)
                {
                    for (int i = 1, loopTo = images.Count - 1; i <= loopTo; i += 2)
                    {
                        var img = Image.FromFile(images[i]);
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        img.Save(images[i]);
                    }
                }
            }

            // Process the images to the desired output
            switch (options.ScanOutput)
            {

                case ScanOutput.File:
                    {
                        if (images.Count == 1)
                        {
                            var img = Image.FromFile(images[0]);
                            if (ReferenceEquals(format, ImageFormat.Jpeg))
                            {
                                var jgpEncoder = GetEncoder(ImageFormat.Jpeg);

                                // Create an Encoder object based on the GUID
                                // for the Quality parameter category.
                                var myEncoder = Encoder.Quality;

                                // Create an EncoderParameters object.
                                // An EncoderParameters object has an array of EncoderParameter
                                // objects. In this case, there is only one
                                // EncoderParameter object in the array.
                                var myEncoderParameters = new EncoderParameters(1);

                                var myEncoderParameter = new EncoderParameter(myEncoder, options.Quality);
                                myEncoderParameters.Param[0] = myEncoderParameter;
                                img.Save(options.Path, jgpEncoder, myEncoderParameters);
                            }
                            else
                            {
                                img.Save(options.Path, format);
                            }
                            img.Dispose();
                            File.Delete(images[0]);
                        }

                        else if (images.Count > 1)
                        {
                            // If more than one page is acquired, we need to rename the files with a counter
                            for (int i = 0, loopTo1 = images.Count - 1; i <= loopTo1; i++)
                            {
                                // Save the images with the pattern "filename 000.ext"
                                string path = pathWoExt + i.ToString(" 000") + "." + ext;
                                var img = Image.FromFile(images[i]);

                                if (ReferenceEquals(format, ImageFormat.Jpeg))
                                {
                                    var jgpEncoder = GetEncoder(ImageFormat.Jpeg);

                                    // Create an Encoder object based on the GUID
                                    // for the Quality parameter category.
                                    var myEncoder = Encoder.Quality;

                                    // Create an EncoderParameters object.
                                    // An EncoderParameters object has an array of EncoderParameter
                                    // objects. In this case, there is only one
                                    // EncoderParameter object in the array.
                                    var myEncoderParameters = new EncoderParameters(1);

                                    var myEncoderParameter = new EncoderParameter(myEncoder, options.Quality);
                                    myEncoderParameters.Param[0] = myEncoderParameter;
                                    img.Save(path, jgpEncoder, myEncoderParameters);
                                }
                                else
                                {
                                    img.Save(path, format);
                                }

                                img.Dispose();
                                File.Delete(images[i]);
                            }
                        }

                        if (Properties.Settings.Default.FileOpenAfterAcquisition | openAfterAcquisition)
                        {
                            OpenFile(options.Path);
                        }

                        break;
                    }

                case ScanOutput.PDF:
                    {
                        var doc = new PDFWriter.PDFDocument();
                        for (int i = 0, loopTo2 = images.Count - 1; i <= loopTo2; i++)
                        {
                            var img = Image.FromFile(images[i]);
                            doc.AddPage(img, options.PaperSize, options.Scaling, options.Center);
                        }

                        doc.Save(options.Path);
                        doc.Close();

                        for (int i = 0, loopTo3 = images.Count - 1; i <= loopTo3; i++)
                            File.Delete(images[i]);

                        if (Properties.Settings.Default.PDFOpenAfterAcquisition | openAfterAcquisition)
                        {
                            OpenFile(options.Path);
                        }

                        break;
                    }

                default:
                    {
                        _printer.AddImages(images, (short)options.Scaling, options.Center);
                        // Prints images
                        try
                        {
                            _printer.Print((short)options.Copies);
                        }
                        catch (ArgumentException ex) // If no images were sent to the printer
                        {

                        }

                        break;
                    }
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;

        }

        public static DeviceEvents GetScannerEvents()
        {
            return _scanner.Events;
        }

        public static string ScannerDescription
        {
            get
            {
                return _scanner.Description;
            }
        }

        public static Scanner Scanner
        {
            get
            {
                return _scanner;
            }
        }

        public static Printer Printer
        {
            get
            {
                return _printer;
            }
        }

        public static List<CultureInfo> AvailableLanguages
        {
            get
            {
                // Wait for the thread that lists the available cultures to end
                GetCulturesThread.Join();
                return _availableCultures;
            }
        }
    }
}