# Phase 1 – Solution Setup

> **Status:** ✅ Complete – this phase is already implemented.
> This guide explains the decisions made so you understand the foundation.

## What Was Done

### 1.1 Solution & Projects Created

```
AudioDeliveryWebApi.sln
src/
├── AudioDelivery.Api/            (.NET 9 Web API – webapi template)
├── AudioDelivery.Application/    (.NET 9 Class Library)
├── AudioDelivery.Domain/         (.NET 9 Class Library)
└── AudioDelivery.Infrastructure/ (.NET 9 Class Library)
```

**Why four projects?**

This follows **Clean Architecture** (also called Onion Architecture):

- **Domain** – Pure C# classes. No framework dependencies. Contains entities and enums that represent your business concepts.
- **Application** – Business logic layer. Contains service interfaces, DTOs, and service implementations. Depends only on Domain.
- **Infrastructure** – Data access layer. Contains EF Core DbContext, entity configurations, and repository implementations. Depends on Domain.
- **Api** – Presentation layer. ASP.NET Core controllers, middleware, and the app entry point. Depends on Application and Infrastructure.

### 1.2 Project References

```
Api → Application, Infrastructure
Application → Domain, Infrastructure
Infrastructure → Domain
```

**Why does Application reference Infrastructure?**

The service implementations in Application inject repository interfaces that are defined in Infrastructure. In a stricter Clean Architecture, you'd define repository interfaces in Domain or Application, but for learning purposes we keep them in Infrastructure alongside their implementations.

### 1.3 NuGet Packages

| Project | Package | Purpose |
|---------|---------|---------|
| Infrastructure | `Microsoft.EntityFrameworkCore.SqlServer` | SQL Server database provider |
| Infrastructure | `Microsoft.EntityFrameworkCore.Tools` | `dotnet ef` CLI commands (migrations) |
| Api | `Microsoft.EntityFrameworkCore.Design` | Design-time EF Core support for migrations |
| Api | `Swashbuckle.AspNetCore` | Swagger/OpenAPI documentation UI |
| Api | `Microsoft.AspNetCore.OpenApi` | OpenAPI metadata for endpoints |

### 1.4 Template Cleanup

The default `webapi` template creates `WeatherForecast.cs` and a sample controller. These were removed since we'll create our own domain-specific controllers.

## Key Concepts

### Clean Architecture Benefits

1. **Testability** – Business logic (Application) has no framework dependencies, making it easy to unit test
2. **Flexibility** – You can swap the database (Infrastructure) without touching business logic
3. **Separation of concerns** – Each layer has a clear responsibility
4. **Dependency rule** – Dependencies point inward (Api → Application → Domain)

### Project Reference vs NuGet Package

- **Project references** connect your own projects within the solution
- **NuGet packages** bring in external libraries (Entity Framework, Swashbuckle, etc.)

## Verify

```bash
dotnet build AudioDeliveryWebApi.sln
# Should compile with 0 errors
```

## Next Phase

→ [Phase 2: Domain Entities](Phase02-DomainEntities.md)
