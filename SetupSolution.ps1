param (
    [string]$NewRootNamespace = "Havit",
	[string]$NewOrganizationName = "HAVIT",
    [string]$NewSolutionName = "YourSolutionName",
	[string]$NewHttpPort = "9901",
	[string]$NewHttpsPort = "44301",
	[string]$NewErrorsRecipient = "errors@mydomain.com", # HAVIT developers: use errors@havit.cz
	[string]$NewErrorsSmptServer = "errorssmtp.server.com" # HAVIT developers: use mx.havit.cz
)

[string]$SolutionFolder = [System.IO.Path]::GetDirectoryName($MyInvocation.MyCommand.Path);

Get-ChildItem -recurse $SolutionFolder -include *.cs,*.csproj,*.config,*.ps1,*.json,*.tsx,*.cshtml,*.props,*.razor,*.json,*.html,*.js | where { $_ -is [System.IO.FileInfo] } | where { !$_.FullName.Contains("\packages\") } | where { !$_.FullName.Contains("\obj\") } | where { !$_.FullName.Contains("package.json") } | where { !$_.Name.Equals("_SetApplicationName.ps1") } |
Foreach-Object {
    Set-ItemProperty $_.FullName -name IsReadOnly -value $false
    [string]$Content = [IO.File]::ReadAllText($_.FullName)
    $Content = $Content.Replace('Havit.NewProjectTemplate', $NewRootNamespace + '.' + $NewSolutionName)
    $Content = $Content.Replace('NewProjectTemplate', $NewSolutionName)
    $Content = $Content.Replace("HAVIT", $NewOrganizationName)
    $Content = $Content.Replace('9900', $NewHttpPort)
    $Content = $Content.Replace("44301", $NewHttpsPort)
    $Content = $Content.Replace("errors@mydomain.com", $NewErrorsRecipient)
    $Content = $Content.Replace("errorssmtp.server.com", $NewErrorsSmptServer)
    [IO.File]::WriteAllText($_.FullName, $Content, [System.Text.Encoding]::UTF8)
}

Rename-Item -path ([System.IO.Path]::Combine($SolutionFolder, 'NewProjectTemplate.sln')) -newName ($NewSolutionName + '.sln')
Rename-Item -path ([System.IO.Path]::Combine($SolutionFolder, 'Entity\NewProjectTemplateDbContext.cs')) -newName ($NewSolutionName + 'DbContext.cs')
Rename-Item -path ([System.IO.Path]::Combine($SolutionFolder, 'Entity\Migrations\NewProjectTemplateDbContextModelSnapshot.cs')) -newName ($NewSolutionName + 'DbContextModelSnapshot.cs')
Rename-Item -path ([System.IO.Path]::Combine($SolutionFolder, 'Entity\NewProjectTemplateDesignTimeDbContextFactory.cs')) -newName ($NewSolutionName + 'DesignTimeDbContextFactory.cs')
Rename-Item -path ([System.IO.Path]::Combine($SolutionFolder, 'Entity.Tests\NewProjectTemplateDbContextTests.cs')) -newName ($NewSolutionName + 'DbContextTests.cs')
Rename-Item -path ([System.IO.Path]::Combine($SolutionFolder, 'Services\HealthChecks\NewProjectTemplateDbContextHealthCheck.cs')) -newName ($NewSolutionName + 'DbContextHealthCheck.cs')	