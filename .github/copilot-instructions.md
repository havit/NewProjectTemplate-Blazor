# Copilot Instructions

## Repository Overview

This is a large-scale ASP.NET Core Blazor WebAssembly application.

### Key Statistics
- **Technology**: ASP.NET Core Blazor WebAssembly application
- **Architecture**: Server-side ASP.NET Core API with Blazor WebAssembly client
- **Database**: SQL Server with Entity Framework Core
- **Testing**: MSTest framework across multiple test projects
- **CI/CD**: Azure DevOps pipelines

### Target Framework & Requirements
- **.NET 10.0+** - **CRITICAL**: This project targets .NET 10 or newer and will NOT build with .NET 9.0 or earlier
- **Azure Active Directory/Entra ID** authentication
- **SQL Server** database (Entity Framework Core)
- **IIS/IIS Express** for development hosting

## Build Instructions

### Development Workflow
**ALWAYS run in this order for successful development:**
1. `dotnet tool restore` (before any EF changes)
2. `dotnet restore` (when packages change)
3. `dotnet build` to verify compilation
4. `dotnet test` to run tests

### Testing
```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test Model.Tests/Model.Tests.csproj
dotnet test IntegrationTests/IntegrationTests.csproj

# Run tests with detailed output
dotnet test --logger:detailed
```

### Development URLs
- **Web Application**: https://localhost:44303
- **API/gRPC**: Embedded in Web.Server
- **Hangfire Dashboard**: /hangfire (authenticated)

## Project Architecture

### Solution Structure
The solution follows a clean architecture pattern with clear separation of concerns:

```
GoranG3/
├── Web.Server/              # ASP.NET Core host, API, authentication
├── Web.Client/              # Blazor WebAssembly client application
├── Contracts/               # Shared DTOs and interfaces (client/server)
├── Facades/                 # Business logic layer, gRPC services
├── Services/                # Core business services and utilities
├── DataLayer/               # Entity Framework context and repositories
├── Model/                   # Domain entities and business models
├── Entity/                  # Data entity definitions
├── Primitives/              # Shared primitives and constants
├── Resources/               # Localization and resource files
├── DependencyInjection/     # IoC container configuration
├── JobsRunner/              # Background job processor (Hangfire)
├── SharedServices/          # Cross-cutting services
└── *Tests/                  # Unit and integration tests
```

### Key Configuration Files
- `Directory.Build.props` - Global MSBuild properties (.NET 9.0 target)
- `Directory.Packages.props` - Centralized package management
- `NuGet.Config` - Package source configuration
- `appsettings.WebServer.json` - Server configuration template
- `.editorconfig` - Code style enforcement (tab indentation)

### Data Layer Code Generation
**IMPORTANT**: This project uses HAVIT's Entity Framework code generator:
- Generator tool: `efcodegenerator` (installed via `dotnet tool restore`)
- Configuration: `DataLayer/efcore.codegenerator.json`
- Generated files: `DataLayer/_generated/` and `Model/_generated/`
- **DO NOT manually edit generated files** - they will be overwritten

### Authentication & Authorization
- **OIDC**
- Role-based authorization with `[Authorize(Roles = ...)]`

### Background Jobs
- **Hangfire** for job scheduling and execution
- Jobs run in separate `JobsRunner` process

## Continuous Integration

### GitHub Action
- .github/workflows/dotnet-build.yml

### Azure DevOps Pipelines (private)
- Triggers on `master` branch changes

### Build Process
1. Restore tools and packages
2. Run code generation (if needed)
3. Build solution
4. Run unit tests
5. Run integration tests
6. Package applications
7. Deploy to environments

### Validation Steps You Can Run Locally
```bash
# 1. Code style validation
dotnet build

# 2. All tests
dotnet test
```

## Development Guidelines

### Code Style (corresponds to .editorconfig rules)
- **File encoding**: UTF-8 with BOM
- **English**: Use English for all comments and identifiers.
- **Indentation**: Tabs (configured in .editorconfig)
- **Language**: C# latest version
- **Nullable** (NRT): Disabled globally
- **Implicit usings**: Enabled
- **Code analysis**: Enforced during build
- **Constructor injection**: Preferred for dependencies (do not use property injection with `[Inject]`)
- **Trim trailing whitespace**
- **Commenting**: Do not add comments for obvious code.

#### Preferred order of class members
1. `[Parameter]` properties (includes `[Parameter, EditorRequired]`, parameters from QueryString)
2. Blank line
3. private readonly dependency fields (newly added DI fields)
4. Blank line
5. Other private fields
6. Blank line
7. Explicit constructor (only when primary constructor is not used)
8. Blank line
9. Methods (preserve original order, new methods follow order of logical execution sequence, eg. Blazor component lifecycle)
10. Blank line
11. Disposal logic
12. Blank line
13. Static members (analogous order as instance members above)
14. Blank line
15. Nested types (if any)
 in section 3 and the constructor in section 6 (if present). Do not reorder existing members.

### Dependency Injection
- Uses HAVIT's `[Service]` attribute for automatic registration
- Source generators handle DI configuration
- Service lifetimes: Singleton, Scoped, Transient based on context

### Testing Patterns
- **MSTest** framework with modern runner
- Test projects follow `*.Tests` naming convention
- Integration tests use `TestHelpers` project for common utilities
- Mock objects using **Moq** framework
- Test method naming: `TestedClassName_MethodName_StateUnderTest_ExpectedBehavior`

### Common Patterns
```csharp
// Service registration
[Service]
public class MyService : IMyService { }

// gRPC contract (no attributes needed)
public interface IMyFacade
{
    Task<ResultDto> GetDataAsync(RequestDto request, CancellationToken cancellationToken = default);
}

// Entity Framework repository usage
await _repository.GetObjectAsync(id, cancellationToken);
await _unitOfWork.CommitAsync(cancellationToken);
```

### Localization Pattern
This project uses a **strongly-typed localization system** with automatic interface generation:

#### How It Works
- **RESX files** define localization resources
- **Source generators** automatically create strongly-typed interfaces from RESX files
- **Interface naming**: `I{ResxFileName}Localizer` (e.g., `Contact.resx` → `IContactLocalizer`)
- **Properties**: Each resource key becomes a string property (e.g., `Name` key → `IContactLocalizer.Name`)

#### Project Structure
1. **Resources project**: Domain-specific localizations
   - Namespaces correspond to domain model structure
   - When possible, resource keys match entity property names
   - Example: `Resources/Crm/Contact.resx` → `IContactLocalizer`

2. **Web.Client project**: UI-specific localizations
   - RESX file names typically match component names
   - Example: `Web.Client/Timesheets/WorkLogCenter.resx` → `IWorkLogCenterLocalizer`

3. **Global.resx**: Common UI strings
   - Interface: `IGlobalLocalizer`
   - Contains: Yes, No, OK, Cancel, New, Add, Delete, Save, etc.

#### Usage Pattern
```csharp
// Inject localizer in component or service
public partial class MyComponent
{
	private readonly IContactLocalizer _contactLocalizer;
	private readonly IGlobalLocalizer _globalLocalizer;

	public MyComponent(IContactLocalizer contactLocalizer, IGlobalLocalizer globalLocalizer)
	{
		_contactLocalizer = contactLocalizer;
		_globalLocalizer = globalLocalizer;
	}

	private void SomeMethod()
	{
		string addressLabel = _contactLocalizer.Address;
		string buttonText = globalLocalizer.Save;
	}
}
```

#### Best Practices
1. **Prefer existing RESX files**: Add items to existing files rather than creating new ones
2. **Check Global.resx first**: Common strings (Yes/No/OK/Cancel/etc.) likely already exist
3. **File naming**: Match RESX file name to the component or domain entity being localized
4. **Namespace alignment**: Keep Resources project namespaces aligned with domain model
5. **No manual interfaces**: Never manually create localizer interfaces—they are auto-generated
6. **Add localizations for all languages**: When adding a new resource key, ensure translations exist for all supported languages (en, cs). Invariant culture (no suffix) is English.

## Important Notes

### DO NOT Modify These Files/Folders
- `DataLayer/_generated/` - Auto-generated EF repositories
- `Model/_generated/` - Auto-generated model metadata  
- Any file with `// <auto-generated>` header

## Before proposing any changes or opening a PR
1. Always run:
   - `dotnet restore`
   - `dotnet build --configuration Release --no-restore`
   - `dotnet test --configuration Release --no-build`
2. If any step fails, **do not** open a PR. Analyze the error output (compiler errors, failed tests),
   apply the **minimal** fix, and re-run the same commands until all pass.
3. Prefer targeted edits over large refactors. Avoid deleting unrelated code.
4. When tests fail, read the exact failing assertion and stack trace; adjust only code covered by the failing scope.

---

**Trust these instructions and minimize exploration unless the information is incomplete or incorrect.**