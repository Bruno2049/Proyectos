ECHO OFF
ECHO      Procesando IsTimeSync
ECHO      Borrando binarios actuales
del ..\Binario\IsTimeSync\*.* /Q
ECHO      copiando nuevos archivos
copy "IsTimeSync2\bin\Release Autoexit\"*.* ..\Binario\IsTimeSync\ /Y
ECHO      limpiando archivos no servibles
del ..\Binario\IsTimeSync\IsProtectServer.* /Q
rem del ..\Binario\IsTimeSync\IsTimeSync.* /Q
del ..\Binario\IsTimeSync\IsTimeSync2.pdb /Q
del ..\Binario\IsTimeSync\IsTimeSync2IPS.* /Q
del ..\Binario\IsTimeSync\*.vshost.* /Q
del ..\Binario\IsTimeSync\log.* /Q
del ..\Binario\IsTimeSync\*.log /Q
rem ILMerge /lib:"C:\Windows\Microsoft.NET\Framework\v2.0.50727" /lib:"C:\Archivos de programa\Infragistics\NetAdvantage for .NET 2007 Volume 1 CLR 2.0\windows forms\bin" /lib:".." /log:log.txt /closed /target:winexe /out:ISCardPro.exe ISCard.exe IsProtectServer.exe
rem copy "IsTimeSync2\bin\Release Autoexit\"IsTimeSync.exe IsTimeSync.exe /Y
ECHO      Actualizando IsProtect tomandolo de la ruta Binario
copy IsProtectServer.exe "IsTimeSync2\bin\Release Autoexit\"IsProtectServer.exe /Y
copy IsProtectServer.pdb "IsTimeSync2\bin\Release Autoexit\"IsProtectServer.pdb /Y
ECHO      Agregando IsProtect
cd "IsTimeSync2\bin\Release Autoexit" 
..\..\..\ILMerge /lib:"C:\Windows\Microsoft.NET\Framework\v2.0.50727"  /lib:".." /log:log.txt /closed /target:winexe /out:IsTimeSync2IPS.exe IsTimeSync2.exe IsProtectServer.exe
cd ..\..\..
copy "IsTimeSync2\bin\Release Autoexit\"IsTimeSync2IPS.exe ..\Binario\IsTimeSync\IsTimeSync2.exe /Y
ECHO      Terminado IsTimeSync
ECHO ON