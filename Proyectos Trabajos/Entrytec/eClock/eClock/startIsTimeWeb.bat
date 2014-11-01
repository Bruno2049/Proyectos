@echo off
call C:\ARCHIV~1\MONO-1~1.18\bin\setmonopath.bat

xsp --root . --port 8088 --applications /:.
