using AdemideInfoWebsite.Application;
using AdemideInfoWebsite.Infrastructure;
using static AdemideInfoWebsite.Application.Validations.FluentValidations;
using FluentValidation;
using FluentValidation.AspNetCore;
using AdemideInfoWebsite.Server.DICollection;
using AdemideInfoWebsite.Infrastructure.Presistance.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add application services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSettings(builder.Configuration);
builder.Services.AddFluentValidation();
builder.Services.AddCors(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Configure API documentation metadata
    options.SwaggerDoc("v1", new()
    {
        Version = "v1",
        Title = "Ademide Info Website API",
        Description = "RESTful API for Ademide's portfolio website - manages profiles, appointments, and user activities",
        TermsOfService = new Uri("https://github.com/Ademide7/AdemideInfoWebsite"),
        Contact = new()
        {
            Name = "Ademide",
            Url = new Uri("https://github.com/Ademide7")
        },
        License = new()
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    Console.WriteLine("[SWAGGER] Basic configuration complete. Bearer auth requires manual Swagger JSON editing due to .NET 10 + Swashbuckle compatibility issues.");
});


var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Use middleware to inject Bearer authentication into Swagger JSON
    app.UseMiddleware<AdemideInfoWebsite.Server.Middleware.SwaggerBearerMiddleware>();

    // Enable Swagger documentation endpoint in development
    app.UseSwagger();
    // Enable Swagger UI (interactive documentation at /swagger) in development
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ademide Info Website API v1");
        options.RoutePrefix = "swagger"; // Serve Swagger UI at /swagger
        options.DocumentTitle = "Ademide Info Website API - Swagger UI";
        options.DefaultModelsExpandDepth(2);
        options.DefaultModelExpandDepth(2);
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        options.DisplayRequestDuration();
    });
}



using (var scope = app.Services.CreateScope())
{
    // Get the database context from dependency injection
    var dbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();

    // Apply any pending database migrations (creates/updates database schema)
    await dbContext.Database.MigrateAsync();
    Console.WriteLine("[DATABASE] Migrations applied successfully");
}

app.UseHttpsRedirection();

// Enable CORS with the configured policy
app.UseCors("CorsPolicy");

// Enable authentication middleware (must come before authorization)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

// Display startup information
app.Lifetime.ApplicationStarted.Register(() =>
{
    var addresses = app.Urls;
    Console.WriteLine("\n" + new string('=', 60));
    Console.WriteLine("  🚀 Ademide Info Website API is running!");
    Console.WriteLine(new string('=', 60));

    foreach (var address in addresses)
    {
        Console.WriteLine($"  📍 Application: {address}");
        if (app.Environment.IsDevelopment())
        {
            Console.WriteLine($"  📚 Swagger UI:  {address}/swagger");
            Console.WriteLine($"  📄 OpenAPI:     {address}/swagger/v1/swagger.json");
        }
    } 

    Console.WriteLine(new string('=', 60));
    Console.WriteLine("  💡 Tip: Use the 'Authorize' button in Swagger to add JWT token");
    Console.WriteLine(new string('=', 60) + "\n");
});

app.Run();
