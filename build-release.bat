@echo off

dotnet restore

echo ###### UNIT TESTS ######
dotnet test "%~dp0\src\ScmBackup.Tests" -c Release
if errorlevel 1 goto end

echo .
echo ###### INTEGRATION TESTS ######
dotnet test "%~dp0\src\ScmBackup.Tests.Integration" -c Release
if errorlevel 1 goto end

echo .
echo ###### PUBLISH ######
dotnet publish "%~dp0\src\ScmBackup" -c Release -o "%~dp0\release"
if errorlevel 1 goto end

:end
IF [%APPVEYOR%] == [] (
    pause
)