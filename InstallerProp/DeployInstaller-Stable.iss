; Script generated by the Inno Script Studio Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
#define AppVersion StringChange(GetFileVersion("..\..\CollapseLauncher-ReleaseRepo\stable-build\CollapseLauncher.exe"), ".0", "")

AppName=Collapse
AppVersion={#AppVersion}
AppCopyright=2023 - neon-nyan
AppPublisher=neon-nyan
VersionInfoVersion={#AppVersion}
VersionInfoCompany=neon-nyan
VersionInfoDescription=Collapse - An advanced launcher for miHoYo Games
VersionInfoCopyright=2023 - neon-nyan
VersionInfoProductName=Collapse
VersionInfoProductVersion={#AppVersion}
VersionInfoProductTextVersion={#AppVersion}-preview
SolidCompression=True
Compression=lzma2/ultra64
InternalCompressLevel=ultra64
MinVersion=0,10.0.17763
DefaultDirName={autopf64}\Collapse Launcher\
DefaultGroupName=Collapse
UninstallDisplayName=Collapse
UninstallDisplayIcon={app}\app-{#AppVersion}\CollapseLauncher.exe
WizardStyle=modern
WizardImageFile=..\InstallerProp\WizardBannerDesign.bmp
WizardSmallImageFile=..\InstallerProp\WizardBannerDesignSmall.bmp
DisableWelcomePage=False
ArchitecturesInstallIn64BitMode=x64
LicenseFile=..\LICENSE
SetupIconFile=..\CollapseLauncher\icon.ico
LZMAUseSeparateProcess=yes
LZMADictionarySize=1048576
LZMAMatchFinder=BT
LZMANumFastBytes=128
PrivilegesRequired=admin

[Icons]
Name: "{group}\Collapse Launcher\Collapse"; Filename: "{app}\CollapseLauncher.exe"; WorkingDir: "{app}\app-{#AppVersion}"; IconFilename: "{app}\app-{#AppVersion}\CollapseLauncher.exe"; IconIndex: 0
Name: "{group}\Collapse Launcher\Collapse (Hi3 Cache Updater)"; Filename: "{app}\app-{#AppVersion}\CollapseLauncher.Hi3CacheUpdater.cmd"; WorkingDir: "{app}\app-{#AppVersion}"; IconFilename: "{app}\app-{#AppVersion}\CollapseLauncher.exe"; IconIndex: 0
Name: "{userdesktop}\Collapse"; Filename: "{app}\CollapseLauncher.exe"; WorkingDir: "{app}\app-{#AppVersion}"; IconFilename: "{app}\app-{#AppVersion}\CollapseLauncher.exe"; IconIndex: 0

[Files]
Source: "..\..\CollapseLauncher-ReleaseRepo\stable-build\*"; DestDir: "{app}\app-{#AppVersion}"; Flags: ignoreversion createallsubdirs recursesubdirs
Source: "..\..\CollapseLauncher-ReleaseRepo\Update.exe"; DestDir: "{app}"
Source: "..\..\CollapseLauncher-ReleaseRepo\CollapseLauncher.exe"; DestDir: "{app}"      

[Tasks]
Name: StartAfterInstall; Description: Run application after install

[Run]
Filename: "{app}\CollapseLauncher.exe"; Description: "Collapse Launcher Installer"; Tasks: StartAfterInstall; Flags: postinstall nowait skipifsilent runascurrentuser
