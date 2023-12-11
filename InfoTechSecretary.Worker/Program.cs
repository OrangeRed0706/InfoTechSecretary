using InfoTechSecretary.Core;

namespace InfoTechSecretary.Worker;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddLogging();
        builder.Services.AddHttpClient();
        builder.Services.AddInfoTechSecretary(builder.Configuration);
        builder.Services.AddHostedService<ScraperWorker>();

        var host = builder.Build();
        host.Run();
    }
}
