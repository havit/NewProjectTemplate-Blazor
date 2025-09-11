# Copilot Instructions for HAVIT Blazor Project Template

This repository contains a project template for building modern web applications using the HAVIT Blazor Stack. When working with this template or projects derived from it, follow these guidelines to ensure consistency and best practices.

## Project Structure and Architecture

This solution follows a clean architecture pattern with the following key projects:

- **Web.Server**: Main ASP.NET Core Blazor Server application
- **Web.Client**: Blazor WebAssembly client components
- **Model**: Domain entities and business objects
- **Entity**: Entity Framework Core DbContext and configurations
- **DataLayer**: Repositories, data sources, and data access patterns
- **Services**: Business logic and domain services
- **Facades**: API facades and application services
- **Contracts**: DTOs and service contracts
- **DependencyInjection**: Service registration and configuration
- **JobsRunner**: Background job processing with Hangfire

## Technology Stack

- **.NET 9**: Target framework
- **Blazor Server/WebAssembly**: UI framework
- **Entity Framework Core 9**: Data access with HAVIT patterns
- **HAVIT Framework**: Enterprise patterns and components
- **Hangfire**: Background job processing
- **MSTest**: Unit testing framework
- **Bootstrap**: CSS framework via Havit.Blazor.Components.Web.Bootstrap

## Coding Guidelines

### Naming Conventions

- Use `PascalCase` for classes, methods, properties, and public members
- Use `camelCase` for private fields and local variables
- Prefix private fields with underscore: `_privateField`
- Use descriptive names that clearly indicate purpose
- Entity classes should be singular (e.g., `User`, not `Users`)
- Repository interfaces should end with `Repository` (e.g., `IUserRepository`)

### Entity Framework Patterns

- Use HAVIT's repository pattern via generated repositories
- Leverage code generation for repositories and data sources
- Follow the Unit of Work pattern for transactions
- Use `IDbContext` interface for dependency injection
- Implement data seeds using `IDataSeed` interface

Example service registration:
```csharp
services
    .AddDbContext<IDbContext, YourProjectDbContext>(optionsBuilder =>
    {
        string connectionString = configuration.GetConnectionString("Database");
        optionsBuilder.UseSqlServer(connectionString, c => c.MaxBatchSize(30));
        optionsBuilder.UseDefaultHavitConventions();
    })
    .AddDataLayerServices()
    .AddDataSeeds(typeof(CoreProfile).Assembly)
    .AddLocalizationServices<Language>();
```

### Business Logic Organization

- Place business logic in the `Services` layer
- Use `Facades` for coordinating multiple services
- Implement DTOs in the `Contracts` project
- Use dependency injection for all service dependencies
- Follow SOLID principles

### Error Handling

- Use structured logging with dependency injection
- Implement proper exception handling at service boundaries
- Use custom exceptions for business rule violations
- Log errors with appropriate context and severity levels

### Testing Patterns

- Write unit tests for all business logic
- Use integration tests for data access patterns
- Mock external dependencies in unit tests
- Use `TestHelpers` project for shared testing utilities
- Follow AAA pattern (Arrange, Act, Assert)

### Blazor Component Guidelines

- Use server-side rendering for better performance
- Implement proper component lifecycle management
- Use `@inject` for dependency injection in components
- Follow component naming conventions (PascalCase)
- Separate markup and code-behind when components become complex

## Template Usage Guidelines

### Initial Setup

When using this template for a new project:

1. Run `SetupSolution.ps1` with appropriate parameters
2. Update all namespace references
3. Remove unnecessary entities (Country, Localizations if not needed)
4. Run the DataLayer code generator
5. Create initial EF migration
6. Configure connection strings and app settings

### Code Generation

- Use `Run-CodeGenerator.ps1` to regenerate data access code
- Never modify generated files directly
- Extend generated classes using partial classes
- Regenerate after model changes

### Configuration Management

- Use structured configuration with `appsettings.json` hierarchy
- Environment-specific settings in separate files
- Secure sensitive data using Azure Key Vault or user secrets
- Use strongly-typed configuration classes

## Best Practices

### Performance

- Use async/await patterns consistently
- Implement proper caching strategies
- Optimize database queries and use appropriate batch sizes
- Use SignalR for real-time features when needed

### Security

- Implement proper authentication and authorization
- Use policy-based authorization
- Validate all user inputs
- Follow OWASP security guidelines
- Secure API endpoints appropriately

### Maintainability

- Keep methods small and focused
- Use meaningful comments for complex business logic
- Implement proper logging throughout the application
- Follow dependency injection patterns
- Write self-documenting code with clear naming

### Migration and Upgrades

- Follow Entity Framework migration best practices
- Test migrations in development before production
- Keep track of breaking changes in framework updates
- Use feature flags for gradual rollouts

## Common Patterns

### Repository Pattern
```csharp
public class UserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User> GetUserAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
}
```

### Service Registration
```csharp
services.AddScoped<IUserService, UserService>();
```

### Component Dependency Injection
```razor
@inject IUserService UserService

<h3>User Management</h3>
```

## Troubleshooting

### Common Issues

- **Build errors after model changes**: Run code generator and rebuild
- **Migration issues**: Check connection string and ensure database accessibility
- **DI registration errors**: Verify service registration order and dependencies
- **Blazor component errors**: Check component lifecycle and state management

When suggesting code changes or improvements, always consider:
- Consistency with existing patterns in the solution
- Performance implications
- Testability of the proposed solution
- Security considerations
- Maintainability and readability

Remember that this is a template project, so suggestions should be applicable to derived projects and follow the established architectural patterns.