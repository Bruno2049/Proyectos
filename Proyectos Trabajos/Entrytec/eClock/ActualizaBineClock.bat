ECHO OFF
ECHO Borrando Archivos de eClockPubBin
del eClockPubBin /Q
del eClockPubBin\Imagenes /Q
del eClockPubBin\BIN /Q
del eClockPubBin\Scripts /Q
del eClockPubBin\Scripts\windowfiles /Q
del eClockPubBin\Imagenes /Q
del eClockPubBin\Imagenes.Main /Q
del eClockPubBin\Imagenes.Main\IconosMenu /Q
del eClockPubBin\Imagenes.Main\IconosPagina /Q

ECHO Creando carpetas en eClockPubBin
mkdir eClockPubBin 
mkdir eClockPubBin\BIN
mkdir eClockPubBin\Scripts
mkdir eClockPubBin\Scripts\windowfiles 
mkdir eClockPubBin\Imagenes
mkdir eClockPubBin\Imagenes.Main
mkdir eClockPubBin\Imagenes.Main\IconosMenu
mkdir eClockPubBin\Imagenes.Main\IconosPagina
mkdir eClockPubBin\skins


ECHO Copiando Archivos
xcopy eClockPub\*.* eClockPubBin /Y /Q
xcopy eClockPub\BIN\*.* eClockPubBin\BIN /Y /Q
xcopy eClockPub\Scripts\*.* eClockPubBin\Scripts /Y /Q
xcopy eClockPub\Scripts\windowfiles\*.* eClockPubBin\Scripts\windowfiles\*.* /Y /Q
xcopy eClockPub\Imagenes\*.* eClockPubBin\Imagenes /Y /Q
xcopy eClockPub\Imagenes.Main\*.* eClockPubBin\Imagenes.Main /Y /Q
xcopy eClockPub\Imagenes.Main\IconosMenu\*.* eClockPubBin\Imagenes.Main\IconosMenu /Y /Q
xcopy eClockPub\Imagenes.Main\IconosPagina\*.* eClockPubBin\Imagenes.Main\IconosPagina /Y /Q
xcopy eClockPub\Imagenes.Main\IconosPagina\*.* eClockPubBin\Imagenes.Main\IconosPagina /Y /Q
xcopy eClockPub\skins\*.* eClockPubBin\skins /Y /Q

ECHO Preparando destino eClock
del ..\Binario\eClock\*.* /Q
del ..\Binario\eClock\bin\*.* /Q
del ..\Binario\eClock\Imagenes\*.* /Q
del ..\Binario\eClock\App_Data\*.* /Q
del ..\Binario\eClock\Scripts\*.* /Q
del ..\Binario\eClock\skins\*.* /Q


ECHO Creando carpetas en eClock
mkdir ..\Binario\eClock
mkdir ..\Binario\eClock\BIN
mkdir ..\Binario\eClock\Scripts
mkdir ..\Binario\eClock\Imagenes
mkdir ..\Binario\eClock\Imagenes\Iconos
mkdir ..\Binario\eClock\Imagenes.Main
mkdir ..\Binario\eClock\Imagenes.Main\IconosMenu
mkdir ..\Binario\eClock\Imagenes.Main\IconosPagina
mkdir ..\Binario\eClock\skins
mkdir ..\Binario\eClock\PB

ECHO Creando carpetas Vacias en eClock
mkdir ..\Binario\eClock\PDF
mkdir ..\Binario\eClock\ChartImages
mkdir ..\Binario\eClock\GaugeImages


ECHO Preparando dlls Infragistics
xcopy "C:\Program Files (x86)\Infragistics\NetAdvantage 2011.1\ASP.NET\CLR2.0\Bin\*.dll" eClockPubBin\BIN /Y /Q
ECHO Scripts
xcopy "C:\Program Files (x86)\Infragistics\NetAdvantage 2011.1\ASP.NET\CLR2.0\Scripts\*.*" eClockPubBin\Scripts /Y /Q


ECHO Copiando a Binario
xcopy eClockPub\*.* ..\Binario\eClock /Y /Q
xcopy eClockPub\BIN\*.* ..\Binario\eClock\BIN /Y /Q
xcopy eClockPub\Scripts\*.* ..\Binario\eClock\Scripts /Y /Q
xcopy eClockPub\Imagenes\*.* ..\Binario\eClock\Imagenes /Y /Q
xcopy eClockPub\Imagenes\Iconos\*.* ..\Binario\eClock\Imagenes\Iconos /Y /Q
xcopy eClockPub\Imagenes.Main\*.* ..\Binario\eClock\Imagenes.Main /Y /Q
xcopy eClockPub\Imagenes.Main\IconosMenu\*.* ..\Binario\eClock\Imagenes.Main\IconosMenu /Y /Q
xcopy eClockPub\Imagenes.Main\IconosPagina\*.* ..\Binario\eClock\Imagenes.Main\IconosPagina /Y /Q
xcopy eClockPub\skins\*.* ..\Binario\eClock\skins /Y /Q
xcopy eClockPub\PB\*.* ..\Binario\eClock\PB /Y /Q

ECHO Preparando dlls Infragisticsa Binario
xcopy "C:\Program Files (x86)\Infragistics\NetAdvantage 2011.1\ASP.NET\CLR2.0\Bin\*.dll" ..\Binario\eClock\BIN /Y /Q
ECHO Scripts a Binario
xcopy "C:\Program Files (x86)\Infragistics\NetAdvantage 2011.1\ASP.NET\CLR2.0\Scripts\*.*" ..\Binario\eClock\Scripts /Y /Q



ECHO Limpiando Binario
del ..\Binario\eClock\PDF\*.* /Q
del ..\Binario\eClock\XLS\*.* /Q
del ..\Binario\eClock\*.log /Q
del ..\Binario\eClock\*.config /Q
del ..\Binario\eClock\*.fla /Q
del ..\Binario\eClock\CIsLog.txt /Q
del ..\Binario\eClock\config.xml /Q
del ..\Binario\eClock\ItwAct /Q
del ..\Binario\eClock\ItwLlave /Q
del ..\Binario\eClock\*.dll  /Q
del ..\Binario\eClock\*.exe  /Q
del ..\Binario\eClock\ankh.*  /Q
del ..\Binario\eClock\app_offline.*  /Q
del ..\Binario\eClock\*.bat  /Q
del ..\Binario\eClock\*.webinfo /Q
del ..\Binario\eClock\*.pdb /Q
del ..\Binario\eClock\bin\*.pdb /Q



ECHO Haciendo Comit
"C:\Program Files\TortoiseSVN\bin\TortoiseProc.exe" /command:commit /path:"..\Binario\eClock"

ECHO ON