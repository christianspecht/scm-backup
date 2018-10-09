Write-Host '###### INITIALIZE ######'

if ($env:APPVEYOR) {

    # We are on AppVeyor
    # - environment variables are set in the AppVeyor settings
    # - version-number.ps1 was already called before AssemblyInfo patching

}
else {

    if (Test-Path -Path 'release') {
        Remove-Item -Recurse -Force 'release'
    }

    if (Test-Path -Path $PSScriptRoot\environment-variables.ps1) {
        & $PSScriptRoot\environment-variables.ps1
    }
    else {
        Write-Host 'environment-variables.ps1 is missing. Build will be canceled!'
        exit
    }

    & $PSScriptRoot\version-number.ps1
}

$release_filename = 'scm-backup-' + $env:ScmBackupLongVersion


dotnet restore


''
Write-Host '###### BUILD SOLUTION ######'
dotnet build -c Release
if ($LASTEXITCODE -eq 1) {
    throw
}


''
Write-Host '###### UNIT TESTS ######'
dotnet test "$PSScriptRoot\src\ScmBackup.Tests\ScmBackup.Tests.csproj" -c Release
if ($LASTEXITCODE -eq 1) {
    throw
}


''
Write-Host '###### INTEGRATION TESTS ######'
dotnet test "$PSScriptRoot\src\ScmBackup.Tests.Integration\ScmBackup.Tests.Integration.csproj" -c Release
if ($LASTEXITCODE -eq 1) {
    throw
}


''
Write-Host '###### PUBLISH ######'
dotnet publish "$PSScriptRoot\src\ScmBackup" -c Release -o "$PSScriptRoot\release\bin"
if ($LASTEXITCODE -eq 1) {
    throw
}


''
Write-Host '###### ZIP ######'
7z a -r "release\$release_filename.zip" .\release\bin\*
