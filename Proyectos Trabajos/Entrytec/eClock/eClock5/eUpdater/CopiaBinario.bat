
cd %1

echo %1
echo %2
if %2==bin\Release\ GOTO Actualizar
if %2==bin\DebugLiHome1\ GOTO CopiarDebugLiHome1
GOTO FIN
:Actualizar
echo Copiando Binario 
ren del ..\..\..\Binario\eUpdater\*.* /Q
xcopy bin\Release\*.* ..\..\..\Binario\eUpdater\ /Y 
del ..\..\..\Binario\eUpdater\*.pdb /Q
del ..\..\..\Binario\eUpdater\*vshost.* /Q
del ..\..\..\Binario\eUpdater\*.log /Q
del ..\..\..\Binario\eUpdater\*.json /Q
del ..\..\..\Binario\eUpdater\*.xml /Q

ECHO Haciendo Comit
TortoiseProc.exe /command:commit /path:"..\..\..\Binario\eUpdater"

:CopiarDebugLiHome1
echo Copiando LiHome1 
xcopy bin\DebugLiHome1\*.* C:\Temp\eUpdater\ /Y 
xcopy C:\Temp\eUpdater\*.* \\lihome1\temp\eUpdater\ /Y 

:FIN
ECHO Finalizado