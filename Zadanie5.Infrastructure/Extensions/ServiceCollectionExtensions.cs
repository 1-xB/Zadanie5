using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zadanie5.Domain.Interfaces;
using Zadanie5.Infrastructure.Data;

namespace Zadanie5.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNpgsql<DatabaseContext>(configuration.GetConnectionString("DefaultConnection"));
        services.AddScoped<IKlientRepository, IKlientRepository>();

        return services;
    }
}