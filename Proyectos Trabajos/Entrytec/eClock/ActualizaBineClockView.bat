ECHO OFF
ECHO      Procesando eClockView
ECHO      Borrando binarios actuales
del ..\Binario\eClockView\*.* /Q
ECHO      copiando nuevos archivos
copy eClockView\bin\Release\*.* ..\Binario\eClockView\ /Y
ECHO      limpiando archivos no servibles
del ..\Binario\eClockView\IsProtectServer.* /Q
rem del ..\Binario\eClockView\eClockView.* /Q
del ..\Binario\eClockView\eClockView.pdb /Q
del ..\Binario\eClockView\eClockViewIPS.* /Q
del ..\Binario\eClockView\log.* /Q
del ..\Binario\eClockView\*.log /Q
del ..\Binario\eClockView\*.vshost.* /Q
del ..\Binario\eClockView\web.config /Q
del ..\Binario\eClockView\ItwAct /Q
del ..\Binario\eClockView\ItwLlave /Q
del ..\Binario\eClockView\CIsLog.txt /Q
rem ILMerge /lib:"C:\Windows\Microsoft.NET\Framework\v2.0.50727" /lib:"C:\Archivos de programa\Infragistics\NetAdvantage for .NET 2007 Volume 1 CLR 2.0\windows forms\bin" /lib:".." /log:log.txt /closed /target:winexe /out:ISCardPro.exe ISCard.exe IsProtectServer.exe
rem copy eClockView\bin\Release\eClockView.exe eClockView.exe /Y
rem ECHO      Actualizando IsProtect tomandolo de la ruta Binario
rem copy IsProtectServer.exe eClockView\bin\Release\IsProtectServer.exe /Y
rem copy IsProtectServer.pdb eClockView\bin\Release\IsProtectServer.pdb /Y
rem ECHO      Agregando IsProtect
rem cd eClockView\bin\Release\
rem ..\..\..\ILMerge /lib:"C:\Windows\Microsoft.NET\Framework\v2.0.50727"  /lib:".." /log:log.txt /closed /target:winexe /out:eClockViewIPS.exe eClockView.exe IsProtectServer.exe
rem cd ..\..\..
rem copy eClockView\bin\Release\eClockViewIPS.exe ..\Binario\eClockView\eClockView.exe /Y
ren ..\Binario\eClockView\eClockView.exe.config eClockView.exe.config.instalado
ECHO      Terminado eClockView
ECHO ON