@echo off

if exist "%~dp0\environment-variables.bat" (
    call "%~dp0\environment-variables.bat"
) else (
    echo environment-variables.bat is missing. Some of the integration tests will fail!
    pause
)

dotnet test "%~dp0\src\ScmBackup.Tests\ScmBackup.Tests.csproj"
dotnet test "%~dp0\src\ScmBackup.Tests.Integration\ScmBackup.Tests.Integration.csproj"

pause