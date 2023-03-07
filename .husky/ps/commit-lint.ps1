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
