ECHO Publicando eClock Web
del d:\Pub\eClockWeb\*.* /Q /S
xcopy /E /F ..\Binario\eClock d:\Pub\eClockWeb
ECHO Haciendo Comit
TortoiseProc.exe /command:commit /path:"d:\Pub\eClockWeb"

