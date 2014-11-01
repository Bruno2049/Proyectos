[Setup]
MinVersion=4.0,4.0
BackColor=$ff0000
BackSolid=false
WindowShowCaption=true
WindowStartMaximized=true
WindowVisible=true
WindowResizable=true
DisableDirPage=false
DisableStartupPrompt=1
CreateAppDir=true
DisableProgramGroupPage=false
;AlwaysCreateUninstallIcon=false
DisableFinishedPage=false
ChangesAssociations=false
Uninstallable=true
AllowNoIcons=false
;-------------------ZKSoftware----------------
DefaultDirName={pf}\Att2007
DefaultGroupName={cm:MyAppName}
AppName={cm:MyAppName}
AppVerName={cm:MyAppName}
;InfoBeforeFile=..\ZKSoftware\readme.txt
AppCopyright=Copyright 1998-2007,All rights reserved
WizardImageFile=Wizard.bmp

;---------------------------------------------
;-------------OEM-----------------------------
;DefaultDirName={pf}\Software\Att
;DefaultGroupName=指纹考勤管理系统
;AppName=指纹考勤管理系统
;AppVerName=指纹考勤管理系统1.8标准版
;LicenseFile=..\ZKSoftware\license.txt
;InfoBeforeFile=..\ZKSoftware\readme.txt
;OutputDir=out
;AppCopyright=Copyright 1998-2004,All right reserved

OutputBaseFilename=setup
[CustomMessages]
cns.MyAppName=考勤管理系统标准版
cns.MyAppVerName=考勤管理系统标准版
cns.MyDefaultDirName=att2007
cns.DocumentName=使用手册

cnt.MyAppName=畉对恨瞶╰参夹非
cnt.MyAppVerName=畉对恨瞶╰参夹非
cnt.MyDefaultDirName=ZKSoftware\att
cnt.DocumentName=ㄏノも

en.MyAppName=Attendance Management
en.MyAppVerName=Attendance Management
en.MyDefaultDirName=ZKSoftware\att
en.DocumentName=Manual

;span.MyAppName=ZK Attendance Management
;span.MyAppVerName=ZK Attendance Management
;span.MyDefaultDirName=ZKSoftware\att
;span.DocumentName=Manual
;--------------中性安装---------------------------
;cns.MyAppName=指纹考勤管理系统
;cns.MyAppVerName=指纹考勤管理系统
;cns.MyDefaultDirName=Software\att
;cns.DocumentName=使用手册

;cnt.MyAppName=畉对恨瞶╰参
;cnt.MyAppVerName=畉对恨瞶╰参
;cnt.MyDefaultDirName=Software\att
;cnt.DocumentName=ㄏノも

;en.MyAppName=Attendance Management
;en.MyAppVerName=Attendance Management
;en.MyDefaultDirName=Software\att
;en.DocumentName=Manual

[Types]
Name: full; Description: 完全安装;Languages:cns
Name: mini; Description: 最小安装;Languages:cns
Name: custom; Description: 定制安装; Flags: iscustom;Languages:cns
Name: full; Description: Ч杆     ;Languages:cnt
Name: mini; Description: 程杆     ;Languages:cnt
Name: custom; Description: ﹚杆; Flags: iscustom;Languages:cnt
Name: full; Description: Full;Languages:en
Name: mini; Description: Minimum;Languages:en
Name: custom; Description: Custom; Flags: iscustom;Languages:en
;Name: full; Description: Full;Languages:span
;Name: mini; Description: Minimum;Languages:span
;Name: custom; Description: Custom; Flags: iscustom;Languages:span

[Components]
Name: admin; Description: 指纹考勤管理程序; Types: full mini custom;Languages:cns
;name: data; Description: 数据库支持组件(Microsoft Data Access Components); ExtraDiskSpaceRequired: 9048576; OnlyBelowVersion: 4.10.2222,5.0;Languages:cns
name: manual; Description: 使用说明书; types: full; ExtraDiskSpaceRequired: 6648576;Languages:cns
Name: admin; Description: σ对恨瞶祘; Types: full mini custom; Languages:cnt
;name: data; Description: 计沮畐や舱ン(Microsoft Data Access Components); ExtraDiskSpaceRequired: 9048576; flags:fixed; Languages:cnt
name: manual; Description: ㄏノ弧; types: full; ExtraDiskSpaceRequired: 6648576; Languages:cnt
Name: admin; Description: Attendance Management; Types: full mini custom; Languages:en
;name: data; Description: Microsoft Data Access Components; ExtraDiskSpaceRequired: 9048576; flags:fixed; OnlyBelowVersion: 4.10.2222,5.0; Languages:en
name: manual; Description: Manual; types: full; ExtraDiskSpaceRequired: 5648576; Languages:en


[Files]
Source: {src}\files\att.exe; DestDir: {app}\; Components: admin;          Flags: external  ignoreversion
;2006-06-8新加
Source: {src}\files\update.exe; DestDir: {app}\; Components: admin;       Flags: external  ignoreversion
Source: {src}\files\update.ini; DestDir: {app}\; Components: admin;       Flags: external  ignoreversion

Source: {src}\files\rptlist.cfg; DestDir: {app}\; Components: admin;      Flags: external
Source: {src}\files\rptlist_e.cfg; DestDir: {app}\; Components: admin;    Flags: external
Source: {src}\files\rptlist_t.cfg; DestDir: {app}\; Components: admin;    Flags: external
;报表
Source: {src}\files\attdaily.rpt; DestDir: {app}\; Components: admin;     Flags: external
Source: {src}\files\attdaily_t.rpt; DestDir: {app}\; Components: admin;   Flags: external
Source: {src}\files\attdaily_e.rpt; DestDir: {app}\; Components: admin;   Flags: external
Source: {src}\files\atttotal.rpt; DestDir: {app}\; Components: admin;     Flags: external
Source: {src}\files\atttotal_t.rpt; DestDir: {app}\; Components: admin;   Flags: external
Source: {src}\files\atttotal_e.rpt; DestDir: {app}\; Components: admin;   Flags: external
Source: {src}\files\depttotal.rpt; DestDir: {app}\; Components: admin;    Flags: external
Source: {src}\files\depttotal_t.rpt; DestDir: {app}\; Components: admin;  Flags: external
Source: {src}\files\depttotal_e.rpt; DestDir: {app}\; Components: admin;  Flags: external
Source: {src}\files\empattrec.rpt; DestDir: {app}\; Components: admin;    Flags: external
Source: {src}\files\empattrec_t.rpt; DestDir: {app}\; Components: admin;  Flags: external
Source: {src}\files\empattrec_e.rpt; DestDir: {app}\; Components: admin;  Flags: external
Source: {src}\files\empinouts.rpt; DestDir: {app}\; Components: admin;    Flags: external
Source: {src}\files\empinouts_t.rpt; DestDir: {app}\; Components: admin;  Flags: external
Source: {src}\files\empinouts_e.rpt; DestDir: {app}\; Components: admin;  Flags: external
Source: {src}\files\schs.rpt; DestDir: {app}\; Components: admin;         Flags: external
Source: {src}\files\schs_t.rpt; DestDir: {app}\; Components: admin;       Flags: external
Source: {src}\files\schs_e.rpt; DestDir: {app}\; Components: admin;       Flags: external
Source: {src}\files\recmodi.rpt; DestDir: {app}\; Components: admin;      Flags: external
Source: {src}\files\recmodi_t.rpt; DestDir: {app}\; Components: admin;    Flags: external
Source: {src}\files\recmodi_e.rpt; DestDir: {app}\; Components: admin;    Flags: external
Source: {src}\files\exc.rpt; DestDir: {app}\; Components: admin;          Flags: external
Source: {src}\files\exc_e.rpt; DestDir: {app}\; Components: admin;        Flags: external
Source: {src}\files\exc_t.rpt; DestDir: {app}\; Components: admin;        Flags: external
Source: {src}\files\attcust.rpt; DestDir: {app}\; Components: admin;      Flags: external
Source: {src}\files\attcust_t.rpt; DestDir: {app}\; Components: admin;    Flags: external
Source: {src}\files\attcust_e.rpt; DestDir: {app}\; Components: admin;    Flags: external
Source: {src}\files\attguard.rpt; DestDir: {app}\; Components: admin;     Flags: external
Source: {src}\files\attguard_t.rpt; DestDir: {app}\; Components: admin;   Flags: external
Source: {src}\files\attguard_e.rpt; DestDir: {app}\; Components: admin;   Flags: external
Source: {src}\files\AttLeave.rpt; DestDir: {app}\; Components: admin;     Flags: external
Source: {src}\files\AttLeave_e.rpt; DestDir: {app}\; Components: admin;     Flags: external
Source: {src}\files\AttLeave_t.rpt; DestDir: {app}\; Components: admin;     Flags: external
Source: {src}\files\empinouts1.rpt; DestDir: {app}\; Components: admin;   Flags: external
Source: {src}\files\attdailysch.rpt; DestDir: {app}\; Components: admin;  Flags: external
Source: {src}\files\attdailysch_e.rpt; DestDir: {app}\; Components: admin;Flags: external
Source: {src}\files\attdailysch_t.rpt; DestDir: {app}\; Components: admin;Flags: external
Source: {src}\files\empinouts2.rpt; DestDir: {app}\; Components: admin;   Flags: external
Source: {src}\files\empinouts2_t.rpt; DestDir: {app}\; Components: admin; Flags: external
Source: {src}\files\empinouts2_e.rpt; DestDir: {app}\; Components: admin; Flags: external

;---------------------------------------
;数据库
Source: {src}\files\template.tdb;   DestDir: {app}\; Components: admin;     Flags:onlyifdoesntexist external
Source: {src}\files\template_e.tdb; DestDir: {app}\; Components: admin;     Flags:onlyifdoesntexist external
Source: {src}\files\template_t.tdb; DestDir: {app}\; Components: admin;     Flags:onlyifdoesntexist external
Source: {src}\files\template.tdb;   DestDir: {app}\; DestName: att2000.mdb; Flags: uninsneveruninstall onlyifdoesntexist external; Components: admin;Languages:cns
Source: {src}\files\template_t.tdb; DestDir: {app}\; DestName: att2000.mdb; flags: uninsneveruninstall onlyifdoesntexist external; Components: admin;Languages:cnt
Source: {src}\files\template_e.tdb; DestDir: {app}\; DestName: att2000.mdb; flags: uninsneveruninstall onlyifdoesntexist external; Components: admin;Languages:en
;-------------------------------------------------------------------------------------
Source: {src}\files\rptviewer.exe; DestDir:  {app}\; Components: admin;     Flags: external
Source: {src}\files\unact.exe;     DestDir:  {app}\; Components: admin;     Flags: external
Source: {src}\files\Vsock28.vxd;   DestDir:  {sys}\; Components: admin;     Flags: external
Source: {src}\files\pquery.bin;    DestDir:  {app}\; Components: admin;     Flags: external
Source: {src}\files\Msrdp25.bin;   DestDir:  {app}\; Components: admin;     Flags: external
;报表语言库
source: {src}\files\FR_Chs.dll;    DestDir:  {app}\; Components: admin;       Flags: external
source: {src}\files\FR_cht.dll;    DestDir:  {app}\; Components: admin;       Flags: external
source: {src}\files\FR_Eng.dll;    DestDir:  {app}\; Components: admin;       Flags: external
;导入/出文件语言库
source: {src}\files\QEChinese.dll; DestDir:  {app}\; Components: admin;       Flags: external
source: {src}\files\QETai.dll;     DestDir:  {app}\; Components: admin;       Flags: external
source: {src}\files\QEEnglish.dll; DestDir:  {app}\; Components: admin;       Flags: external

;-------------------------------------------------------------------------------------
;脱机通讯接口
source: {src}\files\msvcr71.dll;   DestDir: {sys}; Components: admin;       Flags: external
Source: {src}\files\comms.dll;     DestDir: {sys}; Components: admin;       Flags: external ignoreversion
source: {src}\files\rscomm.dll;    DestDir: {sys}; Components: admin;       Flags: external ignoreversion
source: {src}\files\commpro.dll;   DestDir: {sys}; Components: admin;       Flags: external  ignoreversion
Source: {src}\files\zkemsdk.dll;   DestDir: {sys}; Components: admin;       Flags: external ignoreversion
source: {src}\files\zkecap.exe;    DestDir: {app}; Components: admin;       Flags: external
source: {src}\files\zkemkeeper.dll;DestDir: {sys}; Components: admin;       Flags: external  ignoreversion
;source: {src}\files\emfw.cfg;      DestDir: {app}; Components: admin;       Flags: external  ignoreversion
source: {src}\files\zkeupg.exe;    DestDir: {app}; Components: admin;       Flags: external
;-------------------------------------------------------------------------------------
;2004.12.18新加
source: {src}\files\plce.dll; DestDir: {sys}\; Components: admin;        Flags: external
;2005.05.30新加
source: {src}\files\rscagent.dll; DestDir: {sys}\; Components: admin;    Flags: external
;2006.06.08
;source: {src}\files\sepbiokey.dll; DestDir: {app}\; Components: admin;   Flags: external
;3.6.2取消
;source: {src}\files\biokey.ocx; DestDir: {app}\; Components: admin;      Flags: external


;脱机通讯程序 2005.05.30          Modified by wjz 2006-06-08
;source: {src}\files\ATTCOM.exe; DestDir: {app}\; Components: admin; Flags: external
;source: {src}\files\atcommuLan.chs; DestDir: {app}\; Components: admin; Flags: external
;source: {src}\files\atcommuLan.cht; DestDir: {app}\; Components: admin; Flags: external
;source: {src}\files\atcommuLan.en; DestDir: {app}\; Components: admin; Flags: external

;卡通讯
;source: {src}\files\ATTCARDVIEW.exe; DestDir: {app}\; Components: admin; Flags: external
;source: {src}\files\attCardViewLan.chs; DestDir: {app}\; Components: admin; Flags: external
;source: {src}\files\attCardViewLan.cht; DestDir: {app}\; Components: admin; Flags: external
;source: {src}\files\attCardViewLan.en; DestDir: {app}\; Components: admin; Flags: external

;消息管理
source: {src}\files\ATTSMS.exe; DestDir: {app}\; Components: admin; Flags: external
source: {src}\files\attSmsLan.chs; DestDir: {app}\; Components: admin; Flags: external
source: {src}\files\attSmsLan.cht; DestDir: {app}\; Components: admin; Flags: external
source: {src}\files\attSmsLan.en; DestDir: {app}\; Components: admin; Flags: external

;U盘管理
;source: {src}\files\ATTUDISK.exe; DestDir: {app}\; Components: admin; Flags: external
;source: {src}\files\attudiskLan.chs; DestDir: {app}\; Components: admin; Flags: external
;source: {src}\files\attudiskLan.cht; DestDir: {app}\; Components: admin; Flags: external
;source: {src}\files\attudiskLan.en; DestDir: {app}\; Components: admin; Flags: external

;source: {src}\files\udisk.dat; DestDir: {app}\; Components: admin; Flags: external
;---------------------------------------------------------------------------------------------
;SQL Server数据库脚本
Source: {src}\files\sqlserver.sql; DestDir: {app}\; Destname:sqlserver.sql;   Flags: external;Languages:cns
Source: {src}\files\sqlserver_t.sql; DestDir: {app}\; Destname:sqlserver.sql; Flags: external;Languages:cnt
Source: {src}\files\sqlserver_e.sql; DestDir: {app}\; Destname:sqlserver.sql; Flags: external;Languages:en
;-------------------------------------------------------------------------------------
;帮助文档
source: {src}\files\Att.chm; DestDir: {app}\; Components: admin;   Flags: external;Languages:cns
source: {src}\files\Att_e.chm; DestDir: {app}\; DestName: att.chm;Components: admin; Flags: external;Languages:cnt
source: {src}\files\Att_e.chm; DestDir: {app}\; DestName: att.chm;Components: admin; Flags: external;Languages:en


;source: {src}\files\Att2000.hlp; DestDir: {app}\; Components: admin;   Flags: external;Languages:cns
;source: {src}\files\Att2000.cnt; DestDir: {app}\; Components: admin;   Flags: external;Languages:cns
;source: {src}\files\Att2000_t.hlp; DestDir: {app}\; Components: admin; Flags: external;Languages:cnt
;source: {src}\files\Att2000_t.cnt; DestDir: {app}\; Components: admin; Flags: external;Languages:cnt
;source: {src}\files\Att2000_e.hlp; DestDir: {app}\; Components: admin; flags: external;Languages:en
;source: {src}\files\Att2000_e.cnt; DestDir: {app}\; Components: admin; flags: external;Languages:en
Source: {src}\files\att.pdf; DestDir: {app}\; Components: manual;  Flags: external;Languages:cns
Source: {src}\files\att_cht.pdf; DestDir: {app}\;DestName: att.pdf;  Components: manual; Flags: external;Languages:cnt
Source: {src}\files\att_e.pdf; DestDir: {app}\; DestName: att.pdf; Components: manual; Flags: external;Languages:en
;Source: {src}\files\att2000.doc; DestDir: {app}\; Components: manual;  Flags: external;Languages:cns
;Source: {src}\files\att2000_cht.doc; DestDir: {app}\;DestName: att2000.doc;  Components: manual; Flags: external;Languages:cnt
;Source: {src}\files\att2000_e.doc; DestDir: {app}\; DestName: att2000.doc; Components: manual; Flags: external;Languages:en
;-------------------------------------------------------------------------------------
;程序配置文件
Source: {src}\files\att.dat;        DestDir: {app}\; DestName: att.dat ;      Components: admin;  Flags: uninsneveruninstall external onlyifdoesntexist; Languages:cns
Source: {src}\files\att_t.dat;      DestDir: {app}\; DestName: att.dat ;      Components: admin;  Flags: uninsneveruninstall external onlyifdoesntexist; Languages:cnt
Source: {src}\files\att_e.dat;      DestDir: {app}\; DestName: att.dat ;      Components: admin;  Flags:  uninsneveruninstall external onlyifdoesntexist; Languages:en
Source: {src}\files\attcheckin.chs; DestDir: {app}\; DestName: attcheckin.chs; Components: admin; Flags: external
Source: {src}\files\attcheckin.cht; DestDir: {app}\; DestName: attcheckin.cht; Components: admin; Flags: external
Source: {src}\files\attcheckin.en;  DestDir: {app}\; DestName: attcheckin.en; Components: admin;  Flags: external
;Source: {src}\files\attem.cfg; DestDir: {app}\;Components: admin; Flags: external
;-------------------------------------------------------------------------------------
;Source: {src}\files\Readme.txt; DestDir: {app}\; DestName: Readme.txt; Components: admin; Flags: external;Languages:cns
;Source: {src}\files\Readme_t.txt; DestDir: {app}\; DestName: Readme.txt; Components: admin; Flags: external;Languages:cnt
;Source: {src}\files\REGISTER.TXT; DestDir: {app}\; DestName: REGISTER.TXT; Components: admin; Flags: external;Languages:cns
;Source: {src}\files\REGISTER_t.TXT; DestDir: {app}\; DestName: REGISTER.TXT; Components: admin; Flags: external;Languages:cnt

;-------------------- OEM ---------------
Source: {src}\files\splash.jpg; DestDir: {app}\; DestName: splash.jpg;flags:external skipifsourcedoesntexist; Components: admin
Source: {src}\files\_logo.jpg; DestDir: {app}\; DestName: _logo.jpg;flags:external skipifsourcedoesntexist; Components: admin

;Source: {src}\files\_about.bmp; DestDir: {app}\; DestName: _about.bmp;flags:external skipifsourcedoesntexist; Components: admin;Languages:cns
;Source: {src}\files\_about_t.bmp; DestDir: {app}\; DestName: _about.bmp;flags:external skipifsourcedoesntexist; Components: admin;Languages:cnt
;Source: {src}\files\_about_e.bmp; DestDir: {app}\; DestName: _about.bmp;flags:external skipifsourcedoesntexist; Components: admin;Languages:en
;--------------------------------------------
;------------USB Driver----------------------
;Source: fp200a.inf; DestDir: {win}\inf; Flags: ignoreversion
Source: usbser.sys; DestDir: {win}\system32\drivers; Flags: ignoreversion
Source: zkcdc.inf; DestDir: {win}\inf; Flags: ignoreversion


[Icons]
Name: {commondesktop}\考勤管理系统标准版; Filename: {app}\att.exe; WorkingDir: {app}; IconFilename: {app}\att.exe; IconIndex: 0; Components: admin;Languages:cns
Name: {group}\考勤管理系统标准版; Filename: {app}\att.exe; WorkingDir: {app}; IconFilename: {app}\att.exe; IconIndex: 0; Components: admin ;Languages:cns
;Name: {group}\中控指纹考勤系统说明书; Filename: {app}\Att2000.doc; WorkingDir: {app}; Components: manual;Languages:cns
Name: {commondesktop}\畉对恨瞶╰参夹非; Filename: {app}\att.exe; WorkingDir: {app}; IconFilename: {app}\att.exe; IconIndex: 0; Components: admin;Languages:cnt
Name: {group}\畉对恨瞶╰参夹非; Filename: {app}\att.exe; WorkingDir: {app}; IconFilename: {app}\att.exe; IconIndex: 0; Components: admin ;Languages:cnt
;Name: {group}\い北畉对╰参弧; Filename: {app}\Att2000.doc; WorkingDir: {app}; Components: manual;Languages:cnt
Name: {commondesktop}\Attendance Management; Filename: {app}\att.exe; WorkingDir: {app}; IconFilename: {app}\att.exe; IconIndex: 0; Components: admin; Languages:en
Name: {group}\Attendance Management; Filename: {app}\att.exe; WorkingDir: {app}; IconFilename: {app}\att.exe; IconIndex: 0; Components: admin; Languages:en
;Name: {group}\ZK Fingerprint Attendance system Manual; Filename: {app}\att2000.doc; WorkingDir: {app}; Components: manual; Languages:en

[Run]
;Filename: {src}\mdac\mdac_typ_cns.exe; Components: data;Languages:cns
;Filename: {src}\mdac\mdac_typ_cnt.exe; Components: data;Languages:cnt
;Filename: {src}\mdac\mdac_typ_en.exe; Components: data;Languages:en
FileName: {sys}\regsvr32.exe;   Parameters: "/s ""{sys}\zkemkeeper.dll"""; Flags: waituntilidle runminimized
;FileName: {sys}\regsvr32.exe;   Parameters: "/s ""{app}\biokey.ocx"""; Flags: waituntilidle runminimized

[Languages]
Name: "cns"; MessagesFile: "ChineseSimp-11-5.1.0.isl";LicenseFile:"Files\License.txt"
Name: "cnt"; MessagesFile: "ChineseTrad-2-5.1.0.isl";LicenseFile:"Files\License_T.txt"
Name: "en"; MessagesFile: "Default.isl";LicenseFile:"Files\License_e.txt"

[LangOptions]
LanguageCodePage=0

