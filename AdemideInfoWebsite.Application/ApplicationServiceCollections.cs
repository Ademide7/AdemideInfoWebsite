using AdemideInfoWebsite.Application.Abstractions;
using AdemideInfoWebsite.Application.Services;
using AdemideInfoWebsite.Infrastructure.Presistance.Repository;
using AdemideInfoWebsite.Infrastructure.ThirdPartyServices;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using static AdemideInfoWebsite.Application.Validations.FluentValidations;


namespace AdemideInfoWebsite.Application;

// ApplicationServiceCollections class to register application services
public static class ApplicationServiceCollections
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateProfileDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateProfileDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateProfileLanguageDtoValidator>();

        // Register other application services here
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IAppointmentService,AppointmentService>();

        return services;
    }
}