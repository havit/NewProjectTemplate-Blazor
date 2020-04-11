$scriptPath = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition
$csprojPath = "DataLayer.csproj"

Write-Host "Looking for installed Havit.Data.EntityFrameworkCore.CodeGenerator version (in $csprojPath)"
[xml]$csprojContent = Get-Content -Path $csprojPath
$version = ($csprojContent.Project.ItemGroup.PackageReference | Where-Object { $_.Include -eq 'Havit.Data.EntityFrameworkCore.CodeGenerator' } | Select-Object Version).Version
if (-Not $version)
{
	Write-Host 'Havit.Data.EntityFrameworkCore.CodeGenerator not found.'
}
else
{
	Write-Host "Found version $version."
	$codeGenerator = "$env:UserProfile\.nuget\packages\Havit.Data.EntityFrameworkCore.CodeGenerator\$version\tools\CodeGenerator\Havit.Data.EntityFrameworkCore.CodeGenerator.dll"
	if ($codeGenerator)
	{
get-location
		Write-Host "Running code generator ($codeGenerator)"
		#Push-Location $scriptPath
		&dotnet $codeGenerator
		#Pop-Location
	}
}
