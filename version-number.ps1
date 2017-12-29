if ($env:APPVEYOR) {
    # we are on AppVeyor

    $commit = $env:APPVEYOR_REPO_COMMIT.Substring(0,7)

    if ($env:APPVEYOR_REPO_TAG -eq $true) {
        # we are building a tag -> we are doing a release -> use the tag as version number
        $shortversion = $env:APPVEYOR_REPO_TAG_NAME
        $longversion = $shortversion + '.' + $commit
    }
    else {
        # regular CI build, no release
        $shortversion = '0.0.0'
        $longversion = '0.0.0.CI-' + $env:APPVEYOR_BUILD_NUMBER + '-' + $commit
    }
}
else {

    # local build
    
    Set-Location $PSScriptRoot
    $commit = git rev-parse --short HEAD

    $shortversion = '0.0.0'
    $longversion = '0.0.0.DEV-' + $commit
}

$env:ScmBackupCommit=$commit
$env:ScmBackupShortVersion=$shortversion
$env:ScmBackupLongVersion=$longversion

Write-Host 'Commit: ' $env:ScmBackupCommit
Write-Host 'Short Version: ' $env:ScmBackupShortVersion
Write-Host 'Long Version: ' $env:ScmBackupLongVersion