@echo off

cd "%~dp0\src\ScmBackup.Tests"
dnx test
cd "%~dp0\src\ScmBackup.Tests.Integration"
dnx test

echo .
pause