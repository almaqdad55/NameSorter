using name_sorter;
using Utilities;
using Utilities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
        {
            Console.WriteLine("No file path provided.");
            return;
        }

        string filePath = args[0];

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File does not exist: " + filePath);
            return;
        }

        IHost host = CreateHost();
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var nameSorterApp = services.GetRequiredService<NameSorterApp>();
            nameSorterApp.Run(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static IHost CreateHost() =>
        Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(ConfigureApp)
            .UseSerilog(ConfigureLogger)
            .ConfigureServices(ConfigureServices)
            .Build();

    private static void ConfigureApp(HostBuilderContext hostContext, IConfigurationBuilder config)
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONEMNT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
    }

    private static void ConfigureLogger(HostBuilderContext hostContext, LoggerConfiguration loggerConfiguration)
    {
        loggerConfiguration
            .ReadFrom.Configuration(hostContext.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console();
    }

    private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
    {
        services.AddSingleton<IFileReader, FileReader>();
        services.AddSingleton<INameSorter, NameSorter>();
        services.AddSingleton<IOutputHandler, OutputHandler>();
        services.AddSingleton<NameSorterApp>();
    }
}