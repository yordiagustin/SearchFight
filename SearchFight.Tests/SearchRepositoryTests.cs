using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SearchFight.Domain.Mapper;
using SearchFight.Domain.Repository;
using SearchFight.Services.Abstractions;
using SearchFight.Services.Configuration.Settings;
using SearchFight.Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SearchFight.Tests
{
    public class SearchRepositoryTests
    {
        private readonly IConfiguration _configuration;

        public SearchRepositoryTests()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"test-appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build();
        }

        [Fact]
        public async Task Execute_WithNullArgs_ShouldThrowArgumentException()
        {
            var logger = new LoggerFactory().CreateLogger<SearchRepository>();
            var mapper = new SearchTotalResultMapper();

            var bingSearchService = new BingSearchService(new BingSearchSettings(), new LoggerFactory().CreateLogger<BingSearchService>());
            var googleSearchService = new GoogleSearchService(new GoogleSearchSettings(), new LoggerFactory().CreateLogger<GoogleSearchService>());
            var engineSearchService = new List<IEngineSearchService>();
            engineSearchService.Add(bingSearchService);
            engineSearchService.Add(googleSearchService);

            var searchRepository = new SearchRepository(logger, mapper, engineSearchService);

            await Assert.ThrowsAsync<ArgumentException>(() => searchRepository.GetSearchResults(null));
        }

        [Fact]
        public async Task Execute_WithNoArgs_ShouldThrowArgumentException()
        {
            var logger = new LoggerFactory().CreateLogger<SearchRepository>();
            var mapper = new SearchTotalResultMapper();

            var bingSearchService = new BingSearchService(new BingSearchSettings(), new LoggerFactory().CreateLogger<BingSearchService>());
            var googleSearchService = new GoogleSearchService(new GoogleSearchSettings(), new LoggerFactory().CreateLogger<GoogleSearchService>());
            var engineSearchService = new List<IEngineSearchService>();
            engineSearchService.Add(bingSearchService);
            engineSearchService.Add(googleSearchService);

            var searchRepository = new SearchRepository(logger, mapper, engineSearchService);

            var terms = new string[0];
            await Assert.ThrowsAsync<ArgumentException>(() => searchRepository.GetSearchResults(terms));
        }

        [Fact]
        public async Task Execute_WithOnlyOneArg_ShouldThrowArgumentException()
        {
            var logger = new LoggerFactory().CreateLogger<SearchRepository>();
            var mapper = new SearchTotalResultMapper();

            var bingSearchService = new BingSearchService(new BingSearchSettings(), new LoggerFactory().CreateLogger<BingSearchService>());
            var googleSearchService = new GoogleSearchService(new GoogleSearchSettings(), new LoggerFactory().CreateLogger<GoogleSearchService>());
            var engineSearchService = new List<IEngineSearchService>();
            engineSearchService.Add(bingSearchService);
            engineSearchService.Add(googleSearchService);

            var searchRepository = new SearchRepository(logger, mapper, engineSearchService);

            var terms = new[] { "java" };
            await Assert.ThrowsAsync<ArgumentException>(() => searchRepository.GetSearchResults(terms));
        }

        [Fact]
        public async Task Execute_WithServicesTwoArgs_ShouldGetResults()
        {
            var logger = new LoggerFactory().CreateLogger<SearchRepository>();
            var mapper = new SearchTotalResultMapper();

            var bingSearchSettings = new BingSearchSettings();
            var googleSearchSettings = new GoogleSearchSettings();

            _configuration.Bind("BingSearchSettings", bingSearchSettings);
            _configuration.Bind("GoogleSearchSettings", googleSearchSettings);

            var bingSearchService = new BingSearchService(bingSearchSettings, new LoggerFactory().CreateLogger<BingSearchService>());
            var googleSearchService = new GoogleSearchService(googleSearchSettings, new LoggerFactory().CreateLogger<GoogleSearchService>());
            var engineSearchService = new List<IEngineSearchService>();
            engineSearchService.Add(bingSearchService);
            engineSearchService.Add(googleSearchService);

            var searchRepository = new SearchRepository(logger, mapper, engineSearchService);
            var terms = new[] { "java", ".net" };
            await searchRepository.GetSearchResults(terms);
        }

    }
}
