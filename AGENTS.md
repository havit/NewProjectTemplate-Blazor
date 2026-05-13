# NewProjectTemplate-Blazor Coding Agents Instructions

## Repository Overview
**NewProjectTemplate-Blazor** is a project template for new ASP.NET Core Blazor WebAssembly applications built on the HAVIT Blazor stack.

> **HAVIT Stack conventions** (solution structure, DI with `[Service]`, Facade architecture, UoW & Commit ownership, `IDataLoader` data access, QueryCore() pattern, strongly-typed localization, MSTest/Moq testing, code style, class member ordering, …) are provided by the **`havit-blazor-stack` skill**. Use this skill for every session. This file documents only **NewProjectTemplate-specific** details on top of those baseline conventions.

## Build & Validation

Always run before proposing changes or opening a PR:
1. `dotnet tool restore` (before EF code generation changes)
2. `dotnet restore`
3. `dotnet build --no-restore`
4. `dotnet test --no-build`

If any step fails, **do not** open a PR — analyze errors, apply the minimal fix, and re-run. Build warnings must be resolved.