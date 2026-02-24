# Phase 9 – Logging & Monitoring

> **Status:** 🔲 To Do

## Overview

This phase adds:
1. **Structured logging** with Serilog
2. **Correlation IDs** for request tracing
3. **Health checks** for monitoring
4. **Request/response logging** middleware

## 9.1 Install Serilog

```bash
dotnet add src/AudioDelivery.Api package Serilog.AspNetCore
dotnet add src/AudioDelivery.Api package Serilog.Sinks.Console
dotnet add src/AudioDelivery.Api package Serilog.Sinks.File
# Optional: SQL Server sink
dotnet add src/AudioDelivery.Api package Serilog.Sinks.MSSqlServer
```

## 9.2 Configure Serilog

```csharp
// Program.cs – replace default logging
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithProperty("Application", "AudioDeliveryApi")
        .WriteTo.Console(outputTemplate:
            "[{Timestamp:HH:mm:ss} {Level:u3}] {CorrelationId} {Message:lj}{NewLine}{Exception}")
        .WriteTo.File("logs/api-.log",
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 30);
});
```

Add to `appsettings.json`:
```json
"Serilog": {
    "MinimumLevel": {
        "Default": "Information",
        "Override": {
            "Microsoft.AspNetCore": "Warning",
            "Microsoft.EntityFrameworkCore": "Warning"
        }
    }
}
```

## 9.3 Correlation ID Middleware

```csharp
// src/AudioDelivery.Api/Middleware/CorrelationIdMiddleware.cs
public class CorrelationIdMiddleware
{
    private const string CorrelationIdHeader = "X-Correlation-Id";
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        // Use existing correlation ID from request header, or generate a new one
        var correlationId = context.Request.Headers[CorrelationIdHeader].FirstOrDefault()
                            ?? Guid.NewGuid().ToString();

        context.Items["CorrelationId"] = correlationId;
        context.Response.Headers[CorrelationIdHeader] = correlationId;

        // Push to Serilog's LogContext so all logs in this request include it
        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            await _next(context);
        }
    }
}
```

## 9.4 Request Logging

```csharp
// Program.cs – add Serilog request logging
app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.00}ms";
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("CorrelationId", httpContext.Items["CorrelationId"]);
    };
});
```

## 9.5 Health Checks

```csharp
// Program.cs – service registration
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!,
        name: "database",
        tags: new[] { "ready" });

// Program.cs – endpoint mapping
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false  // No checks – just confirms the app is running
});
```

Install health check packages:
```bash
dotnet add src/AudioDelivery.Api package AspNetCore.HealthChecks.SqlServer
```

## 9.6 Logging Best Practices

```csharp
// ✅ DO: Use structured logging with named parameters
_logger.LogInformation("Fetching album {AlbumId} for market {Market}", id, market);

// ❌ DON'T: Use string interpolation (breaks structured logging)
_logger.LogInformation($"Fetching album {id} for market {market}");

// ✅ DO: Log at appropriate levels
_logger.LogDebug("Query returned {Count} results", items.Count);        // Detailed debugging
_logger.LogInformation("Album {AlbumId} retrieved successfully", id);   // Normal operations
_logger.LogWarning("Album {AlbumId} not found", id);                    // Expected issues
_logger.LogError(ex, "Failed to fetch album {AlbumId}", id);           // Errors
```

## Key Concepts

### Why Structured Logging?

Traditional logging: `"Fetching album abc-123 for market US"` → just a string  
Structured logging: `{ AlbumId: "abc-123", Market: "US" }` → searchable, filterable properties

### Correlation IDs

When debugging production issues, you need to trace a single request across logs. The correlation ID links all log entries for one request together.

## Verify

```bash
dotnet run --project src/AudioDelivery.Api

# Check console output – should see structured logs
# curl any endpoint and verify logs include CorrelationId

# Check health endpoints
curl https://localhost:5001/health/live    # 200 = app running
curl https://localhost:5001/health/ready   # 200 = database connected
```

## Next Phase

→ [Phase 10: Performance & Caching](Phase10-PerformanceCaching.md)
