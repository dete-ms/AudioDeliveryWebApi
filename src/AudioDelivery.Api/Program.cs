using AudioDelivery.Api.Extensions;
using AudioDelivery.Api.Middleware;
using AudioDelivery.Infrastructure.Extensions;

// =============================================================================
// Program.cs – Application Entry Point
// =============================================================================
//
// This file configures and launches the ASP.NET Core Web API application.
// It follows the "top-level statements" pattern introduced in .NET 6+.
//
// The setup is divided into two phases:
//   1. SERVICE REGISTRATION  – Add services to the DI container
//   2. MIDDLEWARE PIPELINE   – Configure the HTTP request pipeline
//
// ARCHITECTURE OVERVIEW:
//   • Infrastructure services (DbContext, Repositories) are registered via
//     AddInfrastructureServices() extension method from the Infrastructure layer.
//   • Application services (business logic) are registered via
//     AddApplicationServices() extension method from the Api layer.
//   • Controllers are discovered automatically by AddControllers().
//
// PHASE GUIDE:
//   Phase 5: EF Core configuration & connection string setup
//   Phase 8: Add Authentication & Authorization middleware
//   Phase 9: Add structured logging (Serilog)
//   Phase 10: Add response caching, CORS, rate limiting
//   Phase 11: Add health checks
// =============================================================================

var builder = WebApplication.CreateBuilder(args);

// ── 1. SERVICE REGISTRATION ─────────────────────────────────────────────────

// Add controller support (discovers all [ApiController] classes)
builder.Services.AddControllers();

// Add Swagger/OpenAPI documentation generation
// Swashbuckle scans controllers and generates an OpenAPI spec
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Infrastructure layer services (DbContext + Repositories)
// The connection string is read from appsettings.json → "ConnectionStrings:DefaultConnection"
builder.Services.AddInfrastructure(builder.Configuration);

// Register Application layer services (business logic services)
builder.Services.AddApplicationServices();

// TODO Phase 8: Add authentication
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options => { ... });

// TODO Phase 10: Add CORS policy
// builder.Services.AddCors(options => { ... });

// ── 2. BUILD THE APP ────────────────────────────────────────────────────────

var app = builder.Build();

// ── 3. MIDDLEWARE PIPELINE ──────────────────────────────────────────────────
// Middleware runs in the order it is registered. Order matters!

// Global exception handler – catches unhandled exceptions and returns JSON errors
app.UseGlobalExceptionHandler();

// Enable Swagger UI in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AudioDelivery API v1");
        // Serve Swagger UI at the app root for convenience during development
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// TODO Phase 8: Add authentication & authorization middleware
// app.UseAuthentication();
app.UseAuthorization();

// Map controller routes (attribute routing)
app.MapControllers();

// ── 4. RUN ──────────────────────────────────────────────────────────────────

app.Run();
