using AdemideInfoWebsite.SharedKernel.Models;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace AdemideInfoWebsite.Server.DICollection;

// MainCollections.
public static class MainCollections
{
     
    // AddCors limit to selected origins.
    public static void AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins(allowedOrigins)
                       .AllowAnyHeader()
                       .AllowAnyMethod();
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
        services.Configure<Infrastructure.ThirdPartyServices.Models.EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
    }
}
