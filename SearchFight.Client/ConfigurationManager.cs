using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SearchFight.Domain.Abstractions;
using SearchFight.Domain.Mapper;
using SearchFight.Domain.Repository;
using SearchFight.Domain.Service;
using SearchFight.Services.Abstractions;
using SearchFight.Services.Configuration.Settings;
using SearchFight.Services.Entities;
using SearchFight.Services.Services;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SearchFight.Client
{
    public static class ConfigurationManager
    {
        public static void ConfigSetup(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
        }

        public static IHost AppStartup()
        {
            var builder = new ConfigurationBuilder();
            ConfigSetup(builder);

            // Defining Logger Configurations.
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(builder.Build())
               .Enrich.FromLogContext()
               .WriteTo.Console()
               .CreateLogger();

            //Initialize Service Settings
            var bingSearchSettings = new BingSearchSettings();
            var googleSearchSettings = new GoogleSearchSettings();           


            // Initialized the Dependency Injection Container.
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    IConfiguration configuration = context.Configuration;
                    configuration.Bind("BingSearchSettings", bingSearchSettings);
                    configuration.Bind("GoogleSearchSettings", googleSearchSettings);

                    services.AddSingleton(bingSearchSettings);
                    services.AddSingleton(googleSearchSettings);

                    services.AddScoped<ISearchRepository, SearchRepository>();
                    services.AddScoped<IMapper<List<Task<SearchTotalResult>>, List<SearchTotalResult>>, SearchTotalResultMapper>();
                    services.AddScoped<IEngineSearchService, GoogleSearchService>();
                    services.AddScoped<IEngineSearchService, BingSearchService>();
                    services.AddScoped<ISearchFightService, SearchFightService>();

                })
                .UseSerilog()
                .Build();

            return host;
        }

    }
}
