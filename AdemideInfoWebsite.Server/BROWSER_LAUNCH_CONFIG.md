# Browser Auto-Launch Configuration

## Summary
The API is now configured to **automatically open your browser** and navigate to **Swagger UI** when you start the application.

## What Was Changed

### 1. Launch Settings (`Properties/launchSettings.json`)
Updated all launch profiles to automatically open the browser:

```json
{
  "launchBrowser": true,
  "launchUrl": "swagger"
}
```

**Profiles Configured:**
- ✅ **http** - Opens `http://localhost:5021/swagger`
- ✅ **https** - Opens `https://localhost:7151/swagger`
- ✅ **IIS Express** - Opens `http://localhost:5021/swagger`

### 2. Enhanced Swagger UI (`Program.cs`)
Added improved Swagger UI settings:

```csharp
options.DocumentTitle = "Ademide Info Website API - Swagger UI";
options.DefaultModelsExpandDepth(2);
options.DefaultModelExpandDepth(2);
options.DocExpansion(DocExpansion.None);
options.DisplayRequestDuration();
```

**Features:**
- Custom browser tab title
- Collapsed documentation by default (cleaner view)
- Shows request duration for performance monitoring
- Better model display depth

### 3. Startup Banner
Added a helpful console banner that displays when the app starts:

```
============================================================
  🚀 Ademide Info Website API is running!
============================================================
  📍 Application: https://localhost:7151
  📚 Swagger UI:  https://localhost:7151/swagger
  📄 OpenAPI:     https://localhost:7151/swagger/v1/swagger.json
============================================================
  💡 Tip: Use the 'Authorize' button in Swagger to add JWT token
============================================================
```

## How to Use

### Starting the Application

#### Option 1: Visual Studio
1. Press **F5** (Debug) or **Ctrl+F5** (Run without debugging)
2. Browser automatically opens to Swagger UI
3. Console shows startup banner with URLs

#### Option 2: Command Line
```powershell
dotnet run --project AdemideInfoWebsite.Server
```
Then manually navigate to the Swagger URL shown in the console.

#### Option 3: Visual Studio Launch Profile
- Select **https**, **http**, or **IIS Express** from the launch profile dropdown
- Click the play button (▶)
- Browser opens automatically

## Launch Profiles

### HTTPS (Recommended)
- **URL**: `https://localhost:7151/swagger`
- **Fallback**: `http://localhost:5021`
- **Best for**: Production-like testing with SSL

### HTTP
- **URL**: `http://localhost:5021/swagger`
- **Best for**: Quick development without SSL certificate warnings

### IIS Express
- **URL**: `http://localhost:5021/swagger`
- **Best for**: Testing IIS-specific features

## Swagger UI Features

When the browser opens, you'll see:

### Navigation
- **Swagger UI**: `/swagger`
- **OpenAPI JSON**: `/swagger/v1/swagger.json`
- **OpenAPI Endpoint** (dev only): `/openapi/v1.json`

### Enhanced UI Options
1. **Authorize Button** - Green button at top right for JWT authentication
2. **Request Duration** - Shows how long each request took
3. **Collapsed Sections** - Cleaner view, expand only what you need
4. **Model Schemas** - Expandable request/response models

## URLs Reference

### Development
| Service | URL |
|---------|-----|
| Application (HTTPS) | https://localhost:7151 |
| Application (HTTP) | http://localhost:5021 |
| Swagger UI | https://localhost:7151/swagger |
| OpenAPI JSON | https://localhost:7151/swagger/v1/swagger.json |
| OpenAPI Endpoint | https://localhost:7151/openapi/v1.json |

### Production
Swagger is configured only for Development environment. In Production:
- Swagger UI is disabled
- Only API endpoints are available
- OpenAPI documentation can be enabled if needed

## Customization

### Change Launch URL
To open a different page on startup, edit `launchSettings.json`:

```json
{
  "launchUrl": "index.html"  // or "api/health", etc.
}
```

### Disable Auto-Launch
To prevent automatic browser opening:

```json
{
  "launchBrowser": false
}
```

### Change Ports
Update `applicationUrl` in `launchSettings.json`:

```json
{
  "applicationUrl": "https://localhost:5001;http://localhost:5000"
}
```

## Troubleshooting

### Browser Doesn't Open
1. Check that `launchBrowser` is set to `true`
2. Verify Visual Studio is using the correct launch profile
3. Try running with `dotnet run` and manually open the URL

### Wrong Page Opens
1. Verify `launchUrl` is set to `"swagger"`
2. Check that Swagger is enabled in `Program.cs`
3. Ensure you're in Development environment

### SSL Certificate Warning
When using HTTPS for the first time:
1. Click "Advanced" in browser
2. Click "Proceed to localhost" (unsafe)
3. Or trust the development certificate:
   ```powershell
   dotnet dev-certs https --trust
   ```

### Console Banner Not Showing
- Check that `app.Lifetime.ApplicationStarted.Register()` is in `Program.cs`
- Banner shows only after application fully starts

## Quick Start Guide

1. **Press F5** in Visual Studio
2. Browser opens automatically to Swagger UI
3. Check console for startup URLs
4. Click **Authorize** button
5. Get JWT token from auth endpoint
6. Enter `Bearer <token>` in auth dialog
7. Test your API endpoints!

## Next Steps

- [ ] Configure production Swagger settings if needed
- [ ] Add health check endpoint
- [ ] Configure application insights
- [ ] Set up API versioning
- [ ] Add XML documentation comments for better Swagger docs

---

**Now your API automatically opens in the browser with Swagger UI ready to use! 🚀**
