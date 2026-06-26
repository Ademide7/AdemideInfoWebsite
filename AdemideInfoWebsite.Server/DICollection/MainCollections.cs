using AdemideInfoWebsite.Infrastructure.ThirdPartyServices.Models;
using AdemideInfoWebsite.SharedKernel.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AdemideInfoWebsite.Server.DICollection;

// MainCollections.
public static class MainCollections
{
     
    // AddCors limit to selected origins.
    public static void AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials(); // Only if you use cookies/auth credentials
                       
            });
        });
    }


    // AddFluentValidation.
    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<Application.Validations.FluentValidations.CreateProfileDtoValidator>();
    }
     
    public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
         services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
    }

     
}
