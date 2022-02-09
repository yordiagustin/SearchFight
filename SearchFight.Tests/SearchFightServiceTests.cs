using Microsoft.Extensions.Logging;
using SearchFight.Domain.Mapper;
using SearchFight.Domain.Repository;
using SearchFight.Domain.Service;
using SearchFight.Services.Configuration.Settings;
using SearchFight.Services.Entities;
using SearchFight.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SearchFight.Tests
{
    public class SearchFightServiceTests
    {
        private const string NetTerm = ".NET";
        private const string JavaTerm = "Java";

        private const string BingEngine = "Bing";
        private const string GoogleEngine = "Google";

        [Fact]
        public void GetTotalWinner_WithSameEngine_ShouldReturnTopCount()
        {
            var logger = new LoggerFactory().CreateLogger<SearchFightService>();
            var searchRepository = InitializeSearchRepository();
            var searchFightService = new SearchFightService(logger, searchRepository);

            var results = new List<SearchTotalResult>();
            results.Add(new SearchTotalResult { Engine = BingEngine, TotalResults = 80, Term = JavaTerm });
            results.Add(new SearchTotalResult { Engine = BingEngine, TotalResults = 60, Term = NetTerm });

            var winner = searchFightService.GetTotalWinner(results);

            Assert.Equal(JavaTerm, winner);
        }

        [Fact]
        public void GetTotalWinner_WithDifferentEngines_ShouldReturnTopCountSum()
        {
            var logger = new LoggerFactory().CreateLogger<SearchFightService>();
            var searchRepository = InitializeSearchRepository();
            var searchFightService = new SearchFightService(logger, searchRepository);

            var results = new List<SearchTotalResult>();
            results.Add(new SearchTotalResult { Engine = BingEngine, TotalResults = 80, Term = JavaTerm });
            results.Add(new SearchTotalResult { Engine = BingEngine, TotalResults = 100, Term = NetTerm });
            results.Add(new SearchTotalResult { Engine = GoogleEngine, TotalResults = 30, Term = JavaTerm });
            results.Add(new SearchTotalResult { Engine = GoogleEngine, TotalResults = 20, Term = NetTerm });

            var winner = searchFightService.GetTotalWinner(results);

            Assert.Equal(NetTerm, winner);
        }

        [Fact]
        public void GetTotalWinner_WithNoResults_ShouldReturnNull()
        {
            var logger = new LoggerFactory().CreateLogger<SearchFightService>();
            var searchRepository = InitializeSearchRepository();
            var searchFightService = new SearchFightService(logger, searchRepository);

            var results = new List<SearchTotalResult>();
            var winner = searchFightService.GetTotalWinner(results);

            Assert.Null(winner);
        }

        [Fact]
        public void GetWinners_WithManyEngines_ShouldReturnAsManyWinnersAsEngines()
        {
            var logger = new LoggerFactory().CreateLogger<SearchFightService>();
            var searchRepository = InitializeSearchRepository();
            var searchFightService = new SearchFightService(logger, searchRepository);

            var results = new List<SearchTotalResult>();

            results.Add(new SearchTotalResult { Engine = BingEngine, TotalResults = 80, Term = NetTerm });
            results.Add(new SearchTotalResult { Engine = BingEngine, TotalResults = 40, Term = JavaTerm });
            results.Add(new SearchTotalResult { Engine = GoogleEngine, TotalResults = 80, Term = NetTerm });
            results.Add(new SearchTotalResult { Engine = GoogleEngine, TotalResults = 240, Term = JavaTerm });

            var winners = searchFightService.GetWinnersByEngine(results);

            Assert.Equal(2, winners.Count);
        }

        [Fact]
        public void GetWinners_WithNoResults_ShouldReturnEmptyList()
        {
            var logger = new LoggerFactory().CreateLogger<SearchFightService>();
            var searchRepository = InitializeSearchRepository();
            var searchFightService = new SearchFightService(logger, searchRepository);

            var results = new List<SearchTotalResult>();
            var winners = searchFightService.GetWinnersByEngine(results);

            Assert.NotNull(winners);
            Assert.Empty(winners);
        }

        private SearchRepository InitializeSearchRepository()
        {
            var logger = new LoggerFactory().CreateLogger<SearchRepository>();
            var mapper = new SearchTotalResultMapper();

            var bingSearchService = new BingSearchService(new BingSearchSettings(), new LoggerFactory().CreateLogger<BingSearchService>());
            var googleSearchService = new GoogleSearchService(new GoogleSearchSettings(), new LoggerFactory().CreateLogger<GoogleSearchService>());

            return new SearchRepository(logger, mapper, bingSearchService, googleSearchService);
        }

    }
}
