#define MyAppName "Sprdef2"
#define MyAppVersion "1.2"
#define MyAppPublisher "WinSoft"
#define MyAppURL "https://github.com/Anders-H/Sprdef2/blob/main/README.md"
#define MyAppExeName "Sprdef2.exe"

[Setup]
AppId={{BF4AEE9E-DEDA-4205-BD0A-1B874079B36B}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=D:\GitRepos\Sprdef2\LICENSE
;PrivilegesRequired=lowest
OutputDir=D:\GitRepos\Sprdef2
OutputBaseFilename=SetupSprdef2
SetupIconFile=D:\GitRepos\Sprdef2\sprite.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\GitRepos\Sprdef2\Sprdef2\bin\Release\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Sprdef2\Sprdef2\bin\Release\C64Color.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Sprdef2\Sprdef2\bin\Release\C64ColorControls.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Sprdef2\Sprdef2\bin\Release\EditStateSprite.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Sprdef2\Sprdef2\bin\Release\Sprdef2.exe.config"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent