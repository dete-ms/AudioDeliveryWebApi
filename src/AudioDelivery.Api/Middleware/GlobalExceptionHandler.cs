using System.Net;
using System.Text.Json;

namespace AudioDelivery.Api.Middleware;

/// <summary>
/// Global exception handling middleware.
///
/// PURPOSE:
/// Catches any unhandled exceptions thrown during request processing
/// and returns a consistent JSON error response instead of exposing
/// stack traces or raw server errors to API consumers.
///
/// PHASE GUIDE:
/// - Phase 7 will enhance this with ProblemDetails (RFC 9457) support
/// - Phase 9 will add structured logging with correlation IDs
///
/// HOW IT WORKS:
/// 1. The middleware wraps the entire request pipeline in a try-catch
/// 2. If an exception escapes, it logs the error and returns a 500 response
/// 3. In Development, exception details are included; in Production, they are hidden
/// </summary>
public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _environment;

    public GlobalExceptionHandler(
        RequestDelegate next,
        ILogger<GlobalExceptionHandler> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred while processing {Method} {Path}",
                context.Request.Method, context.Request.Path);

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // TODO: In Phase 7, convert this to a ProblemDetails response (RFC 9457)
        //       which provides a standardized error format:
        //       {
        //         "type": "https://tools.ietf.org/html/rfc9110#section-15.6.1",
        //         "title": "Internal Server Error",
        //         "status": 500,
        //         "detail": "...",
        //         "instance": "/api/v1/albums/123"
        //       }

        var response = new
        {
            status = context.Response.StatusCode,
            message = "An unexpected error occurred.",
            // Only include details in development to avoid leaking sensitive info
            detail = _environment.IsDevelopment() ? exception.Message : null,
            stackTrace = _environment.IsDevelopment() ? exception.StackTrace : null
        };

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        await context.Response.WriteAsJsonAsync(response, jsonOptions);
    }
}

/// <summary>
/// Extension method to register the exception handler middleware in the pipeline.
/// </summary>
public static class GlobalExceptionHandlerExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionHandler>();
    }
}
