ECHO OFF

ECHO Preparando destino eClock
del ..\Binario\eClockReports\*.* /Q
del ..\Binario\eClockReports\bin\*.* /Q


ECHO Creando carpetas en eClockReports
mkdir ..\Binario\eClockReports
mkdir ..\Binario\eClockReports\BIN
mkdir ..\Binario\eClockReports\Logs

ECHO Copiando a Binario
xcopy eClock5\eClockReportsPub\*.* ..\Binario\eClockReports /Y /Q
xcopy eClock5\eClockReportsPub\BIN\*.* ..\Binario\eClockReports\BIN /Y /Q



ECHO Limpiando Binario
del ..\Binario\eClockReports\*.log /Q
del ..\Binario\eClockReports\*.config /Q
del ..\Binario\eClockReports\*.fla /Q
del ..\Binario\eClockReports\CIsLog.txt /Q
del ..\Binario\eClockReports\config.xml /Q
del ..\Binario\eClockReports\ItwAct /Q
del ..\Binario\eClockReports\ItwLlave /Q
del ..\Binario\eClockReports\*.dll  /Q
del ..\Binario\eClockReports\*.exe  /Q
del ..\Binario\eClockReports\ankh.*  /Q
del ..\Binario\eClockReports\app_offline.*  /Q
del ..\Binario\eClockReports\*.bat  /Q
del ..\Binario\eClockReports\*.webinfo /Q
del ..\Binario\eClockReports\*.pdb /Q
del ..\Binario\eClockReports\bin\*.pdb /Q



ECHO Haciendo Comit
"C:\Program Files\TortoiseSVN\bin\TortoiseProc.exe" /command:commit /path:"..\Binario\eClockReports"

ECHO ON