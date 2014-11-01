ECHO OFF

ECHO Preparando destino eClock
del ..\Binario\eClockServices\*.* /Q
del ..\Binario\eClockServices\bin\*.* /Q
del ..\Binario\eClockServices\Imagenes\*.* /Q
del ..\Binario\eClockServices\App_Data\*.* /Q
del ..\Binario\eClockServices\Scripts\*.* /Q
del ..\Binario\eClockServices\skins\*.* /Q


ECHO Creando carpetas en eClockServices
mkdir ..\Binario\eClockServices
mkdir ..\Binario\eClockServices\BIN
mkdir ..\Binario\eClockServices\Scripts
mkdir ..\Binario\eClockServices\Imagenes
mkdir ..\Binario\eClockServices\skins
mkdir ..\Binario\eClockServices\PB
mkdir ..\Binario\eClockServices\Querys

ECHO Creando carpetas Vacias en eClock
mkdir ..\Binario\eClockServices\Logs
mkdir ..\Binario\eClockServices\PDF
mkdir ..\Binario\eClockServices\ChartImages
mkdir ..\Binario\eClockServices\GaugeImages



ECHO Copiando a Binario
xcopy eClockServicesPub\*.* ..\Binario\eClockServices /Y /Q
xcopy eClockServicesPub\BIN\*.* ..\Binario\eClockServices\BIN /Y /Q
xcopy eClockServicesPub\Scripts\*.* ..\Binario\eClockServices\Scripts /Y /Q
xcopy eClockServicesPub\Imagenes\*.* ..\Binario\eClockServices\Imagenes /Y /Q
xcopy eClockServicesPub\skins\*.* ..\Binario\eClockServices\skins /Y /Q
xcopy eClockServicesPub\PB\*.* ..\Binario\eClockServices\PB /Y /Q
xcopy eClockServicesPub\Querys\*.* ..\Binario\eClockServices\Querys /Y /Q

xcopy eClockServices\Global.asax ..\Binario\eClockServices /Y /Q




ECHO Limpiando Binario
del ..\Binario\eClockServices\PDF\*.* /Q
del ..\Binario\eClockServices\XLS\*.* /Q
del ..\Binario\eClockServices\*.log /Q
del ..\Binario\eClockServices\*.config /Q
del ..\Binario\eClockServices\*.fla /Q
del ..\Binario\eClockServices\CIsLog.txt /Q
del ..\Binario\eClockServices\config.xml /Q
del ..\Binario\eClockServices\ItwAct /Q
del ..\Binario\eClockServices\ItwLlave /Q
del ..\Binario\eClockServices\*.dll  /Q
del ..\Binario\eClockServices\*.exe  /Q
del ..\Binario\eClockServices\ankh.*  /Q
del ..\Binario\eClockServices\app_offline.*  /Q
del ..\Binario\eClockServices\*.bat  /Q
del ..\Binario\eClockServices\*.webinfo /Q
del ..\Binario\eClockServices\*.pdb /Q
del ..\Binario\eClockServices\bin\*.pdb /Q
del ..\Binario\eClockServices\bin\*global.asax.* /Q


ECHO Haciendo Comit
"C:\Program Files\TortoiseSVN\bin\TortoiseProc.exe" /command:commit /path:"..\Binario\eClockServices"

ECHO ON