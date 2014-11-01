
cd %1

echo %1
echo %2
if %2==bin\Release\ GOTO Actualizar
if %2==bin\DebugLiTabW8\ GOTO CopiarLiTabW8
GOTO FIN
:Actualizar
echo Copiando Binario 
del ..\..\..\Binario\Kiosco\*.* /Q
xcopy bin\Release\*.* ..\..\..\Binario\Kiosco\ /Y 
del ..\..\..\Binario\Kiosco\*.pdb /Q
del ..\..\..\Binario\Kiosco\*vshost.* /Q
del ..\..\..\Binario\Kiosco\*.log /Q
del ..\..\..\Binario\Kiosco\*.json /Q
ren ..\..\Binario\Kiosco\Kiosco.exe.config Kiosco.exe.inst 
md ..\..\..\Binario\Kiosco\Resources
xcopy bin\Release\Resources\*.* ..\..\..\Binario\Kiosco\Resources\ /Y 

md ..\..\..\Binario\Kiosco\eUpdater
del ..\..\..\Binario\Kiosco\eUpdater\*.* /Q
xcopy ..\..\..\Binario\eUpdater\*.* ..\..\..\Binario\Kiosco\eUpdater /Y 
ren md ..\..\..\Binario\Kiosco\eUpdater\x86
ren xcopy ..\..\..\Binario\eUpdater\x86\*.* ..\..\..\Binario\Kiosco\eUpdater\x86 /Y 


ECHO Haciendo Comit
TortoiseProc.exe /command:commit /path:"..\..\..\Binario\Kiosco"
GOTO FIN

:CopiarLiTabW8
xcopy bin\DebugLiTabW8\*.* C:\Temp\Kiosco\ /Y 
md C:\Temp\Kiosco\Resources
xcopy bin\DebugLiTabW8\Resources\*.* C:\Temp\Kiosco\Resources\ /Y 
md C:\Temp\Kiosco\Datos


xcopy bin\DebugLiTabW8\Kiosco.exe W:\Kiosco\ /Y 
xcopy bin\DebugLiTabW8\eClockBase.dll W:\Kiosco\ /Y 
xcopy bin\DebugLiTabW8\*.pdb W:\Kiosco\ /Y 
ren xcopy bin\DebugLiTabW8\*.* W:\Kiosco\ /Y 
ren md W:\Kiosco\Resources
ren xcopy bin\DebugLiTabW8\Resources\*.* W:\Kiosco\Resources\ /Y 
ren md W:\Kiosco\Datos

:FIN
ECHO Finalizado