@echo off

echo ###### INITIALIZE ######
for /f "tokens=*" %%i in ('git rev-parse --short HEAD') do set COMMITID=%%i

if [%APPVEYOR%] == [] (

    if exist "%~dp0\environment-variables.bat" (
        call "%~dp0\environment-variables.bat"
    ) else (
        echo environment-variables.bat is missing. Build will be canceled!
        goto end
    )

    rd /s /q release
    
    set RELEASE_FILENAME=scm-backup-%COMMITID%
    
) else (
    
    set RELEASE_FILENAME=scm-backup-%APPVEYOR_BUILD_VERSION%-%COMMITID%
)


dotnet restore


echo .
echo ###### BUILD SOLUTION ######
dotnet build -c Release
if errorlevel 1 goto end


echo .
echo ###### UNIT TESTS ######
dotnet test "%~dp0\src\ScmBackup.Tests\ScmBackup.Tests.csproj" -c Release
if errorlevel 1 goto end


echo .
echo ###### INTEGRATION TESTS ######
dotnet test "%~dp0\src\ScmBackup.Tests.Integration\ScmBackup.Tests.Integration.csproj" -c Release
if errorlevel 1 goto end
md "%~dp0\release"
copy "%~dp0\src\ScmBackup.Tests.Integration\bin\Release\netcoreapp1.1\*.log" "%~dp0\release\test.log" /y


echo .
echo ###### PUBLISH ######
dotnet publish "%~dp0\src\ScmBackup" -c Release -o "%~dp0\release\bin"
if errorlevel 1 goto end


echo .
echo ###### ZIP ######
call 7z a -r "release\%RELEASE_FILENAME%.zip" .\release\bin\*



:end
if [%APPVEYOR%] == [] (
    pause
)