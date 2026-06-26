using AdemideInfoWebsite.Application;
using AdemideInfoWebsite.Infrastructure;
using AdemideInfoWebsite.Infrastructure.Presistance.Contexts;
using AdemideInfoWebsite.Server.DICollection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using static AdemideInfoWebsite.Application.Validations.FluentValidations; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
 

// Add application services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSettings(builder.Configuration);
builder.Services.AddFluentValidation();
builder.Services.AddCors(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEndpointsApiExplorer();
 
builder.Services.AddAuthorization();

builder.Services.AddOpenApi();


var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.Title = "AdemideInfoWebsite API";
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

 
app.Run();
