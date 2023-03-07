dotnet new tool-manifest
dotnet tool install Husky
dotnet husky install
dotnet husky add commit-msg -c @"
dotnet husky run --name commit-message-linter --args "`$1"
"@
$huskyDir = "$PSScriptRoot\.husky"
$task = [PsCustomObject]@{
    name    = "commit-message-linter"
    command = "pwsh"
    args    = [string[]]@(
        "-nop",
        ".husky/ps/commit-lint.ps1",
        "`${args}"
    )
}
$taskRunnerJson = (Get-Content "$huskyDir\task-runner.json") -join "`n"
$taskRunnerObj = ConvertFrom-Json $taskRunnerJson -Depth 8
$taskRunnerObj.tasks += $task

$taskRunnerObj | ConvertTo-Json -Depth 8 | Out-File -FilePath "$huskyDir\task-runner.json" -Force

$commitLintFile = "$huskyDir\ps\commit-lint.ps1"

New-Item $commitLintFile -Force

@'
Write-Host "Args length: $($args.Count)"

foreach ($arg in $args) {
    $i = 1
    Write-Host "Arg {$i}: $arg"
}
$pattern = "^(?=.{1,90}$)(?:build|feat|ci|chore|docs|fix|perf|refactor|revert|style|test)(?:\(.+\))*(?::).{4,}(?:#\d+)*(?<![\.\s])$"

if (Test-Path $args[0]) {
    $msg = Get-Content $args[0]
}
if ($msg -is [array]) {
    $msg = $msg[0]
}

if ([System.Text.RegularExpressions.Regex]::IsMatch($msg, $pattern)) {
    Exit 0
}

Write-Host "Invalid commit message" -ForegroundColor Red
Write-Host "e.g: 'feat(scope): subject' or 'fix: subject'"
Write-Host "more info: https://www.conventionalcommits.org/en/v1.0.0/"

Exit 1
'@ | Out-File $commitLintFile -Force
