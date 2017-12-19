@echo off

if exist "%~dp0\environment-variables.bat" (
    call "%~dp0\environment-variables.bat"
) else (
    echo environment-variables.bat is missing. Some of the integration tests will fail!
    pause
)

echo Deleting old temp folders...
for /d %%a in (%temp%\_scm-backup-tests\*.*) do rd /s /q "%%a"

dotnet test "%~dp0\src\ScmBackup.Tests\ScmBackup.Tests.csproj"
dotnet test "%~dp0\src\ScmBackup.Tests.Integration\ScmBackup.Tests.Integration.csproj"

pause