# NewProjectTemplate Agent Instructions

## Repository Overview
**NewProjectTemplate** is a layered ASP.NET Core + Blazor WebAssembly application built on the HAVIT Stack — server API + WASM client, gRPC facades, EF Core on SQL Server, Microsoft Entra ID authentication, Hangfire `JobsRunner`.

> **HAVIT Stack conventions** (solution structure, DI with `[Service]`, Facade architecture, UoW & Commit ownership, `IDataLoader` data access, QueryCore() pattern, strongly-typed localization, MSTest/Moq testing, code style, class member ordering, …) are provided by the **`havit-blazor-stack` skill**. Use this skill for every session. This file documents only **template-specific** details on top of those baseline conventions.

## Build & Validation
Requires the **.NET 10 SDK** (`global.json` pins `10.0.0`, `rollForward: latestMajor`) — the solution does not build on .NET 9 or earlier.

During iteration, use the targeted build/test strategy from the `havit-blazor-stack` skill (build/test only the affected projects). As final verification, always run before opening a PR:
1. `dotnet tool restore` (before EF code generation changes)
2. `dotnet restore`
3. `dotnet build --no-restore --configuration Release`
4. `dotnet test --no-build --configuration Release`

Use the **Release** configuration for the final pass — `TreatWarningsAsErrors` is enabled only in Release (`Directory.Build.props`), so a Debug build does not catch warnings that will fail CI.

If any step fails, **do not** open a PR — analyze errors, apply the minimal fix, and re-run.

## Testing
Tests run on **Microsoft.Testing.Platform** with MSTest (`EnableMSTestRunner` + `TestingPlatformDotnetTestSupport` in `Directory.Build.props`; runner pinned in `global.json`). The VSTest-style filter works here: `dotnet test Model.Tests/Model.Tests.csproj --filter "FullyQualifiedName~MyTest"`. `IntegrationTests` require SQL Server LocalDB (`(localdb)\mssqllocaldb`).

## EF Core Migrations
Migrations live in `Entity/Migrations`. The design-time DbContext comes from `NewProjectTemplateDesignTimeDbContextFactory` in the `Entity` project (connection string from `Entity/appsettings.Entity.json`), so no `--startup-project` is needed:

```bash
dotnet ef migrations add MyMigration --project Entity
```

The same factory and configuration also serve the EF code generator (`efcodegenerator`, restored via `dotnet tool restore`).

## Local Development
- Web application: https://localhost:44301 (`Web.Server` is the startup project)
- Hangfire dashboard: `/hangfire`
- Local database: SQL Server LocalDB (`appsettings.WebServer.Development.json`)

## CI
`.github/workflows/build.yml` calls the local reusable workflow `./.github/workflows/_build-template.yml`, which delegates to the shared pipeline `havit-internal/002.HFW-PipelineTemplates` (`build-net.yml`): builds `NewProjectTemplate.slnx`, runs tests, and publishes `Web.Server` + `JobsRunner`.

## References
- `doc/Auth.md` — authentication & authorization setup
