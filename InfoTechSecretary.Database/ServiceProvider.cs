using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTechSecretary.Database;

public static class ServiceProvider
{
    public static IServiceCollection AddInfoTechSecretaryContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InfoTechSecretaryContext>(options =>
        {
            options.EnableDetailedErrors();
            options.LogTo(Console.WriteLine);
            options.AddInterceptors(new LoggingInterceptor());
            options.UseNpgsql(configuration.GetConnectionString("InfoTechSecretaryContext"));
        });

        return services;
    }

    private class LoggingInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            // Console.WriteLine($"Long running query: {command.CommandText}");
            return base.ReaderExecuting(command, eventData, result);
        }
    }
}
