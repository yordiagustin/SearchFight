using SearchFight.Services.Abstractions;
using SearchFight.Services.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Domain.Abstractions
{
    /// <summary>
    /// Search Repository Abstraction
    /// </summary>
    public interface ISearchRepository
    {
        /// <summary>
        /// Get Search Results with Engine Search Services.
        /// </summary>
        /// <param name="terms">Searched Terms</param>
        /// <returns>List of search total result.</returns>
        Task<IList<SearchTotalResult>> GetSearchResults(IList<string> terms);
    }
}
