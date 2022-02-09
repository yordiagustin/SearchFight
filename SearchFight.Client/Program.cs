using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SearchFight.Domain.Service;
using System;
using System.Threading.Tasks;

namespace SearchFight.Client
{
    class Program
    {
        static ILogger<Program> logger;
        static async Task Main(string[] args)
        {
            try
            {
                var host = ConfigurationManager.AppStartup();
                logger = host.Services.GetService<ILogger<Program>>();

                var searchFightService = ActivatorUtilities.CreateInstance<SearchFightService>(host.Services);
                var searchResults = await searchFightService.ExecuteSearch(args);
                searchFightService.PrintFormattedReport(searchResults);
            }
            catch (Exception ex)
            {
                logger.LogError("Some Error Ocurred.");
                logger.LogError(ex.Message);
            }
        }
    }
}
