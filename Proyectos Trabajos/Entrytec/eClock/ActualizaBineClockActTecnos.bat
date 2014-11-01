ECHO OFF
ECHO ActualizaBineClockAct.bat
ECHO Copiando eClockPubAct
ECHO ON
xcopy eClockPubAct\*.* \\190.190.1.64\c$\Inetpub\wwwroot\eClockWeb /Y /Q
xcopy eClockPubAct\BIN\*.* \\190.190.1.64\c$\Inetpub\wwwroot\eClockWeb\BIN /Y /Q
