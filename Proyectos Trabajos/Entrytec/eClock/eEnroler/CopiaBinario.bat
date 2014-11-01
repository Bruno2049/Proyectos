
cd %1
echo Copiando Binario 
echo %1
echo %2
if %2==bin\Release\ GOTO Actualizar
GOTO FIN
:Actualizar
del ..\..\Binario\eEnroler\*.* /Q
xcopy bin\Release\*.* ..\..\Binario\eEnroler\ /Y 

del ..\..\Binario\eEnroler\*.pdb /Q
del ..\..\Binario\eEnroler\*vshost.* /Q
del ..\..\Binario\eEnroler\*.log /Q
del ..\..\Binario\eEnroler\eEnroler.exe.config /Q
del ..\..\Binario\eEnroler\CIsLog.txt /Q

ECHO Haciendo Comit
TortoiseProc.exe /command:commit /path:"..\..\Binario\eEnroler"

:FIN
ECHO Finalizado