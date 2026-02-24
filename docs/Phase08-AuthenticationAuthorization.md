# Phase 8 – Authentication & Authorization

> **Status:** 🔲 To Do

## Overview

This phase secures the API with:
1. **JWT Bearer token** authentication
2. **Policy-based** authorization
3. **User context** middleware (replacing the temporary userId query param)

## 8.1 Install Packages

```bash
dotnet add src/AudioDelivery.Api package Microsoft.AspNetCore.Authentication.JwtBearer
```

## 8.2 Configure JWT Authentication

```csharp
// Program.cs – add before builder.Build()
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();
```

Add to `appsettings.json`:
```json
"Jwt": {
    "Key": "your-256-bit-secret-key-here-make-it-long-enough",
    "Issuer": "AudioDeliveryApi",
    "Audience": "AudioDeliveryClient",
    "ExpireMinutes": 60
}
```

## 8.3 Enable Middleware

```csharp
// Program.cs – middleware pipeline (order matters!)
app.UseAuthentication();   // Must come before UseAuthorization
app.UseAuthorization();
```

## 8.4 Create Token Service

```csharp
// src/AudioDelivery.Application/Auth/ITokenService.cs
public interface ITokenService
{
    string GenerateToken(User user);
}

// src/AudioDelivery.Infrastructure/Auth/TokenService.cs
public class TokenService : ITokenService
{
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.DisplayName),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"]!)),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
```

## 8.5 Create Auth Controller

```csharp
[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Validate credentials, return JWT token
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        // Create user, return JWT token
    }
}
```

## 8.6 Protect Endpoints

```csharp
// Protect specific endpoints
[Authorize]
[HttpGet("me")]
public async Task<IActionResult> GetCurrentUser()
{
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    // ...
}

// Allow anonymous access to public endpoints
[AllowAnonymous]
[HttpGet("tracks/{id:guid}")]
public async Task<IActionResult> GetTrack(Guid id) { }
```

## 8.7 Get Current User from JWT

Replace the temporary `userId` query parameter:

```csharp
// Before (temporary):
public async Task<IActionResult> GetCurrentUser([FromQuery] Guid userId)

// After (secured):
[Authorize]
public async Task<IActionResult> GetCurrentUser()
{
    var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    var result = await _userService.GetCurrentUserAsync(userId);
    return Ok(result);
}
```

## Key Concepts

### JWT Token Flow

1. Client sends credentials to `/api/v1/auth/login`
2. Server validates credentials and returns a JWT token
3. Client includes token in subsequent requests: `Authorization: Bearer <token>`
4. Server validates the token and extracts user information from claims

### Authentication vs Authorization

- **Authentication:** "Who are you?" → validated by JWT token
- **Authorization:** "What can you do?" → controlled by `[Authorize]` attributes and policies

## Next Phase

→ [Phase 9: Logging & Monitoring](Phase09-LoggingMonitoring.md)
