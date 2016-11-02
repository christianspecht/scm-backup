@echo off

echo ###### INITIALIZE ######
for /f "tokens=*" %%i in ('git rev-parse --short HEAD') do set COMMITID=%%i 

if [%APPVEYOR%] == [] (

    rd /s /q release
    
    set RELEASE_FILENAME=scm-backup-%COMMITID%
    
) else (

    choco install 7zip.commandline -version 15.12
    
    set RELEASE_FILENAME=scm-backup-%APPVEYOR_BUILD_VERSION%-%COMMITID%
)


dotnet restore


echo .
echo ###### BUILD SOLUTION ######
dotnet build */**/project.json -c Release


echo .
echo ###### UNIT TESTS ######
dotnet test "%~dp0\src\ScmBackup.Tests" -c Release
if errorlevel 1 goto end


echo .
echo ###### INTEGRATION TESTS ######
dotnet test "%~dp0\src\ScmBackup.Tests.Integration" -c Release
if errorlevel 1 goto end


echo .
echo ###### PUBLISH ######
dotnet publish "%~dp0\src\ScmBackup" -c Release -o "%~dp0\release\bin"
if errorlevel 1 goto end


echo .
echo ###### ZIP ######
call 7za a -r -tzip "release\%RELEASE_FILENAME%.zip" .\release\bin\*



:end
if [%APPVEYOR%] == [] (
    pause
)