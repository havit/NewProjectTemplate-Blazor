# New Project Template - HAVIT Blazor Stack

## Documentation
See the [documentation](./doc/README.md) (`/doc/README.md`).

## Clonning template contents into new folder
If you have a local repository   
`git checkout-index --prefix=git-export-dir/ -a`

Or create a new GitHub repository from template:  
https://github.com/havit/NewProjectTemplate-Blazor/generate

## Initial Setup
1. SetupSolution.ps1 (replaces `NewProjectTemplate` with `YourProjectName` etc.)
   1. Open SetupSolution.ps1 and set parameters.
   1. Run SetupSolution.ps1.
   1. Delete SetupSolution.ps1
1. Set Web.Server as the startup project.
1. Adjust the Model - remove unnecessary entities (Country, Localizations, ...)
1. Rebuild the solution
1. Run DataLayer CodeGenerator (Run-CodeGenerator.ps1)
1. Create an initial EF migration
   1. Drop the current migrations - delete Entity/Migrations folder
   1. Add new initial migration `Add-Migration Initial`
1. Check all configuration files (including PublishScripts folder).
1. Run the app...

## Further Steps
1. Update NuGet packages in solution.
1. Application Insights - configure connection string

(Use PublishScripts folder for deployment settings.)

# Upgrading existing project from net6 to net7
1. Replace the `<TargetFramework>net6.0</TargetFramework>` to `<TargetFramework>net7.0</TargetFramework>` in all `.csproj` files.
1. Update NuGet package references from 6.0.x to 7.0.x version (all except EF Core!) + update other NuGet packages as needed.
1. Build: Clean solution & Rebuild solution
1. Deal with `[Obsolete]` APIs:
    1. Replace `SignOutSessionStateManager` (`LoginDisplat.razor`) with `NavigationManager.NavigateToLogout()`, see [CS0618](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs0618)
1. Deal with *unsupported platform* APIs
    1. In `Web.Server/Program.cs`, update the `AddEventLog()` call:
    ```csharp
	if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
	{
		logging.AddEventLog();
	}
	```
1. Deal with new code analyzer warnings, e.g.
    1. BL0007: Component parameter 'XY' should be auto property
        1. use `@bind:after` where applicable
		1. use `#pragma warning disable` where needed
1. In `dotnet-tools.json` upgrade `Havit.Data.EntityFrameworkCore.CodeGenerator.Tool` to 2.7.0 version (net7) + try if the `DataLayer/Run-CodeGenerator.ps1` runs currectly
1. Check the `TfsPublish.xml`. There might be explicit `net6` target, update it to `net7`.
1. Update the `Web.Server.csproj` the `EnsureWebJobInPackage` target to use `net7.0` in paths.
1. If you use it, upgrade your GitHub workflow YAML to use net7.
1. If you are hitting the `"undefined" is not valid JSON` when logging in, disable assembly trimming for `Microsoft.AspNetCore.Components.WebAssembly.Authentication`, see https://github.com/dotnet/aspnetcore/issues/44981
1. Remove the Blazor GC gRPC workaround when using facades (revert the `Func<IXyFacade>` usage to direct `IXyFacade` usage))
