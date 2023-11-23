# Automated script for creating setup and zip files
# Project folder
#$ProjDir = "D:\Matteo\Documenti\Visual Studio 2010\Projects\iCopy\iCopy\"
$BuildDir = Split-Path $myinvocation.mycommand.path
cd $BuildDir
cd ..

$ProjDir = pwd

# NEEDED:
#	* NSIS Installer
$NSISPath = "C:\Program Files (x86)\NSIS"
#	* 7za.exe in $PWD
#	* setup.nsi in $PWD
$NSISOutScript = "$ProjDir\Build\setup.nsi"

del "$ProjDir\bin\*.exe"
del "$ProjDir\bin\*.zip"

#Gets verison
$FullVersion = (dir "$ProjDir\bin\Release\iCopy.exe").VersionInfo.FileVersion
$Version = $FullVersion.Substring(0,5)

#Creates the installer
."$NSISPath\makensis.exe" "/DVERSION=$Version" /X"VIProductVersion $FullVersion" "$NSISOutScript" 

#Now creates the zip archive
."$ProjDir\Build\7za.exe" a -tzip -mx9 -r "$ProjDir\bin\iCopy$Version.zip" -x!*log -x!*settings -x!*vshost* -x!*pdb "$ProjDir\bin\Release\*"

cd $BuildDir