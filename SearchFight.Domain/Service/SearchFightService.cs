using Microsoft.Extensions.Logging;
using SearchFight.Domain.Abstractions;
using SearchFight.Domain.Entities;
using SearchFight.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Domain.Service
{
    /// <summary>
    /// Search Fight Service
    /// </summary>
    public class SearchFightService : ISearchFightService
    {
        private readonly ILogger<SearchFightService> _logger;
        private readonly ISearchRepository _searchRepository;

        /// <summary>
        /// Search Fight Service Constructor
        /// </summary>
        /// <param name="logger">Logger Dependency Injection</param>
        /// <param name="searchRepository">Search Repository Dependency Injection</param>
        /// <exception cref="ArgumentNullException">Argument Null Exception</exception>
        public SearchFightService(ILogger<SearchFightService> logger, ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository ?? throw new ArgumentNullException(nameof(searchRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Execute Search
        /// </summary>
        /// <param name="terms"></param>
        /// <returns>List Of Search Results</returns>
        public async Task<IList<SearchTotalResult>> ExecuteSearch(IList<string> terms)
        {
            return await _searchRepository.GetSearchResults(terms);
        }

        /// <summary>
        /// Print formatted report for results.
        /// </summary>
        /// <param name="results"></param>
        public void PrintFormattedReport(IList<SearchTotalResult> results)
        {
            var formattedReport = new StringBuilder();

            var resultsByTerm = GetResultsByTerm(results);
            foreach (var resultGroup in resultsByTerm)
            {
                formattedReport.Append($"{resultGroup.Key}: ");
                foreach (var result in resultGroup)
                {
                    formattedReport.Append($"{result.Engine}: {result.TotalResults}  ");
                }
                formattedReport.AppendLine();
            }

            var winners = GetWinnersByEngine(results);
            foreach (var winner in winners)
            {
                formattedReport.AppendLine(winner.ToString());
            }

            var totalWinner =  GetTotalWinner(results);
            formattedReport.AppendLine($"Total winner: {totalWinner}");

            _logger.LogInformation(formattedReport.ToString());
        }

        /// <summary>
        /// Get Search Results By Term for given results.
        /// </summary>
        /// <param name="results"></param>
        /// <returns>Search Result By Term Data Format</returns>
        public IEnumerable<IGrouping<string, SearchTotalResult>> GetResultsByTerm(IList<SearchTotalResult> results)
        {
            return results.GroupBy(result => result.Term);
        }

        /// <summary>
        /// Get Total Winner Term.
        /// </summary>
        /// <param name="results"></param>
        /// <returns>Total winner Data Format.</returns>
        public string GetTotalWinner(IList<SearchTotalResult> results)
        {
            var totalWinner = results.GroupBy(
                    result => result.Term,
                    (term, result) => new { Term = term, Total = result.Sum(r => r.TotalResults) })
                .OrderByDescending(r => r.Total)
                .FirstOrDefault()?
                .Term;

            return totalWinner;
        }

        /// <summary>
        /// Get Winners By Engine
        /// </summary>
        /// <param name="results"></param>
        /// <returns>Winners Data Format</returns>
        public IList<FightWinner> GetWinnersByEngine(IList<SearchTotalResult> results)
        {
            var data = new StringBuilder();

            var winners = results
                .GroupBy(
                    result => result.Engine,
                    (searchEngine, results) => new FightWinner
                    {
                        SearchEngine = searchEngine,
                        Term = results.OrderByDescending(r => r.TotalResults).FirstOrDefault().Term
                    })
                .ToList();

            return winners;
        }
    }
}
