@echo off

dotnet test "%~dp0\src\ScmBackup.Tests"
dotnet test "%~dp0\src\ScmBackup.Tests.Integration"

pause