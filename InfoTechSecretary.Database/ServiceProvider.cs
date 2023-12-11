using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTechSecretary.Database;

public static class ServiceProvider
{
    public static IServiceCollection AddInfoTechSecretaryContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<InfoTechSecretaryContext>(options => { options.UseNpgsql(configuration.GetConnectionString("InfoTechSecretaryContext")); });

        return services;
    }
}
