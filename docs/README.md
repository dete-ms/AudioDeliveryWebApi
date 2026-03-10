# AudioDelivery API

A Spotify-like REST API with ASP.NET Core 9, Entity Framework Core, and SQL Server.

## Architecture Overview

This project follows **Clean Architecture** with four layers:

```
AudioDeliveryWebApi/
├── src/
│   ├── AudioDelivery.Api/            ← Presentation layer (controllers, middleware)
│   ├── AudioDelivery.Application/    ← Business logic (services, DTOs, interfaces)
│   ├── AudioDelivery.Domain/         ← Core entities and enums (no dependencies)
│   └── AudioDelivery.Infrastructure/ ← Data access (EF Core, repositories, seeding)
├── docs/                             ← You are here – implementation guides
└── AudioDeliveryWebApi.sln
```

## Phase Guides

Work through these phases in order. Each phase builds on the previous one.

| Phase | Guide | Focus Area | Status |
|-------|-------|-----------|--------|
| 1 | [Solution Setup](Phase01-SolutionSetup.md) | Project structure, NuGet packages, project references | ✅ Done |
| 2 | [Domain Entities](Phase02-DomainEntities.md) | Entity classes, enums, BaseEntity | ✅ Done |
| 3 | [Infrastructure Layer](Phase03-InfrastructureLayer.md) | DbContext, entity configurations, repositories | ✅ Config bodies |
| 4 | [Application Layer](Phase04-ApplicationLayer.md) | DTOs, service interfaces, service stubs | ✅ Done |
| 5 | [Database Setup](Phase05-DatabaseSetup.md) | EF Core migrations, connection strings, seeding | ✅ To Do |
| 6 | [Service Implementation](Phase06-ServiceImplementation.md) | Implement all service methods with EF Core queries | 🔲 To Do |
| 7 | [Validation & Error Handling](Phase07-ValidationErrorHandling.md) | FluentValidation, ProblemDetails, middleware | 🔲 To Do |
| 8 | [Authentication & Authorization](Phase08-AuthenticationAuthorization.md) | JWT Bearer tokens, policies | 🔲 To Do |
| 9 | [Logging & Monitoring](Phase09-LoggingMonitoring.md) | Serilog, correlation IDs, health checks | 🔲 To Do |
| 10 | [Performance & Caching](Phase10-PerformanceCaching.md) | Response caching, pagination, CORS | 🔲 To Do |
| 11 | [Testing](Phase11-Testing.md) | Unit tests, integration tests, mocking | 🔲 To Do |
| 12 | [Deployment & Next Steps](Phase12-DeploymentNextSteps.md) | Docker, CI/CD, Azure deployment | 🔲 To Do |

## How to Use These Guides

1. **Read the phase guide** – understand what you're building and why
2. **Look at the existing code** – stubbed classes have `TODO` comments pointing to the relevant phase
3. **Implement step-by-step** – follow the guide's instructions
4. **Build & test** – verify your changes compile and behave correctly
5. **Move to the next phase**

## Quick Start

```bash
# Build the solution
dotnet build AudioDeliveryWebApi.sln

# Run the API (launches Swagger UI at https://localhost:<port>/)
dotnet run --project src/AudioDelivery.Api
```

> **Note:** The API will build and launch out of the box, but all endpoints will return
> `500 Internal Server Error` until you implement the service methods in Phase 6.
> This is by design – the stubs throw `NotImplementedException` to guide you.
