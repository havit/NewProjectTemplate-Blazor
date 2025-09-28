param (
    [string]$Url = "https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.css",
    [string]$OutputFile1 = "Web.Client/wwwroot/css/bootstrap-intellisense-fakes.css",
    [string]$OutputFile2 = "Web.Server/wwwroot/bootstrap-intellisense-fakes.css"
)

Write-Host "Downloading CSS from $Url..."
try {
    $css = Invoke-WebRequest -Uri $Url -UseBasicParsing | Select-Object -ExpandProperty Content
} catch {
    Write-Error "Failed to download CSS from $Url"
    exit 1
}

Write-Host "Searching for CSS classes..."
# Regex: .className { ... } nebo .className:state
$matches = Select-String -InputObject $css -Pattern '\.([a-zA-Z_-][a-zA-Z0-9_-]*)' -AllMatches

$classes = @()
foreach ($m in $matches.Matches) {
    $classes += $m.Groups[1].Value
}

# Unikátní, seřazené
$uniqueClasses = $classes | Sort-Object -Unique

Write-Host "Found $($uniqueClasses.Count) classes. Generating fake CSS..."

# Skeleton CSS
$content = foreach ($cls in $uniqueClasses) {
    ".$cls {}"
}

Set-Content -Path $OutputFile1 -Value $content -Encoding UTF8
Set-Content -Path $OutputFile2 -Value $content -Encoding UTF8

Write-Host "Done! Created file: $OutputFile1"
Write-Host "Done! Created file: $OutputFile2"
