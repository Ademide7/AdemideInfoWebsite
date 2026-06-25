# Swagger Configuration

## Overview
The API uses Swagger/OpenAPI for API documentation and testing. Swagger UI is available at `/swagger` in development mode with **JWT Bearer authentication fully configured**.

## Accessing Swagger UI
When running in development mode:
- Navigate to: `https://localhost:<port>/swagger`
- The Swagger UI provides interactive API documentation with an **"Authorize" button** for JWT authentication
- All endpoints are listed with their request/response models

## 🔐 JWT Bearer Authentication

### How to Use the Authorize Button
1. **Get a JWT Token**: Call your login/authentication endpoint to obtain a JWT token
2. **Click "Authorize"**: Click the green "Authorize" button at the top right of Swagger UI (or the lock icon next to individual endpoints)
3. **Enter Token**: In the dialog that appears, enter: `Bearer <your-jwt-token>`
   - Example: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
4. **Click "Authorize"**: Your token will be automatically added to all subsequent requests
5. **Test Endpoints**: Click "Try it out" on any protected endpoint and execute

### Visual Guide
```
┌─────────────────────────────────────────┐
│  Swagger UI                 [Authorize] │ ← Click here
├─────────────────────────────────────────┤
│                                         │
│  Available authorizations               │
│  ┌───────────────────────────────────┐ │
│  │ Bearer (http, bearer)             │ │
│  │                                   │ │
│  │ Value: [Bearer your-token-here]  │ │
│  │                                   │ │
│  │        [Authorize] [Close]        │ │
│  └───────────────────────────────────┘ │
└─────────────────────────────────────────┘
```

## Testing Authenticated Endpoints

### Option 1: Using Swagger UI (Recommended ✅)
1. Obtain JWT token from authentication endpoint
2. Click "Authorize" button in Swagger UI
3. Enter: `Bearer <your-token>`
4. Click "Authorize" and then "Close"
5. All requests now include the Authorization header automatically!

### Option 2: Postman/Insomnia
1. Import the OpenAPI spec from `/swagger/v1/swagger.json`
2. Add Authorization header: `Authorization: Bearer <your-token>`

### Option 3: cURL
```bash
curl -X GET "https://localhost:5001/api/your-endpoint" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

## Configuration Details

### Swagger Generation
- **Package**: Swashbuckle.AspNetCore 10.2.3
- **OpenAPI Version**: Microsoft.OpenApi 2.7.5 (transitive)
- **Endpoint**: `/swagger/v1/swagger.json`
- **UI**: `/swagger`
- **JWT Config**: Dynamic reflection-based configuration (compatible with .NET 10)

### Security Configuration
The JWT Bearer authentication is configured using a custom extension method (`ConfigureJwtBearer()`) that:
- Adds "Bearer" security definition
- Configures HTTP bearer scheme
- Sets up JWT format requirements
- Applies authentication to all endpoints by default
- Uses reflection to handle Microsoft.OpenApi.Models 2.x namespace compatibility

### API Information
- **Title**: Ademide Info Website API
- **Version**: v1
- **Description**: RESTful API for managing profiles, appointments, and user activities
- **License**: MIT

## Middleware Order
The authentication middleware is configured in the correct order:
```csharp
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();  // Must come before Authorization
app.UseAuthorization();
app.MapControllers();
```

## Controllers
Controllers should use the `[Authorize]` attribute to protect endpoints:
```csharp
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MyController : ControllerBase
{
	// Protected endpoints - requires JWT token
}
```

For public endpoints, use `[AllowAnonymous]`:
```csharp
[AllowAnonymous]
[HttpPost("login")]
public async Task<IActionResult> Login(LoginRequest request)
{
	// Public endpoint - no token required
}
```

## Example Workflow

### 1. Login to Get Token
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "user@example.com",
  "password": "password123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresIn": 3600
}
```

### 2. Authorize in Swagger
- Click "Authorize" button
- Enter: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- Click "Authorize"

### 3. Call Protected Endpoints
All subsequent requests will automatically include:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

## Troubleshooting

### Token Not Working?
- Ensure you include "Bearer " prefix (with space)
- Check token hasn't expired
- Verify JWT configuration in `appsettings.json` matches

### Authorize Button Not Showing?
- Check application is running
- Verify `/swagger/v1/swagger.json` loads
- Check browser console for errors

### 401 Unauthorized Error?
- Token might be expired
- Token format incorrect (missing "Bearer " prefix)
- User doesn't have required permissions

## Technical Implementation

The JWT Bearer configuration uses reflection to dynamically load and configure Microsoft.OpenApi.Models types, which ensures compatibility across different .NET versions. This approach:

1. Loads the Microsoft.OpenApi assembly at runtime
2. Creates OpenApiSecurityScheme using reflection
3. Configures bearer token requirements
4. Adds global security requirements
5. Gracefully falls back if types aren't available

See `Configuration/SwaggerConfiguration.cs` for implementation details.
