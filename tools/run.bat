@echo Off
dotnet tool install --tool-path %~dp0 --version 0.28.0 dotnet-script
CALL %~dp0\dotnet-script.exe %~dp0\build.csx -- %*
if errorlevel 1 (
   exit /b %errorlevel%
)