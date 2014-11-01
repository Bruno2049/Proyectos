ECHO OFF
ECHO Preparando eClockPubAct
del eClockPubAct /Q
del eClockPubAct\Imagenes /Q
del eClockPubAct\BIN /Q
del eClockPubAct\Scripts /Q
del eClockPubAct\Scripts\windowfiles /Q
del eClockPubAct\Imagenes /Q
del eClockPubAct\Imagenes.Main /Q
del eClockPubAct\Imagenes.Main\IconosMenu /Q
del eClockPubAct\Imagenes.Main\IconosPagina /Q
ECHO Copiando eClockPubAct
xcopy eClockPub\*.* eClockPubAct /Y /Q
xcopy eClockPub\BIN\*.* eClockPubAct\BIN /Y /Q
ECHO Comprimiendo CIsLog
del eClockPubAct\CIsLog.txt /Q
del eClockPubAct\web.config /Q
del eClockAct.zip /Q
"c:\Program Files\7-Zip\7z.exe" a -r -tzip eClockAct.zip eClockPubAct\
ECHO ON