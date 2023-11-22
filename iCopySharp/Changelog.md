<link href="https://sourceforge.net/p/icopy/git/ci/master/tree/style.css?format=raw" type="text/css" rel="stylesheet"></link>
## 1.7.0
* NEW: Update to .NET Framework 4.6, as .NET 2.0 is not maintained anymore
* FIX: The regression on the file path is now actually fixed [bug #340]
* FIX: Updated French translation

## 1.6.6
* FIX: Fixed a regression with iCopy not asking for a path for saving file, and trying to use the previous path [bug #340]
* FIX: Paper size dropdown list now shows the entire text [bug #343]

## 1.6.5
* FIX: Fixed a regression with ADF scannners causing an exception [bug #337] 

## 1.6.4
* ADD: `/open` command line switch to open file after acquisition
* FIX: Now iCopy should work on Windows 10
* FIX: The interface now supports High DPI screens such as 2K and 4K monitors
* FIX: Other bug fixes and improvements

## 1.6.3
* ADD: Added `/quality` command line parameter to adjust JPEG quality [bug #289]
* FIX: Fixed a problem that may result in corrupt PDF [bug #298]
* FIX: Fixed a problem with the aquisition area in command line mode [bug #284], [bug #302]
* FIX: Handling of `0x80210008 WIA_ERROR_USER_INTERVENTION` [bug #270]
* FIX: Fixed a problem with settings when computer name starts with a number [bug #253]
* FIX: If folder output is given in command line mode, save directly without asking [bug #285]
* FIX: Paper size changed when going to printer settings with some printers [bug #277]
* CHANGED: Default resolution brought to 300 DPI, and default settings are now tweakable [bug #278]

## 1.6.2
* FIX: Problem with PDF file structure [Bug #261]
* FIX: Focus stealing problem with the dialog windows [Bug #266]
* FIX: Fix exception in settings dialog [Bug #258]
* FIX: Fix error while copying from command line [Bug #260]
* FIX: It is now possible to output `/multiplepages` to `/pdf` with command line
* FIX: Better handling of command line parameters
* FIX: Updated font to Windows standard Segoe UI font

## 1.6.1
* FIX: Various interface fixes
* CHANGED: Settings are saved when changed

## 1.6 
* FIX: Final fixes from beta 2

## 1.6 beta 2 
* FIX: With some scanners (eg. some Canon MX multifunction printers) the acquired image is not centered in the created PDF
* FIX: Unhandled exception when using command line arguments
* FIX: Other bug fixes and overall code improvement

## 1.6 beta 1 
* ADD: Save to PDF function
* ADD: Some improvements to the interface
* ADD: The acquired image can be printed in the center of the image

## 1.5.1 
* ADD: Duplex ADF acquisition
* FIX: Crash with Brother MFC-6800 (wrong image format)
* FIX: Crash at startup when wiaaut.dll not registered

## 1.5 
* FIX: Improvements in ADF support.
* FIX: Less memory consumption. Images are saved to temporary files, which are deleted as long as they are not needed.
* FIX: JPEG quality now works as expected.
* ADD: /silent parameter to suppress message boxes.
* FIX: Shrinked executable file.
* FIX: Sometimes, the main window doesn't get focus at startup
* FIX: Other fixes.

## 1.5 beta3
* FIX: Improved ADF with a bunch of scanners
* FIX: Improvements in logging feature
* FIX: ArgumentException during acquisition with some scanners
* FIX: Exception if the default scanner is no longer connected or has been moved to another USB port

## 1.5 beta2
* FIX: ADF with some scanners was not working (thanks to cmumathwhiz)
* FIX: Some minor stability fixes

## 1.5 beta1
* ADD: Logging for troubleshooting purposes. iCopy will save a .log file either in its folder or in %AppData%\Local\iCopy
* IMPROVED: Revised command line parameters. See README.txt for information or type iCopy.exe /help
* FIX: Scanner handling has been revised, now it's more robust. Should avoid problems when scanning a second page
* FIX: Fixed a regression in version 1.49 that resulted in an error during the acquisition
* FIX: Some scanners (eg. CanoScan 4400F) acquire only a quarter of the page (bad handling of WIA_IPS_XEXTENT & WIA_IPS_YEXTENT)
* FIX: Some scanners (eg. CanoScan 4400F) not supporting WIA_IPS_CUR_INTENT property (which should be required) acquire only in black and white mode

## 1.49 
* FIX: iCopy crashes (or throws exception) when doing a second scan on certain configurations * THANKS to D. Forester for this fix
* FIX: Possible iCopy closing on startup without notifications
* FIX: Some small code improvements
* HOTFIX: Exception when modifying brightness or contrast due to conversion problem

## 1.48 
* ADD: 	Possibility to choose the bit depth of the acquired page. 
		This feature should be used only if the page is not acquired correctly.
* FIX: 	Fixed a bug that prevented iCopy to save settings if the directory is protected (default behavior in Vista and 7)
		If iCopy does not have permissions to write in its execution directory, 
		the file will be saved in the user's settings directory (eg C:\Users\user\AppData\Local\iCopy).
* FIX: 	Fixed a bug that occurs with some Canon scanners on Windows XP. The acquired page has wrong colors.
* FIX:	Fixed some problems with the Settings dialog
* FIX:	Other bugfixes and code optimizations.

## 1.47 
* ADD: New -diag command line parameter to diagnose scanner information
* FIX: COMException on scanner selection due to problems in scanner driver won't make iCopy crash
* FIX: FileIOException on startup due to bad registration of wiaaut.dll is properly handled
* FIX: Fixed ArgumentException when scanning second time
* FIX: iCopy now correctly registers WIA Automation Layer with Vista and 7's UAC
* FIX: Better JPEG compression performance
* FIX: Memory consumption after acquisition has been reduced
* FIX: Other minor bugfixes and code improvements

## 1.46 
* ADD: JPEG Quality selection
* FIX: iCopy process takes a long time to close
* FIX: Exception when scanner is offline
* FIX: InvalidOperationException when closing iCopy
* FIX: Invalid resolutions when using a different scanner
* FIX: Other minor bugfixes and code improvements

## 1.45 
* CHANGED: Settings are now saved in iCopy directory instead of Users AppData folder
* CHANGED: Error reporting functionality improved
* FIX: Only part of the scanner bed area is acquired with some scanners (eg. Canon CanoScan LiDE 100, Canon CanoScan LiDE 600F, HP OfficeJet J4680) 
* FIX: An exception could be thrown then scanner is busy
* FIX: Not all values set in enlargment textbox were accepted
* FIX: Less alarming message when no scanner is connected
* FIX: Small GUI refinements to make all languages appear correctly
* FIX: Other minor bugfixes and code improvements

## 1.44 
* ADD: Run iCopy directly by clicking on the scanner button (installer version only)
* FIX: Possible COM exception when scanning
* FIX: Possible crashes after exceptions
* FIX: [LP 310254] No reaction on copy button
* FIX: Other minor bugfixes and code improvements

## 1.43 
* ADD: Error reporting functionality
* FIX: ArgumentException when clicking Copy
* FIX: Possible UnhandledException when changing resolution
* FIX: [BUG 1977329] Unhandled exception when closing Image Setting windows
* FIX: Possible NullReferenceException when closing iCopy
* FIX: Other code optimizations and bugfixes
* Moved bug tracker to Launchpad: <http://bugs.launchpad.net/icopy>

## 1.42 
* NEW: Command line parameters
* FIX: wiaaut.dll should now be registered properly
* FIX: PaperSize is saved correctly
* FIX: Scan To File dialog not showing images in current folder
* FIX: When scanning directly on iCopy startup, max resolution is used
* FIX: Error on second acquisition should be fixed, but I couldn't reproduce it on my computer
* FIX: Other code optimizations and bugfixes

## 1.41 
* FIX: Possible ThreadStateException on startup
* FIX: Comboboxes are now not editable
* FIX: Low quality printed images and/or incorrect enlargement
* FIX: Now only REALLY available resolutions are shown in Quality combo box
* FIX: ArgumentException when making a copy
* FIX: Max resolution cropped at 2000 DPI to prevent various problems (higher resolutions are useless for iCopy purpose)
* FIX: Other code optimizations and bugfixes

## 1.4 
* ADD: Option to Buffer multiple images before printing (under Other modes > Scan multiple pages)
* ADD: Quality can be set in DPI, and only available resolutions are enabled
* ADD: Remember default printer
* ADD: Paper size selection
* ADD: Shortcuts to Copy (CTRL+C), Multiple pages (CTRL+M), Scan to File (CTRL+F), Image settings (CTRL+I)
* FIX: Various fixes in image settings palette
* FIX: Unhandled Exception when opening image settings palette
* FIX: Unhandled ThreadStateException on form load
* FIX: Preview can now be used with Scan to File
* FIX: Preview now doesn't ask which scanner to use
* FIX: Various code optimizations and bugfixes

## 1.31 
* ADD: Now files can be saved in JPG, GIF, BMP and PNG
* ADD: Restore default settings
* ADD: BAT File to register wiaaut.dll (to be tested)
* FIX: When clicking on settings just after program start, not all the available languages are shown
* FIX: [BUG 1811189] Now preview works with scan to file
* FIX: [BUG 1803807] Exception may be thrown in certain conditions when changing image quality
* FIX: [BUG 1802415] Sometimes an enlarged image is printed
* FIX: When Scanning to file, the file may be corrupt
* FIX: Blank Message box when scanner is used by another application

## 1.3 
* ADD: [REQ 1792273] Image can be resized by percentage (by now from 1 % to 200 %)
* ADD: Settings dialog with language selection and other options
* NEW: Now all strings are in a unique resource file (WinFormStrings.resx), making it simplier to translate iCopy
* NEW: Improved interface:
		* advanced image settings are now in a separate palette
		* [REQ 1792405] option to store window position after program restart 
* FIX: Exception when user doesn't choose any scanner after clicking on Choose scanner
* FIX: [BUG 1792271] In some cases you can't start another copy process after completing the first one. Should be fixed (I couldn't reproduce it on my computer) 
* FIX: Many bugfixes and code improvements

## 1.24 
* ADD: NSIS Installer
* ADD: WIA Diagnosis tool
* FIX: WIA Registration error
* FIX: Other small bugfixes

## 1.23 
* ADD: Scan to file function
* FIX: WIA COM Error. Now wiaaut.dll is automatically registered

## 1.22 
* ADD: Better preview feature
* ADD: MSI Installer Setup
* FIX: Now image is not stretched to fit page
* FIX: COM exception 0x80040154 now handled
* FIX: Scanner disconnection now detected
* FIX: Some fixes in exception handling on startup

## 1.21 
* ADD: Support for localization
* ADD: Italian localization
* FIX: iCopy may close on startup
* FIX: Some code improvements and bugfixes regarding scanner connection

## 1.2 
* ADD: Tooltips
* ADD: Preview option (sperimental)
* ADD: About box
* ADD: Status bar
* FIX: Bug when clicking cancel after changing scanner
* FIX: Minor bugfixes and code improvements

## 1.11 
* ADD: Statusbar
* FIX: Critical error on start
* FIX: Minor bugfixes

## 1.1 
* ADD: Possibility to set the printer to colour or black & white directly in the program
* ADD: Image quality selection with three modes: Draft (100 DPI), Normal (250 DPI), High (400 DPI)
* FIX: Error when starting while scanner isn't ready
* FIX: Exception when closing Choose scanner window
* FIX: Minor bug in keypress interception in brightness and constrast textboxes
* FIX: Some changes to the interface
* FIX: Icon optimized
* FIX: Other bugfixes and code optimizations

## 1.0.1.1 
* FIX: Bug when no scanner is connected
* FIX: Values entered for brightness and contrast are better checked