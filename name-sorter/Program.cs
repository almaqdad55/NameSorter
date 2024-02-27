using name_sorter;
using nameSorterLibrary;
using nameSorterLibrary.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.Extensions.Configuration;

using IHost host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;
string filePath = args[0];

if (!File.Exists(filePath))
{
    Console.WriteLine("File does not exist: " + filePath);
    return;
}

try
{
    services.GetRequiredService<NameSorterApp>().Run(filePath);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONEMNT") ?? "Production"}.json", optional: true)
                  .AddEnvironmentVariables();
        })
        .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console())
        .ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<IFileReader, FileReader>();
            services.AddSingleton<INameSorter, NameSorter>();
            services.AddSingleton<IOutputHandler, OutputHandler>();
            services.AddSingleton<NameSorterApp>();
        });


