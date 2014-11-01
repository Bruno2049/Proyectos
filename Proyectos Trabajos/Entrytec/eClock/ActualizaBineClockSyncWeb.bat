ECHO OFF
ECHO      Procesando eClockSyncWeb
ECHO      Borrando binarios actuales
del ..\Binario\eClockSyncWeb\*.* /Q
ECHO      copiando nuevos archivos
copy "eClockSync\bin\Release eClockWeb\"*.* ..\Binario\eClockSyncWeb\ /Y
ECHO      limpiando archivos no servibles
del ..\Binario\Release eClockWeb\IsProtectServer.* /Q
del ..\Binario\Release eClockWeb\IsTimeSync2.pdb /Q
del ..\Binario\Release eClockWeb\IsTimeSync2IPS.* /Q
del ..\Binario\Release eClockWeb\*.vshost.* /Q
del ..\Binario\Release eClockWeb\log.* /Q
del ..\Binario\Release eClockWeb\*.log /Q
ECHO      Terminado eClockWeb
ECHO ON