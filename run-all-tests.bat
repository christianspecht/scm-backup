@echo off

if exist "%~dp0\environment-variables.bat" (
    call "%~dp0\environment-variables.bat"
)

dotnet test "%~dp0\src\ScmBackup.Tests"
dotnet test "%~dp0\src\ScmBackup.Tests.Integration"

pause