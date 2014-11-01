
cd %1

echo %1
echo %2
if %2==bin\Release\ GOTO Actualizar
GOTO FIN
:Actualizar
echo Copiando Binario 
ren del ..\..\Binario\eClockView\*.* /Q
xcopy bin\Release\*.* ..\..\Binario\eClockView\ /Y 
del ..\..\Binario\eClockView\*.pdb /Q
del ..\..\Binario\eClockView\*vshost.* /Q
del ..\..\Binario\eClockView\*.log /Q

del ..\..\Binario\eClockView\eClockSync.exe.config /Q
rem ren ..\..\Binario\eClockView\eClockSync.exe.config eClockSync.exe.inst 

xcopy bin\Release\*.* ..\..\Binario\eClockSync\ /Y 
del ..\..\Binario\eClockSync\*.pdb /Q
del ..\..\Binario\eClockSync\*vshost.* /Q
del ..\..\Binario\eClockSync\*.log /Q

del ..\..\Binario\eClockSync\eClockSync.exe.config /Q
rem ren ..\..\Binario\eClockSync\eClockSync.exe.config eClockSync.exe.inst 

ECHO Haciendo Comit
TortoiseProc.exe /command:commit /path:"..\..\Binario\eClockView"
TortoiseProc.exe /command:commit /path:"..\..\Binario\eClockSync"
:FIN
ECHO Finalizado