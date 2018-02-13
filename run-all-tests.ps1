if (Test-Path -Path $PSScriptRoot\environment-variables.ps1) {
    & $PSScriptRoot\environment-variables.ps1
}
else {
    Write-Host 'environment-variables.ps1 is missing. Some of the integration tests will fail!'
    Write-Host 'Press ENTER to continue'
    Read-Host
}



''
Write-Host '###### DELETING OLD TEMP FOLDERS ######'
$temppath = "$env:TEMP\_scm-backup-tests\"
if (Test-Path -Path $temppath) {
    Get-ChildItem -Path $temppath -Recurse| Foreach-object {Remove-item -Recurse -Force -path $_.FullName }
}



''
Write-Host '###### UNIT TESTS ######'
dotnet test "$PSScriptRoot\src\ScmBackup.Tests\ScmBackup.Tests.csproj" -c Release
if ($LASTEXITCODE -eq 1) {
    exit
}


''
Write-Host '###### INTEGRATION TESTS ######'
dotnet test "$PSScriptRoot\src\ScmBackup.Tests.Integration\ScmBackup.Tests.Integration.csproj" -c Release
if ($LASTEXITCODE -eq 1) {
    exit
}

