using MemeCardsService;
using Networking;
using Serilog;
using Shared.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        var serilog = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        LoggerFactory loggerFactory = new LoggerFactory();
        loggerFactory.AddSerilog(serilog);

        Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger<Program>();

        builder.Services.AddSingleton(logger);
        builder.Services.AddSingleton<MainConfiguration>();
        builder.Services.AddSingleton<Server>();

        builder.Services.AddHostedService<MainService>();

        var host = builder.Build();
        host.Run();
    }
}