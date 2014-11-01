
cd %1

echo %1
echo %2
if %2==bin\Release\ GOTO Actualizar
if %2==bin\Debug\ GOTO DEBUG
if %2==bin\DebugWin7\ GOTO CopiarDebugWin7

GOTO FIN
:Actualizar
echo Copiando Binario 
ren del ..\..\..\Binario\eClock5\*.* /Q
xcopy bin\Release\*.* ..\..\..\Binario\eClock5\ /Y 
del ..\..\..\Binario\eClock5\*.pdb /Q
del ..\..\..\Binario\eClock5\*vshost.* /Q
del ..\..\..\Binario\eClock5\*.log /Q
del ..\..\..\Binario\eClock5\*.json /Q

md ..\..\..\Binario\eClock5\Resources
xcopy bin\Release\Resources\*.* ..\..\..\Binario\eClock5\Resources\ /Y 

md ..\..\..\Binario\eClock5\eUpdater
del ..\..\..\Binario\eClock5\eUpdater\*.* /Q
xcopy ..\..\..\Binario\eUpdater\*.* ..\..\..\Binario\eClock5\eUpdater /Y 
ren md ..\..\..\Binario\eClock5\eUpdater\x86
ren xcopy ..\..\..\Binario\eUpdater\x86\*.* ..\..\..\Binario\eClock5\eUpdater\x86 /Y 

md ..\..\..\Binario\eClock5\eClockSync
xcopy ..\..\..\Binario\eClockSync\*.* ..\..\..\Binario\eClock5\eClockSync /Y 
md ..\..\..\Binario\eClock5\eClockSync\Temporales
xcopy ..\..\..\Binario\eClock5\eClockSync.exe.config ..\..\..\Binario\eClock5\eClockSync /Y 

ECHO Haciendo Comit
TortoiseProc.exe /command:commit /path:"..\..\..\Binario\eClock5"
GOTO FIN

:CopiarDebugWin7
echo Copiando DebugWin7 
md \\entpc03\Temp\eClock5
xcopy bin\DebugWin7\*.* \\entpc03\Temp\eClock5\ /Y 
ren xcopy bin\DebugWin7\*.exe \\entpc03\Temp\eClock5\ /Y 

md \\entpc03\Temp\eClock5\Resources
xcopy bin\Release\Resources\*.* \\entpc03\Temp\eClock5\Resources\ /Y 

ren md \\entpc03\Temp\eClock5\eClock5\eUpdater
ren del \\entpc03\Temp\eClock5\eUpdater\*.* /Q
ren xcopy ..\..\..\Binario\eUpdater\*.* \\entpc03\Temp\eClock5\eClock5\eUpdater /Y 

ren md \\entpc03\Temp\eClock5\eClock5\eClockSync
ren xcopy ..\..\..\Binario\eClockSync\*.* \\entpc03\Temp\eClock5\eClock5\eClockSync /Y 
ren md \\entpc03\Temp\eClock5\\eClock5\eClockSync\Temporales

GOTO FIN


:DEBUG
xcopy bin\Debug\eClockBase.dll ..\..\eClockServices\ /Y 
xcopy bin\Debug\eClockBase.dll ..\..\eClockServices\Bin /Y 

:FIN
ECHO Finalizado