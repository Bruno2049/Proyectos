
cd %1

echo %1
echo %2
if %2==bin\Release\ GOTO Actualizar
GOTO FIN
:Actualizar
echo Copiando Binario 
del ..\..\..\Binario\eClockSync5\*.* /Q
xcopy bin\Release\*.* ..\..\..\Binario\eClockSync5\ /Y 
del ..\..\..\Binario\eClockSync5\*.pdb /Q
del ..\..\..\Binario\eClockSync5\*vshost.* /Q
del ..\..\..\Binario\eClockSync5\*.log /Q
del ..\..\..\Binario\eClockSync5\*.json /Q
del ..\..\..\Binario\eClockSync5\*Cambios.xml /Q
del ..\..\..\Binario\eClockSync5\CIsLog.txt /Q

ren ..\..\Binario\eClockSync5\eClockSync5.exe.config eClockSync5.exe.inst 
md ..\..\..\Binario\eClockSync5\Resources
xcopy bin\Release\Resources\*.* ..\..\..\Binario\eClockSync5\Resources\ /Y 

md ..\..\..\Binario\eClockSync5\en
xcopy bin\Release\en\*.* ..\..\..\Binario\eClockSync5\en\ /Y 

md ..\..\..\Binario\eClockSync5\eUpdater
xcopy ..\..\..\Binario\eUpdater\*.* ..\..\..\Binario\eClockSync5\eUpdater /Y 
md ..\..\..\Binario\eClockSync5\eUpdater\x86
xcopy ..\..\..\Binario\eUpdater\x86\*.* ..\..\..\Binario\eClockSync5\eUpdater\x86 /Y 


ECHO Haciendo Comit
TortoiseProc.exe /command:commit /path:"..\..\..\Binario\eClockSync5"

:FIN
ECHO Finalizado