# Phase 7 – Validation & Error Handling

> **Status:** 🔲 To Do

## Overview

This phase adds:
1. **Input validation** using FluentValidation
2. **ProblemDetails** responses (RFC 9457) for standardized errors
3. **Enhanced exception handling** middleware

## 7.1 Install FluentValidation

```bash
dotnet add src/AudioDelivery.Application package FluentValidation
dotnet add src/AudioDelivery.Application package FluentValidation.DependencyInjectionExtensions
```

## 7.2 Create Validators

Example for `CreatePlaylistRequest`:

```csharp
// src/AudioDelivery.Application/Playlists/Validators/CreatePlaylistRequestValidator.cs
using FluentValidation;

public class CreatePlaylistRequestValidator : AbstractValidator<CreatePlaylistRequest>
{
    public CreatePlaylistRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Playlist name is required.")
            .MaximumLength(200).WithMessage("Playlist name cannot exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}
```

### Validators to Create

- [ ] `CreatePlaylistRequestValidator`
- [ ] `UpdatePlaylistRequestValidator`
- [ ] `AddItemsRequestValidator`
- [ ] `SaveAlbumRequestValidator` (if you add album creation)

## 7.3 Register Validators

```csharp
// In ServiceCollectionExtensions.cs
using FluentValidation;

services.AddValidatorsFromAssemblyContaining<CreatePlaylistRequestValidator>();
```

## 7.4 Validation Filter (for Controllers)

```csharp
// src/AudioDelivery.Api/Filters/ValidationFilter.cs
public class ValidationFilter<T> : IActionFilter where T : class
{
    private readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var model = context.ActionArguments.Values.OfType<T>().FirstOrDefault();
        if (model is null) return;

        var result = _validator.Validate(model);
        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

            context.Result = new BadRequestObjectResult(new ValidationProblemDetails(errors));
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
```

## 7.5 ProblemDetails (RFC 9457)

Update `GlobalExceptionHandler` to return ProblemDetails:

```csharp
private async Task HandleExceptionAsync(HttpContext context, Exception exception)
{
    context.Response.ContentType = "application/problem+json";

    var problemDetails = exception switch
    {
        KeyNotFoundException => new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Resource Not Found",
            Detail = exception.Message,
            Instance = context.Request.Path
        },
        ArgumentException => new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Detail = exception.Message,
            Instance = context.Request.Path
        },
        _ => new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Detail = _environment.IsDevelopment() ? exception.Message : "An unexpected error occurred.",
            Instance = context.Request.Path
        }
    };

    context.Response.StatusCode = problemDetails.Status ?? 500;
    await context.Response.WriteAsJsonAsync(problemDetails);
}
```

## 7.6 Custom Exception Types

```csharp
// src/AudioDelivery.Application/Common/Exceptions/NotFoundException.cs
public class NotFoundException : Exception
{
    public NotFoundException(string entityName, object key)
        : base($"{entityName} with key '{key}' was not found.") { }
}

// src/AudioDelivery.Application/Common/Exceptions/ValidationException.cs
public class AppValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public AppValidationException(IDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }
}
```

## Key Concepts

### Why ProblemDetails?

RFC 9457 defines a standard format for HTTP API error responses:
```json
{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
    "title": "Not Found",
    "status": 404,
    "detail": "Album with key '...' was not found.",
    "instance": "/api/v1/albums/abc"
}
```

This provides a consistent, machine-readable error format that API consumers can rely on.

## Verify

```bash
# Test validation – send invalid data
curl -X POST https://localhost:5001/api/v1/users/{userId}/playlists \
  -H "Content-Type: application/json" \
  -d '{"name": ""}'
# Should return 400 with validation errors
```

## Next Phase

→ [Phase 8: Authentication & Authorization](Phase08-AuthenticationAuthorization.md)
