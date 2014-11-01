ECHO Publicando eClock View
del d:\Pub\eClockView\*.* /Q /S
xcopy /E /F ..\Binario\eClockView d:\Pub\eClockView

ECHO Publicando eEnroler
del d:\Pub\eEnroler\*.* /Q /S
xcopy /E /F ..\Binario\eEnroler d:\Pub\eEnroler
ECHO Haciendo Comit
TortoiseProc.exe /command:commit /path:"d:\Pub"

