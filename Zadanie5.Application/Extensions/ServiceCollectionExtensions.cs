using Microsoft.Extensions.DependencyInjection;
using Zadanie5.Application.Interfaces;
using Zadanie5.Application.Services;

namespace Zadanie5.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPeselValidator, PeselValidator>();
        services.AddScoped<IFileProcessService,FileProcessService>();
        services.AddScoped<IFileCreatingService,FileCreatingService>();
        services.AddScoped<IKlientService, KlientService>();

        return services;
    }
}