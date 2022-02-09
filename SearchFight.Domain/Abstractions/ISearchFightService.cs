using SearchFight.Domain.Entities;
using SearchFight.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Domain.Abstractions
{
    /// <summary>
    /// Search Fight Service Abstraction
    /// </summary>
    public interface ISearchFightService
    {
        /// <summary>
        /// Execute Search
        /// </summary>
        /// <param name="terms"></param>
        /// <returns>List Of Search Results</returns>
        Task<IList<SearchTotalResult>> ExecuteSearch(IList<string> terms);

        /// <summary>
        /// Get Search Results By Term for given results.
        /// </summary>
        /// <param name="results"></param>
        /// <returns>Search Result By Term Data Format</returns>
        IEnumerable<IGrouping<string, SearchTotalResult>> GetResultsByTerm(IList<SearchTotalResult> results);

        /// <summary>
        /// Get Winners By Engine
        /// </summary>
        /// <param name="results"></param>
        /// <returns>Winners Data Format</returns>
        IList<FightWinner> GetWinnersByEngine(IList<SearchTotalResult> results);

        /// <summary>
        /// Get Total Winner Term.
        /// </summary>
        /// <param name="results"></param>
        /// <returns>Total winner Data Format.</returns>
        string GetTotalWinner(IList<SearchTotalResult> results);

        /// <summary>
        /// Print formatted report for results.
        /// </summary>
        /// <param name="results"></param>>
        void PrintFormattedReport(IList<SearchTotalResult> results);
    }
}
