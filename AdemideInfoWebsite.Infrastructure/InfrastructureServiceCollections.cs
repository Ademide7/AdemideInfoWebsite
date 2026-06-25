using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AdemideInfoWebsite.Application.Abstractions;
using AdemideInfoWebsite.Infrastructure.ThirdPartyServices.Models;
using AdemideInfoWebsite.Infrastructure.ThirdPartyServices;
using AdemideInfoWebsite.Infrastructure.Presistance.Repository;

namespace AdemideInfoWebsite.Infrastructure;

//InfrastructureServiceCollections class that contains extension methods for IServiceCollection to add infrastructure services to the dependency injection container.
public static class InfrastructureServiceCollections
{
    //AddInfrastructureServices method that takes an IServiceCollection and an IConfiguration as parameters. It adds the MainDbContext to the service collection using the connection string from the configuration. It also adds the IProfileRepository, IPasswordRepository, IAppointmentRepository, and IUserActivityRepository implementations to the service collection.
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Presistance.Contexts.MainDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        //efrepository pattern implementation for the repositories ;
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped<IEmailService, EmailService>();

        // settings configuration for email settings
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        return services;
    }
}
