This is the backport to C# of the well known VB.Net application : <http://icopy.sourceforge.net>

# iCopySharp 1.7.0

#### Release Date: 8 January 2020

iCopySharp is a free and open source software that lets you combine your scanner and printer into a powerful,
but easy to use photocopier by only pressing a button.Its simple user interface lets you manage scanner
and printer options, like brightness, contrast, number of copies in a couple of seconds. Automatic Document Feeders (ADF) and duplex scanners are supported. You can also save the acquired image to file or to PDF.
As it's small and no installation is required, iCopySharp is also suitable for USB pen drives.

1. Features
2. Requirements
3. Command line parameters
4. Known bugs and solutions
5. Support
6. Please help!

## 1. Features

* Simple and quick interface
* Scanning mode selection
* Run by pressing scanner button
* Resolution, brightness and contrast, scaling settings
* Scan Multiple Pages before printing, including ADF support
* Scan duplex (with compatible scanners)
* Scan to file function
* Scan to PDF with no external software
* Preview function
* Command-line parameters
* No installation needed and little hard disk space required
* Compatible with all WIA scanners and all printers

## 2. System requirements 

* Windows XP SP1/Vista/7/8/10, or Windows Server 2003 or newer
* Microsoft .NET Framework 4.6 or higher
* A WIA (Windows Image Acquisition) compatible scanner

> NOTE: Some older scanners are not compatible with WIA. If you have troubles with your scanner, please report it to the iCopySharp through GitHub

## 3. Command line parameters
--------------------------
iCopySharp can be run as a Windows application, but its functionality can entirely be controlled from the command line using the following syntax

    icopy.exe [/copy /file /pdf ..] [params]

`[params]` are the parameters to be used. If a parameter is not specified, iCopySharp uses the values stored in settings.

> NOTE: Parameters are not case sensitive

### Actions

| Parameter                         | Description 
|-----------------------------------|----------------
|`/copy` or `/c`				    |   Directly copy from scanner to printer, using settings provided or default settings
|`/file`, `/ScanToFile` or  `/f`	|   Scan to a file. If file path is not provided a dialog will let you choose where to save the acquired image
|`/pdf`                             |   Output to PDF file
|`/copyMultiplePages`	            |   Scan a multipage document.

### Parameters:

| Parameter                         | Description 
|-----------------------------------|-----------------
| `/adf`							|   Enable ADF support
| `/duplex`						    | 	Enable duplex ADF
|`/resolution` or `/r	[value]`	|	Specify a valid scanning resolution in DPI (eg `/resolution 100`, `/r 500`)
|`/color` or `/col`				    |   Color acquisition
|`/grayscale` or `/gray`			|Grayscale acquisition
|`/text` or `/bw`					|Black and white (text) acquisition
|`/copies` or `/nc [value]`		    |The number of copies to be printed (default is one copy)
|`/scaling` or `/s`				    |The scaling percentage (eg `/s 150`) default value: 100
|`/brightness`	or `/b [value]`		|Value from -100 to 100 for brightness
|`/contrast` -or -	`/cnt [value]`	|Value from -100 to 100 for constrast
|`/quality [value]`                 |   Value from 0 to 100 for JPEG quality
|`/preview` -or- `/p`				|Enables preview mode
|`/path "path"`					    |Specify the path for file acquisition. Paths containing spaces must be put between inverted commas (eg. `/path "C:\my folder\file.jpg"`). Valid file estensions are .jpg, .bmp, .gif, .png, or .pdf for PDF docs
|`/printer "printer name"`			|Specify the name of the printer between inverted commas. If not provided, the system default printer is used
|`/open`              | Open the file after the acquisition

### Examples

* Copy with 200 DPI resolution in text mode, with default brightness and contrast:

		icopy.exe /copy /r 200 /text

* Save to file "C:\my folder\file.jpg" with brighness 0 and contrast 10:

		icopy.exe /file /brightness 0 /contrast 10 /path "C:\my folder\file.jpg"

* Prints the file to PDF

		icopy.exe /pdf


#### Advanced parameters

| Parameter             | Description 
|-----------------------|-------
|`/silent`				|No message boxes.
|`/wiareg /wr`			|Registers WIA components. Use if WIA errors are thrown during execution.
|`/register /reg`		|Registers iCopySharp to the scanner buttons.
|`/unregister /unreg`	|Unregisters iCopySharp from the scanner button applications.

## 4. Known errors & solutions

### Wiaaut.dll not registered error
iCopySharp asks if you want to register WIA Automation Layer. If it fails in doing so, you can register it manually:

* Copy the file wiaaut.dll that you find in iCopySharp directory to C:\Windows\system32\
* Open a Prompt command with administrator rights
* Enter commands:

        cd C:\Windows\system32\
        regsvr32 wiaaut.dll

* You should receive a confirmation of wiaaut.dll registration
* Now iCopySharp should run as expected.

## 5. Please Help!

iCopySharp is free software, so it is supported only by your generous donations and by advertisements on the website.
If you like iCopy, please help me to make it a better software! You can:

* Report problems, tell me your ideas or let me know what you like or don't like in iCopy.
* Tell your friends about iCopy, on your blog, on Facebook, Twitter or wherever you like, so that it gets
more and more famous.
* Surf the website, leave comments, and maybe give a look (and a click) at the advertisements :-).
