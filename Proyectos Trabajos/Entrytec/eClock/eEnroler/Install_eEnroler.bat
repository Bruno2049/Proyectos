
copy eEnroler.Config.xml eEnroler.exe.config /Y 
SETLOCAL
SET Computer=%ComputerName%
FOR /F "tokens=2 delims==" %%A IN ('WMIC /Node:%Computer% Path Win32_Processor Get AddressWidth /Format:list') DO SET OSB=%%A
ECHO %OSB%-bits

if %OSB%==64 GOTO Win64

c:\Windows\System32\regsvr32.exe  X32\zkemkeeper.dll
c:\Windows\System32\regsvr32.exe  X32\Biokey.ocx

GOTO FIN
:Win64
c:\Windows\System32\regsvr32.exe  X64\zkemkeeper.dll
c:\Windows\System32\regsvr32.exe  X64\Biokey.ocx

:FIN

mkdir "%ALLUSERSPROFILE%\Microsoft\Windows\Start Menu\Programs\eClock\"
mklink "%ALLUSERSPROFILE%\Microsoft\Windows\Start Menu\Programs\eClock\%1" "%CD%\eEnroler.exe"