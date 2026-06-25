using AdemideInfoWebsite.Application;
using AdemideInfoWebsite.Infrastructure;
using static AdemideInfoWebsite.Application.Validations.FluentValidations;
using FluentValidation;
using FluentValidation.AspNetCore;
using AdemideInfoWebsite.Server.DICollection;

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

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
