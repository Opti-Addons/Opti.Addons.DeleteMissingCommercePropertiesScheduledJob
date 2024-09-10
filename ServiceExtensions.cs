using Microsoft.Extensions.DependencyInjection;

namespace OptimizelyDeleteMissingCommerceProperties;

public static class ServiceExtensions
{
    public static IServiceCollection AddDeleteMissingCommerceProperties(this IServiceCollection services)
    {
        services.AddTransient<IMissingCommercePropertiesService, MissingCommercePropertiesService>();
        services.AddTransient<DeleteMissingCommercePropertiesScheduledJob>();
        services.AddTransient<ListMissingCommercePropertiesScheduledJob>();

        return services;
    }
}