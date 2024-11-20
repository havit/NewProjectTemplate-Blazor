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
    1. Remove connection string AzureKeyVault from appsettings.WebServer.Development.json (or update the endpoint).
1. Run the app...



## Further Steps
1. Update NuGet packages in solution.
1. Application Insights - configure connection string

(Use PublishScripts folder for deployment settings.)


# Upgrading existing project from net8 to net9

1. Replace the `<TargetFramework>net8.0</TargetFramework>` to `<TargetFramework>net9.0</TargetFramework>` in all `.csproj` files.
1. Update NuGet package references from 8.0.x to 9.0.x version + update other NuGet packages as needed (**for EF Core 9 upgrade see below**).
1. Build: Clean solution & Rebuild solution
1. Check the `TfsPublish.xml`. There might be explicit `net8.0` target, remove the line.
1. Update the `Web.Server.csproj` the `EnsureWebJobInPackage` target to use `net9.0` in paths.
1. Implement [Static Assets Middleware](https://learn.microsoft.com/en-us/aspnet/core/migration/80-90?view=aspnetcore-9.0&tabs=visual-studio#replace-usestaticfiles-with-mapstaticassets) (`MapStaticAssets` instead of `UseStaticFiles`, `App.razor` adjustments, custom versioning removal, ...)
1. Remove link to `_content/Havit.Blazor.Components.Web.Bootstrap/defaults.css`.
1. If you use it, upgrade your GitHub workflow YAML to use net9.


### EF Core 8 to EF Core 9 Migration Guide

1. Update HFW NuGet packages and Microsoft packages to EF Core 9.
1. Update the dotnet tool `Havit.Data.EntityFrameworkCore.CodeGenerator.Tool` (`dotnet tool update Havit.Data.EntityFrameworkCore.CodeGenerator.Tool`).
1. Build the Entity project (building the entire solution will fail initially).
1. Run the code generator.
1. Adjust Before Commit Processors (if any) to accommodate the new return values.
1. Update method overrides for `PerformAddForInsert/Update/Delete` in custom Unit of Work, if needed.
1. Modify the service registration in dependency injection:
    1. Remove the call to `WithEntityPatterns`,
    1. Remove the call to `AddEntityPatterns`,
    1. Add the generic parameter `IDbContext` to the `AddDbContext` call,
    1. Add `UseDefaultHavitConventions()` to the `optionsBuilder` in the `AddDbContext` call,
    1. Replace `AddDataLayer` with `AddDataLayerServices` (remove the assembly argument),
    1. Add a call to `AddDataSeeds` if data seeds are used.
1. Methods `AddLocalizationServices` and `AddLookupServices` remain unchanged.

#### Updated Service Registration Example
```csharp
services
    .AddDbContext<IDbContext, MyShopDbContext>(optionsBuilder =>
    {
        string databaseConnectionString = configuration.Configuration.GetConnectionString("Database");
        optionsBuilder.UseSqlServer(databaseConnectionString, c => c.MaxBatchSize(30));
        optionsBuilder.UseDefaultHavitConventions();
    })
    .AddDataLayerServices()
    .AddDataSeeds(typeof(CoreProfile).Assembly)
    .AddLocalizationServices<Language>();
```