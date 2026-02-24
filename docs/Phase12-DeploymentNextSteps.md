# Phase 12 – Deployment & Next Steps

> **Status:** 🔲 To Do

## Overview

This final phase covers:
1. **Docker** containerization
2. **CI/CD** pipeline setup
3. **Azure deployment** options
4. **Next steps** for extending the API

## 12.1 Docker Containerization

### Option A: .NET Built-in Container Support (Recommended)

```bash
# Publish as a container image directly – no Dockerfile needed!
dotnet publish src/AudioDelivery.Api \
  --os linux --arch x64 \
  -p:PublishProfile=DefaultContainer \
  -p:ContainerImageName=audiodelivery-api \
  -p:ContainerImageTag=latest
```

Add to `AudioDelivery.Api.csproj`:
```xml
<PropertyGroup>
    <ContainerBaseImage>mcr.microsoft.com/dotnet/aspnet:9.0</ContainerBaseImage>
</PropertyGroup>
```

### Option B: Traditional Dockerfile

```dockerfile
# Dockerfile at solution root
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.sln .
COPY src/AudioDelivery.Api/*.csproj src/AudioDelivery.Api/
COPY src/AudioDelivery.Application/*.csproj src/AudioDelivery.Application/
COPY src/AudioDelivery.Domain/*.csproj src/AudioDelivery.Domain/
COPY src/AudioDelivery.Infrastructure/*.csproj src/AudioDelivery.Infrastructure/
RUN dotnet restore

COPY . .
RUN dotnet publish src/AudioDelivery.Api -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "AudioDelivery.Api.dll"]
```

```bash
docker build -t audiodelivery-api .
docker run -p 8080:8080 -e "ConnectionStrings__DefaultConnection=Server=host.docker.internal;..." audiodelivery-api
```

### Docker Compose (API + SQL Server)

```yaml
# docker-compose.yml
services:
  api:
    build: .
    ports:
      - "8080:8080"
    environment:
      - ConnectionStrings__DefaultConnection=Server=db;Database=AudioDeliveryDb;User Id=sa;Password=YourStrong!Pass123;TrustServerCertificate=True;
    depends_on:
      - db

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong!Pass123
    ports:
      - "1433:1433"
    volumes:
      - sqldata:/var/opt/mssql

volumes:
  sqldata:
```

## 12.2 CI/CD with GitHub Actions

```yaml
# .github/workflows/ci.yml
name: CI

on:
  push:
    branches: [main]
  pull_request:
    branches: [main]

jobs:
  build-and-test:
    runs-on: ubuntu-latest

    steps:
      - uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '9.0.x'

      - name: Restore
        run: dotnet restore

      - name: Build
        run: dotnet build --no-restore

      - name: Test
        run: dotnet test --no-build --verbosity normal
```

## 12.3 Azure Deployment Options

### Azure App Service

```bash
# Create and deploy
az webapp up --name audiodelivery-api --resource-group myRG --runtime "DOTNETCORE:9.0"
```

### Azure Container Apps

```bash
# Build and push to Azure Container Registry
az acr build --registry myregistry --image audiodelivery-api:latest .

# Deploy to Container Apps
az containerapp create \
  --name audiodelivery-api \
  --resource-group myRG \
  --image myregistry.azurecr.io/audiodelivery-api:latest \
  --target-port 8080 \
  --ingress external
```

## 12.4 Environment Configuration

Use environment variables for sensitive settings in production:

```csharp
// Program.cs – configuration sources (in priority order)
// 1. appsettings.json
// 2. appsettings.{Environment}.json
// 3. User secrets (Development only)
// 4. Environment variables (Production)
// 5. Command-line arguments
```

```bash
# Set via environment variables (double underscore = JSON nesting)
export ConnectionStrings__DefaultConnection="Server=prod-server;..."
export Jwt__Key="production-secret-key"
```

## 12.5 What's Next?

### Feature Extensions

| Feature | Description |
|---------|------------|
| **Player endpoints** | Implement /me/player/* for playback state |
| **Recommendations** | Build recommendation engine based on audio features |
| **Recently played** | Track listening history |
| **Saved items** | User's saved albums, tracks, episodes |
| **Follow** | Follow artists and playlists |
| **Shows & Episodes** | Podcast content support |

### Technical Improvements

| Area | Improvement |
|------|-------------|
| **AutoMapper** | Replace manual mapping with AutoMapper profiles |
| **MediatR** | Introduce CQRS pattern with MediatR |
| **Redis caching** | Move from in-memory to distributed cache |
| **Background jobs** | Use Hangfire for async processing |
| **API versioning** | Add `Asp.Versioning.Http` for proper versioning |
| **Rate limiting** | Add `Microsoft.AspNetCore.RateLimiting` |
| **SignalR** | Real-time notifications (now playing, playlist updates) |
| **GraphQL** | Alternative to REST with HotChocolate |
| **gRPC** | High-performance inter-service communication |

### Architecture Evolution

```
Current: Monolithic API
         ↓
Next:    Feature slices (vertical slice architecture)
         ↓
Future:  Microservices (if/when scale demands it)
```

## Congratulations! 🎉

You've built a production-ready REST API that mirrors Spotify's Web API. You've learned:

- Clean Architecture with ASP.NET Core
- Entity Framework Core with SQL Server
- Repository pattern and dependency injection
- RESTful API design with proper HTTP semantics
- Input validation and error handling
- Authentication with JWT
- Structured logging and monitoring
- Performance optimization and caching
- Testing strategies
- Containerization and deployment

This foundation prepares you for building real-world APIs at scale.
