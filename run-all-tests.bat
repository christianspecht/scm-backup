@echo off

cd "%~dp0\src\ScmBackup.Tests"
dotnet test
cd "%~dp0\src\ScmBackup.Tests.Integration"
dotnet test

echo .
pause