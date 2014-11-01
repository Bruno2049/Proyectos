
ECHO Copiando setups
xcopy eClock5\KioscoSetup\KioscoSetup\Express\SingleImage\DiskImages\DISK1\setup.exe ..\Binario\Instalables\ /Y 
del ..\Binario\Instalables\KioscoSetup.exe /Q
rename ..\Binario\Instalables\setup.exe KioscoSetup.exe
xcopy eClock5\eClock5Setup\eClock5Setup\Express\SingleImage\DiskImages\DISK1\setup.exe ..\Binario\Instalables\ /Y 
del ..\Binario\Instalables\eClock5Setup.exe /Q
rename ..\Binario\Instalables\setup.exe eClock5Setup.exe

ECHO Haciendo Comit
TortoiseProc.exe /command:commit /path:"..\Binario\Instalables"