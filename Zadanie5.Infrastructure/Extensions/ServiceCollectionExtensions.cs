using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zadanie5.Domain.Interfaces;
using Zadanie5.Infrastructure.Data;
using Zadanie5.Infrastructure.Repositories;

namespace Zadanie5.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNpgsql<DatabaseContext>(configuration.GetConnectionString("DefaultConnection"));
        services.AddScoped<IKlientRepository, KlientRepository>();

        return services;
    }
}